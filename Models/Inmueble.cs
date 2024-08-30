namespace net.Models;

public class Inmueble{
    public int id_inmueble { get; set; }

    public int id_direccion { get; set; }

    public string? latitud { get; set; }

    public string? longitud { get; set; }

    public int id_propietario { get; set; }

    public int id_uso_inmueble { get; set; }

    public int id_tipo_inmueble { get; set; }

    public int ambientes { get; set; }

    public double precio { get; set; }

    public bool estado { get; set; }  = true;

    public Propietario? propietario { get; set; }

    public Direccion? direccion { get; set; }

    public TipoInmueble? tipoInmueble { get; set; }

    public UsoInmueble? usoInmueble { get; set; }

}