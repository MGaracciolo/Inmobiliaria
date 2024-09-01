namespace net.Models;

public class Inmueble{
    public int InmuebleId { get; set; }

    public int IdDireccion { get; set; }

    public string? Latitud { get; set; }

    public string? Longitud { get; set; }

    public int IdPropietario { get; set; }

    public int IdUso { get; set; }

    public int IdTipo { get; set; }

    public int Ambientes { get; set; }

    public decimal Precio { get; set; }

    public bool Estado { get; set; }  = true;

    public Propietario? Propietario { get; set; }

    public Direccion? Direccion { get; set; }

    public TipoInmueble? TipoInmueble { get; set; }

    public UsoInmueble? UsoInmueble { get; set; }

}