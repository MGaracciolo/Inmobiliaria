using MySql.Data.MySqlClient;

namespace net.Models;

public class RepositorioInmueble: RepositorioBase
{
    public List<Inmueble> ObtenerTodos(){
        List<Inmueble> inmuebles = new List<Inmueble>();
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
           var query = $@"SELECT {nameof(Inmueble.id_inmueble)},{nameof(Inmueble.id_direccion)},{nameof(Inmueble.latitud)},{nameof(Inmueble.longitud)},{nameof(Inmueble.id_propietario)},{nameof(Inmueble.uso)},{nameof(Inmueble.tipo)},{nameof(Inmueble.ambientes)},{nameof(Inmueble.precio)},{nameof(Inmueble.estado)}
           FROM inmueble";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
               connection.Open();
               var reader = command.ExecuteReader();
               while(reader.Read()){
                  inmuebles.Add(new Inmueble{
                        id_inmueble = reader.GetInt32(nameof(Inmueble.id_inmueble)),
                        id_direccion = reader.GetInt32(nameof(Inmueble.id_direccion)),
                        latitud = reader.GetString(nameof(Inmueble.latitud)),
                        longitud = reader.GetString(nameof(Inmueble.longitud)),
                        id_propietario = reader.GetInt32(nameof(Inmueble.id_propietario)),
                        uso = reader.GetString(nameof(Inmueble.uso)),
                        tipo = reader.GetString(nameof(Inmueble.tipo)),
                        ambientes = reader.GetInt32(nameof(Inmueble.ambientes)),
                        precio = reader.GetDouble(nameof(Inmueble.precio)),
                        estado = reader.GetBoolean(nameof(Inmueble.estado))
                   });
               }
           }
        }
        return inmuebles;
    }
    public Inmueble? ObtenerUno(int id){
        Inmueble? inmueble = null;
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
           var query = $@"SELECT {nameof(Inmueble.id_inmueble)},{nameof(Inmueble.id_direccion)},{nameof(Inmueble.latitud)},{nameof(Inmueble.longitud)},{nameof(Inmueble.id_propietario)},{nameof(Inmueble.uso)},{nameof(Inmueble.tipo)},{nameof(Inmueble.ambientes)},{nameof(Inmueble.precio)},{nameof(Inmueble.estado)}
           FROM inmueble
           WHERE {nameof(Inmueble.id_inmueble)} = @id";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
                command.Parameters.AddWithValue("@id", id);
               connection.Open();
               var reader = command.ExecuteReader();
               if(reader.Read()){
                   inmueble = new Inmueble{
                        id_inmueble = reader.GetInt32(nameof(Inmueble.id_inmueble)),
                        id_direccion = reader.GetInt32(nameof(Inmueble.id_direccion)),
                        latitud = reader.GetString(nameof(Inmueble.latitud)),
                        longitud = reader.GetString(nameof(Inmueble.longitud)),
                        id_propietario = reader.GetInt32(nameof(Inmueble.id_propietario)),
                        uso = reader.GetString(nameof(Inmueble.uso)),
                        tipo = reader.GetString(nameof(Inmueble.tipo)),
                        ambientes = reader.GetInt32(nameof(Inmueble.ambientes)),
                        precio = reader.GetDouble(nameof(Inmueble.precio)),
                        estado = reader.GetBoolean(nameof(Inmueble.estado))
                   };
               }
               connection.Close();
           }
        }
        return inmueble;
    }

    public int Alta(Inmueble inmueble){
        int res = -1;
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
           var query = $@"INSERT INTO inmueble
           ({nameof(Inmueble.id_direccion)},{nameof(Inmueble.latitud)},{nameof(Inmueble.longitud)},{nameof(Inmueble.id_propietario)},{nameof(Inmueble.uso)},{nameof(Inmueble.tipo)},{nameof(Inmueble.ambientes)},{nameof(Inmueble.precio)},{nameof(Inmueble.estado)})
           VALUES(@id_direccion,@latitud,@longitud,@id_propietario,@uso,@tipo,@ambientes,@precio,@estado);
           SELECT LAST_INSERT_ID();";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
               command.Parameters.AddWithValue("@id_direccion", inmueble.id_direccion);
               command.Parameters.AddWithValue("@latitud", inmueble.latitud);
               command.Parameters.AddWithValue("@longitud", inmueble.longitud);
               command.Parameters.AddWithValue("@id_propietario", inmueble.id_propietario);
               command.Parameters.AddWithValue("@uso", inmueble.uso);
               command.Parameters.AddWithValue("@tipo", inmueble.tipo);
               command.Parameters.AddWithValue("@ambientes", inmueble.ambientes);
               command.Parameters.AddWithValue("@precio", inmueble.precio);
               command.Parameters.AddWithValue("@estado", inmueble.estado);
               connection.Open();   
               res = Convert.ToInt32(command.ExecuteScalar());
               connection.Close();
           }
           return res;
        }
    }

    public int Modificar(Inmueble inmueble){
        int res = -1;
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
           var query = $@"UPDATE inmueble
           SET {nameof(Inmueble.id_direccion)} = @id_direccion,
           {nameof(Inmueble.latitud)} = @latitud,
           {nameof(Inmueble.longitud)} = @longitud,
           {nameof(Inmueble.id_propietario)} = @id_propietario,
           {nameof(Inmueble.uso)} = @uso,
           {nameof(Inmueble.tipo)} = @tipo,
           {nameof(Inmueble.ambientes)} = @ambientes,
           {nameof(Inmueble.precio)} = @precio,
           {nameof(Inmueble.estado)} = @estado
           WHERE {nameof(Inmueble.id_inmueble)} = @id_inmueble";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
               command.Parameters.AddWithValue("@id_inmueble", inmueble.id_inmueble);
               command.Parameters.AddWithValue("@id_direccion", inmueble.id_direccion);
               command.Parameters.AddWithValue("@latitud", inmueble.latitud);
               command.Parameters.AddWithValue("@longitud", inmueble.longitud);
               command.Parameters.AddWithValue("@id_propietario", inmueble.id_propietario);
               command.Parameters.AddWithValue("@uso", inmueble.uso);
               command.Parameters.AddWithValue("@uso", inmueble.uso);
               command.Parameters.AddWithValue("@ambientes", inmueble.ambientes);
               command.Parameters.AddWithValue("@precio", inmueble.precio);
               command.Parameters.AddWithValue("@estado", inmueble.estado);
               connection.Open();
               res = command.ExecuteNonQuery();
               connection.Close();
           }
        }
        return res;
    }


    
    //Para borrar el inmueble hacer baja LOGICA, a estado asignarle false



    // public int Baja(int id){
    //     int res = -1;
    //     using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
    //        var query = $@"UPDATE inmueble
    //        SET 
    //        {nameof(Inmueble.estado)} = @estado
    //        WHERE {nameof(Inmueble.id_inmueble)} = @id";
    //        using(MySqlCommand command = new MySqlCommand(query, connection)){
              
    //            command.Parameters.AddWithValue("@estado", inmueble.estado);
    //            connection.Open();
    //            res = command.ExecuteNonQuery();
    //            connection.Close();
    //        }
    //     }
    //     return res;
    // }


}
