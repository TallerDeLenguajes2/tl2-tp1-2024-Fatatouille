namespace Clientes
{
    class Cliente
    {
        private string Nombre;
        private string Direccion;
        private int Telefono;
        private string DatosReferenciaDireccion;

        public string nombre {get => Nombre;}
        public string direccion{get=>Direccion;}
        public int telefono{get=>Telefono;}
        public string datosReferenciaDireccion{get=>DatosReferenciaDireccion;}

        public Cliente(string nombre, string direccion, int telefono, string referencias)
        {
            this.Nombre = nombre;
            this.Direccion = direccion;
            this.Telefono = telefono;
            this.DatosReferenciaDireccion = referencias;
        }
    }
}