using Microsoft.AspNetCore.Http.Timeouts;
using MySql.Data.MySqlClient;

namespace net.Models;

public class RepositorioDireccion : RepositorioBase
{
    public List<Direccion> ObtenerTodos(){
        List<Direccion> direcciones = new List<Direccion>();
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
           var query = $@"SELECT 
           id_direccion AS Id,
           calle AS Calle,
           altura AS Altura
           FROM direccion";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
               connection.Open();
               var reader = command.ExecuteReader();
               while(reader.Read()){
                   direcciones.Add(new Direccion{
                       Id = reader.GetInt32(nameof(Direccion.Id)),
                       Calle = reader.GetString(nameof(Direccion.Calle)),
                       Altura = reader.GetInt32(nameof(Direccion.Altura))
                   });
               }
           }
        }
        return direcciones;
    }
    public Direccion? ObtenerUno(int id){
        Direccion? direccion = null;
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
           var query = $@"SELECT
           id_direccion AS Id,
           calle AS Calle,
           altura AS Altura,
           piso AS Piso,
           departamento AS Departamento,
           observaciones AS Observaciones
           FROM direccion
           WHERE id_direccion = @id";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
                command.Parameters.AddWithValue("@id", id);
               connection.Open();
               var reader = command.ExecuteReader();
               if(reader.Read()){
                  direccion = new Direccion{
                     Id = reader.GetInt32(nameof(Direccion.Id)),
                       Calle = reader.GetString(nameof(Direccion.Calle)),
                       Altura = reader.GetInt32(nameof(Direccion.Altura)),
                       Piso = reader.GetInt32(nameof(Direccion.Piso)),
                       Departamento = reader.GetString(nameof(Direccion.Departamento)),
                       Observaciones = reader.GetString(nameof(Direccion.Observaciones))
                  };
               }
               connection.Close();
           }
        }
        return direccion;
    }

    public int Alta(Direccion direccion){
        int res = -1;
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
           var query = $@"INSERT INTO direccion
          (calle,
           altura,
           piso,
           departamento,
           observaciones)
           VALUES(@calle,@altura,@piso,@departamento,@observaciones);
           SELECT LAST_INSERT_ID();";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
               command.Parameters.AddWithValue("@calle", direccion.Calle);
               command.Parameters.AddWithValue("@altura", direccion.Altura);
               command.Parameters.AddWithValue("@piso", direccion.Piso);
               command.Parameters.AddWithValue("@departamento", direccion.Departamento);
               command.Parameters.AddWithValue("@observaciones", direccion.Observaciones);
               connection.Open();   
               res = Convert.ToInt32(command.ExecuteScalar());
               connection.Close();
           }
           return res;
        }
    }

    public int Modificar(Direccion direccion){
        int res = -1;
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
           var query = $@"UPDATE direccion
           SET calle = @calle,
           altura = @altura,
           piso= @piso,
           departamento = @departamento,
           observaciones= @observaciones
           WHERE id_direccion= @id_direccion";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
               command.Parameters.AddWithValue("@id_direccion", direccion.Id);
               command.Parameters.AddWithValue("@calle", direccion.Calle);
               command.Parameters.AddWithValue("@altura", direccion.Altura);
               command.Parameters.AddWithValue("@piso", direccion.Piso);
               command.Parameters.AddWithValue("@departamento", direccion.Departamento);
               command.Parameters.AddWithValue("@observaciones", direccion.Observaciones);
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
           var query = $@"DELETE FROM direccion
           WHERE id_direccion = @id";
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
