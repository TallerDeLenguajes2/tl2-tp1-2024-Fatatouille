using System.IO.Compression;
using Pedido;

namespace Cadetes
{
    class Cadete
    {
        private int Id;
        private string Nombre;
        private string Direccion;
        private int Telefono;
        private List<Pedidos> ListadoPedidos;

        public int id {get=>Id;}
        public string nombre { get=> Nombre;}
        public string direccion {get=> Direccion;}
        public int telefono {get=>Telefono;}
        public List<Pedidos> listadoPedidos{get=> ListadoPedidos;}

        public Cadete(int id, string nombre, string direccion, int telefono){
            this.Id = id; 
            this.Nombre = nombre; 
            this.Direccion = direccion; 
            this.Telefono = telefono; 
            ListadoPedidos = new List<Pedidos>(); 
        }

        public int JornalACobrar()
        {
            return 500* (ListadoPedidos.Count(a=> a.Estado == Estados.Entregado));
        }
    }
}