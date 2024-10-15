using Cadetes;
using Pedido;
using Clientes;

namespace Cadeteria
{
    public class Cadeterias
    {
        private string Nombre;
        private int Telefono;
        private List<Cadete> ListadoCadetes;
        private List<Pedidos> listaPedido;
        Random random = new Random();

        public string nombre {get => Nombre; set => Nombre= value;}
        public int telefono {get=>Telefono; set => Telefono= value;}
        public List<Cadete> listadoCadetes{get=> ListadoCadetes; set => ListadoCadetes = value;}
        public List<Pedidos> ListaPedido { get => listaPedido; set => listaPedido = value; }

        public Cadeterias()
        {
            this.ListadoCadetes = new List<Cadete>();
            this.listaPedido = new List<Pedidos>();
        }

        public bool AsignarPedido(int idCadete, int idPedido){
            var pedido = listaPedido.FirstOrDefault(x => x.Nro == idPedido);
            var cadete = ListadoCadetes.FirstOrDefault(x => x.id == idCadete);

            if(pedido != null && cadete != null){
                pedido.cadete = cadete;

                return true;
            }
            return false;
        }
        public bool ReasignarCadete(Pedidos pedido)
        {
            Cadete nCadete = ListadoCadetes[random.Next(ListadoCadetes.Count)];

            if (nCadete == null)
            {
                return false;
            }

            pedido.cadete = nCadete;

            return true;
        }
        public void AgregarCadete(Cadete cadete)
        {
            ListadoCadetes.Add(cadete);
        }
        public void DespedirCadete(Cadete cadete)
        {
            ListadoCadetes.Remove(cadete);
        }

        public Pedidos AltaPedido()
        {
            int NroPedido = random.Next(100);

            Console.WriteLine("Observaciones: ");
            string obs = Console.ReadLine();

            Console.WriteLine("Nombre: ");
            string nombre = Console.ReadLine();

            Console.WriteLine("Direccion: ");
            string dir = Console.ReadLine();

            Console.WriteLine("Telefono: ");
            int tel = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Referencias de su direccion: ");
            string RefDir = Console.ReadLine();

            return new Pedidos(NroPedido, obs, nombre, dir, tel, RefDir, null);
        }

        public int JornalACobrar(int idCadete){
        int pedidosRealizados = listaPedido.Count(x => x.cadete.id == idCadete);
        return pedidosRealizados*1500;
        }
        public void AgregarPedido(Pedidos pedido){
            ListaPedido.Add(pedido);
        }

        public void EliminarPedido(Pedidos pedido){
            ListaPedido.Remove(pedido);
        }
    }
}