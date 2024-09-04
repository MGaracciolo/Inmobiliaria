namespace net.Models;
using System.ComponentModel.DataAnnotations;

public class Pago{

    public int PagoId { get; set; }

    public int IdContrato { get; set; }

    
    public int Numero { get; set; }
    
    [Required(ErrorMessage = "Ingrese el importe")]
    public decimal Importe { get; set; }

    public DateOnly Fecha { get; set; }
}