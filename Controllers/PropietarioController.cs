using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using net.Models;

namespace net.Controllers;
[Authorize]
public class PropietarioController : Controller
{
    private readonly ILogger<PropietarioController> _logger;
    private RepositorioPropietario repo = new RepositorioPropietario();

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
    public IActionResult Guardar(int id, Propietario propietario){

        if (!ModelState.IsValid) {
            return View("Edicion", propietario);
        }
        id=propietario.PropietarioId;
        if(id == 0){          
            repo.Alta(propietario);
            TempData["Mensaje"] = "Propietario guardado";
        }
        else
        {
            repo.Modificar(propietario);
            TempData["Mensaje"] = "Cambios guardados";
        }
        return RedirectToAction("Index");
    }
    //va a eliminar tanto al propietario como la direccion del PROPIETARIO siempre y cuando no tenga inmubeles registrados
    public IActionResult Eliminar(int id){
        if(!User.IsInRole("Administrador")){
			TempData["Error"] = "Acceso denegado";
			return Redirect("/Home/Index");
		}else{
            int res=repo.Baja(id);
            if(res == -1)
                TempData["Error"] = "No se pudo eliminar el propietario";
            else
                TempData["Mensaje"] = "El propietario se elimino";
            return RedirectToAction("Index");
        }
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

      public IActionResult Activar(int id)
    {
        if(!User.IsInRole("Administrador")){
			TempData["Error"] = "Acceso denegado";
			return Redirect("/Home/Index");
		}else{
            int res = repo.Restore(id);
            if (res == -1)
                TempData["Error"] = "No se pudo activar el propietario";
            else
                TempData["Mensaje"] = "El propietario se activo";
            return RedirectToAction("Index");
        }
    }

   
}
