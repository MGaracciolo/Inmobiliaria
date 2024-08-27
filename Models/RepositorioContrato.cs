using System.Security.Authentication;
using MySql.Data.MySqlClient;

namespace net.Models;

public class RepositorioContrato: RepositorioBase
{
    public List<Contrato> ObtenerTodos(){
        List<Contrato> contratos = new List<Contrato>();
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
           var query = $@"SELECT {nameof(Contrato.id_contrato)}, {nameof(Contrato.id_inquilino)}, {nameof(Contrato.id_inmueble)},
           {nameof(Contrato.desde)}, {nameof(Contrato.hasta)}, {nameof(Contrato.precio)},
           {nameof(Contrato.estado)}
           FROM contrato";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
               connection.Open();
               var reader = command.ExecuteReader();
               while(reader.Read()){
                  contratos.Add(new Contrato{
                       id_contrato = reader.GetInt32(nameof(Contrato.id_contrato)),
                       id_inquilino = reader.GetInt32(nameof(Contrato.id_inquilino)),
                       id_inmueble = reader.GetInt32(nameof(Contrato.id_inmueble)),
                       desde = reader.GetDateTime(nameof(Contrato.desde)),
                       hasta = reader.GetDateTime(nameof(Contrato.hasta)),
                       precio = reader.GetDouble(nameof(Contrato.precio)),
                       estado = reader.GetBoolean(nameof(Contrato.estado))
                   });
               }
           }
        }
        return contratos;
    }
    public Contrato? ObtenerUno(int id){
        Contrato? contrato = null;
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
           var query = $@"SELECT {nameof(Contrato.id_contrato)}, {nameof(Contrato.id_inquilino)}, {nameof(Contrato.id_inmueble)},
           {nameof(Contrato.desde)}, {nameof(Contrato.hasta)}, {nameof(Contrato.precio)},
           {nameof(Contrato.estado)}
           FROM contrato
           WHERE {nameof(Contrato.id_contrato)} = @id";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
                command.Parameters.AddWithValue("@id", id);
               connection.Open();
               var reader = command.ExecuteReader();
               if(reader.Read()){
                   contrato = new Contrato{
                       id_contrato = reader.GetInt32(nameof(Contrato.id_contrato)),
                       id_inquilino = reader.GetInt32(nameof(Contrato.id_inquilino)),
                       id_inmueble = reader.GetInt32(nameof(Contrato.id_inmueble)),
                       desde = reader.GetDateTime(nameof(Contrato.desde)),
                       hasta = reader.GetDateTime(nameof(Contrato.hasta)),
                       precio = reader.GetDouble(nameof(Contrato.precio)),
                       estado = reader.GetBoolean(nameof(Contrato.estado))
                   };
               }
               connection.Close();
           }
        }
        return contrato;
    }

    public int Alta(Contrato contrato){
        int res = -1;
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
           var query = $@"INSERT INTO contrato
            {nameof(Contrato.id_inquilino)}, {nameof(Contrato.id_inmueble)},
           {nameof(Contrato.desde)}, {nameof(Contrato.hasta)}, {nameof(Contrato.precio)},
           {nameof(Contrato.estado)}
           VALUES(@id_inquilino,@id_inmueble,@desde,@hasta,@precio,@estado);
           SELECT LAST_INSERT_ID();";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
               command.Parameters.AddWithValue("@id_inquilino", contrato.id_inquilino);
               command.Parameters.AddWithValue("@id_inmueble", contrato.id_inmueble);
               command.Parameters.AddWithValue("@desde", contrato.desde);
               command.Parameters.AddWithValue("@hasta", contrato.hasta);
               command.Parameters.AddWithValue("@precio", contrato.precio);
               command.Parameters.AddWithValue("@estado", contrato.estado);
               connection.Open();   
               res = Convert.ToInt32(command.ExecuteScalar());
               connection.Close();
           }
           return res;
        }
    }

    public int Modificar(Contrato contrato){
        int res = -1;
        using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
           var query = $@"UPDATE contrato
           SET 
           {nameof(Contrato.id_inquilino)} = @id_inquilino,
           {nameof(Contrato.id_inmueble)} = @id_inmueble,
           {nameof(Contrato.desde)} = @desde,
           {nameof(Contrato.hasta)} = @hasta,
           {nameof(Contrato.precio)} = @precio,
           {nameof(Contrato.estado)} = @estado
           WHERE {nameof(Contrato.id_contrato)} = @id_contrato";
           using(MySqlCommand command = new MySqlCommand(query, connection)){
               command.Parameters.AddWithValue("@id_inquilino", contrato.id_inquilino);
               command.Parameters.AddWithValue("@id_inmueble", contrato.id_inmueble);
               command.Parameters.AddWithValue("@desde", contrato.desde);
               command.Parameters.AddWithValue("@hasta", contrato.hasta);
               command.Parameters.AddWithValue("@precio", contrato.precio);
               command.Parameters.AddWithValue("@estado", contrato.estado);
               connection.Open();
               res = command.ExecuteNonQuery();
               connection.Close();
           }
        }
          
        return res;
    }


    
    //Para borrar el contrato hacer baja LOGICA, a estado asignarle false



    // public int Baja(int id){
    //     int res = -1;
    //     using(MySqlConnection connection = new MySqlConnection(ConnectionString)){
    //        var query = $@"UPDATE inmueble
    //        SET 
    //        {nameof(Inmueble.estado)} = @estado
    //        WHERE {nameof(Inmueble.id_inmueble)} = @id";
    //        using(MySqlCommand command = new MySqlCommand(query, connection)){
              
    //            command.Parameters.AddWithValue("@estado", inmueble.estado);
    //            connection.Open();
    //            res = command.ExecuteNonQuery();
    //            connection.Close();
    //        }
    //     }
    //     return res;
    // }


}
