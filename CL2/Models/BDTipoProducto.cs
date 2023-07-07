
using System.Data.SqlClient;

namespace CL2.Models
{
    public class BDTipoProducto
    {






        //conexion para la base de datos
        // Cambiar la cadena de conexion segun tu configuración
        //estas 3 partes en la cadena tiene que estar si o si
        string cadenaConexion = "Data Source=DESKTOP-5BJSQKC;" +
            "Initial Catalog=CL2;" +
            "Integrated Security=True;";










        //tabla combo tipocliente
        //recordar que queremos
        // id + tipocliente (como un combo como en java)

        public List<TipoProducto> ObtenerTodos()
        {
            List<TipoProducto> listaTipoProducto = new List<TipoProducto>();
            SqlConnection con = new SqlConnection(cadenaConexion);
            SqlCommand cmd = new SqlCommand("SELECT * FROM Tipoproducto", con);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                TipoProducto tipoProducto = new TipoProducto();
                tipoProducto.Id = dr.GetInt32(0);
                tipoProducto.Tipo = dr.GetString(1);
                listaTipoProducto.Add(tipoProducto);
            }
            return listaTipoProducto;
        }





        public List<TipoProducto> ObtenerTodosPorDni(string id)
        {
            List<TipoProducto> listaTipoProducto = new List<TipoProducto>();
            SqlConnection con = new SqlConnection(cadenaConexion);
            SqlCommand cmd = new SqlCommand("select t.Id , t.tipo from TipoProducto t inner join Producto p  on t.Id = p.idTipo where p.Id =  @id ", con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                TipoProducto tipoProducto = new TipoProducto();
                tipoProducto.Id = dr.GetInt32(0);
                tipoProducto.Tipo = dr.GetString(1);
                listaTipoProducto.Add(tipoProducto);
            }
            return listaTipoProducto;
        }





    }
}
