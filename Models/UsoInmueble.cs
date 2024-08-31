namespace net.Models;

public class UsoInmueble
{
    public int Id { get; set; }

    public string? Valor { get; set; }

    public override string ToString()
		{
			var res = $"{Valor}";
			return res;
		}
}