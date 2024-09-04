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
            int idDireccion = repositorioDireccion.Alta(direccion);
            propietario.IdDireccion = idDireccion;
            
            repo.Alta(propietario);
            TempData["Mensaje"] = "Propietario guardado";
        }
        else
        {
            repositorioDireccion.Modificar(direccion);
            propietario.IdDireccion = direccion.DireccionId;
            
            repo.Modificar(propietario);
            TempData["Mensaje"] = "Cambios guardados";
        }
        return RedirectToAction("Index");
    }
    //va a eliminar tanto al propietario como la direccion del PROPIETARIO siempre y cuando no tenga inmubeles registrados
    public IActionResult Eliminar(int id, int direccion){
        int res=repo.Baja(id,direccion);
        if(res == -1)
            TempData["Error"] = "No se pudo eliminar el propietario";
        else
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
            return View(propietario);
        }
    }
   
}
