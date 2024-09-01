namespace net.Models;

public class Contrato{

    public int ContratoId { get; set; }

    public int IdInquilino { get; set; }
    
    public int IdInmueble { get; set; }

    public DateOnly Desde { get; set; }

    public DateOnly Hasta { get; set; }

    public decimal Precio { get; set; }

    public bool Estado { get; set; }  = true;

    public Inquilino? Inquilino { get; set; }

    public Inmueble? Inmueble { get; set; }
}