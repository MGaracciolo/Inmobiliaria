namespace net.Models;

public class Contrato{

    public int id_contrato { get; set; }

    public int id_inquilino { get; set; }
    //deberia tener el id del propietario?
    public int id_inmueble { get; set; }

    public DateTime desde { get; set; }

    public DateTime hasta { get; set; }

    public double precio { get; set; }

    public bool estado { get; set; }
}