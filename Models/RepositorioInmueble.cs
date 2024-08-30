using MySql.Data.MySqlClient;

namespace net.Models;

public class RepositorioInmueble: RepositorioBase
{
    public List<Inmueble> ObtenerTodos(){
        List<Inmueble> inmuebles = new List<Inmueble>();
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
           var query = $@"SELECT 
           i.{nameof(Inmueble.id_inmueble)},
           i.{nameof(Inmueble.id_direccion)},
           i.{nameof(Inmueble.latitud)},
           i.{nameof(Inmueble.longitud)},
           i.{nameof(Inmueble.id_propietario)},
           i.{nameof(Inmueble.id_uso_inmueble)},
           i.{nameof(Inmueble.id_tipo_inmueble)},
           i.{nameof(Inmueble.ambientes)},
           i.{nameof(Inmueble.precio)},
           i.{nameof(Inmueble.estado)},
           p.{nameof(Propietario.id_propietario)},
           p.{nameof(Propietario.nombre)},
           p.{nameof(Propietario.apellido)},
           d.{nameof(Direccion.id_direccion)},
           d.{nameof(Direccion.calle)},
           d.{nameof(Direccion.altura)},
           t.{nameof(TipoInmueble.id_tipo_inmueble)},
           t.valor as tipo_inmueble,
           u.{nameof(UsoInmueble.id_uso_inmueble)},
           u.valor as uso_inmueble
            FROM inmueble i
            INNER JOIN propietario p ON i.id_propietario = p.id_propietario
            INNER JOIN direccion d ON i.id_direccion = d.id_direccion
            INNER JOIN tipo_inmueble t ON i.id_tipo_inmueble = t.id_tipo_inmueble
            INNER JOIN uso_inmueble u ON i.id_uso_inmueble = u.id_uso_inmueble";
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
                        id_uso_inmueble = reader.GetInt32(nameof(Inmueble.id_uso_inmueble)),
                        id_tipo_inmueble = reader.GetInt32(nameof(Inmueble.id_tipo_inmueble)),
                        ambientes = reader.GetInt32(nameof(Inmueble.ambientes)),
                        precio = reader.GetDouble(nameof(Inmueble.precio)),
                        estado = reader.GetBoolean(nameof(Inmueble.estado)),
                        propietario = new Propietario
							{
								id_propietario = reader.GetInt32(nameof(Inmueble.id_propietario)),
								nombre = reader.GetString(nameof(Propietario.nombre)),
								apellido = reader.GetString(nameof(Propietario.apellido)),
							},
                        direccion = new Direccion
                            {
                                id_direccion = reader.GetInt32(nameof(Inmueble.id_direccion)),
                                calle = reader.GetString(nameof(Direccion.calle)),
                                altura = reader.GetInt32(nameof(Direccion.altura))
                            },
                        usoInmueble = new UsoInmueble
                            {
                                id_uso_inmueble = reader.GetInt32(nameof(Inmueble.id_uso_inmueble)),
                                valor = reader.GetString("uso_inmueble")
                            },
                        tipoInmueble = new TipoInmueble
                            {
                                id_tipo_inmueble = reader.GetInt32(nameof(Inmueble.id_tipo_inmueble)),
                                valor = reader.GetString("tipo_inmueble")
                            }
                   });
               }
           }
        }
        return inmuebles;
    }
    public Inmueble? ObtenerUno(int id){
        Inmueble? inmueble = null;
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
           var query = $@"SELECT 
           i.{nameof(Inmueble.id_inmueble)},
           i.{nameof(Inmueble.id_direccion)},
           i.{nameof(Inmueble.latitud)},
           i.{nameof(Inmueble.longitud)},
           i.{nameof(Inmueble.id_propietario)},
           i.{nameof(Inmueble.id_uso_inmueble)},
           i.{nameof(Inmueble.id_tipo_inmueble)},
           i.{nameof(Inmueble.ambientes)},
           i.{nameof(Inmueble.precio)},
           i.{nameof(Inmueble.estado)},
           p.{nameof(Propietario.id_propietario)},
           p.{nameof(Propietario.nombre)},
           p.{nameof(Propietario.apellido)},
           d.{nameof(Direccion.id_direccion)},
           d.{nameof(Direccion.calle)},
           d.{nameof(Direccion.altura)},
           t.{nameof(TipoInmueble.id_tipo_inmueble)},
           t.valor AS tipo_inmueble,
           u.{nameof(UsoInmueble.id_uso_inmueble)},
           u.valor AS uso_inmueble
            FROM inmueble i
            INNER JOIN propietario p ON i.id_propietario = p.id_propietario
            INNER JOIN direccion d ON i.id_direccion = d.id_direccion
            INNER JOIN tipo_inmueble t ON t.id_tipo_inmueble = i.id_tipo_inmueble
            INNER JOIN uso_inmueble u ON u.id_uso_inmueble = i.id_uso_inmueble
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
                        id_uso_inmueble = reader.GetInt32(nameof(Inmueble.id_uso_inmueble)),
                        id_tipo_inmueble = reader.GetInt32(nameof(Inmueble.id_tipo_inmueble)),
                        ambientes = reader.GetInt32(nameof(Inmueble.ambientes)),
                        precio = reader.GetDouble(nameof(Inmueble.precio)),
                        estado = reader.GetBoolean(nameof(Inmueble.estado)),
                        propietario = new Propietario
							{
								id_propietario = reader.GetInt32(nameof(Inmueble.id_propietario)),
								nombre = reader.GetString(nameof(Propietario.nombre)),
								apellido = reader.GetString(nameof(Propietario.apellido)),
							},
                        direccion = new Direccion
                            {
                                id_direccion = reader.GetInt32(nameof(Inmueble.id_direccion)),
                                calle = reader.GetString(nameof(Direccion.calle)),
                                altura = reader.GetInt32(nameof(Direccion.altura))
                            },
                         usoInmueble = new UsoInmueble
                            {
                                id_uso_inmueble = reader.GetInt32(nameof(Inmueble.id_uso_inmueble)),
                                valor = reader.GetString("uso_inmueble")
                            },
                        tipoInmueble = new TipoInmueble
                            {
                                id_tipo_inmueble = reader.GetInt32(nameof(Inmueble.id_tipo_inmueble)),
                                valor = reader.GetString("tipo_inmueble")
                            }
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
           ({nameof(Inmueble.id_direccion)},
           {nameof(Inmueble.latitud)},
           {nameof(Inmueble.longitud)},
           {nameof(Inmueble.id_propietario)},
           {nameof(Inmueble.id_uso_inmueble)},
           {nameof(Inmueble.id_tipo_inmueble)},
           {nameof(Inmueble.ambientes)},
           {nameof(Inmueble.precio)},
           {nameof(Inmueble.estado)})
           VALUES(@id_direccion,@latitud,@longitud,@id_propietario,@id_uso_inmueble,@id_tipo_inmueble,@ambientes,@precio,@estado);
           SELECT LAST_INSERT_ID();";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
               command.Parameters.AddWithValue("@id_direccion", inmueble.id_direccion);
               command.Parameters.AddWithValue("@latitud", inmueble.latitud);
               command.Parameters.AddWithValue("@longitud", inmueble.longitud);
               command.Parameters.AddWithValue("@id_propietario", inmueble.id_propietario);
               command.Parameters.AddWithValue("@id_uso_inmueble", inmueble.id_uso_inmueble);
               command.Parameters.AddWithValue("@id_tipo_inmueble", inmueble.id_tipo_inmueble);
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
           {nameof(Inmueble.id_uso_inmueble)} = @id_uso_inmueble,
           {nameof(Inmueble.id_tipo_inmueble)} = @id_tipo_inmueble,
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
               command.Parameters.AddWithValue("@id_uso_inmueble", inmueble.id_uso_inmueble);
               command.Parameters.AddWithValue("@id_tipo_inmueble", inmueble.id_tipo_inmueble);
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
