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
           c.precio AS Precio,
           c.estado AS Estado,
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
           p.dni AS Dni
           FROM contrato c
           INNER JOIN inquilino i ON i.id_inquilino = c.id_inquilino
           INNER JOIN inmueble inm ON inm.id_inmueble = c.id_inmueble
           INNER JOIN propietario p ON p.id_propietario = inm.id_propietario";
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
                       Precio = reader.GetDecimal(nameof(Contrato.Precio)),
                       Estado = reader.GetBoolean(nameof(Contrato.Estado)),
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
           c.precio AS Precio,
           c.estado AS Estado,
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
           p.dni AS Dni
           FROM contrato c
           INNER JOIN inquilino i ON i.id_inquilino = c.id_inquilino
           INNER JOIN inmueble inm ON inm.id_inmueble = c.id_inmueble
           INNER JOIN propietario p ON p.id_propietario = inm.id_propietario
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
                       Precio = reader.GetDecimal(nameof(Contrato.Precio)),
                       Estado = reader.GetBoolean(nameof(Contrato.Estado)),
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
           c.precio AS Precio,
           c.estado AS Estado,
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
           p.dni AS Dni
           FROM contrato c
           INNER JOIN inquilino i ON i.id_inquilino = c.id_inquilino
           INNER JOIN inmueble inm ON inm.id_inmueble = c.id_inmueble
           INNER JOIN propietario p ON p.id_propietario = inm.id_propietario
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
                       Precio = reader.GetDecimal(nameof(Contrato.Precio)),
                       Estado = reader.GetBoolean(nameof(Contrato.Estado)),
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
          c.precio AS Precio,
          c.estado AS Estado,
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
          p.dni AS Dni
          FROM contrato c
          INNER JOIN inquilino i ON i.id_inquilino = c.id_inquilino
          INNER JOIN inmueble inm ON inm.id_inmueble = c.id_inmueble
          INNER JOIN propietario p ON p.id_propietario = inm.id_propietario
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
                       Precio = reader.GetDecimal(nameof(Contrato.Precio)),
                       Estado = reader.GetBoolean(nameof(Contrato.Estado)),
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
           estado)
           VALUES(@id_inquilino,@id_inmueble,@desde,@hasta,@precio,@estado);
           SELECT LAST_INSERT_ID();";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
               command.Parameters.AddWithValue("@id_inquilino", contrato.IdInquilino);
               command.Parameters.AddWithValue("@id_inmueble", contrato.IdInmueble);
               command.Parameters.AddWithValue("@desde", contrato.Desde);
               command.Parameters.AddWithValue("@hasta", contrato.Hasta);
               command.Parameters.AddWithValue("@precio", contrato.Precio);
               command.Parameters.AddWithValue("@estado", contrato.Estado);
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
           estado = @estado
           WHERE id_contrato = @id_contrato";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
               command.Parameters.AddWithValue("@id_contrato", contrato.ContratoId);
               command.Parameters.AddWithValue("@id_inquilino", contrato.IdInquilino);
               command.Parameters.AddWithValue("@id_inmueble", contrato.IdInmueble);
               command.Parameters.AddWithValue("@desde", contrato.Desde);
               command.Parameters.AddWithValue("@hasta", contrato.Hasta);
               command.Parameters.AddWithValue("@precio", contrato.Precio);
               command.Parameters.AddWithValue("@estado", contrato.Estado);
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
