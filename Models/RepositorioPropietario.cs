using MySql.Data.MySqlClient;

namespace net.Models;

public class RepositorioPropietario
{
    public string ConnectionString="Server=localhost;User=root;Password=;Database=inmobiliaria;SslMode=none";
    public List<Propietario> ObtenerTodos(){
        List<Propietario> propietarios = new List<Propietario>();
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
           var query = $@"SELECT {nameof(Propietario.id_propietario)},{nameof(Propietario.nombre)},{nameof(Propietario.apellido)},{nameof(Propietario.dni)},{nameof(Propietario.email)},{nameof(Propietario.telefono)} 
           FROM propietario";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
               connection.Open();
               var reader = command.ExecuteReader();
               while(reader.Read()){
                   propietarios.Add(new Propietario{
                        id_propietario = reader.GetInt32(nameof(Propietario.id_propietario)),
                        nombre = reader.GetString(nameof(Propietario.nombre)),
                        apellido = reader.GetString(nameof(Propietario.apellido)),
                        dni = reader.GetString(nameof(Propietario.dni)),
                        email = reader.GetString(nameof(Propietario.email)),
                        telefono = reader.GetString(nameof(Propietario.telefono)),
                            
                   });
               }
           }
        }
        return propietarios;
    }
    public Propietario? ObtenerUno(int id){
        Propietario? propietario = null;
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
           var query = $@"SELECT {nameof(Propietario.id_propietario)},{nameof(Propietario.nombre)},{nameof(Propietario.apellido)},{nameof(Propietario.dni)},{nameof(Propietario.email)},{nameof(Propietario.telefono)} 
           FROM propietario
           WHERE {nameof(Propietario.id_propietario)} = @id";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
                command.Parameters.AddWithValue("@id", id);
               connection.Open();
               var reader = command.ExecuteReader();
               if(reader.Read()){
                   propietario = new Propietario{
                        id_propietario = reader.GetInt32(nameof(Propietario.id_propietario)),
                        nombre = reader.GetString(nameof(Propietario.nombre)),
                        apellido = reader.GetString(nameof(Propietario.apellido)),
                        dni = reader.GetString(nameof(Propietario.dni)),
                        email = reader.GetString(nameof(Propietario.email)),
                        telefono = reader.GetString(nameof(Propietario.telefono)),
                            
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
           ({nameof(Propietario.nombre)},{nameof(Propietario.apellido)},{nameof(Propietario.dni)},{nameof(Propietario.email)},{nameof(Propietario.telefono)})
           VALUES(@nombre,@apellido,@dni,@email,@telefono);
           SELECT LAST_INSERT_ID();";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
               command.Parameters.AddWithValue("@nombre", propietario.nombre);
               command.Parameters.AddWithValue("@apellido", propietario.apellido);
               command.Parameters.AddWithValue("@dni", propietario.dni);
               command.Parameters.AddWithValue("@email", propietario.email);
               command.Parameters.AddWithValue("@telefono", propietario.telefono);
               connection.Open();   
               res = Convert.ToInt32(command.ExecuteScalar());
               connection.Close();
           }
           return res;
        }
    }

    public int Modificar(Propietario propietario){
        int res = -1;
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
           var query = $@"UPDATE propietario
           SET {nameof(Propietario.nombre)} = @nombre,
           {nameof(Propietario.apellido)} = @apellido,
           {nameof(Propietario.dni)} = @dni,
           {nameof(Propietario.email)} = @email,
           {nameof(Propietario.telefono)} = @telefono
           WHERE {nameof(Propietario.id_propietario)} = @id_propietario";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
               command.Parameters.AddWithValue("@id_propietario", propietario.id_propietario);
               command.Parameters.AddWithValue("@nombre", propietario.nombre);
               command.Parameters.AddWithValue("@apellido", propietario.apellido);
               command.Parameters.AddWithValue("@dni", propietario.dni);
               command.Parameters.AddWithValue("@email", propietario.email);
               command.Parameters.AddWithValue("@telefono", propietario.telefono);
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
           WHERE {nameof(Propietario.id_propietario)} = @id";
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
