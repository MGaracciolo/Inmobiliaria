
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using net.Models;

namespace net.Controllers;
[Authorize]//para q permita solo a logueados, la validacion si es admin la hago abajo
public class UsuarioController : Controller
{
	private readonly IConfiguration configuration;
	private readonly ILogger<UsuarioController> _logger;
	private readonly IWebHostEnvironment environment;
	private RepositorioUsuario repo = new RepositorioUsuario();

	public UsuarioController(IConfiguration configuration, ILogger<UsuarioController> logger, IWebHostEnvironment environment)
	{
		this.configuration = configuration;
		_logger = logger;
		this.environment = environment;
	}

	public IActionResult Index()
	{
		if (!User.IsInRole("Administrador"))
		{
			TempData["Error"] = "Acceso denegado";
			return Redirect("/Home/Index");
		}
		var lista = repo.ObtenerTodos();
		return View(lista);

	}

	public IActionResult Detalle(int id)
	{
		if (!User.IsInRole("Administrador"))
		{
			TempData["Error"] = "Acceso denegado";
			return Redirect("/Home/Index");
		}
		//cuando es admin
			var usuario = repo.ObtenerPorEmail(User.Identity.Name);
		//cuando no es admin
		// var usuario =repo.ObtenerUno(id);
		ViewBag.Roles = Usuario.ObtenerRoles();
		return View(usuario);

	}

	public ActionResult Perfil()
	{

		var usuario = repo.ObtenerPorEmail(User.Identity.Name);
		ViewBag.Roles = Usuario.ObtenerRoles();
		return View("Detalle", usuario);
	}

