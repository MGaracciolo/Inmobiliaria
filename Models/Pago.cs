namespace net.Models;

public class Pago{

    public int Id { get; set; }
    public int IdContrato { get; set; }
    public int Numero { get; set; }
    public decimal Importe { get; set; }
    public DateTime Fecha { get; set; }
}