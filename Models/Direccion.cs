namespace net.Models;

public class Direccion
{
    public int id_direccion { get; set; }
    public string? calle { get; set; }
    public int altura { get; set; }
    public string? piso { get; set; }
    public string? departamento { get; set; }
    // latitud y longitud lo dejamos en direccion o 
    //en inmueble?
    public string? latitud { get; set; }
    public string? longitud { get; set; }
     public string? observaciones { get; set; }
}