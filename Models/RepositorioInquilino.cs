using MySql.Data.MySqlClient;

namespace net.Models;

public class RepositorioInquilino: RepositorioBase
{
    public List<Inquilino> ObtenerTodos(){
        List<Inquilino> inquilinos = new List<Inquilino>();
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
           var query = $@"SELECT 
            id_inquilino AS Id,
            nombre AS Nombre,
            apellido AS Apellido,
            dni AS Dni,
            email AS Email,
            telefono AS Telefono 
           FROM inquilino";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
               connection.Open();
               var reader = command.ExecuteReader();
               while(reader.Read()){
                  inquilinos.Add(new Inquilino{
                        Id = reader.GetInt32(nameof(Inquilino.Id)),
                        Nombre = reader.GetString(nameof(Inquilino.Nombre)),
                        Apellido = reader.GetString(nameof(Inquilino.Apellido)),
                        Dni = reader.GetString(nameof(Inquilino.Dni)),
                        Email = reader.GetString(nameof(Inquilino.Email)),
                        Telefono = reader.GetString(nameof(Inquilino.Telefono)),
                            
                   });
               }
           }
        }
        return inquilinos;
    }
    public Inquilino? ObtenerUno(int id){
        Inquilino? inquilino = null;
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
           var query = $@"SELECT id_inquilino AS Id,
            nombre AS Nombre,
            apellido AS Apellido,
            dni AS Dni,
            email AS Email,
            telefono AS Telefono 
           FROM inquilino
           WHERE id_inquilino = @id";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
                command.Parameters.AddWithValue("@id", id);
               connection.Open();
               var reader = command.ExecuteReader();
               if(reader.Read()){
                   inquilino = new Inquilino{
                        Id = reader.GetInt32(nameof(Inquilino.Id)),
                        Nombre = reader.GetString(nameof(Inquilino.Nombre)),
                        Apellido = reader.GetString(nameof(Inquilino.Apellido)),
                        Dni = reader.GetString(nameof(Inquilino.Dni)),
                        Email = reader.GetString(nameof(Inquilino.Email)),
                        Telefono = reader.GetString(nameof(Inquilino.Telefono)),
                            
                   };
               }
               connection.Close();
           }
        }
        return inquilino;
    }

    public int Alta(Inquilino inquilino){

        //Melian
        if (EmailYaRegistrado(inquilino.Email))
        {
            throw new Exception("El Email ya esta registrado.");
        }

        int res = -1;
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
           var query = $@"INSERT INTO inquilino
           (nombre,
            apellido,
            dni,
            email,
            telefono)
           VALUES(@nombre,@apellido,@dni,@email,@telefono);
           SELECT LAST_INSERT_ID();";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
               command.Parameters.AddWithValue("@nombre", inquilino.Nombre);
               command.Parameters.AddWithValue("@apellido", inquilino.Apellido);
               command.Parameters.AddWithValue("@dni", inquilino.Dni);
               command.Parameters.AddWithValue("@email", inquilino.Email);
               command.Parameters.AddWithValue("@telefono", inquilino.Telefono);
               connection.Open();   
               res = Convert.ToInt32(command.ExecuteScalar());
               connection.Close();
           }
           return res;
        }
    }

    private bool EmailYaRegistrado(string? email)//Melian
    {
        using (MySqlConnection connection = new MySqlConnection(ConnectionString))
        {
            var query = "SELECT COUNT(*) FROM inquilino WHERE email = @Email";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Email", email);
                connection.Open();
                var count = Convert.ToInt32(command.ExecuteScalar());
                return count > 0;
            }
        }
    }

    public int Modificar(Inquilino inquilino){
        int res = -1;
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
           var query = $@"UPDATE inquilino
           SET nombre= @nombre,
           apellido= @apellido,
           dni = @dni,
           email= @email,
           telefono = @telefono
           WHERE id_inquilino = @id_inquilino";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
               command.Parameters.AddWithValue("@id_inquilino", inquilino.Id);
               command.Parameters.AddWithValue("@nombre", inquilino.Nombre);
               command.Parameters.AddWithValue("@apellido", inquilino.Apellido);
               command.Parameters.AddWithValue("@dni", inquilino.Dni);
               command.Parameters.AddWithValue("@email", inquilino.Email);
               command.Parameters.AddWithValue("@telefono", inquilino.Telefono);
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
           var query = $@"DELETE FROM inquilino
           WHERE id_inquilino = @id";
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
