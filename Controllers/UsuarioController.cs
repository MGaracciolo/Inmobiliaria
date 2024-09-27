
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

	// public IActionResult Detalle(int id)
	// {
	// 	if (!User.IsInRole("Administrador"))
	// 	{
	// 		TempData["Error"] = "Acceso denegado";
	// 		return Redirect("/Home/Index");
	// 	}
	// 	//cuando es admin
	// 		var usuario = repo.ObtenerPorEmail(User.Identity.Name);
	// 	//cuando no es admin
	// 	// var usuario =repo.ObtenerUno(id);
	// 	ViewBag.Roles = Usuario.ObtenerRoles();
	// 	return View(usuario);

	// }
	public IActionResult Detalle(int? id)
	{
		// var usuarioIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
		// if (!User.IsInRole("Administrador") && usuarioIdClaim != null && usuario.UsuarioId != int.Parse(usuarioIdClaim))
		// {
		// 	TempData["Error"] = "Acceso denegado";
		// 	return Redirect("/Home/Index");
		// }

		Usuario usuario;

		if (User.IsInRole("Administrador"))
		{
			if (id.HasValue)
			{
				usuario = repo.ObtenerUno(id.Value);
			}
			else
			{
				usuario = repo.ObtenerPorEmail(User.FindFirstValue(ClaimTypes.Email));
			}
		}
		else
		{
			usuario = repo.ObtenerPorEmail(User.FindFirstValue(ClaimTypes.Email));
			if (usuario == null)
			{
				TempData["Error"] = "Acceso denegado";
				return Redirect("/Home/Index");
			}
		}

		ViewBag.Roles = Usuario.ObtenerRoles();
		return View(usuario);

	}

	public ActionResult Perfil()
	{

		var usuario = repo.ObtenerPorEmail(User.FindFirstValue(ClaimTypes.Email));
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
		if (!User.IsInRole("Administrador") && id != int.Parse(User.Claims.First().Value))
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

	public async Task<IActionResult> ModificarUsuario(Usuario usuario)
	{
		Usuario usuarioExistente = repo.ObtenerUno(usuario.UsuarioId);
		if (usuarioExistente == null)
		{
			TempData["Error"] = "Usuario no encontrado";
			return RedirectToAction("Index", "Home");
		}

		var usuarioIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
		if (!User.IsInRole("Administrador") && usuarioIdClaim != null && usuario.UsuarioId != int.Parse(usuarioIdClaim))
		{
			TempData["Error"] = "Acceso denegado";
			return Redirect("/Home/Index");
		}
		if (usuarioExistente.Email != usuario.Email || usuarioExistente.RolNombre != usuario.RolNombre)
		{
			try
			{
				await ActualizarClaim(usuario);
			}
			catch (Exception ex)
			{
				TempData["Error"] = $"No se pudo actualizar el claim: {ex.Message}";
				return RedirectToAction("Index", "Home");
			}
		}
		int res = repo.ModificarDatos(usuario);
		if (res == -1)
		{
			TempData["Error"] = "No se pudo modificar el usuario";
		}
		else
		{
			TempData["Mensaje"] = "Usuario modificado correctamente";
		}

		return RedirectToAction("Index", "Home");
	}
	public async Task ActualizarClaim(Usuario usuario)
	{
		var identity = (ClaimsIdentity)User.Identity;
		var emailClaim = identity.FindFirst(ClaimTypes.Email);
		if (emailClaim != null)
		{
			identity.RemoveClaim(emailClaim);
		}
		var roleClaim = identity.FindFirst(ClaimTypes.Role);
		if (roleClaim != null)
		{
			identity.RemoveClaim(roleClaim);
		}
		identity.AddClaim(new Claim(ClaimTypes.Email, usuario.Email));
		identity.AddClaim(new Claim(ClaimTypes.Role, usuario.RolNombre));
		await HttpContext.SignInAsync(
			CookieAuthenticationDefaults.AuthenticationScheme,
			new ClaimsPrincipal(identity));
	}

	[HttpPost]//Melian
	public async Task<IActionResult> CambiarEmail(string newEmail)
	{
		var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
		var usuario = repo.ObtenerUno(userId);

		if (usuario != null)
		{
			usuario.Email = newEmail;
			int res = repo.ModificarEmail(usuario);

			if (res == -1)
				TempData["Error"] = "No se pudo cambiar el email.";
			else
			{
				//Actualizar la claim del mail
				var identity = (ClaimsIdentity)User.Identity;
				identity.RemoveClaim(identity.FindFirst(ClaimTypes.Email));
				identity.AddClaim(new Claim(ClaimTypes.Email, newEmail));

				await HttpContext.SignInAsync(
					CookieAuthenticationDefaults.AuthenticationScheme,
					new ClaimsPrincipal(identity));

				TempData["Mensaje"] = "Email cambiado exitosamente.";
			}
		}
		else
		{
			TempData["Error"] = "Usuario no encontrado.";
		}

		return RedirectToAction("Index", "Home");
	}

	public IActionResult CambiarPass(int id)//Melian
	{
		var usuario = repo.ObtenerUno(id);
		return View(usuario);
	}
	[HttpPost]
	public IActionResult ModificarPassword(Usuario usuario, string currentPassword, string newPassword, string confirmPassword)
	{
		var usuarioDb = repo.ObtenerUno(usuario.UsuarioId);

		if (usuarioDb == null)
		{
			TempData["Error"] = "Usuario no encontrado";
			return Redirect("/Home/Index");
		}

		var usuarioIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
		if (!User.IsInRole("Administrador") && usuarioIdClaim != null && usuario.UsuarioId != int.Parse(usuarioIdClaim))
		{
			TempData["Error"] = "Acceso denegado";
			return Redirect("/Home/Index");
		}

		// Verificar que las nuevas contraseñas coincidan
		if (newPassword != confirmPassword)
		{
			TempData["Error"] = "Las contraseñas no coinciden";
			return RedirectToAction("Perfil");
		}

		// Verificar la contraseña actual
		string hashedCurrentPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
								password: currentPassword,
								salt: System.Text.Encoding.ASCII.GetBytes(configuration["Salt"]),
								prf: KeyDerivationPrf.HMACSHA1,
								iterationCount: 1000,
								numBytesRequested: 256 / 8));

		if (hashedCurrentPassword != usuarioDb.Password)
		{
			TempData["Error"] = "Contraseña actual incorrecta";
			return RedirectToAction("Perfil");
		}

		// Cambiar la contraseña
		string hashedNewPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
								password: newPassword,
								salt: System.Text.Encoding.ASCII.GetBytes(configuration["Salt"]),
								prf: KeyDerivationPrf.HMACSHA1,
								iterationCount: 1000,
								numBytesRequested: 256 / 8));

		usuario.Password = hashedNewPassword;
		int res = repo.ModificarClave(usuario);

		if (res == -1)
			TempData["Error"] = "No se pudo modificar la clave";
		else
			TempData["Mensaje"] = "Cambios guardados";

		return RedirectToAction("Perfil");
	}

	public ActionResult EliminarAvatar(int id)
	{
		Usuario usuario = repo.ObtenerUno(id);
		var usuarioIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

		if (!User.IsInRole("Administrador") && usuarioIdClaim != null && usuario.UsuarioId != int.Parse(usuarioIdClaim))
		{
			TempData["Error"] = "Acceso denegado";
			return Redirect("/Home/Index");
		}
		string wwwPath = environment.WebRootPath;
		string path = Path.Combine(wwwPath, "avatars");
		string oldAvatarPath = Path.Combine(wwwPath, usuario.Avatar.TrimStart('/'));
		
		if (System.IO.File.Exists(oldAvatarPath))
		{
			System.IO.File.Delete(oldAvatarPath);
		}
		usuario.Avatar = null;

		int res = repo.ModificarAvatar(usuario);
		if (res == -1)
		{
			TempData["Error"] = "No se pudo eliminar el avatar";
		}
		else
		{
			TempData["Mensaje"] = "Avatar eliminado correctamente";
		}

		return RedirectToAction("Detalle", "Usuario",id);
	}


	public ActionResult ModificarAvatar(Usuario usuario)
	{
		var usuarioIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
		if (!User.IsInRole("Administrador") && usuarioIdClaim != null && usuario.UsuarioId != int.Parse(usuarioIdClaim))
		{
			TempData["Error"] = "Acceso denegado";
			return Redirect("/Home/Index");
		}
		if (usuario.AvatarFile != null)
		{
			string wwwPath = environment.WebRootPath;
			string path = Path.Combine(wwwPath, "avatars");

			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path); 
			}

			string oldAvatarPath = Path.Combine(wwwPath, usuario.Avatar.TrimStart('/')); 
			if (System.IO.File.Exists(oldAvatarPath))
			{
				System.IO.File.Delete(oldAvatarPath); 
			}

			string fileName = "avatar_" + usuario.UsuarioId + Path.GetExtension(usuario.AvatarFile.FileName);
			string pathCompleto = Path.Combine(path, fileName);
			usuario.Avatar = Path.Combine("/avatars", fileName);

			using (FileStream stream = new FileStream(pathCompleto, FileMode.Create))
			{
				usuario.AvatarFile.CopyTo(stream); 
			}
		}

		int res = repo.ModificarAvatar(usuario);
		if (res == -1)
			TempData["Error"] = "No se pudo modificar el avatar";
		else
			TempData["Mensaje"] = "Cambios guardados";
		return RedirectToAction("Detalle", "Usuario");
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
					new Claim(ClaimTypes.Email, usuario.Email),
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

