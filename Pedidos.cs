using Clientes;

namespace Pedido
{
    class Pedidos
    {
        private int nro;
        private string obs;
        private Cliente cliente;
        private Estados estado;

        public int Nro { get => nro;}
        public string Obs { get => obs;}
        public Cliente Cliente { get => cliente; set => cliente = value; }
        public Estados Estado { get => estado; set => estado = value; }

        public Pedidos(int numero, string observacion, string nombre, string direccion, int telefono, string referencias)
        {
            nro = numero; 
            obs = observacion; 
            Estado = Estados.Preparando; 
            cliente = new Cliente(nombre, direccion, telefono,referencias); 
        }

        public void VerDireccionCliente()
        {
            Console.WriteLine($"Dirección: {cliente.direccion}");
            Console.WriteLine($"Referencias: {cliente.datosReferenciaDireccion}");
        }

        public void VerDatosCliente()
        {
            Console.WriteLine($"Cliente: {cliente.nombre}");
            Console.WriteLine($"Direccion: {cliente.direccion}");
            Console.WriteLine($"Telefono: {cliente.telefono}");
        }

    }
    public enum Estados {
        Preparando, 
        EnCamino, 
        Entregado
    }
}