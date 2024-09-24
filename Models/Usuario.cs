namespace net.Models;
using System.ComponentModel.DataAnnotations;

public class Usuario
{
    public enum Roles
	{
		Administrador = 1,
		Empleado = 2,
	}
    public int UsuarioId { get; set; }
    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "El nombre solo puede contener letras y espacios.")]
    [MinLength(2, ErrorMessage = "El nombre debe tener al menos 2 caracteres.")]    
    public string? Nombre { get; set; }
    [Required(ErrorMessage = "El apellido es obligatorio.")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "El apellido solo puede contener letras y espacios.")]
    [MinLength(2, ErrorMessage = "El apellido debe tener al menos 2 caracteres.")]
    public string? Apellido { get; set; }

    [Required(ErrorMessage = "El email es obligatorio.")]
    [EmailAddress(ErrorMessage = "El email no tiene un formato válido.")]
    public string? Email { get; set; }
    public string? Password { get; set; }
    [Required(ErrorMessage = "El rol es obligatorio.")]
    public int Rol { get; set; }
    //Ruta del avatar
    public string? Avatar { get; set; } ="";
    public IFormFile? AvatarFile { get; set; }
    public string RolNombre => Rol > 0 ? ((Roles)Rol).ToString() : "-";
    public static IDictionary<int, string> ObtenerRoles()
		{
			SortedDictionary<int, string> roles = new SortedDictionary<int, string>();
			Type tipoRoles = typeof(Roles);
			foreach (var valor in Enum.GetValues(tipoRoles))
			{
				roles.Add((int)valor, Enum.GetName(tipoRoles, valor));
			}
			return roles;
		}
    
    public override string ToString()
    {
        return $"{Nombre} {Apellido}";
    }
}