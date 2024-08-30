namespace net.Models;

public class Direccion
{
    public int id_direccion { get; set; }
    public string? calle { get; set; }
    public int altura { get; set; }
    public int piso { get; set; }
    public string? departamento { get; set; }
     public string? observaciones { get; set; }

     public override string ToString()
		{
			var res = $"{calle} {altura}";
			return res;
		}
}