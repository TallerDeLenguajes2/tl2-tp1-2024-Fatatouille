using System.Runtime.InteropServices;
using Cadeteria;
using datos;
using Pedido;

Cadeterias miCadeteria = new Cadeterias();
List<Pedidos> pedidosSinAsignar = new List<Pedidos>();
List<Pedidos> pedidosAsignados = new List<Pedidos>();
AccesoADatos accesoDatos;
string extension;
string carpeta;

Console.WriteLine("1. CSV");
Console.WriteLine("2. JSON");
Console.Write("\nSeleccione el tipo de acceso a datos:");
int opcion1 = int.Parse(Console.ReadLine());

switch (opcion1)
{
    case 1:
        accesoDatos = new AccesoCSV();
        extension = ".csv";
        carpeta = "CSV";
        break;
    case 2:
        accesoDatos = new AccesoJSON();
        extension = ".json";
        carpeta = "JSON";
        break;
    default:
        Console.WriteLine("Opción no válida. Se utilizará acceso CSV por defecto.");
        accesoDatos = new AccesoCSV();
        extension = ".csv";
        carpeta = "CSV";
        break;
}

miCadeteria = accesoDatos.CargarCadeteria(@$"D:\Facultad\Taller de Lenguajes II\tl2-tp1-2024-Fatatouille\{carpeta}\Cadetes{extension}", $@"D:\Facultad\Taller de Lenguajes II\tl2-tp1-2024-Fatatouille\{carpeta}\Cadeteria{extension}", miCadeteria);

Console.WriteLine($"{miCadeteria.nombre}");

int opcion;
do
{
    Console.WriteLine("1. Dar de alta un pedido");
    Console.WriteLine("2. Asignar un pedido");
    Console.WriteLine("3. Cambiar de estado un pedido");
    Console.WriteLine("4. Reasginar el pedido a otro cadete");
    Console.WriteLine("5. Leer cadetes con pedidos");
    Console.WriteLine("6. Leer Todos los cadetes");
    Console.WriteLine("7. Salir"); // Cambié el texto de opción "4" a "5" para salir
    opcion = int.Parse(Console.ReadLine());


switch (opcion)
{
    case 1:
        var pedidoCargado = miCadeteria.AltaPedido();
        miCadeteria.ListaPedido.Add(pedidoCargado);

        
        break;
    case 2:

            Console.WriteLine("Ingresar el id del pedido a asignar");
            int idPedidoRequerido1 = int.Parse(Console.ReadLine());
            Console.WriteLine("Ingresar el id del Cadete a asignar el pedido");
            int idCadeteRequerido = int.Parse(Console.ReadLine());
            miCadeteria.AsignarPedido(idCadeteRequerido, idPedidoRequerido1);

        break;
    case 3:
        Console.WriteLine("Ingresar el numero del pedido a  buscar");
        int nPed = int.Parse(Console.ReadLine());

        Pedidos pedidoEncontrado = null; // Inicializo la variable

        // Uso de LINQ fuera del bucle
        pedidoEncontrado = miCadeteria.ListaPedido.FirstOrDefault(c => c.Nro == nPed);
        if (pedidoEncontrado != null) // Verifica si se encontró el pedido antes de cambiar su estado
        {
            if (pedidoEncontrado.Estado == Estados.Entregado)
            {
                pedidoEncontrado.Estado = Estados.EnCamino;
                
            }
            else
            {
                pedidoEncontrado.Estado = Estados.Entregado;
                
            }
        }
        else
        {
            Console.WriteLine("Pedido no encontrado.");
        }
        break;
    case 4:
        // Uso de LINQ fuera del bucle
        Console.WriteLine("Ingresar el id del pedido a asignar");
        int idPedidoRequerido = int.Parse(Console.ReadLine());
        Pedidos pedidoACambiar = miCadeteria.ListaPedido.FirstOrDefault(x => x.Nro == idPedidoRequerido);
        miCadeteria.ReasignarCadete(pedidoACambiar);

    break;
    case 5:
        


        foreach (var pedido in miCadeteria.ListaPedido)
        {
            Console.WriteLine("Informacion de Cadete\n");
            Console.WriteLine("ID: " + pedido.cadete.id);
            Console.WriteLine("Nombre: " + pedido.cadete.nombre);
            Console.WriteLine("Domicilio: " + pedido.cadete.direccion);
            Console.WriteLine("Telefono: " + pedido.cadete.telefono);
            Console.WriteLine("Informacion del Pedido\n");
            Console.WriteLine("Pedido Nro: " + pedido.Nro);
            Console.WriteLine("Observacion del Pedido: " + pedido.Obs);
            Console.WriteLine("Informacion Cliente \n");
            Console.WriteLine("Nombre: " + pedido.Cliente.nombre);
            Console.WriteLine("Direccion: " + pedido.Cliente.direccion);
            Console.WriteLine("Telefono: " + pedido.Cliente.telefono);
            Console.WriteLine("Alguna referencia para ubicar al cadete: " + pedido.Cliente.datosReferenciaDireccion);
            Console.WriteLine("\nEstado del Pedido: " + pedido.Estado);
        }


        
    break;
    case 6:
        foreach (var x in miCadeteria.listadoCadetes)
        {
            Console.WriteLine("Informacion de Cadete\n");
            Console.WriteLine("ID: " + x.id);
            Console.WriteLine("Nombre: " + x.nombre);
            Console.WriteLine("Domicilio: " + x.direccion);
            Console.WriteLine("Telefono: " + x.telefono);
        }
    break;
    default:
        Console.WriteLine("Opcion no valida");
        break;
}

} while (opcion != 7); 