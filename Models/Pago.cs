namespace net.Models;

public class Pago{

    public int PagoId { get; set; }
    public int IdContrato { get; set; }
    public int Numero { get; set; }
    public decimal Importe { get; set; }
    public DateOnly Fecha { get; set; }
}