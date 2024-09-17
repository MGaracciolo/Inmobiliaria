
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using net.Models;

namespace net.Controllers;
//[Authorize]//para q permita solo a logueados, la validacion si es admin la hago abajo
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
		if(!User.IsInRole("Administrador")){
			TempData["Error"] = "Acceso denegado";
			return Redirect("/Home/Index");
		}else{
			var lista = repo.ObtenerTodos();
			return View(lista);
		}
	}
	
	public IActionResult Detalle()
	{
		if(!User.IsInRole("Administrador")){
			TempData["Error"] = "Acceso denegado";
			return Redirect("/Home/Index");
		}else{
			var usuario = repo.ObtenerPorEmail(User.Identity.Name);
			ViewBag.Roles = Usuario.ObtenerRoles();
			return View(usuario);
		}
	}

	public ActionResult Perfil()
		{
		
			var usuario = repo.ObtenerPorEmail(User.Identity.Name);
			ViewBag.Roles = Usuario.ObtenerRoles();
			return View("Detalle", usuario);
		}
	
	public IActionResult Creacion()
	{
		if(!User.IsInRole("Administrador")){
			TempData["Error"] = "Acceso denegado";
			return Redirect("/Home/Index");
		}else{
			ViewBag.Roles = Usuario.ObtenerRoles();
			return View();
		}
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
				string fileName = "avatar_" + usuario.UsuarioId + Path.GetExtension(usuario.AvatarFile.FileName);
				string pathCompleto = Path.Combine(path, fileName);
				usuario.Avatar = Path.Combine("/Avatars", fileName);



				 if (ModelState.ContainsKey("AvatarFile"))
				{
					var errors = ModelState["AvatarFile"].Errors;
				}
				return View(usuario);

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

	public ActionResult Edicion(int id)
	{
		if(!User.IsInRole("Administrador")){
			TempData["Error"] = "Acceso denegado";
			return Redirect("/Home/Index");
		}else{
			var usuario = repo.ObtenerUno(id);
			ViewBag.Roles = Usuario.ObtenerRoles();
			return View(usuario);
		}
		
	}

	[HttpPost]
	public ActionResult Edicion(int id, Usuario u)
	{
		var vista = nameof(Edicion);//de que vista provengo
		try
		{
			if (!User.IsInRole("Administrador"))//no soy admin
			{
				vista = nameof(Perfil);//solo puedo ver mi perfil
				var usuarioActual = repo.ObtenerPorEmail(User.Identity.Name);
				if (usuarioActual.UsuarioId != id)//si no es admin, solo puede modificarse él mismo
					return RedirectToAction(nameof(Index), "Home");
			}

			return RedirectToAction(vista);
		}
		catch (Exception ex)
		{//colocar breakpoints en la siguiente línea por si algo falla
			TempData["Error"] = ex.Message;
			return RedirectToAction(vista);
			throw;
		}
	}
	
	public IActionResult Eliminar(int id)
	{
		if(!User.IsInRole("Administrador")){
			TempData["Error"] = "Acceso denegado";
			return Redirect("/Home/Index");
		}else{
			int res = repo.Baja(id);
			if (res == -1)
				TempData["Error"] = "No se pudo eliminar el usuario";
			else
				TempData["Mensaje"] = "El usuario se elimino";
			return RedirectToAction("Index");
		}
		
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

