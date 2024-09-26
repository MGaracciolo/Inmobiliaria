namespace net.Models;

public class Contrato{

    public int ContratoId { get; set; }

    public int IdInquilino { get; set; }
    
    public int IdInmueble { get; set; }

    public DateTime Desde { get; set; }

    public DateTime Hasta { get; set; }

    public int Meses { get; set; }

    public decimal PrecioContrato { get; set; }

    public bool Estado { get; set; }  = true;

    public int IdCreador { get; set; }

    public int? IdAnulador { get; set; }

    public DateTime? Anulacion { get; set; } // FechaAnulacion

    public decimal Multa { get; set; }

    public Inquilino? Inquilino { get; set; }

    public Inmueble? Inmueble { get; set; }

    public Usuario? Creador { get; set; }

    public Usuario? Anulador { get; set; }

    public List<Pago>? Pagos { get; set; }//Nuevo. Melian

    public override string ToString()
    {
        var res = $"{ContratoId} - {Inquilino?.ToString()} - {Inmueble.ToString()}";
        return $"{res}";
    }
}