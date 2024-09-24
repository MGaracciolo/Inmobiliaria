using System.Reflection.Metadata.Ecma335;
using System.Security.Authentication;
using MySql.Data.MySqlClient;

namespace net.Models;

public class RepositorioPago: RepositorioBase
{
    public List<Pago> ObtenerTodos()
    {
        List<Pago> lista = new List<Pago>();
        using (MySqlConnection connection = new MySqlConnection(ConnectionString))
        {
            var query = $@"SELECT 
            p.id_pago AS PagoId,
            p.id_contrato AS IdContrato, 
            p.numero AS Numero, 
            p.importe AS Importe, 
            p.fecha AS Fecha, 
            p.id_creacion AS CreadorId, 
            p.id_anulacion AS AnuladorId, 
            p.concepto AS Concepto, 
            p.estado AS Estado,
            c.id_contrato AS ContratoId,
            c.id_inmueble AS IdInmueble,
            i.id_inmueble AS InmuebleId,
            i.direccion AS Direccion,
            uc.id_usuario AS CreadorId,
            uc.nombre AS CreadorNombre,
            uc.apellido AS CreadorApellido,
            ua.id_usuario AS AnuladorId,
            ua.nombre AS AnuladorNombre,
            ua.apellido AS AnuladorApellido
            FROM pago p
            INNER JOIN contrato c ON p.id_contrato = c.id_contrato
            INNER JOIN inmueble i ON c.id_inmueble = i.id_inmueble
            INNER JOIN usuario uc ON p.id_creacion = uc.id_usuario
            LEFT JOIN usuario ua ON p.id_anulacion = ua.id_usuario
            ";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Pago
                    {
                        PagoId = reader.GetInt32("id_pago"),
                        IdContrato = reader.GetInt32("id_contrato"),
                        Numero = reader.GetInt32("numero"),
                        Importe = reader.GetDecimal("importe"),
                        Fecha = reader.GetDateTime("fecha"),
                        CreadorId = reader.GetInt32("id_creacion"),
                        AnuladorId = reader.IsDBNull(reader.GetOrdinal("id_anulacion")) ? (int?)null : reader.GetInt32("id_anulacion"),
                        Concepto = reader.IsDBNull(reader.GetOrdinal("concepto")) ? null : reader.GetString("concepto"), // Verificar nulo
                        Estado = reader.GetBoolean("estado"),
                        Contrato = new Contrato{    
                            ContratoId = reader.GetInt32("id_contrato"),
                            IdInmueble = reader.GetInt32("IdInmueble"),
                            Inmueble = new Inmueble
                            {
                                InmuebleId = reader.GetInt32("InmuebleId"),
                                DireccionI = reader.GetString("direccion")
                            }
                        },
                        Creador = new Usuario
                        {
                            UsuarioId = reader.GetInt32("CreadorId"),
                            Nombre = reader.GetString("CreadorNombre"),
                            Apellido = reader.GetString("CreadorApellido")
                        },
                        Anulador = reader.IsDBNull(reader.GetOrdinal("AnuladorId")) ? null : new Usuario
                        {
                            UsuarioId = reader.GetInt32("AnuladorId"),
                            Nombre = reader.GetString("AnuladorNombre"),
                            Apellido = reader.GetString("AnuladorApellido")
                        }
                    });
                }
            }
        }
        return lista;
    }

    public Pago? ObtenerUno(int id){
        Pago? pago = null;
        using (MySqlConnection connection = new MySqlConnection(ConnectionString))
        {
            var query = $@" 
            SELECT 
            p.id_pago,
            p.id_contrato, 
            p.numero, 
            p.importe, 
            p.fecha, 
            p.id_creacion, 
            p.id_anulacion, 
            p.concepto, 
            p.estado,
            c.id_contrato,
            c.id_inmueble,
            i.id_inmueble,
            i.direccion,
            uc.id_usuario AS CreadorId,
            uc.nombre AS CreadorNombre,
            uc.apellido AS CreadorApellido,
            ua.id_usuario AS AnuladorId,
            ua.nombre AS AnuladorNombre,
            ua.apellido AS AnuladorApellido
            FROM pago p
            INNER JOIN contrato c ON p.id_contrato = c.id_contrato
            INNER JOIN inmueble i ON c.id_inmueble = i.id_inmueble
            INNER JOIN usuario uc ON p.id_creacion = uc.id_usuario
            LEFT JOIN usuario ua ON p.id_anulacion = ua.id_usuario
             WHERE id_pago = @id";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    pago = new Pago
                    {
                        PagoId = reader.GetInt32("id_pago"),
                        IdContrato = reader.GetInt32("id_contrato"),
                        Numero = reader.GetInt32("numero"),
                        Importe = reader.GetDecimal("importe"),
                        Fecha = reader.GetDateTime("fecha"),
                        CreadorId = reader.GetInt32("id_creacion"),
                        AnuladorId = reader.IsDBNull(reader.GetOrdinal("id_anulacion")) ? (int?)null : reader.GetInt32("id_anulacion"),
                        Concepto = reader.IsDBNull(reader.GetOrdinal("concepto")) ? null : reader.GetString("concepto"), // Verificar nulo
                        Estado = reader.GetBoolean("estado"),
                        Contrato = new Contrato{    
                            ContratoId = reader.GetInt32("id_contrato"),
                            IdInmueble = reader.GetInt32("id_inmueble"),
                            Inmueble = new Inmueble
                            {
                                InmuebleId = reader.GetInt32("id_inmueble"),
                                DireccionI = reader.GetString("direccion")
                            }
                        },
                        Creador = new Usuario
                        {
                            UsuarioId = reader.GetInt32("CreadorId"),
                            Nombre = reader.GetString("CreadorNombre"),
                            Apellido = reader.GetString("CreadorApellido")
                        },
                        Anulador = reader.IsDBNull(reader.GetOrdinal("AnuladorId")) ? null : new Usuario
                        {
                            UsuarioId = reader.GetInt32("AnuladorId"),
                            Nombre = reader.GetString("AnuladorNombre"),
                            Apellido = reader.GetString("AnuladorApellido")
                        }
                    };
                }
            }
        }
        return pago;
    }
    public List<Pago> ObtenerPorContrato(int id)
    {
        List<Pago> lista = new List<Pago>();
        using (MySqlConnection connection = new MySqlConnection(ConnectionString))
        {
            var query = $@"SELECT 
            p.id_pago AS PagoId,
            p.id_contrato AS IdContrato, 
            p.numero AS Numero, 
            p.importe AS Importe, 
            p.fecha AS Fecha, 
            p.id_creacion AS CreadorId, 
            p.id_anulacion AS AnuladorId, 
            p.concepto AS Concepto, 
            p.estado AS Estado,
            c.id_contrato AS ContratoId,
            c.id_inmueble AS IdInmueble,
            i.id_inmueble AS InmuebleId,
            i.direccion AS DireccionI,
            uc.id_usuario AS CreadorId,
            uc.nombre AS CreadorNombre,
            uc.apellido AS CreadorApellido,
            ua.id_usuario AS AnuladorId,
            ua.nombre AS AnuladorNombre,
            ua.apellido AS AnuladorApellido
            FROM pago p
            INNER JOIN contrato c ON p.id_contrato = c.id_contrato
            INNER JOIN inmueble i ON c.id_inmueble = i.id_inmueble
            INNER JOIN usuario uc ON p.id_creacion = uc.id_usuario
            LEFT JOIN usuario ua ON p.id_anulacion = ua.id_usuario
            WHERE p.id_contrato = @id";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Pago
                    {
                        PagoId = reader.GetInt32(nameof(Pago.PagoId)),
                        IdContrato = reader.GetInt32(nameof(Pago.IdContrato)),
                        Numero = reader.GetInt32(nameof(Pago.Numero)),
                        Importe = reader.GetDecimal(nameof(Pago.Importe)),
                        Fecha = reader.GetDateTime(nameof(Pago.Fecha)),
                        CreadorId = reader.GetInt32(nameof(Pago.CreadorId)),
                        AnuladorId = reader.IsDBNull(reader.GetOrdinal(nameof(Pago.AnuladorId))) ? (int?)null : reader.GetInt32(nameof(Pago.AnuladorId)),
                        Concepto = reader.IsDBNull(reader.GetOrdinal(nameof(Pago.Concepto))) ? null : reader.GetString(nameof(Pago.Concepto)), // Verificar nulo
                        Estado = reader.GetBoolean(nameof(Pago.Estado)),
                        Contrato = new Contrato{    
                            ContratoId = reader.GetInt32(nameof(Contrato.ContratoId)),
                            IdInmueble = reader.GetInt32(nameof(Contrato.IdInmueble)),
                            Inmueble = new Inmueble
                            {
                                InmuebleId = reader.GetInt32(nameof(Inmueble.InmuebleId)),
                                DireccionI = reader.GetString(nameof(Inmueble.DireccionI))
                            }
                        },
                        Creador = new Usuario
                        {
                            UsuarioId = reader.GetInt32("CreadorId"),
                            Nombre = reader.GetString("CreadorNombre"),
                            Apellido = reader.GetString("CreadorApellido")
                        },
                        Anulador = reader.IsDBNull(reader.GetOrdinal("AnuladorId")) ? null : new Usuario
                        {
                            UsuarioId = reader.GetInt32("AnuladorId"),
                            Nombre = reader.GetString("AnuladorNombre"),
                            Apellido = reader.GetString("AnuladorApellido")
                        }
                    });
                }
            }
        }
        return lista;
    }

    public int Agregar(Pago pago)
    {
        var res = -1;
        using (MySqlConnection connection = new MySqlConnection(ConnectionString))
        {
            var query = $@"INSERT INTO pago
            (id_contrato,
            importe, 
            fecha, 
            id_creacion,
            concepto)
            VALUES
            (@id_contrato, @importe, @fecha, @id_creacion, @concepto);
            SELECT LAST_INSERT_ID();";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id_contrato", pago.IdContrato);
                command.Parameters.AddWithValue("@importe", pago.Importe);
                command.Parameters.AddWithValue("@fecha", pago.Fecha);
                command.Parameters.AddWithValue("@id_creacion", pago.CreadorId);
                command.Parameters.AddWithValue("@concepto", pago.Concepto);
                connection.Open();
                command.ExecuteNonQuery();
                res = Convert.ToInt32(command.ExecuteScalar());
                connection.Close();
            }
        }
        return res;
    }
    

    public int Modificar(Pago pago){
        var res = -1;
        using (MySqlConnection connection = new MySqlConnection(ConnectionString))
        {
            var query = $@"UPDATE pago SET 
            concepto = @concepto

            WHERE id_pago = @id";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@concepto", pago.Concepto);
                command.Parameters.AddWithValue("@id", pago.PagoId);
                connection.Open();
                
                res = command.ExecuteNonQuery();
                connection.Close();
            }
        }
        return res;
    }
    
    public int Baja(int id){
        int res = -1;
        using (MySqlConnection connection = new MySqlConnection(ConnectionString))
        {
            var query = $@"UPDATE pago
            SET estado = 0
            WHERE id_pago = @id";
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