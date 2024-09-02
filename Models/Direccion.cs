namespace net.Models;
using System.ComponentModel.DataAnnotations;

public class Direccion
{
    public int DireccionId { get; set; }

    [Required(ErrorMessage = "La calle es obligatoria.")]//Melian
    [MinLength(2, ErrorMessage = "La calle debe tener al menos 2 caracteres.")]
    public string? Calle { get; set; }
    
    [Required(ErrorMessage = "La altura es obligatoria.")]
    [Range(1, int.MaxValue, ErrorMessage = "La altura debe ser un número entero positivo.")]
    public int Altura { get; set; }
    public int? Piso { get; set; }
    public string? Departamento { get; set; }
     public string? Observaciones { get; set; }

     public override string ToString()
		{
			var res = $"{Calle} {Altura}";
			return res;
		}
}