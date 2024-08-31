using MySql.Data.MySqlClient;

namespace net.Models;

public class RepositorioInmueble: RepositorioBase
{
    public List<Inmueble> ObtenerTodos(){
        List<Inmueble> inmuebles = new List<Inmueble>();
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
           var query = $@"SELECT 
           i.id_inmueble AS Id,
           i.id_direccion AS IdDireccion,
           i.latitud AS Latitud,
           i.longitud AS Longitud,
           i.id_propietario AS IdPropietario,
           i.id_uso_inmueble AS IdUso,
           i.id_tipo_inmueble AS IdTipo,
           i.ambientes AS Ambientes,
           i.precio AS Precio,
           i.estado AS Estado,
           p.id_propietario AS Id,
            p.nombre AS Nombre,
            p.apellido AS Apellido,
            p.dni AS Dni,
           d.id_direccion AS Id,
           d.calle AS Calle,
           d.altura AS Altura,
           t.id_tipo_inmueble AS Id,
           t.valor as Valor,
           u.id_uso_inmueble AS Id,
           u.valor as Valor
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
                        Id = reader.GetInt32(nameof(Inmueble.Id)),
                        IdDireccion = reader.GetInt32(nameof(Inmueble.IdDireccion)),
                        Latitud = reader.GetString(nameof(Inmueble.Latitud)),
                        Longitud = reader.GetString(nameof(Inmueble.Longitud)),
                        IdPropietario = reader.GetInt32(nameof(Inmueble.IdPropietario)),
                        IdUso = reader.GetInt32(nameof(Inmueble.IdUso)),
                        IdTipo = reader.GetInt32(nameof(Inmueble.IdTipo)),
                        Ambientes = reader.GetInt32(nameof(Inmueble.Ambientes)),
                        Precio = reader.GetDecimal(nameof(Inmueble.Precio)),
                        Estado = reader.GetBoolean(nameof(Inmueble.Estado)),
                        Propietario = new Propietario
							{
								Id = reader.GetInt32(nameof(Inmueble.Id)),
								Nombre = reader.GetString(nameof(Propietario.Nombre)),
								Apellido = reader.GetString(nameof(Propietario.Apellido)),
                                Dni = reader.GetString(nameof(Propietario.Dni))
							},
                        Direccion = new Direccion
                            {
                                Id = reader.GetInt32(nameof(Inmueble.Id)),
                                Calle = reader.GetString(nameof(Direccion.Calle)),
                                Altura = reader.GetInt32(nameof(Direccion.Altura))
                            },
                        UsoInmueble = new UsoInmueble
                            {
                                Id = reader.GetInt32(nameof(Inmueble.Id)),
                               Valor = reader.GetString(nameof(UsoInmueble.Valor))
                            },
                        TipoInmueble = new TipoInmueble
                            {
                                Id = reader.GetInt32(nameof(Inmueble.Id)),
                               Valor = reader.GetString(nameof(TipoInmueble.Valor))
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
          i.id_inmueble AS Id,
           i.id_direccion AS IdDireccion,
           i.latitud AS Latitud,
           i.longitud AS Longitud,
           i.id_propietario AS IdPropietario,
           i.id_uso_inmueble AS IdUso,
           i.id_tipo_inmueble AS IdTipo,
           i.ambientes AS Ambientes,
           i.precio AS Precio,
           i.estado AS Estado,
           p.id_propietario AS Id,
            p.nombre AS Nombre,
            p.apellido AS Apellido,
            p.dni AS Dni,
           d.id_direccion AS Id,
           d.calle AS Calle,
           d.altura AS Altura,
           t.id_tipo_inmueble AS Id,
           t.valor as TipoInmueble,
           u.id_uso_inmueble AS Id,
           u.valor as UsoInmueble
            FROM inmueble i
            INNER JOIN propietario p ON i.id_propietario = p.id_propietario
            INNER JOIN direccion d ON i.id_direccion = d.id_direccion
            INNER JOIN tipo_inmueble t ON t.id_tipo_inmueble = i.id_tipo_inmueble
            INNER JOIN uso_inmueble u ON u.id_uso_inmueble = i.id_uso_inmueble
           WHERE {nameof(Inmueble.Id)} = @id";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
                command.Parameters.AddWithValue("@id", id);
               connection.Open();
               var reader = command.ExecuteReader();
               if(reader.Read()){
                   inmueble = new Inmueble{
                       Id = reader.GetInt32(nameof(Inmueble.Id)),
                        IdDireccion = reader.GetInt32(nameof(Inmueble.IdDireccion)),
                        Latitud = reader.GetString(nameof(Inmueble.Latitud)),
                        Longitud = reader.GetString(nameof(Inmueble.Longitud)),
                        IdPropietario = reader.GetInt32(nameof(Inmueble.IdPropietario)),
                        IdUso = reader.GetInt32(nameof(Inmueble.IdUso)),
                        IdTipo = reader.GetInt32(nameof(Inmueble.IdTipo)),
                        Ambientes = reader.GetInt32(nameof(Inmueble.Ambientes)),
                        Precio = reader.GetDecimal(nameof(Inmueble.Precio)),
                        Estado = reader.GetBoolean(nameof(Inmueble.Estado)),
                        Propietario = new Propietario
							{
								Id = reader.GetInt32(nameof(Inmueble.Id)),
								Nombre = reader.GetString(nameof(Propietario.Nombre)),
								Apellido = reader.GetString(nameof(Propietario.Apellido)),
                                Dni = reader.GetString(nameof(Propietario.Dni))
							},
                        Direccion = new Direccion
                            {
                                Id = reader.GetInt32(nameof(Inmueble.Id)),
                                Calle = reader.GetString(nameof(Direccion.Calle)),
                                Altura = reader.GetInt32(nameof(Direccion.Altura))
                            },
                        UsoInmueble = new UsoInmueble
                            {
                                Id = reader.GetInt32(nameof(Inmueble.Id)),
                               Valor = reader.GetString(nameof(UsoInmueble.Valor))
                            },
                        TipoInmueble = new TipoInmueble
                            {
                                Id = reader.GetInt32(nameof(Inmueble.Id)),
                               Valor = reader.GetString(nameof(TipoInmueble.Valor))
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
           (id_direccion AS IdDireccion,
           latitud AS Latitud,
           longitud AS Longitud,
           id_propietario AS IdPropietario,
           id_uso_inmueble AS IdUso,
           id_tipo_inmueble AS IdTipo,
           ambientes AS Ambientes,
           precio AS Precio,
           estado AS Estado,)
           VALUES(@id_direccion,@latitud,@longitud,@id_propietario,@id_uso_inmueble,@id_tipo_inmueble,@ambientes,@precio,@estado);
           SELECT LAST_INSERT_ID();";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
               command.Parameters.AddWithValue("@id_direccion", inmueble.IdDireccion);
               command.Parameters.AddWithValue("@latitud", inmueble.Latitud);
               command.Parameters.AddWithValue("@longitud", inmueble.Longitud);
               command.Parameters.AddWithValue("@id_propietario", inmueble.IdPropietario);
               command.Parameters.AddWithValue("@id_uso_inmueble", inmueble.IdUso);
               command.Parameters.AddWithValue("@id_tipo_inmueble", inmueble.IdTipo);
               command.Parameters.AddWithValue("@ambientes", inmueble.Ambientes);
               command.Parameters.AddWithValue("@precio", inmueble.Precio);
               command.Parameters.AddWithValue("@estado", inmueble.Estado);
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
           SET id_direccion = @id_direccion,
           latitud = @latitud,
           longitud = @longitud,
           id_propietario = @id_propietario,
           id_uso_inmueble= @id_uso_inmueble,
           id_tipo_inmueble = @id_tipo_inmueble,
           ambientes = @ambientes,
           precio= @precio,
           estado = @estado
           WHERE id_inmueble = @id_inmueble";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
               command.Parameters.AddWithValue("@id_inmueble", inmueble.Id);
               command.Parameters.AddWithValue("@id_direccion", inmueble.IdDireccion);
               command.Parameters.AddWithValue("@latitud", inmueble.Latitud);
               command.Parameters.AddWithValue("@longitud", inmueble.Longitud);
               command.Parameters.AddWithValue("@id_propietario", inmueble.IdPropietario);
               command.Parameters.AddWithValue("@id_uso_inmueble", inmueble.IdUso);
               command.Parameters.AddWithValue("@id_tipo_inmueble", inmueble.IdTipo);
               command.Parameters.AddWithValue("@ambientes", inmueble.Ambientes);
               command.Parameters.AddWithValue("@precio", inmueble.Precio);
               command.Parameters.AddWithValue("@estado", inmueble.Estado);
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
