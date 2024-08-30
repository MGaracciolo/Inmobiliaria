namespace net.Models;

public class TipoInmueble
{
    public int id_tipo_inmueble { get; set; }

    public string? valor { get; set; }

    public override string ToString()
		{
			var res = $"{valor}";
			return res;
		}
}