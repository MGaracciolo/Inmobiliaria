using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using net.Models;

namespace net.Controllers;

public class InquilinoController : Controller
{
    private readonly ILogger<InquilinoController> _logger;
    private RepositorioInquilino repo = new RepositorioInquilino();

    public InquilinoController(ILogger<InquilinoController> logger)
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
            var inquilino = repo.ObtenerUno(id);
            return View(inquilino);
        }
    }
    public IActionResult Detalle(int id)
    {
        if(id == 0)  
            return View();
        else
        {
            var inquilino = repo.ObtenerUno(id);
            return View(inquilino);
        }
    }
    [HttpPost]
    public IActionResult Guardar(int id, Inquilino inquilino){

        //Melian
        if (!ModelState.IsValid)
        {
            //Si el modelo no es valido, se regresa a la vista de edicion
            return View("Edicion", inquilino);
        }

        id=inquilino.Id;
        if(id == 0)
            repo.Alta(inquilino);
        else
            repo.Modificar(inquilino);
        return RedirectToAction("Index");
    }

    public IActionResult Eliminar(int id){
        repo.Baja(id);
        return RedirectToAction("Index");
    }
   
}
