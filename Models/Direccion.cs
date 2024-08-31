namespace net.Models;

public class Direccion
{
    public int Id { get; set; }
    public string? Calle { get; set; }
    public int Altura { get; set; }
    public int Piso { get; set; }
    public string? Departamento { get; set; }
     public string? Observaciones { get; set; }

     public override string ToString()
		{
			var res = $"{Calle} {Altura}";
			return res;
		}
}