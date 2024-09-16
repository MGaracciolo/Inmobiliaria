using MySql.Data.MySqlClient;

namespace net.Models;

public class RepositorioPropietario : RepositorioBase
{
    public List<Propietario> ObtenerTodos(){
        List<Propietario> propietarios = new List<Propietario>();
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
           var query = $@"SELECT
            p.id_propietario AS PropietarioId,
            p.nombre AS Nombre,
            p.apellido AS Apellido,
            p.dni AS Dni,
            p.email AS Email,
            p.telefono AS Telefono,
            p.id_direccion AS IdDireccion,
            d.id_direccion AS DireccionId,
            d.calle AS Calle,
            d.altura AS Altura
           FROM propietario p
           INNER JOIN direccion d ON d.id_direccion = p.id_direccion";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
               connection.Open();
               var reader = command.ExecuteReader();
               while(reader.Read()){
                   propietarios.Add(new Propietario{
                        PropietarioId = reader.GetInt32(nameof(Propietario.PropietarioId)),
                        Nombre = reader.GetString(nameof(Propietario.Nombre)),
                        Apellido = reader.GetString(nameof(Propietario.Apellido)),
                        Dni = reader.GetString(nameof(Propietario.Dni)),
                        Email = reader.GetString(nameof(Propietario.Email)),
                        Telefono = reader.GetString(nameof(Propietario.Telefono)),
                        IdDireccion = reader.GetInt32(nameof(Propietario.IdDireccion)),
                        Direccion = new Direccion{
                            DireccionId = reader.GetInt32(nameof(Direccion.DireccionId)),
                            Calle = reader.GetString(nameof(Direccion.Calle)),
                            Altura = reader.GetInt32(nameof(Direccion.Altura))
                        }
                   });
               }
           }
        }
        return propietarios;
    }

    /*public List<Propietario> ObtenerPropietariosActivos(){
        List<Propietario> propietarios = new List<Propietario>();
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
           var query = $@"SELECT 
           p.id_propietario AS PropietarioId,
           p.nombre AS Nombre,
           p.apellido AS Apellido,
           p.dni AS Dni,
           p.email AS Email,
           p.telefono AS Telefono,
           p.estado AS Estado,
           d.id_direccion AS DireccionId,
           d.calle AS Calle,
           d.altura AS Altura,
           d.piso AS Piso,
           d.departamento AS Departamento,
           d.observaciones AS Observaciones
            FROM propietario p
            INNER JOIN direccion d ON p.id_direccion = d.id_direccion
            WHERE p.estado = 1";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
               connection.Open();
               MySqlDataReader reader = command.ExecuteReader();
               while(reader.Read()){
                   propietarios.Add(new Propietario{
                       PropietarioId = reader.GetInt32(0),
                       Nombre = reader.GetString(1),
                       Apellido = reader.GetString(2),
                       Dni = reader.GetString(3),
                       Email = reader.GetString(4),
                       Telefono = reader.GetString(5),
                       Estado = reader.GetInt32(6),
                       IdDireccion = reader.GetInt32(7),
                       Direccion = new Direccion{
                           Calle = reader.GetString(8),
                           Altura = reader.GetInt32(9),
                           Piso = reader.GetInt32(10),
                           Departamento = reader.GetString(11),
                           Observaciones = reader.GetString(12)
                       }
                   });
               }
               connection.Close();
           }    
        }
        return propietarios;
    }*/

    public Propietario? ObtenerUno(int id){
        Propietario? propietario = null;
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
           var query = $@"SELECT  
           p.id_propietario AS PropietarioId,
            p.nombre AS Nombre,
            p.apellido AS Apellido,
            p.dni AS Dni,
            p.email AS Email,
            p.telefono AS Telefono,
            p.id_direccion AS IdDireccion,
            d.id_direccion AS DireccionId,
            d.calle AS Calle,
            d.altura AS Altura,
            d.piso AS Piso,
            d.departamento AS Departamento,
            d.observaciones AS Observaciones
           FROM propietario p
           INNER JOIN direccion d ON d.id_direccion = p.id_direccion
           WHERE id_propietario = @id";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
                command.Parameters.AddWithValue("@id", id);
               connection.Open();
               var reader = command.ExecuteReader();
               if(reader.Read()){
                   propietario = new Propietario{
                        PropietarioId = reader.GetInt32(nameof(Propietario.PropietarioId)),
                        Nombre = reader.GetString(nameof(Propietario.Nombre)),
                        Apellido = reader.GetString(nameof(Propietario.Apellido)),
                        Dni = reader.GetString(nameof(Propietario.Dni)),
                        Email = reader.GetString(nameof(Propietario.Email)),
                        Telefono = reader.GetString(nameof(Propietario.Telefono)),
                        IdDireccion = reader.GetInt32(nameof(Propietario.IdDireccion)),
                        Direccion = new Direccion{
                            DireccionId = reader.GetInt32(nameof(Direccion.DireccionId)),
                            Calle = reader.GetString(nameof(Direccion.Calle)),
                            Altura = reader.GetInt32(nameof(Direccion.Altura)),
                            Piso = reader.IsDBNull(reader.GetOrdinal(nameof(Direccion.Piso))) ? (int?)null : reader.GetInt32(nameof(Direccion.Piso)),
                            Departamento = reader.IsDBNull(reader.GetOrdinal(nameof(Direccion.Departamento))) ? null : reader.GetString(nameof(Direccion.Departamento)),
                            Observaciones = reader.IsDBNull(reader.GetOrdinal(nameof(Direccion.Observaciones))) ? null : reader.GetString(nameof(Direccion.Observaciones))
                        }
                   };
               }
               connection.Close();
           }
        }
        return propietario;
    }
    
    public int Alta(Propietario propietario){
        int res = -1;
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
           var query = $@"INSERT INTO propietario
           (nombre,
            apellido,
            dni,
            email,
            telefono,
            id_direccion)
           VALUES(@nombre,@apellido,@dni,@email,@telefono,@id_direccion);
           SELECT LAST_INSERT_ID();";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
               command.Parameters.AddWithValue("@nombre", propietario.Nombre);
               command.Parameters.AddWithValue("@apellido", propietario.Apellido);
               command.Parameters.AddWithValue("@dni", propietario.Dni);
               command.Parameters.AddWithValue("@email", propietario.Email);
               command.Parameters.AddWithValue("@telefono", propietario.Telefono);
               command.Parameters.AddWithValue("@id_direccion", propietario.IdDireccion);
               connection.Open();   
               res = Convert.ToInt32(command.ExecuteScalar());
               connection.Close();
           }
           return res;
        }
    }

    public bool EmailYaRegistrado(string email){
        using (MySqlConnection connection = new MySqlConnection(ConnectionString))
        {
            using (MySqlCommand command = new MySqlCommand("SELECT COUNT(*) FROM propietario WHERE email = @email", connection))
            {
                command.Parameters.AddWithValue("@email", email);
                connection.Open();
                int count = Convert.ToInt32(command.ExecuteScalar());
                connection.Close();
                return count > 0;
            }
        }
    }

    public int Modificar(Propietario propietario){
        int res = -1;
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
           var query = $@"UPDATE propietario
           SET nombre= @nombre,
           apellido = @apellido,
           dni = @dni,
           email  = @email,
           telefono = @telefono,
           id_direccion = @id_direccion
           WHERE id_propietario = @id_propietario";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
               command.Parameters.AddWithValue("@id_propietario", propietario.PropietarioId);
               command.Parameters.AddWithValue("@nombre", propietario.Nombre);
               command.Parameters.AddWithValue("@apellido", propietario.Apellido);
               command.Parameters.AddWithValue("@dni", propietario.Dni);
               command.Parameters.AddWithValue("@email", propietario.Email);
               command.Parameters.AddWithValue("@telefono", propietario.Telefono);
               command.Parameters.AddWithValue("@id_direccion", propietario.IdDireccion);
               connection.Open();
               res = command.ExecuteNonQuery();
               connection.Close();
           }
        }
        return res;
    }

    public int Baja(int id, int id_direccion){
        int res = -1;
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
           var query = $@"DELETE FROM direccion
            WHERE id_direccion = @id_direccion;
            DELETE FROM propietario
            WHERE  id_propietario = @id;";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
              command.Parameters.AddWithValue("@id_direccion", id_direccion);
               command.Parameters.AddWithValue("@id", id);
               connection.Open();
               res = command.ExecuteNonQuery();
               connection.Close();
           }
        }
        return res;
    }
}
