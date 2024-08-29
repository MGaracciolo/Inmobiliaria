using MySql.Data.MySqlClient;

namespace net.Models;

public class RepositorioInquilino: RepositorioBase
{
    public List<Inquilino> ObtenerTodos(){
        List<Inquilino> inquilinos = new List<Inquilino>();
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
           var query = $@"SELECT {nameof(Inquilino.id_inquilino)},{nameof(Inquilino.nombre)},{nameof(Inquilino.apellido)},{nameof(Inquilino.dni)},{nameof(Inquilino.email)},{nameof(Inquilino.telefono)} 
           FROM inquilino";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
               connection.Open();
               var reader = command.ExecuteReader();
               while(reader.Read()){
                  inquilinos.Add(new Inquilino{
                        id_inquilino = reader.GetInt32(nameof(Inquilino.id_inquilino)),
                        nombre = reader.GetString(nameof(Inquilino.nombre)),
                        apellido = reader.GetString(nameof(Inquilino.apellido)),
                        dni = reader.GetString(nameof(Inquilino.dni)),
                        email = reader.GetString(nameof(Inquilino.email)),
                        telefono = reader.GetString(nameof(Inquilino.telefono)),
                            
                   });
               }
           }
        }
        return inquilinos;
    }
    public Inquilino? ObtenerUno(int id){
        Inquilino? inquilino = null;
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
           var query = $@"SELECT {nameof(Inquilino.id_inquilino)},{nameof(Inquilino.nombre)},{nameof(Inquilino.apellido)},{nameof(Inquilino.dni)},{nameof(Inquilino.email)},{nameof(Inquilino.telefono)} 
           FROM inquilino
           WHERE {nameof(Inquilino.id_inquilino)} = @id";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
                command.Parameters.AddWithValue("@id", id);
               connection.Open();
               var reader = command.ExecuteReader();
               if(reader.Read()){
                   inquilino = new Inquilino{
                        id_inquilino = reader.GetInt32(nameof(Inquilino.id_inquilino)),
                        nombre = reader.GetString(nameof(Inquilino.nombre)),
                        apellido = reader.GetString(nameof(Inquilino.apellido)),
                        dni = reader.GetString(nameof(Inquilino.dni)),
                        email = reader.GetString(nameof(Inquilino.email)),
                        telefono = reader.GetString(nameof(Inquilino.telefono)),
                            
                   };
               }
               connection.Close();
           }
        }
        return inquilino;
    }

    public int Alta(Inquilino inquilino){

        //Melian
        if (EmailYaRegistrado(inquilino.email))
        {
            throw new Exception("El Email ya esta registrado.");
        }

        int res = -1;
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
           var query = $@"INSERT INTO inquilino
           ({nameof(Inquilino.nombre)},{nameof(Inquilino.apellido)},{nameof(Inquilino.dni)},{nameof(Inquilino.email)},{nameof(Inquilino.telefono)})
           VALUES(@nombre,@apellido,@dni,@email,@telefono);
           SELECT LAST_INSERT_ID();";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
               command.Parameters.AddWithValue("@nombre", inquilino.nombre);
               command.Parameters.AddWithValue("@apellido", inquilino.apellido);
               command.Parameters.AddWithValue("@dni", inquilino.dni);
               command.Parameters.AddWithValue("@email", inquilino.email);
               command.Parameters.AddWithValue("@telefono", inquilino.telefono);
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
           SET {nameof(Inquilino.nombre)} = @nombre,
           {nameof(Inquilino.apellido)} = @apellido,
           {nameof(Inquilino.dni)} = @dni,
           {nameof(Inquilino.email)} = @email,
           {nameof(Inquilino.telefono)} = @telefono
           WHERE {nameof(Inquilino.id_inquilino)} = @id_inquilino";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
               command.Parameters.AddWithValue("@id_inquilino", inquilino.id_inquilino);
               command.Parameters.AddWithValue("@nombre", inquilino.nombre);
               command.Parameters.AddWithValue("@apellido", inquilino.apellido);
               command.Parameters.AddWithValue("@dni", inquilino.dni);
               command.Parameters.AddWithValue("@email", inquilino.email);
               command.Parameters.AddWithValue("@telefono", inquilino.telefono);
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
           WHERE {nameof(Inquilino.id_inquilino)} = @id";
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
