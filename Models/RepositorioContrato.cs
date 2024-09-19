using System.Security.Authentication;
using MySql.Data.MySqlClient;

namespace net.Models;

public class RepositorioContrato: RepositorioBase
{
    public List<Contrato> ObtenerTodos(){
        List<Contrato> contratos = new List<Contrato>();
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
           var query = $@"SELECT 
           c.id_contrato AS ContratoId, 
           c.id_inquilino AS IdInquilino, 
           c.id_inmueble AS IdInmueble,
           c.desde AS Desde, 
           c.hasta AS Hasta, 
           c.precio AS PrecioContrato,
           c.estado AS Estado,
           c.id_creacion AS IdCreador,
           c.id_anulacion AS IdAnulador,
           c.fecha_anulacion AS Anulacion,
           i.id_inquilino AS InquilinoId,
           i.nombre AS NombreI,
           i.apellido AS ApellidoI,
           i.dni AS DniI,
           inm.id_inmueble AS InmuebleId,
           inm.direccion AS DireccionI,
           inm.id_propietario AS IdPropietario,
           inm.precio AS Precio,
           p.id_propietario AS PropietarioId,
           p.nombre AS Nombre,
           p.apellido AS Apellido,
           p.dni AS Dni,
           u.id_usuario AS CreadorId,
           u.nombre AS NombreC,
           u.apellido AS ApellidoC,
           ua.id_usuario AS AnuladorId,
           ua.nombre AS NombreA,
           ua.apellido AS ApellidoA
           FROM contrato c
           INNER JOIN inquilino i ON i.id_inquilino = c.id_inquilino
           INNER JOIN inmueble inm ON inm.id_inmueble = c.id_inmueble
           INNER JOIN propietario p ON p.id_propietario = inm.id_propietario
           INNER JOIN usuario u ON u.id_usuario = c.id_creacion
           LEFT JOIN usuario ua ON ua.id_usuario = c.id_anulacion";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
               connection.Open();
               var reader = command.ExecuteReader();
               while(reader.Read()){
                  contratos.Add(new Contrato{
                       ContratoId = reader.GetInt32(nameof(Contrato.ContratoId)),
                       IdInquilino = reader.GetInt32(nameof(Contrato.IdInquilino)),
                       IdInmueble = reader.GetInt32(nameof(Contrato.IdInmueble)),
                       Desde= reader.GetDateTime(nameof(Contrato.Desde)),
                       Hasta = reader.GetDateTime(nameof(Contrato.Hasta)),
                       PrecioContrato = reader.GetDecimal(nameof(Contrato.PrecioContrato)),
                       Estado = reader.GetBoolean(nameof(Contrato.Estado)),
                       IdCreador = reader.GetInt32(nameof(Contrato.IdCreador)),
                       IdAnulador = reader.IsDBNull(reader.GetOrdinal(nameof(Contrato.IdAnulador))) 
                            ? (int?)null : reader.GetInt32(nameof(Contrato.IdAnulador)),
                        Anulacion = reader.IsDBNull(reader.GetOrdinal(nameof(Contrato.Anulacion))) 
                            ? (DateTime?)null : reader.GetDateTime(nameof(Contrato.Anulacion)),
                       Creador = new Usuario{
                            UsuarioId = reader.GetInt32("CreadorId"),
                            Nombre = reader.GetString("NombreC"),
                            Apellido = reader.GetString("ApellidoC")
                        },
                        Anulador = reader.IsDBNull(reader.GetOrdinal("AnuladorId")) ? null : new Usuario {
                            UsuarioId = reader.GetInt32("AnuladorId"),
                            Nombre = reader.GetString("NombreA"),
                            Apellido = reader.GetString("ApellidoA")
                        },
                       Inquilino = new Inquilino{
                           InquilinoId = reader.GetInt32(nameof(Inquilino.InquilinoId)),
                           NombreI = reader.GetString(nameof(Inquilino.NombreI)),
                           ApellidoI = reader.GetString(nameof(Inquilino.ApellidoI)),
                           DniI = reader.GetString(nameof(Inquilino.DniI))
                       },
                       Inmueble = new Inmueble{
                           InmuebleId = reader.GetInt32(nameof(Inmueble.InmuebleId)),
                           DireccionI = reader.GetString(nameof(Inmueble.DireccionI)),
                           IdPropietario = reader.GetInt32(nameof(Inmueble.IdPropietario)),
                           Precio = reader.GetDecimal(nameof(Inmueble.Precio)),
                           Propietario = new Propietario{
                               PropietarioId = reader.GetInt32(nameof(Propietario.PropietarioId)),
                               Nombre = reader.GetString(nameof(Propietario.Nombre)),
                               Apellido = reader.GetString(nameof(Propietario.Apellido)),
                               Dni = reader.GetString(nameof(Propietario.Dni))
                           }
                       }
                   });
               }
           }
        }
        return contratos;
    }
    public List<Contrato> ObtenerActivos(){
        List<Contrato> contratos = new List<Contrato>();
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
           var query = $@"SELECT 
           c.id_contrato AS ContratoId, 
           c.id_inquilino AS IdInquilino, 
           c.id_inmueble AS IdInmueble,
           c.desde AS Desde, 
           c.hasta AS Hasta, 
           c.precio AS PrecioContrato,
           c.estado AS Estado,
           c.id_creacion AS IdCreador,
           c.id_anulacion AS IdAnulador,
           c.fecha_anulacion AS Anulacion,
           i.id_inquilino AS InquilinoId,
           i.nombre AS NombreI,
           i.apellido AS ApellidoI,
           i.dni AS DniI,
           inm.id_inmueble AS InmuebleId,
           inm.direccion AS DireccionI,
           inm.id_propietario AS IdPropietario,
           inm.precio AS Precio,
           p.id_propietario AS PropietarioId,
           p.nombre AS Nombre,
           p.apellido AS Apellido,
           p.dni AS Dni,
           u.id_usuario AS CreadorId,
           u.nombre AS NombreC,
           u.apellido AS ApellidoC,
           ua.id_usuario AS AnuladorId,
           ua.nombre AS NombreA,
           ua.apellido AS ApellidoA
           FROM contrato c
           INNER JOIN inquilino i ON i.id_inquilino = c.id_inquilino
           INNER JOIN inmueble inm ON inm.id_inmueble = c.id_inmueble
           INNER JOIN propietario p ON p.id_propietario = inm.id_propietario
           INNER JOIN usuario u ON u.id_usuario = c.id_creacion
           LEFT JOIN usuario ua ON ua.id_usuario = c.id_anulacion
           WHERE c.estado = 1";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
               connection.Open();
               var reader = command.ExecuteReader();
               while(reader.Read()){
                  contratos.Add(new Contrato{
                       ContratoId = reader.GetInt32(nameof(Contrato.ContratoId)),
                       IdInquilino = reader.GetInt32(nameof(Contrato.IdInquilino)),
                       IdInmueble = reader.GetInt32(nameof(Contrato.IdInmueble)),
                       Desde= reader.GetDateTime(nameof(Contrato.Desde)),
                       Hasta = reader.GetDateTime(nameof(Contrato.Hasta)),
                       PrecioContrato = reader.GetDecimal(nameof(Contrato.PrecioContrato)),
                       Estado = reader.GetBoolean(nameof(Contrato.Estado)),
                       IdCreador = reader.GetInt32(nameof(Contrato.IdCreador)),
                       IdAnulador = reader.IsDBNull(reader.GetOrdinal(nameof(Contrato.IdAnulador))) 
                            ? (int?)null : reader.GetInt32(nameof(Contrato.IdAnulador)),
                        Anulacion = reader.IsDBNull(reader.GetOrdinal(nameof(Contrato.Anulacion))) 
                            ? (DateTime?)null : reader.GetDateTime(nameof(Contrato.Anulacion)),
                       Creador = new Usuario{
                            UsuarioId = reader.GetInt32("CreadorId"),
                            Nombre = reader.GetString("NombreC"),
                            Apellido = reader.GetString("ApellidoC")
                        },
                        Anulador = reader.IsDBNull(reader.GetOrdinal("AnuladorId")) ? null : new Usuario {
                            UsuarioId = reader.GetInt32("AnuladorId"),
                            Nombre = reader.GetString("NombreA"),
                            Apellido = reader.GetString("ApellidoA")
                        },
                       Inquilino = new Inquilino{
                           InquilinoId = reader.GetInt32(nameof(Inquilino.InquilinoId)),
                           NombreI = reader.GetString(nameof(Inquilino.NombreI)),
                           ApellidoI = reader.GetString(nameof(Inquilino.ApellidoI)),
                           DniI = reader.GetString(nameof(Inquilino.DniI))
                       },
                       Inmueble = new Inmueble{
                           InmuebleId = reader.GetInt32(nameof(Inmueble.InmuebleId)),
                           DireccionI = reader.GetString(nameof(Inmueble.DireccionI)),
                           IdPropietario = reader.GetInt32(nameof(Inmueble.IdPropietario)),
                           Precio = reader.GetDecimal(nameof(Inmueble.Precio)),
                           Propietario = new Propietario{
                               PropietarioId = reader.GetInt32(nameof(Propietario.PropietarioId)),
                               Nombre = reader.GetString(nameof(Propietario.Nombre)),
                               Apellido = reader.GetString(nameof(Propietario.Apellido)),
                               Dni = reader.GetString(nameof(Propietario.Dni))
                           }
                       }
                   });
               }
           }
        }
        return contratos;
    }
    public List<Contrato> ObtenerInactivos(){
        List<Contrato> contratos = new List<Contrato>();
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
           var query = $@"SELECT 
           c.id_contrato AS ContratoId, 
           c.id_inquilino AS IdInquilino, 
           c.id_inmueble AS IdInmueble,
           c.desde AS Desde, 
           c.hasta AS Hasta, 
           c.precio AS PrecioContrato,
           c.estado AS Estado,
           c.id_creacion AS IdCreador,
           c.id_anulacion AS IdAnulador,
           c.fecha_anulacion AS Anulacion,
           i.id_inquilino AS InquilinoId,
           i.nombre AS NombreI,
           i.apellido AS ApellidoI,
           i.dni AS DniI,
           inm.id_inmueble AS InmuebleId,
           inm.direccion AS DireccionI,
           inm.id_propietario AS IdPropietario,
           inm.precio AS Precio,
           p.id_propietario AS PropietarioId,
           p.nombre AS Nombre,
           p.apellido AS Apellido,
           p.dni AS Dni,
           u.id_usuario AS CreadorId,
           u.nombre AS NombreC,
           u.apellido AS ApellidoC,
           ua.id_usuario AS AnuladorId,
           ua.nombre AS NombreA,
           ua.apellido AS ApellidoA
           FROM contrato c
           INNER JOIN inquilino i ON i.id_inquilino = c.id_inquilino
           INNER JOIN inmueble inm ON inm.id_inmueble = c.id_inmueble
           INNER JOIN propietario p ON p.id_propietario = inm.id_propietario
           INNER JOIN usuario u ON u.id_usuario = c.id_creacion
           LEFT JOIN usuario ua ON ua.id_usuario = c.id_anulacion
           WHERE c.estado = 0";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
               connection.Open();
               var reader = command.ExecuteReader();
               while(reader.Read()){
                  contratos.Add(new Contrato{
                       ContratoId = reader.GetInt32(nameof(Contrato.ContratoId)),
                       IdInquilino = reader.GetInt32(nameof(Contrato.IdInquilino)),
                       IdInmueble = reader.GetInt32(nameof(Contrato.IdInmueble)),
                       Desde= reader.GetDateTime(nameof(Contrato.Desde)),
                       Hasta = reader.GetDateTime(nameof(Contrato.Hasta)),
                       PrecioContrato = reader.GetDecimal(nameof(Contrato.PrecioContrato)),
                       Estado = reader.GetBoolean(nameof(Contrato.Estado)),
                       IdCreador = reader.GetInt32(nameof(Contrato.IdCreador)),
                       IdAnulador = reader.IsDBNull(reader.GetOrdinal(nameof(Contrato.IdAnulador))) 
                            ? (int?)null : reader.GetInt32(nameof(Contrato.IdAnulador)),
                        Anulacion = reader.IsDBNull(reader.GetOrdinal(nameof(Contrato.Anulacion))) 
                            ? (DateTime?)null : reader.GetDateTime(nameof(Contrato.Anulacion)),
                       Creador = new Usuario{
                            UsuarioId = reader.GetInt32("CreadorId"),
                            Nombre = reader.GetString("NombreC"),
                            Apellido = reader.GetString("ApellidoC")
                        },
                        Anulador = reader.IsDBNull(reader.GetOrdinal("AnuladorId")) ? null : new Usuario {
                            UsuarioId = reader.GetInt32("AnuladorId"),
                            Nombre = reader.GetString("NombreA"),
                            Apellido = reader.GetString("ApellidoA")
                        },
                       Inquilino = new Inquilino{
                           InquilinoId = reader.GetInt32(nameof(Inquilino.InquilinoId)),
                           NombreI = reader.GetString(nameof(Inquilino.NombreI)),
                           ApellidoI = reader.GetString(nameof(Inquilino.ApellidoI)),
                           DniI = reader.GetString(nameof(Inquilino.DniI))
                       },
                       Inmueble = new Inmueble{
                           InmuebleId = reader.GetInt32(nameof(Inmueble.InmuebleId)),
                           DireccionI = reader.GetString(nameof(Inmueble.DireccionI)),
                           IdPropietario = reader.GetInt32(nameof(Inmueble.IdPropietario)),
                           Precio = reader.GetDecimal(nameof(Inmueble.Precio)),
                           Propietario = new Propietario{
                               PropietarioId = reader.GetInt32(nameof(Propietario.PropietarioId)),
                               Nombre = reader.GetString(nameof(Propietario.Nombre)),
                               Apellido = reader.GetString(nameof(Propietario.Apellido)),
                               Dni = reader.GetString(nameof(Propietario.Dni))
                           }
                       }
                   });
               }
           }
        }
        return contratos;
    }
    public Contrato? ObtenerUno(int id){
        Contrato? contrato = null;
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
          var query = $@"SELECT 
           c.id_contrato AS ContratoId, 
           c.id_inquilino AS IdInquilino, 
           c.id_inmueble AS IdInmueble,
           c.desde AS Desde, 
           c.hasta AS Hasta, 
           c.precio AS PrecioContrato,
           c.estado AS Estado,
           c.id_creacion AS IdCreador,
           c.id_anulacion AS IdAnulador,
           c.fecha_anulacion AS Anulacion,
           i.id_inquilino AS InquilinoId,
           i.nombre AS NombreI,
           i.apellido AS ApellidoI,
           i.dni AS DniI,
           inm.id_inmueble AS InmuebleId,
           inm.direccion AS DireccionI,
           inm.id_propietario AS IdPropietario,
           inm.precio AS Precio,
           p.id_propietario AS PropietarioId,
           p.nombre AS Nombre,
           p.apellido AS Apellido,
           p.dni AS Dni,
           u.id_usuario AS CreadorId,
           u.nombre AS NombreC,
           u.apellido AS ApellidoC,
           ua.id_usuario AS AnuladorId,
           ua.nombre AS NombreA,
           ua.apellido AS ApellidoA
           FROM contrato c
           INNER JOIN inquilino i ON i.id_inquilino = c.id_inquilino
           INNER JOIN inmueble inm ON inm.id_inmueble = c.id_inmueble
           INNER JOIN propietario p ON p.id_propietario = inm.id_propietario
           INNER JOIN usuario u ON u.id_usuario = c.id_creacion
           LEFT JOIN usuario ua ON ua.id_usuario = c.id_anulacion
          WHERE c.id_contrato = @id";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
                command.Parameters.AddWithValue("@id", id);
               connection.Open();
               var reader = command.ExecuteReader();
               if(reader.Read()){
                   contrato = new Contrato{
                       ContratoId = reader.GetInt32(nameof(Contrato.ContratoId)),
                       IdInquilino = reader.GetInt32(nameof(Contrato.IdInquilino)),
                       IdInmueble = reader.GetInt32(nameof(Contrato.IdInmueble)),
                       Desde= reader.GetDateTime(nameof(Contrato.Desde)),
                       Hasta = reader.GetDateTime(nameof(Contrato.Hasta)),
                       PrecioContrato = reader.GetDecimal(nameof(Contrato.PrecioContrato)),
                       Estado = reader.GetBoolean(nameof(Contrato.Estado)),
                       IdCreador = reader.GetInt32(nameof(Contrato.IdCreador)),
                       IdAnulador = reader.IsDBNull(reader.GetOrdinal(nameof(Contrato.IdAnulador))) 
                            ? (int?)null : reader.GetInt32(nameof(Contrato.IdAnulador)),
                        Anulacion = reader.IsDBNull(reader.GetOrdinal(nameof(Contrato.Anulacion))) 
                            ? (DateTime?)null : reader.GetDateTime(nameof(Contrato.Anulacion)),
                       Creador = new Usuario{
                            UsuarioId = reader.GetInt32("CreadorId"),
                            Nombre = reader.GetString("NombreC"),
                            Apellido = reader.GetString("ApellidoC")
                        },
                        Anulador = reader.IsDBNull(reader.GetOrdinal("AnuladorId")) ? null : new Usuario {
                            UsuarioId = reader.GetInt32("AnuladorId"),
                            Nombre = reader.GetString("NombreA"),
                            Apellido = reader.GetString("ApellidoA")
                        },
                       Inquilino = new Inquilino{
                           InquilinoId = reader.GetInt32(nameof(Inquilino.InquilinoId)),
                           NombreI = reader.GetString(nameof(Inquilino.NombreI)),
                           ApellidoI = reader.GetString(nameof(Inquilino.ApellidoI)),
                           DniI = reader.GetString(nameof(Inquilino.DniI))
                       },
                       Inmueble = new Inmueble{
                           InmuebleId = reader.GetInt32(nameof(Inmueble.InmuebleId)),
                           DireccionI = reader.GetString(nameof(Inmueble.DireccionI)),
                           IdPropietario = reader.GetInt32(nameof(Inmueble.IdPropietario)),
                           Precio = reader.GetDecimal(nameof(Inmueble.Precio)),
                           Propietario = new Propietario{
                               PropietarioId = reader.GetInt32(nameof(Propietario.PropietarioId)),
                               Nombre = reader.GetString(nameof(Propietario.Nombre)),
                               Apellido = reader.GetString(nameof(Propietario.Apellido)),
                               Dni = reader.GetString(nameof(Propietario.Dni))
                           }
                       }
                   };
               }
               connection.Close();
           }
        }
        return contrato;
    }

    public int Alta(Contrato contrato){
        int res = -1;
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
           var query = $@"INSERT INTO contrato
           (id_inquilino, 
           id_inmueble,
           desde, 
           hasta, 
           precio,
           estado,
           id_creacion,
           id_anulacion,
           fecha_anulacion)
           VALUES(@id_inquilino,@id_inmueble,@desde,@hasta,@precio,@estado,@id_creacion,@id_anulacion,@fecha_anulacion);
           SELECT LAST_INSERT_ID();";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
               command.Parameters.AddWithValue("@id_inquilino", contrato.IdInquilino);
               command.Parameters.AddWithValue("@id_inmueble", contrato.IdInmueble);
               command.Parameters.AddWithValue("@desde", contrato.Desde);
               command.Parameters.AddWithValue("@hasta", contrato.Hasta);
               command.Parameters.AddWithValue("@precio", contrato.PrecioContrato);
               command.Parameters.AddWithValue("@estado", contrato.Estado);
               command.Parameters.AddWithValue("@id_creacion", contrato.IdCreador);
               command.Parameters.AddWithValue("@id_anulacion", contrato.IdAnulador);
               command.Parameters.AddWithValue("@fecha_anulacion", contrato.Anulacion);
               connection.Open();   
               res = Convert.ToInt32(command.ExecuteScalar());
               connection.Close();
           }
           return res;
        }
    }

    public int Modificar(Contrato contrato){
        int res = -1;
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
           var query = $@"UPDATE contrato SET 
           id_inquilino = @id_inquilino,
           id_inmueble= @id_inmueble,
           desde = @desde,
           hasta = @hasta,
           precio = @precio,
           estado = @estado,
           id_creacion = @id_creacion,
           id_anulacion = @id_anulacion,
           fecha_anulacion = @fecha_anulacion
           WHERE id_contrato = @id_contrato";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
               command.Parameters.AddWithValue("@id_contrato", contrato.ContratoId);
               command.Parameters.AddWithValue("@id_inquilino", contrato.IdInquilino);
               command.Parameters.AddWithValue("@id_inmueble", contrato.IdInmueble);
               command.Parameters.AddWithValue("@desde", contrato.Desde);
               command.Parameters.AddWithValue("@hasta", contrato.Hasta);
               command.Parameters.AddWithValue("@precio", contrato.PrecioContrato);
               command.Parameters.AddWithValue("@estado", contrato.Estado);
               command.Parameters.AddWithValue("@id_creacion", contrato.IdCreador);
               command.Parameters.AddWithValue("@id_anulacion", contrato.IdAnulador);
               command.Parameters.AddWithValue("@fecha_anulacion", contrato.Anulacion);
               connection.Open();
               res = command.ExecuteNonQuery();
               connection.Close();
           }
        }
        return res;
    }


    public int Baja(int id)
    {
        int res = -1;
        using (MySqlConnection connection = new MySqlConnection(ConnectionString))
        {
            var query = $@"UPDATE contrato
            SET estado = 0
            WHERE id_contrato = @id";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                res = command.ExecuteNonQuery();
                connection.Close();
            }
        }
        return res;
    }

      public int Restore(int id)
    {
        int res = -1;
        using (MySqlConnection connection = new MySqlConnection(ConnectionString))
        {
            var query = $@"UPDATE contrato
            SET estado = 1
            WHERE id_contrato = @id";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                res = command.ExecuteNonQuery();
                connection.Close();
            }
        }
        return res;
    }

}
