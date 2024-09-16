using MySql.Data.MySqlClient;

namespace net.Models;

public class RepositorioUsuario: RepositorioBase
{
    public List<Usuario> ObtenerTodos(){
        List<Usuario> usuarios = new List<Usuario>();
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
           var query = $@"SELECT 
            id_usuario AS UsuarioId,
            nombre AS Nombre,
            apellido AS Apellido,
            email AS Email,
            password AS Password,
            rol AS Rol,
            avatar AS Avatar
           FROM usuario";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
               connection.Open();
               var reader = command.ExecuteReader();
               while(reader.Read()){
                  usuarios.Add(new Usuario{
                        UsuarioId = reader.GetInt32(nameof(Usuario.UsuarioId)),
                        Nombre = reader.GetString(nameof(Usuario.Nombre)),
                        Apellido = reader.GetString(nameof(Usuario.Apellido)),
                        Email = reader.GetString(nameof(Usuario.Email)),
                        Password = reader.GetString(nameof(Usuario.Password)),
                        Rol = reader.GetInt32(nameof(Usuario.Rol)),
                        Avatar = reader.IsDBNull(reader.GetOrdinal(nameof(Usuario.Avatar))) ? null : reader.GetString(nameof(Usuario.Avatar)) 
                   });
               }
           }
        }
        return usuarios;
    }
    
    public Usuario? ObtenerUno(int id){
        Usuario? usuario = null;
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
           var query = $@"SELECT 
            id_usuario AS UsuarioId,
            nombre AS Nombre,
            apellido AS Apellido,
            email AS Email,
            password AS Password,
            rol AS Rol,
            avatar AS Avatar
           FROM usuario
           WHERE id_usuario = @id";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
                command.Parameters.AddWithValue("@id", id);
               connection.Open();
               var reader = command.ExecuteReader();
               if(reader.Read()){
                   usuario = new Usuario{
                        UsuarioId = reader.GetInt32(nameof(Usuario.UsuarioId)),
                        Nombre = reader.GetString(nameof(Usuario.Nombre)),
                        Apellido = reader.GetString(nameof(Usuario.Apellido)),
                        Email = reader.GetString(nameof(Usuario.Email)),
                        Password = reader.GetString(nameof(Usuario.Password)),
                        Rol = reader.GetInt32(nameof(Usuario.Rol)),
                        Avatar = reader.IsDBNull(reader.GetOrdinal(nameof(Usuario.Avatar))) ? null : reader.GetString(nameof(Usuario.Avatar))
                   };
               }
               connection.Close();
           }
        }
        return usuario;
    }

    public int Alta(Usuario usuario){
        // modificar esto para que sea un try catch y que muestre un alert cuando el email ya este registrado
        //Melian
        if (EmailYaRegistrado(usuario.Email))
        {
            throw new Exception("El Email ya esta registrado.");
        }

        int res = -1;
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
           var query = $@"INSERT INTO usuario
           (nombre,
            apellido,
            email,
            password,
            rol,
            avatar)
           VALUES(@nombre,@apellido,@email,@password,@rol,@avatar);
           SELECT LAST_INSERT_ID();";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
               command.Parameters.AddWithValue("@nombre", usuario.Nombre);
               command.Parameters.AddWithValue("@apellido", usuario.Apellido);
               command.Parameters.AddWithValue("@email", usuario.Email);
               command.Parameters.AddWithValue("@rol", usuario.Rol);
               command.Parameters.AddWithValue("@password", usuario.Password);
               if (String.IsNullOrEmpty(usuario.Avatar))
					command.Parameters.AddWithValue("@avatar", DBNull.Value);
				else
					command.Parameters.AddWithValue("@avatar", usuario.Avatar);
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
            var query = "SELECT COUNT(*) FROM usuario WHERE email = @Email";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Email", email);
                connection.Open();
                var count = Convert.ToInt32(command.ExecuteScalar());
                return count > 0;
            }
        }
    }

    public int Modificar(Usuario usuario){
        int res = -1;
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
           var query = $@"UPDATE usuario
           SET nombre= @nombre,
           apellido= @apellido,
           email= @email,
           rol = @rol,
           password = @password,
           avatar = @avatar
           WHERE id_usuario = @id_usuario";
           
           using(MySqlCommand command = new MySqlCommand(query, connection)){
               command.Parameters.AddWithValue("@id_usuario", usuario.UsuarioId);
               command.Parameters.AddWithValue("@nombre", usuario.Nombre);
               command.Parameters.AddWithValue("@apellido", usuario.Apellido);
               command.Parameters.AddWithValue("@email", usuario.Email);
               command.Parameters.AddWithValue("@rol", usuario.Rol);
               command.Parameters.AddWithValue("@password", usuario.Password);
               command.Parameters.AddWithValue("@avatar", usuario.Avatar);
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
           var query = $@"DELETE FROM usuario
           WHERE id_usuario = @id";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
               command.Parameters.AddWithValue("@id", id);
               connection.Open();
               res = command.ExecuteNonQuery();
               connection.Close();
           }
        }
        return res;
    }

   public Usuario? ObtenerPorEmail(string email){
        Usuario? usuario = null;
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
           var query = $@"SELECT 
            id_usuario AS UsuarioId,
            nombre AS Nombre,
            apellido AS Apellido,
            email AS Email,
            password AS Password,
            rol AS Rol,
            avatar AS Avatar
           FROM usuario
           WHERE email = @email";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
                command.Parameters.AddWithValue("@email", email);
               connection.Open();
               var reader = command.ExecuteReader();
               if(reader.Read()){
                   usuario = new Usuario{
                        UsuarioId = reader.GetInt32(nameof(Usuario.UsuarioId)),
                        Nombre = reader.GetString(nameof(Usuario.Nombre)),
                        Apellido = reader.GetString(nameof(Usuario.Apellido)),
                        Email = reader.GetString(nameof(Usuario.Email)),
                        Password = reader.GetString(nameof(Usuario.Password)),
                        Rol = reader.GetInt32(nameof(Usuario.Rol)),
                        Avatar = reader.IsDBNull(reader.GetOrdinal(nameof(Usuario.Avatar))) ? null : reader.GetString(nameof(Usuario.Avatar))
                   };
               }
               connection.Close();
           }
        }
        return usuario;
    }
}
