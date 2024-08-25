namespace net.Models;

public class Inmueble{
    public int id_inmueble { get; set; }

    public int id_direccion { get; set; }

    public string? latitud { get; set; }

    public string? longitud { get; set; }

    public int id_propietario { get; set; }

    public string? uso { get; set; }

    public string? tipo { get; set; }

    public int ambientes { get; set; }

    public double precio { get; set; }

    public bool estado { get; set; }
}