using Microsoft.AspNetCore.Http.Timeouts;
using MySql.Data.MySqlClient;

namespace net.Models;

public class RepositorioDireccion : RepositorioBase
{
    public List<Direccion> ObtenerTodos(){
        List<Direccion> direcciones = new List<Direccion>();
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
           var query = $@"SELECT 
           {nameof(Direccion.id_direccion)},
           {nameof(Direccion.calle)},
           {nameof(Direccion.altura)}
           FROM direccion";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
               connection.Open();
               var reader = command.ExecuteReader();
               while(reader.Read()){
                   direcciones.Add(new Direccion{
                       id_direccion = reader.GetInt32(nameof(Direccion.id_direccion)),
                       calle = reader.GetString(nameof(Direccion.calle)),
                       altura = reader.GetInt32(nameof(Direccion.altura))
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
           {nameof(Direccion.id_direccion)},
           {nameof(Direccion.calle)},
           {nameof(Direccion.altura)}
       
           FROM direccion
           WHERE {nameof(Direccion.id_direccion)} = @id";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
                command.Parameters.AddWithValue("@id", id);
               connection.Open();
               var reader = command.ExecuteReader();
               if(reader.Read()){
                  direccion = new Direccion{
                      id_direccion = reader.GetInt32(nameof(Direccion.id_direccion)),
                      calle = reader.GetString(nameof(Direccion.calle)),
                      altura = reader.GetInt32(nameof(Direccion.altura)),
                    //   piso = reader.GetInt32(nameof(Direccion.piso)),
                    //   departamento = reader.GetString(nameof(Direccion.departamento)),
                    //   observaciones = reader.GetString(nameof(Direccion.observaciones))
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
          ({nameof(Direccion.calle)},
           {nameof(Direccion.altura)},
           {nameof(Direccion.piso)},
           {nameof(Direccion.departamento)},
           {nameof(Direccion.observaciones)})
           VALUES(@calle,@altura,@piso,@departamento,@observaciones);
           SELECT LAST_INSERT_ID();";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
               command.Parameters.AddWithValue("@calle", direccion.calle);
               command.Parameters.AddWithValue("@altura", direccion.altura);
               command.Parameters.AddWithValue("@piso", direccion.piso);
               command.Parameters.AddWithValue("@departamento", direccion.departamento);
               command.Parameters.AddWithValue("@observaciones", direccion.observaciones);
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
           SET {nameof(Direccion.calle)} = @calle,
           {nameof(Direccion.altura)} = @altura,
           {nameof(Direccion.piso)} = @piso,
           {nameof(Direccion.departamento)} = @departamento,
           {nameof(Direccion.observaciones)} = @observaciones
           WHERE {nameof(Direccion.id_direccion)} = @id_direccion";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
               command.Parameters.AddWithValue("@id_direccion", direccion.id_direccion);
               command.Parameters.AddWithValue("@calle", direccion.calle);
               command.Parameters.AddWithValue("@altura", direccion.altura);
               command.Parameters.AddWithValue("@piso", direccion.piso);
               command.Parameters.AddWithValue("@departamento", direccion.departamento);
               command.Parameters.AddWithValue("@observaciones", direccion.observaciones);
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
           WHERE {nameof(Direccion.id_direccion)} = @id";
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