	public IActionResult Creacion()
	{
		if (!User.IsInRole("Administrador"))
		{
			TempData["Error"] = "Acceso denegado";
			return Redirect("/Home/Index");
		}

		ViewBag.Roles = Usuario.ObtenerRoles();
		return View();

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
			usuario.UsuarioId = res;
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
			// }else{
			// 	TempData["Error"] = "No se pudo subir el archivo";
			// }
			return RedirectToAction("Index");
		}
		catch (Exception ex)
		{
			ViewBag.Roles = Usuario.ObtenerRoles();
			TempData["Error"] = ex.Message;
			return View();
		}
	}

	public ActionResult Edicion(int id)
	{
		if (!User.IsInRole("Administrador")  && id != int.Parse(User.Claims.First().Value))
		{
			TempData["Error"] = "Acceso denegado";
			return Redirect("/Home/Index");
		}
		if (id == 0)
            return View();
        ViewBag.Roles = Usuario.ObtenerRoles();
		var usuario = repo.ObtenerUno(id);
        return View(usuario);
	}

	public IActionResult ModificarDatos(Usuario usuario){
		if (!User.IsInRole("Administrador") && usuario.UsuarioId != int.Parse(User.Claims.First().Value))
		{
			TempData["Error"] = "Acceso denegado";
			return Redirect("/Home/Index");
		}
		int res = repo.ModificarDatos(usuario);
		if (res == -1)
			TempData["Error"] = "No se pudo modificar los datos";
		else
			TempData["Mensaje"] = "Cambios guardados";
		return RedirectToAction("Index","Home");
	}
	
	public IActionResult ModificarPassword(Usuario usuario){
		if (!User.IsInRole("Administrador") && usuario.UsuarioId != int.Parse(User.Claims.First().Value))
		{
			TempData["Error"] = "Acceso denegado";
			return Redirect("/Home/Index");
		}

		//verificar primero que las claves coincidan 

		string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
								password: usuario.Password,
								salt: System.Text.Encoding.ASCII.GetBytes(configuration["Salt"]),
								prf: KeyDerivationPrf.HMACSHA1,
								iterationCount: 1000,
								numBytesRequested: 256 / 8));
		usuario.Password = hashed;
		int res = repo.ModificarClave(usuario);
		if (res == -1)
			TempData["Error"] = "No se pudo modificar la clave";
		else
			TempData["Mensaje"] = "Cambios guardados";
		return RedirectToAction("Index","Home");
	}
	public IActionResult EliminarAvatar(Usuario usuario){
		if (!User.IsInRole("Administrador") && usuario.UsuarioId != int.Parse(User.Claims.First().Value))
		{
			TempData["Error"] = "Acceso denegado";
			return Redirect("/Home/Index");
		}
		int res = repo.ModificarAvatar(usuario);
		if (res == -1)
			TempData["Error"] = "No se pudo modificar el avatar";
		else
			TempData["Mensaje"] = "Se elimino el avatar";
		return RedirectToAction("Index","Home");
	}
	public ActionResult ModificarAvatar(Usuario usuario){
		if (!User.IsInRole("Administrador") && usuario.UsuarioId != int.Parse(User.Claims.First().Value))
		{
			TempData["Error"] = "Acceso denegado";
			return Redirect("/Home/Index");
		}
		int res = repo.ModificarAvatar(usuario);
		if (res == -1)
			TempData["Error"] = "No se pudo modificar el avatar";
		else
			TempData["Mensaje"] = "Cambios guardados";
		return RedirectToAction("Index","Home");
	}
	public IActionResult Eliminar(int id)
	{
		if (!User.IsInRole("Administrador"))
		{
			TempData["Error"] = "Acceso denegado";
			return Redirect("/Home/Index");
		}

		int res = repo.Baja(id);
		if (res == -1)
			TempData["Error"] = "No se pudo eliminar el usuario";
		else
			TempData["Mensaje"] = "El usuario se elimino";
		return RedirectToAction("Index");


	}

	[AllowAnonymous]
	public IActionResult LoginModal()
	{
		return PartialView("_Login");
	}

	[AllowAnonymous]
	public ActionResult Login(string returnUrl)
	{
		TempData["returnUrl"] = returnUrl;
		return RedirectToAction("Index", "Home");
	}

	[HttpPost]
	[AllowAnonymous]
	public async Task<IActionResult> Login(LoginView login)
	{
		try
		{
			var returnUrl = String.IsNullOrEmpty(TempData["returnUrl"] as string) ? "/" : TempData["returnUrl"].ToString();
			if (ModelState.IsValid)
			{
				string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
					password: login.Clave,
					salt: System.Text.Encoding.ASCII.GetBytes(configuration["Salt"]),
					prf: KeyDerivationPrf.HMACSHA1,
					iterationCount: 1000,
					numBytesRequested: 256 / 8));

				var usuario = repo.ObtenerPorEmail(login.Usuario);
				if (usuario == null || usuario.Password != hashed)
				{
					TempData["ErrorMessage"] = "El email o la clave no son correctos";
					TempData["returnUrl"] = returnUrl;
					return RedirectToAction("Index", "Home");
				}

				var claims = new List<Claim>
				{
					new Claim("Id", usuario.UsuarioId.ToString()),
					new Claim(ClaimTypes.Name, usuario.Email),
					new Claim("FullName", usuario.Nombre + " " + usuario.Apellido),
					new Claim(ClaimTypes.Role, usuario.RolNombre)
				};

				var claimsIdentity = new ClaimsIdentity(
					claims, CookieAuthenticationDefaults.AuthenticationScheme);

				await HttpContext.SignInAsync(
					CookieAuthenticationDefaults.AuthenticationScheme,
					new ClaimsPrincipal(claimsIdentity));

				TempData.Remove("returnUrl");
				return Redirect(returnUrl);
			}

			TempData["returnUrl"] = returnUrl;
			return RedirectToAction("Index", "Home");
		}
		catch (Exception ex)
		{
			TempData["ErrorMessage"] = "Ocurrió un error: " + ex.Message;
			return RedirectToAction("Index", "Home");
		}
	}

	[AllowAnonymous]
	public async Task<ActionResult> Logout()
	{
		await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
		return RedirectToAction("Index", "Home");
	}

}

