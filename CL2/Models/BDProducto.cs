
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using static System.Net.Mime.MediaTypeNames;










namespace CL2.Models
{
    public class BDProducto
    {









        //conexion para la base de datos
        // Cambiar la cadena de conexion segun tu configuración
        //estas 3 partes en la cadena tiene que estar si o si
        string cadenaConexion = "Data Source=DESKTOP-5BJSQKC;" +
            "Initial Catalog=CL2;" +
            "Integrated Security=True;";








        //METODO PARA OBTENER FECHA PREGUNTA 1
        public List<Producto> ObtenerPorFecha(DateTime fecOne)
        {
            List<Producto> listaProducto = new List<Producto>();//donde se almcena toda la data
            SqlConnection con = new SqlConnection(cadenaConexion);// se conecta ojo importar la libreria
            SqlCommand cmd = new SqlCommand("ListaPorFecha", con);//comando o consulta a ejecutar

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@fecOne", fecOne);


            con.Open();// abre la coneccion
                       //aqui se almacena temporalmente toda la data(en filas ) ose en la memoria
            SqlDataReader dr = cmd.ExecuteReader();//ejectua la consulta





            //recorremos todo el dr donde esta todo fila x fila
            while (dr.Read())
            {
                // GetString(0) son las posiciones de las columnas

                Producto Producto = new Producto();//clase guia y donde se almcena el objeto
                Producto.Id = dr.GetString(0);
                Producto.Nombre = dr.GetString(1);
                Producto.Idtipo = dr.GetInt32(2);
                Producto.Precio = dr.GetFloat(3);
                Producto.Fecha = dr.GetDateTime(4);
                listaProducto.Add(Producto); //agregamos todo se guarda todo
            }

            //quien llama este metodo devuelve esto
            return listaProducto;


        }














        //ESTE METODO LISTA CLIENTE  + TIPOCLIENTE(TIPO) 
        public List<Producto> ObtenerTodosJoin()
        {
            List<Producto> listaProductos = new List<Producto>();
            SqlConnection con = new SqlConnection(cadenaConexion);
            //con inner join a partir del id queremos el nombre 
            //desjar espacio al final
            string sql = "select c.Id,c.nombre, c.precio, c.fecha ,c.idTipo , c.foto , t.tipo " +
                        "from Producto c " +
                        "inner join TipoProducto t " +
                        "on c.idTipo = t.Id ";
            SqlCommand cmd = new SqlCommand(sql, con);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Producto producto = new Producto();
                producto.Id = dr.GetString(0);
                producto.Nombre = dr.GetString(1);
                producto.Precio = dr.GetFloat(2);
                producto.Fecha = dr.GetDateTime(3);
                producto.Idtipo = dr.GetInt32(4);
                producto.Foto = dr.GetString(5);
                producto.Tipo = dr.GetString(6);
                listaProductos.Add(producto);
            }
            return listaProductos;
        }


















        //METODO PARA OBTENER TODO PRODUCTO PREGUNTA 2
        public List<Producto> ObtenerTodos()
        {
            List<Producto> listaProducto = new List<Producto>();//donde se almcena toda la data
            SqlConnection con = new SqlConnection(cadenaConexion);// se conecta ojo importar la libreria
            SqlCommand cmd = new SqlCommand("ListaTodo", con);//comando o consulta a ejecutar
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();// abre la coneccion
            //aqui se almacena temporalmente toda la data(en filas ) ose en la memoria
            SqlDataReader dr = cmd.ExecuteReader();//ejectua la consulta SOLO PARA LEER


            while (dr.Read())
            {
                // GetString(0) son las posiciones de las columnas

                Producto Producto = new Producto();//clase guia y donde se almcena el objeto
                Producto.Id = dr.GetString(0);
                Producto.Nombre = dr.GetString(1);
                Producto.Idtipo = dr.GetInt32(2);
                Producto.Precio = dr.GetFloat(3);
                Producto.Fecha = dr.GetDateTime(4);
                listaProducto.Add(Producto); //agregamos todo se guarda todo
            }

            //quien llama este metodo devuelve esto
            return listaProducto;
        }
































