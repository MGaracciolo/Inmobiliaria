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
        //Melian
        var hoy = DateTime.Now;//En base a la fecha de hoy muestro los contratos vigentes
        var contratosVigentes = lista.Where(c => c.Desde <= hoy && c.Hasta >= hoy).ToList();

        return View(lista);
    }
    /*endpoint*/
    public IActionResult Edicion(int id)
    {
        ViewBag.Inquilinos = repoInquilino.ObtenerTodos();
        ViewBag.Inmuebles = repoInmueble.ObtenerTodos();
        if(id == 0) {
            
            return View();
        }
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
        if(id == 0){
            repo.Alta(contrato);
            TempData["Mensaje"] = "Contrato generado";
        }
        else
        {
            repo.Modificar(contrato);
            TempData["Mensaje"] = "Cambios guardados";
        }
        return RedirectToAction("Index");
    }

    public IActionResult Eliminar(int id){
        int res =repo.Baja(id);
        if(res == -1)
            TempData["Error"] = "No se pudo dar de baja el contrato";
        else
            TempData["Mensaje"] = "El contrato quedo inactivo";
        return RedirectToAction("Index");
    }

     public IActionResult Activar(int id){
        int res =repo.Restore(id);
        if(res == -1)
            TempData["Error"] = "No se pudo activar el contrato";
        else
            TempData["Mensaje"] = "El contrato se activo";
        return RedirectToAction("Index");
    }
   
}
