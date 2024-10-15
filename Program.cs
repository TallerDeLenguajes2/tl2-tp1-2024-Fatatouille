﻿using Cadeteria;
using Cadetes;
using Pedido;

Cadeterias miCadeteria = new Cadeterias();
List<Pedidos> pedidosSinAsignar = new List<Pedidos>();
List<Pedidos> pedidosAsignados = new List<Pedidos>();

miCadeteria = LecturaCsv.TraerDatosDeCsv(@"D:\Facultad\Taller de Lenguajes II\tl2-tp1-2024-Fatatouille\CSV\Cadetes.csv", @"D:\Facultad\Taller de Lenguajes II\tl2-tp1-2024-Fatatouille\CSV\Cadeteria.csv", miCadeteria);

Console.WriteLine($"{miCadeteria.nombre}");

foreach (var x in miCadeteria.listadoCadetes)
{
    Console.WriteLine("Informacion de Cadete\n");
    Console.WriteLine("ID: " + x.id);
    Console.WriteLine("Nombre: " + x.nombre);
    Console.WriteLine("Domicilio: " + x.direccion);
    Console.WriteLine("Telefono: " + x.telefono);
    foreach (var y in x.listadoPedidos)
    {
        Console.WriteLine("Informacion del Pedido\n");
        Console.WriteLine("Pedido Nro: " + y.Nro);
        Console.WriteLine("Observacion del Pedido: " + y.Obs);
        Console.WriteLine("Informacion Cliente \n");
        Console.WriteLine("Nombre: " + y.Cliente.nombre);
        Console.WriteLine("Direccion: " + y.Cliente.direccion);
        Console.WriteLine("Telefono: " + y.Cliente.telefono);
        Console.WriteLine("Alguna referencia para ubicar al cadete: " + y.Cliente.datosReferenciaDireccion);
        Console.WriteLine("\nEstado del Pedido: " + y.Estado);
        Console.WriteLine("");
        pedidosAsignados.Add(y);
    }
}

int opcion;
do
{
    Console.WriteLine("1. Dar de alta un pedido");
    Console.WriteLine("2. Asignar un pedido");
    Console.WriteLine("3. Cambiar de estado un pedido");
    Console.WriteLine("4. Reasginar el pedido a otro cadete");
    Console.WriteLine("5. Salir"); // Cambié el texto de opción "4" a "5" para salir
    opcion = int.Parse(Console.ReadLine());

} while (opcion < 1 || opcion > 5); // Cambié a 1 para que las opciones sean válidas desde 1 a 5

switch (opcion)
{
    case 1:
        Pedidos pedidoCargado = miCadeteria.AltaPedido();
        pedidosSinAsignar.Add(pedidoCargado);
        break;
    case 2:
        if (pedidosSinAsignar.Count > 0) // Cambié null check a Count check para verificar si hay elementos
        {
            miCadeteria.AsignarPedido(pedidosSinAsignar[0]);
            pedidosSinAsignar.RemoveAt(0);
            Console.WriteLine("Pedido asignado con exito");
        }
        else
        {
            Console.WriteLine("Sin pedidos para asignar");
        }
        break;
    case 3:
        Console.WriteLine("Ingresar el numero del pedido a  buscar (42, 43, 44)");
        int nPed = int.Parse(Console.ReadLine());

        Pedidos pedidoEncontrado = null; // Inicializo la variable

        // Uso de LINQ fuera del bucle
        pedidoEncontrado = pedidosAsignados.FirstOrDefault(c => c.Nro == nPed);
        if (pedidoEncontrado != null) // Verifica si se encontró el pedido antes de cambiar su estado
        {
            if (pedidoEncontrado.Estado == Estados.Entregado)
            {
                pedidoEncontrado.Estado = Estados.EnCamino;
                LecturaCsv.AgregarPedidoAlCSV(@"D:\Facultad\Taller de Lenguajes II\tl2-tp1-2024-Fatatouille\CSV\Cadetes.csv", pedidoEncontrado);
            }
            else
            {
                pedidoEncontrado.Estado = Estados.Entregado;
                LecturaCsv.AgregarPedidoAlCSV(@"D:\Facultad\Taller de Lenguajes II\tl2-tp1-2024-Fatatouille\CSV\Cadetes.csv", pedidoEncontrado);
            }
        }
        else
        {
            Console.WriteLine("Pedido no encontrado.");
        }
        break;
    case 4:

        Console.WriteLine("Ingresar numero de pedido a cambiar de cadete");
        int nPedN = int.Parse(Console.ReadLine());

        Console.WriteLine("Ingresar ID del cadete a entregar pedido");
        int ID = int.Parse(Console.ReadLine());

        // Uso de LINQ fuera del bucle
        Pedidos pedidoEncontradoCambiar = pedidosAsignados.FirstOrDefault(c => c.Nro == nPedN);
        Cadete cadeteEncontrado = miCadeteria.listadoCadetes.FirstOrDefault(x => x.id == ID);

        miCadeteria.ReasignarCadete(pedidoEncontradoCambiar, cadeteEncontrado);

    break;
}