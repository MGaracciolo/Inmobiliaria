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
            d.altura AS Altura
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
                            Altura = reader.GetInt32(nameof(Direccion.Altura))
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
           VALUES(@nombre,@apellido,@dni,@email,@telefono);
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
           id_direccion = @id_direccion,
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

    public int Baja(int id){
        int res = -1;
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
           var query = $@"DELETE FROM propietario
           WHERE  id_propietario = @id";
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
