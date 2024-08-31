using Microsoft.AspNetCore.Http.Timeouts;
using MySql.Data.MySqlClient;

namespace net.Models;

public class RepositorioTipo : RepositorioBase
{
    public List<TipoInmueble> ObtenerTodos(){
        List<TipoInmueble> tipos = new List<TipoInmueble>();
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
           var query = $@"SELECT 
           id_tipo_inmueble AS Id,
           valor AS Valor
           FROM tipo_inmueble";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
               connection.Open();
               var reader = command.ExecuteReader();
               while(reader.Read()){
                   tipos.Add(new TipoInmueble{
                        Id = reader.GetInt32(nameof(TipoInmueble.Id)),
                        Valor = reader.GetString(nameof(TipoInmueble.Valor))
                            
                   });
               }
           }
        }
        return tipos;
    }
    public TipoInmueble? ObtenerUno(int id){
        TipoInmueble? tipo = null;
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
           var query = $@"SELECT 
           id_tipo_inmueble AS Id,
           valor AS Valor
           FROM tipo_inmueble
           WHERE id_tipo_inmueble = @id";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
                command.Parameters.AddWithValue("@id", id);
               connection.Open();
               var reader = command.ExecuteReader();
               if(reader.Read()){
                   tipo = new TipoInmueble{
                        Id = reader.GetInt32(nameof(TipoInmueble.Id)),
                        Valor = reader.GetString(nameof(TipoInmueble.Valor))
                            
                   };
               }
               connection.Close();
           }
        }
        return tipo;
    }

    public int Alta(TipoInmueble tipo){
        int res = -1;
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
           var query = $@"INSERT INTO tipo_inmueble
          (valor)
           VALUES(@valor);
           SELECT LAST_INSERT_ID();";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
               command.Parameters.AddWithValue("@valor", tipo.Valor);
               connection.Open();   
               res = Convert.ToInt32(command.ExecuteScalar());
               connection.Close();
           }
           return res;
        }
    }

    public int Modificar(TipoInmueble tipo){
        int res = -1;
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
           var query = $@"UPDATE tipo_inmueble
           SET valor = @valor
           WHERE id_tipo_inmueble = @id_tipo_inmueble";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
               command.Parameters.AddWithValue("@id_tipo_inmueble", tipo.Id);
               command.Parameters.AddWithValue("@valor", tipo.Valor);
               connection.Open();
               res = command.ExecuteNonQuery();
               connection.Close();
           }
        }
        return res;
    }
    public int Baja(int id){
        int res = -1;
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
           var query = $@"DELETE FROM tipo_inmueble
           WHERE id_tipo_inmueble = @id";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
               command.Parameters.AddWithValue("@id", id);
               connection.Open();
               res = command.ExecuteNonQuery();
               connection.Close();
           }
        }
        return res;
    }


}
