using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using net.Models;

namespace net.Controllers;

public class ContratoController : Controller
{
    private readonly ILogger<ContratoController> _logger;
    private RepositorioContrato repo = new RepositorioContrato();
    private readonly RepositorioInquilino repoInquilino = new RepositorioInquilino();
    private RepositorioInmueble repoInmueble = new RepositorioInmueble();

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
        ViewBag.Inquilinos = repoInquilino.ObtenerTodos();
        ViewBag.Inmuebles = repoInmueble.ObtenerTodos();
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
            var inquilino = repoInquilino.ObtenerUno(contrato.IdInquilino);
            var inmueble = repoInmueble.ObtenerUno(contrato.IdInmueble);
            contrato.Inquilino = inquilino;
            contrato.Inmueble = inmueble;
            return View(contrato);
        }
    }
    [HttpPost]
    public IActionResult Guardar(int id, Contrato contrato){
        id=contrato.ContratoId;
        if(id == 0)
            repo.Alta(contrato);
        else
            repo.Modificar(contrato);
        return RedirectToAction("Index");
    }

    public IActionResult Eliminar(int id){
        repo.Baja(id);
        return RedirectToAction("Index");
    }
   
}
