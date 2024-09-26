using MySql.Data.MySqlClient;

namespace net.Models;

public class RepositorioUso : RepositorioBase
{
    public List<UsoInmueble> ObtenerTodos(){
        List<UsoInmueble> usos = new List<UsoInmueble>();
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
           var query = $@"SELECT 
           id_uso_inmueble AS UsoId,
           valor AS UsoValor
           FROM uso_inmueble";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
               connection.Open();
               var reader = command.ExecuteReader();
               while(reader.Read()){
                   usos.Add(new UsoInmueble{
                        UsoId = reader.GetInt32(nameof(UsoInmueble.UsoId)),
                        UsoValor = reader.GetString(nameof(UsoInmueble.UsoValor)),
                            
                   });
               }
           }
        }
        return usos;
    }
    public UsoInmueble? ObtenerUno(int id){
        UsoInmueble? uso = null;
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
           var query = $@"SELECT 
           id_uso_inmueble AS UsoId,
           valor AS UsoValor
           FROM uso_inmueble
           WHERE id_uso_inmueble = @id";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
                command.Parameters.AddWithValue("@id", id);
               connection.Open();
               var reader = command.ExecuteReader();
               if(reader.Read()){
                   uso = new UsoInmueble{
                        UsoId = reader.GetInt32(nameof(UsoInmueble.UsoId)),
                        UsoValor = reader.GetString(nameof(UsoInmueble.UsoValor)),
                            
                   };
               }
               connection.Close();
           }
        }
        return uso;
    }

}
