using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using net.Models;

namespace net.Controllers;

public class UsuarioController : Controller
{
    private readonly ILogger<UsuarioController> _logger;
    private RepositorioUsuario repo = new RepositorioUsuario();

    public UsuarioController(ILogger<UsuarioController> logger)
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
            var usuario = repo.ObtenerUno(id);
            return View(usuario);
        }
    }
    public IActionResult Detalle(int id)
    {
        if(id == 0)  
            return View();
        else
        {
            var usuario = repo.ObtenerUno(id);
            return View(usuario);
        }
    }
    
    public IActionResult Guardar(int id, Usuario usuario)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return View("Edicion", usuario);
            }

            id = usuario.UsuarioId;
            if (id == 0)
            {
                repo.Alta(usuario);
                TempData["Mensaje"] = "Usuario guardado";
            }
            else
            {
                repo.Modificar(usuario);
                TempData["Mensaje"] = "Cambios guardados";
            }
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
            return RedirectToAction("Edicion");
        }
    }


    public IActionResult Eliminar(int id){
        int res=repo.Baja(id);
        if(res==-1)
            TempData["Error"] = "No se pudo eliminar el usuario";
        else
            TempData["Mensaje"] = "El usuario se elimino";
        return RedirectToAction("Index");
    }
     
}
