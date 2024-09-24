using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using net.Models;

namespace net.Controllers;
[Authorize]
public class PagoController : Controller
{

    private readonly ILogger<PagoController> _logger;
    private RepositorioPago repo = new RepositorioPago();
    private readonly RepositorioPago repoInquilino = new RepositorioPago();
    private RepositorioPago repoInmueble = new RepositorioPago();

    public PagoController(ILogger<PagoController> logger)
    {
        _logger = logger;
    }


    public IActionResult Index()
    {
        var lista = repo.ObtenerTodos();
        return View(lista);
    }

    public IActionResult Detalle(int id)
    {
        var pago = repo.ObtenerUno(id);
        return View(pago);
    }

    public IActionResult Edicion(int id, Pago pago)
    {
        id=pago.PagoId;
        if (id == 0)
        {
            repo.Agregar(pago);
            TempData["Mensaje"] = "Pago guardado";
        }
        else
        {
            repo.Modificar(pago);
            TempData["Mensaje"] = "Cambios guardados";
        }
        return RedirectToAction("Index","Contrato");
    }
    [Authorize(Policy = "Administrador")]
    public IActionResult Eliminar(int id)
    {
        var res = repo.Baja(id);
        if (res == -1)
        {
            TempData["Error"] = "No se pudo eliminar el pago";
        }
        else
        {
            TempData["Exito"] = "El pago se elimino";
        }
        return RedirectToAction("Index");
    }
}