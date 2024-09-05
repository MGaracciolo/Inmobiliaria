namespace net.Models;
using System.ComponentModel.DataAnnotations;

public class Propietario
{
    public int PropietarioId { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "El nombre solo puede contener letras y espacios.")]
    [MinLength(2, ErrorMessage = "El nombre debe tener al menos 2 caracteres.")]
    public string? Nombre { get; set; }

    [Required(ErrorMessage = "El apellido es obligatorio.")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "El apellido solo puede contener letras y espacios.")]
    [MinLength(2, ErrorMessage = "El apellido debe tener al menos 2 caracteres.")]
    public string? Apellido { get; set; }

    [Required(ErrorMessage = "El DNI es obligatorio.")]
    [RegularExpression(@"^\d{7,8}[A-Z]?$", ErrorMessage = "El DNI debe tener entre 7 y 8 digitos, y puede incluir una letra al final.")]
    public string? Dni { get; set; }

    [Required(ErrorMessage = "El email es obligatorio.")]
    [EmailAddress(ErrorMessage = "El email no tiene un formato válido.")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "El telefono es obligatorio.")]
    [Phone(ErrorMessage = "El telefono no tiene un formato válido.")]
    public string? Telefono { get; set; }

    [Required(ErrorMessage = "La dirección es obligatoria.")]
    [MinLength(10, ErrorMessage = "La dirección debe tener al menos 10 caracteres.")]
    public string? DireccionP { get; set; }

    public bool EstadoP { get; set; }
    public override string ToString()
		{
			var res = $"{Dni} {Nombre} {Apellido}";
			return res;
		}

}
