using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using net.Models;

namespace net.Controllers;

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
        id=propietario.id_propietario;
        if(id == 0)
            repo.Alta(propietario);
        else
            repo.Modificar(propietario);
        return RedirectToAction("Index");
    }

    public IActionResult Eliminar(int id){
        repo.Baja(id);
        return RedirectToAction("Index");
    }
   
}
