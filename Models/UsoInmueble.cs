namespace net.Models;

public class UsoInmueble
{
    public int id_uso_inmueble { get; set; }

    public string? valor { get; set; }

    public override string ToString()
		{
			var res = $"{valor}";
			return res;
		}
}