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

    public InmuebleController(ILogger<InmuebleController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index(bool? disponible)
    {
        var lista = repo.ObtenerTodos();
        if (disponible.HasValue) {//Se filtra por estado
            lista = lista.Where(inmueble => inmueble.Estado == disponible.Value).ToList();
        }
        return View(lista);
    }
    /*endpoint*/
    public IActionResult Edicion(int id)
    {
        ViewBag.Propietarios = repoPropietario.ObtenerTodos();
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
            inmueble.Propietario = propietario;
            inmueble.UsoInmueble = uso;
            inmueble.TipoInmueble = tipo;
            return View(inmueble);
        }
    }

    [HttpPost]
    public IActionResult Guardar(int id, Inmueble inmueble)
    {
        if (!ModelState.IsValid)
        {
            return View("Edicion", inmueble);
        }
        id = inmueble.InmuebleId;
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
    // [HttpPost]
    // public IActionResult Guardar(int id, Inmueble inmueble)
    // {
    //     if (!ModelState.IsValid)
    //     {
    //         ViewBag.Propietarios = repoPropietario.ObtenerTodos();
    //         ViewBag.Usos = repoUso.ObtenerTodos();
    //         ViewBag.Tipos = repoTipo.ObtenerTodos();
    //         return View("Edicion", inmueble);
    //     }

    //     if (inmueble == null)
    //     {
    //         ModelState.AddModelError("", "Inmueble no puede ser nulo.");
    //         return View("Edicion", inmueble);
    //     }


    //     id = inmueble.InmuebleId;
    //     if (id == 0)
    //     {
    //         repo.Alta(inmueble);
    //         TempData["Mensaje"] = "Inmueble guardado";
    //     }
    //     else
    //     {
    //         repo.Modificar(inmueble);
    //         TempData["Mensaje"] = "Cambios guardados";
    //     }

    //     return RedirectToAction("Index");
    // }

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
            else
                TempData["Mensaje"] = "El inmueble quedo inactivo";
            return RedirectToAction("Index");
		}
    }
    //solo puede admin
    public IActionResult Activar(int id)
    {
        if(!User.IsInRole("Administrador")){
			TempData["Error"] = "Acceso denegado";
			return Redirect("/Home/Index");
		}else{
            int res = repo.Restore(id);
            if (res == -1)
                TempData["Error"] = "No se pudo activar el inmueble";
            else
                TempData["Mensaje"] = "El inmueble se activo";
            return RedirectToAction("Index");
        }
    }
}
