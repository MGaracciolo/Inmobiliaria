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
        if(VerificarDisponibilidad(contrato.Desde, contrato.Hasta, contrato.IdInmueble)){
            TempData["Error"] = "El inmueble ya se encuentra ocupado entre esas fechas";
            return RedirectToAction("Edicion");
        }
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
    public bool VerificarDisponibilidad(DateTime inicio, DateTime fin, int idInmueble){
        string desde = inicio.ToString("yyyy-MM-dd");
        string hasta = fin.ToString("yyyy-MM-dd");
        return repo.estaOcupado(idInmueble, desde, hasta);
    }
    public IActionResult Eliminar(int id)
    {
        if (!User.IsInRole("Administrador"))
        {
            TempData["Error"] = "Acceso denegado";
            return Redirect("/Home/Index");
        }
        int res = repo.Baja(id, int.Parse(User.Claims.First().Value), DateTime.Now);
        if (res == -1)
            TempData["Error"] = "No se pudo dar de baja el contrato";
        else
            TempData["Mensaje"] = "El contrato quedo inactivo";
        return RedirectToAction("Index");

    }

    public IActionResult Activar(int id)
    {
        if (!User.IsInRole("Administrador"))
        {
            TempData["Error"] = "Acceso denegado";
            return Redirect("/Home/Index");
        }

        if (repo.ObtenerUno(id) == null)
        {
            TempData["Error"] = "No se encontro el contrato";
            return RedirectToAction("Index");
        }

        int res = repo.Restore(id);
        if (res == -1)
            TempData["Error"] = "No se pudo activar el contrato";
        else
            TempData["Mensaje"] = "El contrato se activo";
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

    [HttpPost]
    public IActionResult RegistrarPago(Pago pago)
    {
        if (ModelState.IsValid)//Nuevo. Melian
        {
            var contratoExistente = repo.ObtenerUno(pago.IdContrato);

            if (contratoExistente == null)
            {
                ModelState.AddModelError("", "El contrato no existe.");
                return View("Detalle", repo.ObtenerUno(pago.IdContrato)); 
            }

            pago.CreadorId = int.Parse(User.Claims.First().Value);
            pago.Fecha = DateTime.Now;
            pago.AnuladorId = null;
            pago.IdContrato = contratoExistente.ContratoId; 
            repoPago.Agregar(pago);
            return RedirectToAction("Detalle", new { id = pago.IdContrato });
        }

        return View("Detalle", repo.ObtenerUno(pago.IdContrato));
    }
}
