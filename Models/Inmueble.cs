namespace net.Models;
using System.ComponentModel.DataAnnotations;

public class Inmueble{
    public int InmuebleId { get; set; }

    [Required(ErrorMessage = "La dirección es obligatoria.")]
    [MinLength(10, ErrorMessage = "La dirección debe tener al menos 10 caracteres.")]
    public string? DireccionI { get; set; }

    [Required(ErrorMessage = "La latitud es obligatoria.")]
    public string? Latitud { get; set; }

     [Required(ErrorMessage = "La longitud es obligatoria.")]
    public string? Longitud { get; set; }

     [Required(ErrorMessage = "Seleccione un propietario.")]
    public int IdPropietario { get; set; }

    [Required(ErrorMessage = "Seleccione un uso.")]
    public int IdUso { get; set; }

    [Required(ErrorMessage = "Seleccione un tipo de inmueble.")]
    public int IdTipo { get; set; }

    [Required(ErrorMessage = "La cantidad de ambientes es obligatoria.")]
    public int Ambientes { get; set; }

    [Required(ErrorMessage = "El precio es obligatorio.")]
    public decimal Precio { get; set; }

    public bool Estado { get; set; }  = true;

    public Propietario? Propietario { get; set; }

    public TipoInmueble? TipoInmueble { get; set; }

    public UsoInmueble? UsoInmueble { get; set; }

}