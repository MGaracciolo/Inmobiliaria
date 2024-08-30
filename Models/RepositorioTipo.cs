using Microsoft.AspNetCore.Http.Timeouts;
using MySql.Data.MySqlClient;

namespace net.Models;

public class RepositorioTipo : RepositorioBase
{
    public List<TipoInmueble> ObtenerTodos(){
        List<TipoInmueble> tipos = new List<TipoInmueble>();
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
           var query = $@"SELECT {nameof(TipoInmueble.id_tipo_inmueble)},{nameof(TipoInmueble.valor)}
           FROM tipo_inmueble";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
               connection.Open();
               var reader = command.ExecuteReader();
               while(reader.Read()){
                   tipos.Add(new TipoInmueble{
                        id_tipo_inmueble = reader.GetInt32(nameof(TipoInmueble.id_tipo_inmueble)),
                        valor = reader.GetString(nameof(TipoInmueble.valor))
                            
                   });
               }
           }
        }
        return tipos;
    }
    public TipoInmueble? ObtenerUno(int id){
        TipoInmueble? tipo = null;
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
           var query = $@"SELECT {nameof(TipoInmueble.id_tipo_inmueble)},{nameof(TipoInmueble.valor)}
           FROM tipo_inmueble
           WHERE {nameof(TipoInmueble.id_tipo_inmueble)} = @id";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
                command.Parameters.AddWithValue("@id", id);
               connection.Open();
               var reader = command.ExecuteReader();
               if(reader.Read()){
                   tipo = new TipoInmueble{
                        id_tipo_inmueble = reader.GetInt32(nameof(TipoInmueble.id_tipo_inmueble)),
                        valor = reader.GetString(nameof(TipoInmueble.valor))
                            
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
          ({nameof(TipoInmueble.valor)})
           VALUES(@valor);
           SELECT LAST_INSERT_ID();";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
               command.Parameters.AddWithValue("@valor", tipo.valor);
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
           SET {nameof(TipoInmueble.valor)} = @valor
           WHERE {nameof(TipoInmueble.id_tipo_inmueble)} = @id_tipo_inmueble";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
               command.Parameters.AddWithValue("@id_tipo_inmueble", tipo.id_tipo_inmueble);
               command.Parameters.AddWithValue("@valor", tipo.valor);
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
           WHERE {nameof(TipoInmueble.id_tipo_inmueble)} = @id";
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
