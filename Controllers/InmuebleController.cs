using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using net.Models;

namespace net.Controllers;

public class InmuebleController : Controller
{
    private readonly ILogger<InmuebleController> _logger;
    private RepositorioInmueble repo = new RepositorioInmueble();
    private RepositorioPropietario repoPropietario = new RepositorioPropietario();
    private RepositorioTipo repoTipo = new RepositorioTipo();
    private RepositorioUso repoUso = new RepositorioUso();
    private RepositorioDireccion repoDireccion = new RepositorioDireccion();

    public InmuebleController(ILogger<InmuebleController> logger)
    {
        _logger = logger;
    }

    /*public IActionResult Index()
    {
        var lista = repo.ObtenerTodos();
        return View(lista);
    }*/
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
            var direccion = repoDireccion.ObtenerUno(inmueble.IdDireccion);
            var tipo = repoTipo.ObtenerUno(inmueble.IdTipo);
            inmueble.Propietario = propietario;
            inmueble.UsoInmueble = uso;
            inmueble.Direccion = direccion;
            inmueble.TipoInmueble = tipo;
            return View(inmueble);
        }
    }

    public IActionResult Index(bool? disponible) {

        var lista = repo.ObtenerTodos();//Melian

        if (disponible.HasValue) {//Se filtra por estado
            lista = lista.Where(inmueble => inmueble.Estado == disponible.Value).ToList();
        }

        return View(lista);
    }

    /*public IActionResult Estado(String estado) {
        var lista = repo.ObtenerXEstado(estado);//Melian
        return View(lista);
    }*/

    // [HttpPost]
    // public IActionResult Guardar(int id, Inmueble inmueble, Direccion direccion)
    // {
    //     if (!ModelState.IsValid)
    //     {
    //         return View("Edicion", inmueble);
    //     }
    //     id = inmueble.InmuebleId;
    //     if (id == 0)
    //     {
    //         int idDireccion = repoDireccion.Alta(direccion);
    //         inmueble.IdDireccion = idDireccion;

    //         repo.Alta(inmueble);
    //         TempData["Mensaje"] = "Inmueble guardado";
    //     }
    //     else
    //     {
    //         repoDireccion.Modificar(direccion);
    //         inmueble.IdDireccion = direccion.DireccionId;

    //         repo.Modificar(inmueble);
    //         TempData["Mensaje"] = "Cambios guardados";
    //     }
    //     return RedirectToAction("Index");
    // }
    [HttpPost]
public IActionResult Guardar(int id, Inmueble inmueble, Direccion direccion)
{
    if (!ModelState.IsValid)
    {
        ViewBag.Propietarios = repoPropietario.ObtenerTodos();
        ViewBag.Usos = repoUso.ObtenerTodos();
        ViewBag.Tipos = repoTipo.ObtenerTodos();
        return View("Edicion", inmueble);
    }

    if (inmueble == null)
    {
        ModelState.AddModelError("", "Inmueble no puede ser nulo.");
        return View("Edicion", inmueble);
    }

    if (direccion == null)
    {
        ModelState.AddModelError("", "Dirección no puede ser nula.");
        return View("Edicion", inmueble);
    }

    id = inmueble.InmuebleId;
    if (id == 0)
    {
        int idDireccion = repoDireccion.Alta(direccion);
        inmueble.IdDireccion = idDireccion;

        repo.Alta(inmueble);
        TempData["Mensaje"] = "Inmueble guardado";
    }
    else
    {
        repoDireccion.Modificar(direccion);
        inmueble.IdDireccion = direccion.DireccionId;

        repo.Modificar(inmueble);
        TempData["Mensaje"] = "Cambios guardados";
    }

    return RedirectToAction("Index");
}


    public IActionResult Eliminar(int id)
    {
        int res = repo.Baja(id);
        if (res == -1)
            TempData["Error"] = "No se pudo eliminar el inmueble";
        else
            TempData["Mensaje"] = "El inmueble quedo inactivo";
        return RedirectToAction("Index");
    }

    public IActionResult Activar(int id)
    {
        int res = repo.Restore(id);
        if (res == -1)
            TempData["Error"] = "No se pudo activar el inmueble";
        else
            TempData["Mensaje"] = "El inmueble se activo";
        return RedirectToAction("Index");
    }

}
