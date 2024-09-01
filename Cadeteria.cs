using Cadetes;
using Pedido;

namespace Cadeteria
{
    class Cadeteria
    {
        private string Nombre;
        private int Telefono;
        private List<Cadete> ListadoCadetes;
        Random random = new Random();

        public string nombre {get => Nombre;}
        public int telefono {get=>Telefono;}
        public List<Cadete> listadoCadetes{get=> ListadoCadetes;}

        public Cadeteria(string nombre, int telefono, List<Cadete> cadetes)
        {
            this.Nombre = nombre;
            this.Telefono = telefono;
            this.ListadoCadetes = cadetes;
        }

        public void AsignarPedido(Pedidos pedido){
            if(ListadoCadetes.Count == 0)
            {
                Console.WriteLine("No hay cadetes disponibles.");
            }else{
                Cadete cadete = ListadoCadetes[random.Next(ListadoCadetes.Count)];
                cadete.AgregarPedido(pedido);

                Console.WriteLine($"El cadete {cadete.nombre} está a cargo del pedido {pedido.Nro}");
            }
        }
        public void ReasignarCadete(Pedidos pedido, Cadete cadete)
        {
            cadete.EliminarPedido(pedido, 1);
            Cadete NuevoCadete = ListadoCadetes[random.Next(ListadoCadetes.Count)];
            NuevoCadete.AgregarPedido(pedido);
            Console.WriteLine($"El pedido Nro {pedido.Nro} fue reasignado al cadete {NuevoCadete.nombre}");
        }
        public void AgregarCadete(Cadete cadete)
        {
            ListadoCadetes.Add(cadete);
        }
        public void DespedirCadete(Cadete cadete)
        {
            ListadoCadetes.Remove(cadete);
        }
    }
}