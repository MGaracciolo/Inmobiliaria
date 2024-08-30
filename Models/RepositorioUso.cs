using Microsoft.AspNetCore.Http.Timeouts;
using MySql.Data.MySqlClient;

namespace net.Models;

public class RepositorioUso : RepositorioBase
{
    public List<UsoInmueble> ObtenerTodos(){
        List<UsoInmueble> usos = new List<UsoInmueble>();
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
           var query = $@"SELECT {nameof(UsoInmueble.id_uso_inmueble)},{nameof(UsoInmueble.valor)}
           FROM uso_inmueble";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
               connection.Open();
               var reader = command.ExecuteReader();
               while(reader.Read()){
                   usos.Add(new UsoInmueble{
                        id_uso_inmueble = reader.GetInt32(nameof(UsoInmueble.id_uso_inmueble)),
                        valor = reader.GetString(nameof(UsoInmueble.valor))
                            
                   });
               }
           }
        }
        return usos;
    }
    public UsoInmueble? ObtenerUno(int id){
        UsoInmueble? uso = null;
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
           var query = $@"SELECT {nameof(UsoInmueble.id_uso_inmueble)},{nameof(UsoInmueble.valor)}
           FROM uso_inmueble
           WHERE {nameof(UsoInmueble.id_uso_inmueble)} = @id";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
                command.Parameters.AddWithValue("@id", id);
               connection.Open();
               var reader = command.ExecuteReader();
               if(reader.Read()){
                   uso = new UsoInmueble{
                        id_uso_inmueble = reader.GetInt32(nameof(UsoInmueble.id_uso_inmueble)),
                        valor = reader.GetString(nameof(UsoInmueble.valor))
                            
                   };
               }
               connection.Close();
           }
        }
        return uso;
    }

    public int Alta(UsoInmueble uso){
        int res = -1;
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
           var query = $@"INSERT INTO uso_inmueble
          ({nameof(UsoInmueble.valor)})
           VALUES(@valor);
           SELECT LAST_INSERT_ID();";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
               command.Parameters.AddWithValue("@valor", uso.valor);
               connection.Open();   
               res = Convert.ToInt32(command.ExecuteScalar());
               connection.Close();
           }
           return res;
        }
    }

    public int Modificar(UsoInmueble uso){
        int res = -1;
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
           var query = $@"UPDATE uso_inmueble
           SET {nameof(UsoInmueble.valor)} = @valor
           WHERE {nameof(UsoInmueble.id_uso_inmueble)} = @id_uso_inmueble";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
               command.Parameters.AddWithValue("@id_uso_inmueble", uso.id_uso_inmueble);
               command.Parameters.AddWithValue("@valor", uso.valor);
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
           var query = $@"DELETE FROM uso_inmueble
           WHERE {nameof(UsoInmueble.id_uso_inmueble)} = @id";
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
