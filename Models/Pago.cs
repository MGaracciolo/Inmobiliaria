namespace net.Models;

public class Pago{

    public int id_pago { get; set; }
    public int id_contrato { get; set; }
    public int numero { get; set; }
    public double importe { get; set; }
    public DateTime fecha { get; set; }
}