        //METODO PARA CREAR UN OBJETO CLIENTE PREGUNTA 2
        //los nombres de los parametros ponle cualquiera
        public int Crear(string idpro, string nombre,float precio, DateTime fecha, int idtipo , string Foto)
        {





            SqlConnection con = new SqlConnection(cadenaConexion);// se conecta ojo importar la libreria
            SqlCommand cmd = new SqlCommand("spCrearProducto", con);//nombre sp procedimiento


            //ojo poner el cursor sobre  CommandType e importar el System.Data
            cmd.CommandType = CommandType.StoredProcedure;

            //enviado la data de los inputs ingresado a los parametro del procedure
            cmd.Parameters.AddWithValue("@id", idpro);
            cmd.Parameters.AddWithValue("@nombre", nombre);
            cmd.Parameters.AddWithValue("@precio", precio);
            cmd.Parameters.AddWithValue("@fecha", fecha);
            cmd.Parameters.AddWithValue("@idTipo", idtipo);
            cmd.Parameters.AddWithValue("@Foto", Foto);






            con.Open();


            //ejecuta algo q no es 1 consulta osea actulizacion como insertar o actualizar
            //ademas almcenaremos la cantidad de objetos insertados
            int nrProductos = cmd.ExecuteNonQuery();


            con.Close();  //cierra la conexion recordar ojo



            //regresa a q llama este metod la cantidad de insertados
            return nrProductos;
        }







        public Producto ObtenerPorId(string id)
        {
            Producto producto = new Producto();
            SqlConnection con = new SqlConnection(cadenaConexion);
            SqlCommand cmd = new SqlCommand("select  id , nombre , precio , fecha , idTipo " +
              " from producto where id = @id ", con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                producto.Id = dr.GetString(0);
                producto.Nombre = dr.GetString(1);
                producto.Precio = dr.GetFloat(2);
                producto.Fecha = dr.GetDateTime(3);
                producto.Idtipo = dr.GetInt32(4);
            }
            return producto;
        }






        //METODO PARA actualizar UN OBJETO CLIENTE PREGUNTA 2
        //los nombres de los parametros ponle cualquiera
        public int Actualizar(string Id, string Nombre, float Precio, DateTime Fecha, int Idtipo, string Foto)
        {





            SqlConnection con = new SqlConnection(cadenaConexion);// se conecta ojo importar la libreria
            SqlCommand cmd = new SqlCommand("spActuliazarProducto", con);//nombre sp procedimiento


            //ojo poner el cursor sobre  CommandType e importar el System.Data
            cmd.CommandType = CommandType.StoredProcedure;

            //enviado la data de los inputs ingresado a los parametro del procedure
            cmd.Parameters.AddWithValue("@id", Id);
            cmd.Parameters.AddWithValue("@nombre", Nombre);
            cmd.Parameters.AddWithValue("@precio", Precio);
            cmd.Parameters.AddWithValue("@fecha", Fecha);
            cmd.Parameters.AddWithValue("@idTipo", Idtipo);
            cmd.Parameters.AddWithValue("@foto", Foto);






            con.Open();


            //ejecuta algo q no es 1 consulta osea actulizacion como insertar o actualizar
            //ademas almcenaremos la cantidad de objetos insertados
            int nrProductos = cmd.ExecuteNonQuery();


            con.Close();  //cierra la conexion recordar ojo



            //regresa a q llama este metod la cantidad de insertados
            return nrProductos;
        }












        //METODO PARA eliminar
        public int Borrar(string Id)
        {
            SqlConnection con = new SqlConnection(cadenaConexion);
            SqlCommand cmd = new SqlCommand("spBorrarProducto", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", Id);

            con.Open();
            int nroProductos = cmd.ExecuteNonQuery();
            con.Close();
            return nroProductos;
        }























    }





}
