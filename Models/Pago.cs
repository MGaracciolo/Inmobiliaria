namespace net.Models;
using System.ComponentModel.DataAnnotations;

public class Pago{

    public int PagoId { get; set; }

    public int IdContrato { get; set; }

    public int Numero { get; set; }
    
    [Required(ErrorMessage = "Ingrese el importe")]
    public decimal Importe { get; set; }

    public DateTime Fecha { get; set; }

    public int CreadorId { get; set; }

    public int? AnuladorId { get; set; }

    public string? Concepto { get; set; }

    public bool Estado { get; set; }

    public Contrato? Contrato { get; set; }

    public Usuario? Creador { get; set; }

    public Usuario? Anulador { get; set; }
}