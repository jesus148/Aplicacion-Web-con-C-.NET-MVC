using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using CL2.Models;
using System.Diagnostics;
using System.Net;


namespace CL2.Controllers
{
    public class ProductoController : Controller
    {

        BDProducto bdc = new BDProducto();



        BDTipoProducto bdtc = new BDTipoProducto();



        List<TipoProducto> tipos;



        public IActionResult Inicio()
        {
            return View();
        }

















        //pregunta 1 lista por fecha 


        public IActionResult ListadoPorFecha()
        {
            return View();
        }





        public IActionResult ListaPorDosFec(DateTime fecOne)
        {


            List<Producto> listProducto = bdc.ObtenerPorFecha(fecOne);





            return View(listProducto);
        }







        //pregunta 2 




        //listado total









        //METODO LISTA TODO LA DATA DE CLIENTE
        //Views > cliente > Listado
        public IActionResult ListadoTotal()
        {



     
            List<Producto> listaProducto = bdc.ObtenerTodosJoin();


       
            return View(listaProducto);


        }


















        //PREGUNTA 2 CREAR PRODUCTO


        //metodo para crear producto



        [HttpGet]
        public IActionResult Crear()
        {



            tipos = bdtc.ObtenerTodos();


            ViewBag.tipos = new SelectList(tipos, "Id", "Tipo", 1);






            return View();



        }












        //ESTA VISTA SE LLAMARA CUANDO EL FORM CREAR TENGA LOS DATOS CARGADOS
        //OSEA ES COMO UN CONTRUCTOR DIFERENCIANDO CON SUS PARAMETRO ALGO ASI RECORDARSE DE JAVA
        //ESTA VISTA TIENE PARAMETROS Y ESOS PARAMETROS SERAN LOS INPUTS ENVIADOR DESDE LA VISTA



        //recomendable estos parametro los nombres
        //(string Dni, string Nombre , string Direccion ,int Credito)
        //deben ser iguales en la vista crear en su form en el   <input asp-for=

        //ademas cuando se envia de un 1 form <form asp-action="Crear"> esta en la vista Crear
        //debe usarse este HttpPost ademas pq usa  asp-action ademas tambien pq enviamos y creamos
        [HttpPost]
        public IActionResult Crear(string Id, string Nombre, float Precio, DateTime Fecha, int Idtipo)
        {


            string Foto = "";

            //ejecutamos y almacenamos en 1 variabla pa ver cuantos registrados
            int nr = bdc.Crear(Id, Nombre, Precio, Fecha, Idtipo, Foto);


            //este le devolvemos a la misma vista para mostrale el mensaje obvio luego de enviar pa registrar
            //recordar q siempre se retorna algo a la vista
           

         
            if (nr == 1)
            {
                ViewBag.mensaje = "producto creado correctamente";
            }
            else
            {
                ViewBag.mensaje = "Cliente NO creado correctamente";
            }



            tipos = bdtc.ObtenerTodos();


            ViewBag.tipos = new SelectList(tipos, "Id", "Tipo", 1);



            return View();



        }








       //EDITAR


        [HttpGet]
        public IActionResult Editar(string Id)
        {




            Producto producto = bdc.ObtenerPorId(Id);


            tipos = bdtc.ObtenerTodos();

            //"Id", "Tipo" : igual a la clase guia del clase combo tipoproducto
            ViewBag.tipos = new SelectList(tipos, "Id", "Tipo", producto.Idtipo);


            return View(producto);



        }





        public IActionResult Editar(string Id, string Nombre, float Precio, DateTime Fecha, int Idtipo , IFormFile Foto)
        {


            Producto producto = bdc.ObtenerPorId(Id);

            //la foto  IFormFile foto lo convertimos a formato para almcanear en la bd 
            string rutaFoto = "~/imagenes/" + Foto.FileName;

            //ejecutamos y almacenamos en 1 variabla pa ver cuantos registrados
            int nr = bdc.Actualizar(Id, Nombre, Precio, Fecha, Idtipo , rutaFoto);



            //ademas esto agrega tanto el nombre como el archvi jpg
            //osea esto agrega a la carpeta imagenes de carpeta POO2-ASPMVC de tu maquina , el archvivo jpg
            //Environment : entorno de la aplicacion
            //"/wwwroot/imagenes/" : nombre carpeta de tu proyecto
            string rutaImagenes = Environment.CurrentDirectory + "/wwwroot/imagenes/" + Foto.FileName;
            FileStream flujo = new FileStream(rutaImagenes, FileMode.Create);//crea 
            Foto.CopyTo(flujo);




            //ejecutamos y almacenamos en 1 variabla pa ver cuantos registrados
            //  int nr = bdc.Actualizar(producto);


            if (nr == 0){
                ViewBag.mensaje = "producto no actualizado correctamente";
            }else{
                //este le devolvemos a la misma vista para mostrale el mensaje obvio luego de enviar pa registrar
                //recordar q siempre se retorna algo a la vista
                ViewBag.mensaje = "producto actualizado correctamente";
            }






            //este le devolvemos a la misma vista para mostrale el mensaje obvio luego de enviar pa registrar
            //recordar q siempre se retorna algo a la vista
           // ViewBag.mensaje = "producto actualizado correctamente";





            tipos = bdtc.ObtenerTodos();

            //"Id", "Tipo" : igual a la clase guia del clase combo tipoproducto
            ViewBag.tipos = new SelectList(tipos, "Id", "Tipo", producto.Idtipo);


            return View(producto);



        }















        [HttpGet]
        public IActionResult Eliminar(string Id)
        {
            Producto producto = bdc.ObtenerPorId(Id);




            tipos = bdtc.ObtenerTodos();

            //"Id", "Tipo" : igual a la clase guia del clase combo tipoproducto
            ViewBag.tipos = new SelectList(tipos, "Id", "Tipo", producto.Idtipo);


            return View(producto);
        }




        //ENVIA EL DNI CON ESO ELIMINA y muestra mensajes
        public IActionResult Eliminar(Producto producto)
        {
            Producto ProductoEliminar = bdc.ObtenerPorId(producto.Id); //devuelve el objeto elimnado
            int nroRegistros = bdc.Borrar(ProductoEliminar.Id);//borrar de la bd





            if (nroRegistros == 1)
            {
                ViewBag.mensaje = "producto eliminado correctamente";
            }
            else
            {
                ViewBag.mensaje = "producto NO eliminado correctamente";
            }



            tipos = bdtc.ObtenerTodos();

            //"Id", "Tipo" : igual a la clase guia del clase combo tipoproducto
            ViewBag.tipos = new SelectList(tipos, "Id", "Tipo", ProductoEliminar.Idtipo);


            //retorna el mismo objeto a eliminar 
            return View(ProductoEliminar);
        }

































    }
}
