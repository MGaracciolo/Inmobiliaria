namespace net.Models;
using System.ComponentModel.DataAnnotations;

public class Inquilino
{
    public int id_inquilino { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]//Melian
    public string? nombre { get; set; }

    [Required(ErrorMessage = "El apellido es obligatorio.")]
    public string? apellido { get; set; }

    [Required(ErrorMessage = "El DNI es obligatorio.")]
    [RegularExpression(@"\d{7,8}", ErrorMessage = "El DNI debe tener entre 7 y 8 dígitos.")]
    public string? dni { get; set; }

    [Required(ErrorMessage = "El email es obligatorio.")]
    [EmailAddress(ErrorMessage = "El email no tiene un formato válido.")]
    public string? email { get; set; }

    [Required(ErrorMessage = "El teléfono es obligatorio.")]
    [Phone(ErrorMessage = "El teléfono no tiene un formato válido.")]
    public string? telefono { get; set; }

    public override string ToString()
		{
			var res = $"{nombre} {apellido} {dni}";
			return res;
		}
}
