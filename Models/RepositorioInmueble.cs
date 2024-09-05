using MySql.Data.MySqlClient;

namespace net.Models;

public class RepositorioInmueble: RepositorioBase
{
    public List<Inmueble> ObtenerTodos(){
        List<Inmueble> inmuebles = new List<Inmueble>();
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
           var query = $@"SELECT 
           i.id_inmueble AS InmuebleId,
           i.direccion AS DireccionI,
           i.latitud AS Latitud,
           i.longitud AS Longitud,
           i.id_propietario AS IdPropietario,
           i.id_uso_inmueble AS IdUso,
           i.id_tipo_inmueble AS IdTipo,
           i.ambientes AS Ambientes,
           i.precio AS Precio,
           i.estado AS Estado,
           p.id_propietario AS PropietarioId,
            p.nombre AS Nombre,
            p.apellido AS Apellido,
            p.dni AS Dni,
           t.id_tipo_inmueble AS TipoId,
           t.valor as Valor,
           u.id_uso_inmueble AS UsoId,
           u.valor as UsoValor
            FROM inmueble i
            INNER JOIN propietario p ON i.id_propietario = p.id_propietario
            INNER JOIN tipo_inmueble t ON i.id_tipo_inmueble = t.id_tipo_inmueble
            INNER JOIN uso_inmueble u ON i.id_uso_inmueble = u.id_uso_inmueble";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
               connection.Open();
               var reader = command.ExecuteReader();
               while(reader.Read()){
                  inmuebles.Add(new Inmueble{
                        InmuebleId = reader.GetInt32(nameof(Inmueble.InmuebleId)),
                        DireccionI = reader.GetString(nameof(Inmueble.DireccionI)),
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
								PropietarioId = reader.GetInt32(nameof(Propietario.PropietarioId)),
								Nombre = reader.GetString(nameof(Propietario.Nombre)),
								Apellido = reader.GetString(nameof(Propietario.Apellido)),
                                Dni = reader.GetString(nameof(Propietario.Dni))
							},
                        UsoInmueble = new UsoInmueble
                            {
                                UsoId = reader.GetInt32(nameof(UsoInmueble.UsoId)),
                               UsoValor = reader.GetString(nameof(UsoInmueble.UsoValor))
                            },
                        TipoInmueble = new TipoInmueble
                            {
                                TipoId = reader.GetInt32(nameof(TipoInmueble.TipoId)),
                               Valor = reader.GetString(nameof(TipoInmueble.Valor))
                            }
                   });
               }
           }
        }
        return inmuebles;
    }
    public List<Inmueble> ObtenerActivos(){
        List<Inmueble> inmuebles = new List<Inmueble>();
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
           var query = $@"SELECT 
           i.id_inmueble AS InmuebleId,
           i.direccion AS DireccionI,
           i.latitud AS Latitud,
           i.longitud AS Longitud,
           i.id_propietario AS IdPropietario,
           i.id_uso_inmueble AS IdUso,
           i.id_tipo_inmueble AS IdTipo,
           i.ambientes AS Ambientes,
           i.precio AS Precio,
           i.estado AS Estado,
           p.id_propietario AS PropietarioId,
            p.nombre AS Nombre,
            p.apellido AS Apellido,
            p.dni AS Dni,
           t.id_tipo_inmueble AS TipoId,
           t.valor as Valor,
           u.id_uso_inmueble AS UsoId,
           u.valor as UsoValor
            FROM inmueble i
            INNER JOIN propietario p ON i.id_propietario = p.id_propietario
            INNER JOIN tipo_inmueble t ON i.id_tipo_inmueble = t.id_tipo_inmueble
            INNER JOIN uso_inmueble u ON i.id_uso_inmueble = u.id_uso_inmueble
            WHERE i.estado = 1";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
               connection.Open();
               var reader = command.ExecuteReader();
               while(reader.Read()){
                  inmuebles.Add(new Inmueble{
                        InmuebleId = reader.GetInt32(nameof(Inmueble.InmuebleId)),
                        DireccionI = reader.GetString(nameof(Inmueble.DireccionI)),
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
								PropietarioId = reader.GetInt32(nameof(Propietario.PropietarioId)),
								Nombre = reader.GetString(nameof(Propietario.Nombre)),
								Apellido = reader.GetString(nameof(Propietario.Apellido)),
                                Dni = reader.GetString(nameof(Propietario.Dni))
							},
                        UsoInmueble = new UsoInmueble
                            {
                                UsoId = reader.GetInt32(nameof(UsoInmueble.UsoId)),
                               UsoValor = reader.GetString(nameof(UsoInmueble.UsoValor))
                            },
                        TipoInmueble = new TipoInmueble
                            {
                                TipoId = reader.GetInt32(nameof(TipoInmueble.TipoId)),
                               Valor = reader.GetString(nameof(TipoInmueble.Valor))
                            }
                   });
               }
           }
        }
        return inmuebles;
    }
    public List<Inmueble> ObtenerInactivos(){
        List<Inmueble> inmuebles = new List<Inmueble>();
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
           var query = $@"SELECT 
           i.id_inmueble AS InmuebleId,
           i.direccion AS DireccionI,
           i.latitud AS Latitud,
           i.longitud AS Longitud,
           i.id_propietario AS IdPropietario,
           i.id_uso_inmueble AS IdUso,
           i.id_tipo_inmueble AS IdTipo,
           i.ambientes AS Ambientes,
           i.precio AS Precio,
           i.estado AS Estado,
           p.id_propietario AS PropietarioId,
            p.nombre AS Nombre,
            p.apellido AS Apellido,
            p.dni AS Dni,
           t.id_tipo_inmueble AS TipoId,
           t.valor as Valor,
           u.id_uso_inmueble AS UsoId,
           u.valor as UsoValor
            FROM inmueble i
            INNER JOIN propietario p ON i.id_propietario = p.id_propietario
            INNER JOIN tipo_inmueble t ON i.id_tipo_inmueble = t.id_tipo_inmueble
            INNER JOIN uso_inmueble u ON i.id_uso_inmueble = u.id_uso_inmueble
            WHERE i.estado = 0";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
               connection.Open();
               var reader = command.ExecuteReader();
               while(reader.Read()){
                  inmuebles.Add(new Inmueble{
                        InmuebleId = reader.GetInt32(nameof(Inmueble.InmuebleId)),
                        DireccionI = reader.GetString(nameof(Inmueble.DireccionI)),
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
								PropietarioId = reader.GetInt32(nameof(Propietario.PropietarioId)),
								Nombre = reader.GetString(nameof(Propietario.Nombre)),
								Apellido = reader.GetString(nameof(Propietario.Apellido)),
                                Dni = reader.GetString(nameof(Propietario.Dni))
							},
                        UsoInmueble = new UsoInmueble
                            {
                                UsoId = reader.GetInt32(nameof(UsoInmueble.UsoId)),
                               UsoValor = reader.GetString(nameof(UsoInmueble.UsoValor))
                            },
                        TipoInmueble = new TipoInmueble
                            {
                                TipoId = reader.GetInt32(nameof(TipoInmueble.TipoId)),
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
          i.id_inmueble AS InmuebleId,
           i.direccion AS DireccionI,
           i.latitud AS Latitud,
           i.longitud AS Longitud,
           i.id_propietario AS IdPropietario,
           i.id_uso_inmueble AS IdUso,
           i.id_tipo_inmueble AS IdTipo,
           i.ambientes AS Ambientes,
           i.precio AS Precio,
           i.estado AS Estado,
           p.id_propietario AS PropietarioId,
            p.nombre AS Nombre,
            p.apellido AS Apellido,
            p.dni AS Dni,
           t.id_tipo_inmueble AS TipoId,
           t.valor as Valor,
           u.id_uso_inmueble AS UsoId,
           u.valor as UsoValor
            FROM inmueble i
            INNER JOIN propietario p ON i.id_propietario = p.id_propietario
            INNER JOIN tipo_inmueble t ON t.id_tipo_inmueble = i.id_tipo_inmueble
            INNER JOIN uso_inmueble u ON u.id_uso_inmueble = i.id_uso_inmueble
           WHERE i.id_inmueble = @id";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
                command.Parameters.AddWithValue("@id", id);
               connection.Open();
               var reader = command.ExecuteReader();
               if(reader.Read()){
                   inmueble = new Inmueble{
                       InmuebleId = reader.GetInt32(nameof(Inmueble.InmuebleId)),
                        DireccionI = reader.GetString(nameof(Inmueble.DireccionI)),
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
								PropietarioId = reader.GetInt32(nameof(Propietario.PropietarioId)),
								Nombre = reader.GetString(nameof(Propietario.Nombre)),
								Apellido = reader.GetString(nameof(Propietario.Apellido)),
                                Dni = reader.GetString(nameof(Propietario.Dni))
							},
                        UsoInmueble = new UsoInmueble
                            {
                                UsoId = reader.GetInt32(nameof(UsoInmueble.UsoId)),
                               UsoValor = reader.GetString(nameof(UsoInmueble.UsoValor))
                            },
                        TipoInmueble = new TipoInmueble
                            {
                                TipoId = reader.GetInt32(nameof(TipoInmueble.TipoId)),
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
           (direccion,
           latitud,
           longitud,
           id_propietario,
           id_uso_inmueble,
           id_tipo_inmueble,
           ambientes,
           precio,
           estado)
           VALUES(@direccion,@latitud,@longitud,@id_propietario,@id_uso_inmueble,@id_tipo_inmueble,@ambientes,@precio,@estado);
           SELECT LAST_INSERT_ID();";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
               command.Parameters.AddWithValue("@direccion", inmueble.DireccionI);
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
           SET direccion = @direccion,
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
               command.Parameters.AddWithValue("@id_inmueble", inmueble.InmuebleId);
               command.Parameters.AddWithValue("@direccion", inmueble.DireccionI);
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



    public int Baja(int id){
        int res = -1;
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
           var query = $@"UPDATE inmueble
           SET estado = 0
           WHERE id_inmueble = @id";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
                command.Parameters.AddWithValue("@id", id);
               connection.Open();
               res = command.ExecuteNonQuery();
               connection.Close();
           }
        }
        return res;
    }

    public int Restore(int id){
        int res = -1;
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
           var query = $@"UPDATE inmueble
           SET estado = 1
           WHERE id_inmueble = @id";
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
