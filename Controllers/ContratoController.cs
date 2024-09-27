using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using net.Models;

namespace net.Controllers;

[Authorize]
public class ContratoController : Controller
{
    private readonly ILogger<ContratoController> _logger;
    private RepositorioContrato repo = new RepositorioContrato();

    private RepositorioPago repoPago = new RepositorioPago();//Nuevo.Melian
    private readonly RepositorioInquilino repoInquilino = new RepositorioInquilino();
    private RepositorioInmueble repoInmueble = new RepositorioInmueble();

    public ContratoController(ILogger<ContratoController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index(DateTime? desde, DateTime? hasta, string estado)
    {
        List<Contrato> lista;
        lista = repo.ObtenerTodos();

        if (desde.HasValue && hasta.HasValue)
        {
            var d = desde.Value.ToString("yyyy-MM-dd");
            var h = hasta.Value.ToString("yyyy-MM-dd");
            lista = repo.ObtenerPorFecha(d, h);
        }
        if (estado != null)
        {
            if (estado == "activo")
            {
                lista = repo.ObtenerActivos();
            }
            if (estado == "inactivo")
            {
                lista = repo.ObtenerInactivos();
            }
            if (estado == "vigente")
            {
                lista = repo.ObtenerVigentes();
            }
        }
        return View(lista);
    }
    /*endpoint*/
    public IActionResult Edicion(int id)
    {
        ViewBag.Inquilinos = repoInquilino.ObtenerActivos();
        ViewBag.Inmuebles = repoInmueble.ObtenerActivos();
        if (id == 0)
        {

            return View();
        }
        else
        {
            var contrato = repo.ObtenerUno(id);
            return View(contrato);
        }
    }

    public IActionResult Renovar(int id)
    {

        var contrato = repo.ObtenerUno(id);
        if (contrato == null)
        {
            TempData["Error"] = "No se encontró el contrato";
            return RedirectToAction("Index");
        }
        var inquilino = repoInquilino.ObtenerUno(contrato.IdInquilino);
        var inmueble = repoInmueble.ObtenerUno(contrato.IdInmueble);
        contrato.Inquilino = inquilino;
        contrato.Inmueble = inmueble;

        return View(contrato);
    }

    [HttpPost]
    public IActionResult Renovar(Contrato contrato)
    {

        if (VerificarDisponibilidad(contrato.Desde, contrato.Hasta, contrato.IdInmueble,contrato.ContratoId))
        {
            TempData["Error"] = "El inmueble ya se encuentra ocupado entre esas fechas";
            return RedirectToAction("Index");
        }

        // Calcular la cantidad de meses entre Desde y Hasta
        var dias = (contrato.Hasta - contrato.Desde).TotalDays; // Obtener la diferencia en días
        contrato.Meses = (int)Math.Ceiling(dias / 30.0); // Calcular y redondear hacia arriba
        contrato.ContratoId = 0;
        repo.Alta(contrato);
        TempData["Mensaje"] = "Contrato generado";
        return RedirectToAction("Index");
    }
    public IActionResult Detalle(int id)
    {
        if (id == 0)
            return View();
        else
        {
            var contrato = repo.ObtenerUno(id);
            var inquilino = repoInquilino.ObtenerUno(contrato.IdInquilino);
            var inmueble = repoInmueble.ObtenerUno(contrato.IdInmueble);
            contrato.Inquilino = inquilino;
            contrato.Inmueble = inmueble;

            // Esto es nuevo. Melian
            var pagos = repoPago.ObtenerPorContrato(id);
            contrato.Pagos = pagos;

            return View(contrato);
        }
    }
    [HttpPost]
   public IActionResult Guardar(int id, Contrato contrato)
{
    id = contrato.ContratoId;

    // Verificar la disponibilidad del inmueble, excluyendo el contrato actual
    if (VerificarDisponibilidad(contrato.Desde, contrato.Hasta, contrato.IdInmueble, contrato.ContratoId))
    {
        TempData["Error"] = "El inmueble ya se encuentra ocupado entre esas fechas";
        return RedirectToAction("Edicion");
    }
    var dias = (contrato.Hasta - contrato.Desde).TotalDays;
    var mesesCalculados = (int)Math.Ceiling(dias / 30.0);
    contrato.Meses = mesesCalculados > 0 ? mesesCalculados : 1;


    if (id == 0)
    {
        repo.Alta(contrato);
        TempData["Mensaje"] = "Contrato generado";
    }
    else
    {
        repo.Modificar(contrato);
        TempData["Mensaje"] = "Cambios guardados";
    }

    return RedirectToAction("Index");
}

   public bool VerificarDisponibilidad(DateTime inicio, DateTime fin, int idInmueble, int contratoId = 0)
{
    string desde = inicio.ToString("yyyy-MM-dd");
    string hasta = fin.ToString("yyyy-MM-dd");

    return repo.estaOcupado(idInmueble, desde, hasta, contratoId);
}

    public IActionResult Eliminar(Contrato contrato)
    {
        
        int res = repo.Baja(contrato.ContratoId, int.Parse(User.Claims.First().Value), DateTime.Now, contrato.Multa);
        if (res == -1)
            TempData["Error"] = "No se pudo cancelar el contrato";
        else
            TempData["Mensaje"] = "Se canceló el contrato";
        return RedirectToAction("Index");

    }

    public IActionResult FiltrarPorPlazo(int? plazo)
    {
        var lista = repo.ObtenerTodos();
        var hoy = DateTime.Now;
        List<Contrato> contratosFiltrados = lista;//Si no se filtra, se muestra todos los contratos

        if (plazo.HasValue && plazo != 0)
        {
            //Se calcula en funcion del plazo seleccionado
            var fechaLimite = hoy.AddDays(plazo.Value);
            //Esto filtra los contratos que expiran dentro del plazo seleccionado
            contratosFiltrados = lista.Where(c => c.Hasta <= fechaLimite && c.Hasta >= hoy).ToList();
        }
        //ViewBag para mostrar el plazo seleccionado, osea le setea el plazo a la vista
        ViewBag.PlazoSeleccionado = plazo;

        return View("Index", contratosFiltrados);
    }

    public async Task<IActionResult> Pagar()
    {
        var model = new Pago();
        return PartialView("Pagar", model); 
    }


    public IActionResult RegistrarPago(Pago pago)
    {
        if (ModelState.IsValid)//Nuevo. Melian
        {
            var contratoExistente = repo.ObtenerUno(pago.IdContrato);

            if (contratoExistente == null)
            {
               TempData["Error"] = "El contrato no existe.";
                return View("Index","Contrato");
            }

            pago.CreadorId = int.Parse(User.Claims.First().Value);
            repoPago.Agregar(pago);
             return RedirectToAction("Detalle", new { id = pago.IdContrato });
        }

        return View("Index","Contrato");
    }

    public IActionResult Cancelar(int id)
    {
        var contrato = repo.ObtenerUno(id); // Obtener el contrato por ID
        if (contrato == null)
        {
            TempData["Error"] = "No se encontro el contrato";
            return RedirectToAction("Index");
        }
        //con esto consigo el ultimo pago
        var pagosRealizados = repoPago.ObtenerPorContrato(id);
        var ultimoPago = pagosRealizados.OrderByDescending(p => p.Numero).FirstOrDefault();
        int pagosHechos = ultimoPago?.Numero ?? 0;
        //a los pagos que deberia haber hecho le resto la cantidad de pagos hechos
        var pagosTotales = contrato.Meses;
        var pagosFaltantes = pagosTotales - pagosHechos;

        ViewBag.PagosRealizados = pagosHechos;
        ViewBag.PagosFaltantes = pagosFaltantes;
        contrato.Multa = CalcularMulta(contrato, pagosFaltantes);

        return View(contrato);
    }

    private decimal CalcularMulta(Contrato contrato, int pagosFaltantes)
    {
        if (pagosFaltantes > 0)
        {
            bool esMenosDeLaMitad = pagosFaltantes > contrato.Meses / 2;
            if (esMenosDeLaMitad)
            {
                return 2 * contrato.PrecioContrato;
            }
            return 1 * contrato.PrecioContrato;
        }
        return 0;
    }


}
