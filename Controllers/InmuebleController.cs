using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using net.Models;

namespace net.Controllers;
[Authorize]
public class InmuebleController : Controller
{
    private readonly ILogger<InmuebleController> _logger;
    private RepositorioInmueble repo = new RepositorioInmueble();
    private RepositorioPropietario repoPropietario = new RepositorioPropietario();
    private RepositorioTipo repoTipo = new RepositorioTipo();
    private RepositorioUso repoUso = new RepositorioUso();
    private RepositorioContrato repoContrato = new RepositorioContrato();

    public InmuebleController(ILogger<InmuebleController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index(DateTime? desde, DateTime? hasta, string estado)
    {
        List<Inmueble> lista;
        if (desde.HasValue && hasta.HasValue)
        {
            lista = repo.ObtenerDisponibles(desde.Value.ToString("yyyy-MM-dd"), hasta.Value.ToString("yyyy-MM-dd"));
        }
        else
        {
            lista = repo.ObtenerTodos();
        }
        if (!string.IsNullOrEmpty(estado))
        {
            if (estado == "activo")
            {
                lista = repo.ObtenerActivos();
            }
            else if (estado == "inactivo")
            {
                lista = repo.ObtenerInactivos();
            }
        }
        return View(lista);
    }

    /*endpoint*/
    public IActionResult Edicion(int id)
    {
        ViewBag.Propietarios = repoPropietario.ObtenerActivos();
        ViewBag.Usos = repoUso.ObtenerTodos();
        ViewBag.Tipos = repoTipo.ObtenerTodos();

        if (id == 0)
        {
            return View();
        }
        else
        {
            var inmueble = repo.ObtenerUno(id);
            return View(inmueble);
        }
    }
    public IActionResult Detalle(int id)
    {
        if (id == 0)
            return View();
        else
        {
            var inmueble = repo.ObtenerUno(id);
            var propietario = repoPropietario.ObtenerUno(inmueble.IdPropietario);
            var uso = repoUso.ObtenerUno(inmueble.IdUso);
            var tipo = repoTipo.ObtenerUno(inmueble.IdTipo);
            ViewBag.Contratos = repoContrato.ObtenerPorInmueble(id);
            inmueble.Propietario = propietario;
            inmueble.UsoInmueble = uso;
            inmueble.TipoInmueble = tipo;
            return View(inmueble);
        }
    }

    [HttpPost]
    public IActionResult Guardar(int id, Inmueble inmueble)
    {
        ViewBag.Propietarios = repoPropietario.ObtenerActivos();
        ViewBag.Usos = repoUso.ObtenerTodos();
        ViewBag.Tipos = repoTipo.ObtenerTodos();
        if (!ModelState.IsValid)
        {
            return View("Edicion", inmueble);
        }
        id = inmueble.InmuebleId;
        bool direccionDuplicada = repo.VerificarDireccion(inmueble.DireccionI);
    
        if (direccionDuplicada && id == 0) // Solo si es un nuevo inmueble
        {
            TempData["Error"]= "Ya existe un inmueble activo con esta dirección.";
            return View("Edicion", inmueble);
        }
        try
        {
            if (id == 0)
            {
                repo.Alta(inmueble);
                TempData["Mensaje"] = "Inmueble guardado";
            }
            else
            {
                repo.Modificar(inmueble);
                TempData["Mensaje"] = "Cambios guardados";
            }
            return RedirectToAction("Index");
        }
        catch(Exception ex)
        {
           TempData["Error"] = ex.Message;
            return View("Edicion", inmueble);
        }
    }
   

    //solo puede admin
    public IActionResult Eliminar(int id)
    {
        if(!User.IsInRole("Administrador")){
			TempData["Error"] = "Acceso denegado";
			return Redirect("/Home/Index");
		}else{
			int res = repo.Baja(id);
            if (res == -1)
                TempData["Error"] = "No se pudo eliminar el inmueble";
            else if(res == 0)
                TempData["Error"] = "No se encontró el inmueble";
            else
                TempData["Mensaje"] = "El inmueble quedo inactivo";
            return RedirectToAction("Index");
		}
    }
    //solo puede admin
    public IActionResult Activar(int id)
    {
        if(!User.IsInRole("Administrador")){//primero controlo que sea admin
			TempData["Error"] = "Acceso denegado";
			return Redirect("/Home/Index");
		}

        if(!Verificaciones(id)){
            return RedirectToAction("Index");
        }
        
        int res = repo.Restore(id); 
        if (res == -1)
            TempData["Error"] = "No se pudo activar el inmueble";
        else
            TempData["Mensaje"] = "Se activó el inmueble";
        return RedirectToAction("Index");
            
    }

    public bool Verificaciones(int id){
        Inmueble inmueble = repo.ObtenerUno(id);
        
        if(inmueble==null){
            TempData["Error"] = "No se encontró el inmueble";
            return false;
        }

        Propietario propietario = repoPropietario.ObtenerUno(inmueble.IdPropietario);

        if(propietario == null || propietario.EstadoP == false){
            TempData["Error"] = "El propietario no se encuentra activo";
            return false;
        }
        
        return true;
        
    }
}
