namespace CL2.Models
{
    public class Producto
    {


        public string Id { get; set; }

        public string Nombre { get; set; }

        public float Precio { get; set; }

        public DateTime Fecha { get; set; }


        public int Idtipo { get; set; }

        public string Tipo { get; set; }//este dato no le pertenece aqui solo lo ponemos para 
                                        //relacionarlo con la otra tabla tipocliente y almacenar su tipo


        public string Foto { get; set; } //aca se almacena la ruta de la foto
    }
}
