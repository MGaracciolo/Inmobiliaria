using MySql.Data.MySqlClient;

namespace net.Models;

public class RepositorioInquilino: RepositorioBase
{
    public List<Inquilino> ObtenerTodos(){
        List<Inquilino> inquilinos = new List<Inquilino>();
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
           var query = $@"SELECT 
            id_inquilino AS InquilinoId,
            nombre AS NombreI,
            apellido AS ApellidoI,
            dni AS DniI,
            email AS EmailI,
            telefono AS TelefonoI 
           FROM inquilino";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
               connection.Open();
               var reader = command.ExecuteReader();
               while(reader.Read()){
                  inquilinos.Add(new Inquilino{
                        InquilinoId = reader.GetInt32(nameof(Inquilino.InquilinoId)),
                        NombreI = reader.GetString(nameof(Inquilino.NombreI)),
                        ApellidoI = reader.GetString(nameof(Inquilino.ApellidoI)),
                        DniI = reader.GetString(nameof(Inquilino.DniI)),
                        EmailI = reader.GetString(nameof(Inquilino.EmailI)),
                        TelefonoI = reader.GetString(nameof(Inquilino.TelefonoI)),
                            
                   });
               }
           }
        }
        return inquilinos;
    }
    public Inquilino? ObtenerUno(int id){
        Inquilino? inquilino = null;
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
           var query = $@"SELECT 
            id_inquilino AS InquilinoId,
            nombre AS NombreI,
            apellido AS ApellidoI,
            dni AS DniI,
            email AS EmailI,
            telefono AS TelefonoI 
           FROM inquilino
           WHERE id_inquilino = @id";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
                command.Parameters.AddWithValue("@id", id);
               connection.Open();
               var reader = command.ExecuteReader();
               if(reader.Read()){
                   inquilino = new Inquilino{
                        InquilinoId = reader.GetInt32(nameof(Inquilino.InquilinoId)),
                        NombreI = reader.GetString(nameof(Inquilino.NombreI)),
                        ApellidoI = reader.GetString(nameof(Inquilino.ApellidoI)),
                        DniI = reader.GetString(nameof(Inquilino.DniI)),
                        EmailI = reader.GetString(nameof(Inquilino.EmailI)),
                        TelefonoI = reader.GetString(nameof(Inquilino.TelefonoI)),
                            
                   };
               }
               connection.Close();
           }
        }
        return inquilino;
    }

    public int Alta(Inquilino inquilino){

        //Melian
        if (EmailYaRegistrado(inquilino.EmailI))
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
               command.Parameters.AddWithValue("@nombre", inquilino.NombreI);
               command.Parameters.AddWithValue("@apellido", inquilino.ApellidoI);
               command.Parameters.AddWithValue("@dni", inquilino.DniI);
               command.Parameters.AddWithValue("@email", inquilino.EmailI);
               command.Parameters.AddWithValue("@telefono", inquilino.TelefonoI);
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
               command.Parameters.AddWithValue("@id_inquilino", inquilino.InquilinoId);
               command.Parameters.AddWithValue("@nombre", inquilino.NombreI);
               command.Parameters.AddWithValue("@apellido", inquilino.ApellidoI);
               command.Parameters.AddWithValue("@dni", inquilino.DniI);
               command.Parameters.AddWithValue("@email", inquilino.EmailI);
               command.Parameters.AddWithValue("@telefono", inquilino.TelefonoI);
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
