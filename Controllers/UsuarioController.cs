
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using net.Models;

namespace net.Controllers;

public class UsuarioController : Controller
{
    private readonly IConfiguration configuration;
    private readonly ILogger<UsuarioController> _logger;
    private readonly IWebHostEnvironment environment;
    private RepositorioUsuario repo = new RepositorioUsuario();

    public UsuarioController(IConfiguration configuration,ILogger<UsuarioController> logger, IWebHostEnvironment environment)
    {
        this.configuration = configuration;
        _logger = logger;
        this.environment = environment;
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
            ViewBag.Roles = Usuario.ObtenerRoles();
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

    public IActionResult Creacion(){
        ViewBag.Roles = Usuario.ObtenerRoles();
		return View(new Usuario());
    }

    [HttpPost]
    public IActionResult Creacion(Usuario usuario)
    {
        
            if (!ModelState.IsValid)
            {
                return View();
            }
        try
        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
								password: usuario.Password,
								salt: System.Text.Encoding.ASCII.GetBytes(configuration["Salt"]),
								prf: KeyDerivationPrf.HMACSHA1,
								iterationCount: 1000,
								numBytesRequested: 256 / 8));
				usuario.Password = hashed;
            int res = repo.Alta(usuario);
            if (usuario.AvatarFile != null && usuario.UsuarioId > 0)
				{
                    //para entrar a wwwroot
					string wwwPath = environment.WebRootPath;
                    //para entrar a avatars dentro de wwwroot
					string path = Path.Combine(wwwPath, "avatars");
					if (!Directory.Exists(path))
					{//si no existe la carpeta la crea
						Directory.CreateDirectory(path);
					}
					//Path.GetFileName(u.AvatarFile.FileName);//este nombre se puede repetir
					string fileName = "avatar_" + usuario.UsuarioId + Path.GetExtension(usuario.AvatarFile.FileName);
					string pathCompleto = Path.Combine(path, fileName);
					usuario.Avatar = Path.Combine("/avatars", fileName);
					// Esta operación guarda la foto en memoria en la ruta que necesitamos
					using (FileStream stream = new FileStream(pathCompleto, FileMode.Create))
					{
						usuario.AvatarFile.CopyTo(stream);
					}
					repo.Modificar(usuario);//para agregar la url en avatar
				}
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            ViewBag.Roles = Usuario.ObtenerRoles();
            TempData["Error"] = ex.Message;
            return View();
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
