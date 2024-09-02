using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using net.Models;

namespace net.Controllers;

public class PropietarioController : Controller
{
    private readonly ILogger<PropietarioController> _logger;
    private RepositorioPropietario repo = new RepositorioPropietario();
    private RepositorioDireccion repositorioDireccion = new RepositorioDireccion();

    public PropietarioController(ILogger<PropietarioController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        var lista = repo.ObtenerTodos();
        return View(lista);
    }
    /*endpoint*/
    public IActionResult Edicion(int id)
    {
        if(id == 0)  
            return View();
        else
        {
            TempData["Mensaje"] = "El propietario se editó con exito";
            var propietario = repo.ObtenerUno(id);
            return View(propietario);
        }
    }
    [HttpPost]
    public IActionResult Guardar(int id, Propietario propietario,Direccion direccion){

        if (!ModelState.IsValid) {
            return View("Edicion", propietario);
        }
        id=propietario.PropietarioId;
        if(id == 0){
            // repositorioDireccion.Alta(direccion);
            TempData["Mensaje"] = "Propietario guardado";
            repo.Alta(propietario);
        }
        else
        {
            // repositorioDireccion.Modificar(direccion);
            TempData["Mensaje"] = "Cambios guardados";
            repo.Modificar(propietario);
        }
        return RedirectToAction("Index");
    }

    public IActionResult Eliminar(int id){
        repo.Baja(id);
        TempData["Mensaje"] = "El propietario se elimino";
        return RedirectToAction("Index");
    }

     public IActionResult Detalle(int id)
    {
        if(id == 0)  
            return View();
        else
        {
            var propietario = repo.ObtenerUno(id);
            // var direccion = repositorioDireccion.ObtenerUno(propietario.IdDireccion);
            return View(propietario);
        }
    }
   
}
