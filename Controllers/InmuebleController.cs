using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using net.Models;

namespace net.Controllers;

public class InmuebleController : Controller
{
    private readonly ILogger<InmuebleController> _logger;
    private RepositorioInmueble repo = new RepositorioInmueble();

    public InmuebleController(ILogger<InmuebleController> logger)
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
            var inmueble = repo.ObtenerUno(id);
            return View(inmueble);
        }
    }
    public IActionResult Detalle(int id)
    {
        if(id == 0)  
            return View();
        else
        {
            var inmueble = repo.ObtenerUno(id);
            return View(inmueble);
        }
    }
    [HttpPost]
    public IActionResult Guardar(int id, Inmueble inmueble){
        id=inmueble.id_inmueble;
        if(id == 0)
            repo.Alta(inmueble);
        else
            repo.Modificar(inmueble);
        return RedirectToAction("Index");
    }

    // public IActionResult Eliminar(int id){
    //     repo.Baja(id);
    //     return RedirectToAction("Index");
    // }
   
}
