using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using net.Models;

namespace net.Controllers;

public class ContratoController : Controller
{
    private readonly ILogger<ContratoController> _logger;
    private RepositorioContrato repo = new RepositorioContrato();

    public ContratoController(ILogger<ContratoController> logger)
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
            var contrato = repo.ObtenerUno(id);
            return View(contrato);
        }
    }
    public IActionResult Detalle(int id)
    {
        if(id == 0)  
            return View();
        else
        {
            var contrato = repo.ObtenerUno(id);
            return View(contrato);
        }
    }
    [HttpPost]
    public IActionResult Guardar(int id, Contrato contrato){
        id=contrato.id_contrato;
        if(id == 0)
            repo.Alta(contrato);
        else
            repo.Modificar(contrato);
        return RedirectToAction("Index");
    }

    // public IActionResult Eliminar(int id){
    //     repo.Baja(id);
    //     return RedirectToAction("Index");
    // }
   
}
