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
    // public IActionResult Guardar(int id, Inquilino inquilino)
    // {

    //     //Melian
    //     if (!ModelState.IsValid)
    //     {
    //         //Si el modelo no es valido, se regresa a la vista de edicion
    //         return View("Edicion", inquilino);
    //     }

    //     id = inquilino.InquilinoId;
    //     if (id == 0)
    //     {
    //         repo.Alta(inquilino);
    //         TempData["Mensaje"] = "Inquilino guardado";
    //     }

    //     else
    //     {
    //         repo.Modificar(inquilino);
    //         TempData["Mensaje"] = "Cambios guardados";
    //     }
    //     return RedirectToAction("Index");
    // }
    public IActionResult Guardar(int id, Inquilino inquilino)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return View("Edicion", inquilino);
            }

            id = inquilino.InquilinoId;
            if (id == 0)
            {
                repo.Alta(inquilino);
                TempData["Mensaje"] = "Inquilino guardado";
            }
            else
            {
                repo.Modificar(inquilino);
                TempData["Mensaje"] = "Cambios guardados";
            }

            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return RedirectToAction("Edicion", new { id = inquilino.InquilinoId });
        }
    }


    public IActionResult Eliminar(int id){
        int res=repo.Baja(id);
        if(res==-1)
            TempData["Error"] = "No se pudo eliminar el inquilino";
        else
            TempData["Mensaje"] = "El inquilino se elimino";
        return RedirectToAction("Index");
    }
   
}
