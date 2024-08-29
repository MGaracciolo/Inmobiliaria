namespace net.Models;
using System.ComponentModel.DataAnnotations;

public class Inquilino
{
    public int id_inquilino { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]//Melian
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "El nombre solo puede contener letras y espacios.")]
    [MinLength(2, ErrorMessage = "El nombre debe tener al menos 2 caracteres.")]
    public string? nombre { get; set; }

    [Required(ErrorMessage = "El apellido es obligatorio.")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "El apellido solo puede contener letras y espacios.")]
    [MinLength(2, ErrorMessage = "El apellido debe tener al menos 2 caracteres.")]
    public string? apellido { get; set; }

    [Required(ErrorMessage = "El DNI es obligatorio.")]
    [RegularExpression(@"^\d{7,8}[A-Z]?$", ErrorMessage = "El DNI debe tener entre 7 y 8 digitos, y puede incluir una letra al final.")]
    public string? dni { get; set; }

    [Required(ErrorMessage = "El email es obligatorio.")]
    [EmailAddress(ErrorMessage = "El email no tiene un formato válido.")]
    public string? email { get; set; }

    [Required(ErrorMessage = "El telefono es obligatorio.")]
    [Phone(ErrorMessage = "El telefono no tiene un formato válido.")]
    public string? telefono { get; set; }

    public override string ToString()
		{
			var res = $"{nombre} {apellido} {dni}";
			return res;
		}
}
