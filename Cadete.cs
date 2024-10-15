using System.IO.Compression;
using Pedido;

namespace Cadetes
{
    public class Cadete
    {
        private int Id;
        private string Nombre;
        private string Direccion;
        private int Telefono;

        public int id {get=>Id;}
        public string nombre { get=> Nombre;}
        public string direccion {get=> Direccion;}
        public int telefono {get=>Telefono;}

        public Cadete(int id, string nombre, string direccion, int telefono){
            this.Id = id; 
            this.Nombre = nombre; 
            this.Direccion = direccion; 
            this.Telefono = telefono;
        }
    }
}