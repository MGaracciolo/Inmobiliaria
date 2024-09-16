using MySql.Data.MySqlClient;

namespace net.Models;

public class RepositorioInmueble: RepositorioBase
{
    public List<Inmueble> ObtenerTodos(){
        List<Inmueble> inmuebles = new List<Inmueble>();
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
           var query = $@"SELECT 
           i.id_inmueble AS InmuebleId,
           i.id_direccion AS IdDireccion,
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
           d.id_direccion AS DireccionId,
           d.calle AS Calle,
           d.altura AS Altura,
           t.id_tipo_inmueble AS TipoId,
           t.valor as Valor,
           u.id_uso_inmueble AS UsoId,
           u.valor as UsoValor
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
                        InmuebleId = reader.GetInt32(nameof(Inmueble.InmuebleId)),
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
								PropietarioId = reader.GetInt32(nameof(Propietario.PropietarioId)),
								Nombre = reader.GetString(nameof(Propietario.Nombre)),
								Apellido = reader.GetString(nameof(Propietario.Apellido)),
                                Dni = reader.GetString(nameof(Propietario.Dni))
							},
                        Direccion = new Direccion
                            {
                                DireccionId = reader.GetInt32(nameof(Direccion.DireccionId)),
                                Calle = reader.GetString(nameof(Direccion.Calle)),
                                Altura = reader.GetInt32(nameof(Direccion.Altura))
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
           i.id_direccion AS IdDireccion,
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
           d.id_direccion AS DireccionId,
           d.calle AS Calle,
           d.altura AS Altura,
           t.id_tipo_inmueble AS TipoId,
           t.valor as Valor,
           u.id_uso_inmueble AS UsoId,
           u.valor as UsoValor
            FROM inmueble i
            INNER JOIN propietario p ON i.id_propietario = p.id_propietario
            INNER JOIN direccion d ON i.id_direccion = d.id_direccion
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
								PropietarioId = reader.GetInt32(nameof(Propietario.PropietarioId)),
								Nombre = reader.GetString(nameof(Propietario.Nombre)),
								Apellido = reader.GetString(nameof(Propietario.Apellido)),
                                Dni = reader.GetString(nameof(Propietario.Dni))
							},
                        Direccion = new Direccion
                            {
                                DireccionId = reader.GetInt32(nameof(Direccion.DireccionId)),
                                Calle = reader.GetString(nameof(Direccion.Calle)),
                                Altura = reader.GetInt32(nameof(Direccion.Altura))
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
           (id_direccion,
           latitud,
           longitud,
           id_propietario,
           id_uso_inmueble,
           id_tipo_inmueble,
           ambientes,
           precio,
           estado)
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
               command.Parameters.AddWithValue("@id_inmueble", inmueble.InmuebleId);
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

    /*public List<Inmueble> ObtenerXEstado(String estado){//Melian
        List<Inmueble> inmuebles = new List<Inmueble>();
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
           var query = $@"SELECT 
           i.id_inmueble AS InmuebleId,
           i.id_direccion AS IdDireccion,
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
           d.id_direccion AS DireccionId,
           d.calle AS Calle,
           d.altura AS Altura,
           t.id_tipo_inmueble AS TipoId,
           t.valor as Valor,
           u.id_uso_inmueble AS UsoId,
           u.valor as UsoValor
            FROM inmueble i
            INNER JOIN propietario p ON i.id_propietario = p.id_propietario
            INNER JOIN direccion d ON i.id_direccion = d.id_direccion
            INNER JOIN tipo_inmueble t ON i.id_tipo_inmueble = t.id_tipo_inmueble
            INNER JOIN uso_inmueble u ON i.id_uso_inmueble = u.id_uso_inmueble
            WHERE i.estado = @estado";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
               command.Parameters.AddWithValue("@estado", estado);//Bindear estado
               connection.Open();
               var reader = command.ExecuteReader();
               while(reader.Read()){
                  inmuebles.Add(new Inmueble{
                        InmuebleId = reader.GetInt32(nameof(Inmueble.InmuebleId)),
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
								PropietarioId = reader.GetInt32(nameof(Propietario.PropietarioId)),
								Nombre = reader.GetString(nameof(Propietario.Nombre)),
								Apellido = reader.GetString(nameof(Propietario.Apellido)),
                                Dni = reader.GetString(nameof(Propietario.Dni))
							},
                        Direccion = new Direccion
                            {
                                DireccionId = reader.GetInt32(nameof(Direccion.DireccionId)),
                                Calle = reader.GetString(nameof(Direccion.Calle)),
                                Altura = reader.GetInt32(nameof(Direccion.Altura))
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
    }*/
           


    /*public List<Inmueble> listarInmueblesxDuenos(){
        List<Inmueble> inmuebles = new List<Inmueble>();
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
           var query = $@"SELECT 
           i.id_inmueble AS InmuebleId,
           i.id_direccion AS IdDireccion,
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
           d.id_direccion AS DireccionId,
           d.calle AS Calle,
           d.altura AS Altura,
           t.id_tipo_inmueble AS TipoId,
           t.valor as Valor,
           u.id_uso_inmueble AS UsoId,
           u.valor as UsoValor
            FROM inmueble i
            INNER JOIN propietario p ON i.id_propietario = p.id_propietario
            INNER JOIN direccion d ON i.id_direccion = d.id_direccion
            INNER JOIN tipo_inmueble t ON i.id_tipo_inmueble = t.id_tipo_inmueble
            INNER JOIN uso_inmueble u ON i.id_uso_inmueble = u.id_uso_inmueble
            WHERE i.estado = 1";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
               connection.Open();
               using(MySqlDataReader reader = command.ExecuteReader()){
                   while(reader.Read()){
                       Inmueble inmueble = new Inmueble();
                       inmueble.InmuebleId = reader.GetInt32("InmuebleId");
                       inmueble.IdPropietario = reader.GetInt32("IdPropietario");
                       inmueble.IdUso = reader.GetInt32("IdUso");
                       inmueble.IdTipo = reader.GetInt32("IdTipo");
                       inmueble.Ambientes = reader.GetInt32("Ambientes");
                       inmueble.Precio = reader.GetInt32("Precio");
                       inmueble.Estado = reader.GetBoolean("Estado");
                       inmueble.Latitud = reader.GetDouble("Latitud");
                       inmueble.Longitud = reader.GetDouble("Longitud");
                       inmueble.IdDireccion = reader.GetInt32("IdDireccion");
                       inmueble.Calle = reader.GetString("Calle");
                       inmueble.Altura = reader.GetInt32("Altura");
                       inmueble.IdTipo = reader.GetInt32("IdTipo");
                       inmueble.Valor = reader.GetInt32("Valor");
                       inmueble.IdUso = reader.GetInt32("IdUso");
                       inmueble.Valor = reader.GetInt32("UsoValor");
                       inmuebles.Add(inmueble);
                   }
               }
               connection.Close();
           }
        }*/


}
