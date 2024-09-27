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
            c.id_inquilino AS IdInquilino,
            c.meses AS Meses,
            inq.id_inquilino AS InquilinoId,
            inq.dni AS DniI,
            inq.nombre AS NombreI,
            inq.apellido AS ApellidoI,
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
            INNER JOIN inquilino inq ON c.id_inquilino = inq.id_inquilino
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
                            IdInquilino = reader.GetInt32(nameof(Contrato.IdInquilino)),
                            Meses = reader.GetInt32(nameof(Contrato.Meses)),
                            Inmueble = new Inmueble
                            {
                                InmuebleId = reader.GetInt32(nameof(Inmueble.InmuebleId)),
                                DireccionI = reader.GetString(nameof(Inmueble.DireccionI))
                            },
                            Inquilino = new Inquilino
                            {
                                InquilinoId = reader.GetInt32(nameof(Inquilino.InquilinoId)),
                                NombreI = reader.GetString(nameof(Inquilino.NombreI)),
                                ApellidoI = reader.GetString(nameof(Inquilino.ApellidoI)),
                                DniI = reader.GetString(nameof(Inquilino.DniI))
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
            c.id_inquilino AS IdInquilino,
            c.meses AS Meses,
            inq.id_inquilino AS InquilinoId,
            inq.dni AS DniI,
            inq.nombre AS NombreI,
            inq.apellido AS ApellidoI,
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
            INNER JOIN inquilino inq ON c.id_inquilino = inq.id_inquilino
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
                            IdInquilino = reader.GetInt32(nameof(Contrato.IdInquilino)),
                            Meses = reader.GetInt32(nameof(Contrato.Meses)),
                            Inmueble = new Inmueble
                            {
                                InmuebleId = reader.GetInt32(nameof(Inmueble.InmuebleId)),
                                DireccionI = reader.GetString(nameof(Inmueble.DireccionI))
                            },
                            Inquilino = new Inquilino
                            {
                                InquilinoId = reader.GetInt32(nameof(Inquilino.InquilinoId)),
                                NombreI = reader.GetString(nameof(Inquilino.NombreI)),
                                ApellidoI = reader.GetString(nameof(Inquilino.ApellidoI)),
                                DniI = reader.GetString(nameof(Inquilino.DniI))
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
            c.id_inquilino AS IdInquilino,
            c.meses AS Meses,
            inq.id_inquilino AS InquilinoId,
            inq.dni AS DniI,
            inq.nombre AS NombreI,
            inq.apellido AS ApellidoI,
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
            INNER JOIN inquilino inq ON c.id_inquilino = inq.id_inquilino
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
                            IdInquilino = reader.GetInt32(nameof(Contrato.IdInquilino)),
                            Meses = reader.GetInt32(nameof(Contrato.Meses)),
                            Inmueble = new Inmueble
                            {
                                InmuebleId = reader.GetInt32(nameof(Inmueble.InmuebleId)),
                                DireccionI = reader.GetString(nameof(Inmueble.DireccionI))
                            },
                            Inquilino = new Inquilino
                            {
                                InquilinoId = reader.GetInt32(nameof(Inquilino.InquilinoId)),
                                NombreI = reader.GetString(nameof(Inquilino.NombreI)),
                                ApellidoI = reader.GetString(nameof(Inquilino.ApellidoI)),
                                DniI = reader.GetString(nameof(Inquilino.DniI))
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
            
            
            res = Convert.ToInt32(command.ExecuteScalar());
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
    

    public int Baja(int id, int anulador, int numero){
        int res = -1;
        using (MySqlConnection connection = new MySqlConnection(ConnectionString))
        {
            var query = $@"UPDATE pago
            SET estado = 0,
            id_anulacion = @anulador,
            numero = @numero,
            concepto = 'Pago anulado'
            WHERE id_pago = @id";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id",id);
                command.Parameters.AddWithValue("@anulador", anulador);
                command.Parameters.AddWithValue("@numero", numero);
                connection.Open();
                
                res = command.ExecuteNonQuery();
                connection.Close();
            }
        }
        return res;
    }
}