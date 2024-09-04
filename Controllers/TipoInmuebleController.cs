using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using net.Models;

namespace net.Controllers;

public class TipoInmuebleController : Controller
{
    private readonly ILogger<TipoInmuebleController> _logger;
    private RepositorioTipo repo = new RepositorioTipo();

    public TipoInmuebleController(ILogger<TipoInmuebleController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        var lista = repo.ObtenerTodos();
        return View(lista);
    }

    public IActionResult Edicion(int id)
    {
        if(id == 0)  
            return View();
        else
        {   
            var tipo = repo.ObtenerUno(id);
            return View(tipo);
        }
    }

    public IActionResult Detalle(int id)
    {
        if(id == 0)  
            return View();
        else
        {
            var tipo = repo.ObtenerUno(id);
            return View(tipo);
        }
    }

    [HttpPost]
    public IActionResult Guardar(int id, TipoInmueble tipo)
    {
        if (!ModelState.IsValid)
        {
            //Si el modelo no es valido, se regresa a la vista de edicion
            return View("Edicion", tipo);
        }   
        id = tipo.TipoId;
        if (id == 0)
        {
            repo.Alta(tipo);
            TempData["Aviso"] = "Tipo de inmueble guardado";
        }
        else
        {
            repo.Modificar(tipo);
            TempData["Aviso"] = "Tipo de inmueble modificado";
        }
        return RedirectToAction("Index");
    }

    public IActionResult Baja(int id)
    {
        repo.Baja(id);
        return RedirectToAction("Index");
    }
    

}