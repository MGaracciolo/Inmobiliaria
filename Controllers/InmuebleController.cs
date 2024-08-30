using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using net.Models;

namespace net.Controllers;

public class InmuebleController : Controller
{
    private readonly ILogger<InmuebleController> _logger;
    private RepositorioInmueble repo = new RepositorioInmueble();
    private readonly RepositorioPropietario repoPropietario = new RepositorioPropietario();
    private readonly RepositorioTipo repoTipo = new RepositorioTipo();
    private readonly RepositorioUso repoUso = new RepositorioUso();
    private readonly RepositorioDireccion repoDireccion = new RepositorioDireccion();

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
        ViewBag.Propietarios = repoPropietario.ObtenerTodos();
        ViewBag.Direcciones = repoDireccion.ObtenerTodos();
        ViewBag.Usos = repoUso.ObtenerTodos();
        ViewBag.Tipos = repoTipo.ObtenerTodos();
        if(id == 0) 
        {
            
            return View();
        }
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
            var propietario = repoPropietario.ObtenerUno(inmueble.id_propietario);
            var uso = repoUso.ObtenerUno(inmueble.id_uso_inmueble);
            var direccion = repoDireccion.ObtenerUno(inmueble.id_direccion);
            var tipo = repoTipo.ObtenerUno(inmueble.id_tipo_inmueble);
            inmueble.propietario = propietario;
            inmueble.usoInmueble = uso;
            inmueble.direccion = direccion;
            inmueble.tipoInmueble = tipo;
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
