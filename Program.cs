using Cadeteria;
using Pedido;
using Clientes;
using Cadetes;
using System.Linq;
using System.Collections.Generic;

namespace EspacioCadeteria
{
    class Program
    {
        static void Main(string[] args)
        {
            // Cargar los datos de la cadetería y los cadetes
            CargarCadeteria cargador = new CargarCadeteria();
            Cadeterias cadeteria = cargador.Cargar("csv/cadeteria.csv", "csv/cadete.csv");

            bool salir = false;

            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("Sistema de Gestión de Pedidos - Cadetería");
                Console.WriteLine("Seleccione una opción:");
                Console.WriteLine("1. Dar de alta un pedido");
                Console.WriteLine("2. Asignar pedido a un cadete");
                Console.WriteLine("3. Cambiar estado de un pedido");
                Console.WriteLine("4. Reasignar pedido a otro cadete");
                Console.WriteLine("5. Mostrar informe al finalizar la jornada");
                Console.WriteLine("6. Salir");

                Console.Write("Opción: ");
                int opcion;
                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    Console.WriteLine("Por favor, ingrese una opción válida.");
                    Console.ReadLine();
                    continue;
                }

                switch (opcion)
                {
                    case 1:
                        DarDeAltaPedido(cadeteria);
                        break;

                    case 2:
                        AsignarPedidoACadete(cadeteria);
                        break;

                    case 3:
                        CambiarEstadoPedido(cadeteria);
                        break;

                    case 4:
                        ReasignarPedido(cadeteria);
                        break;

                    case 5:
                        MostrarInforme(cadeteria);
                        break;

                    case 6:
                        salir = true;
                        Console.WriteLine("Saliendo del sistema...");
                        break;

                    default:
                        Console.WriteLine("Opción no válida. Presione Enter para intentar nuevamente.");
                        Console.ReadLine();
                        break;
                }
            }
        }

        static void DarDeAltaPedido(Cadeteria cadeteria)
        {
            Console.WriteLine("Dar de alta un pedido");

            Console.Write("Ingrese el número del pedido: ");
            int nro;
            while (!int.TryParse(Console.ReadLine(), out nro))
            {
                Console.WriteLine("Número de pedido inválido. Intente nuevamente.");
            }

            Console.Write("Ingrese las observaciones del pedido: ");
            string obs = Console.ReadLine();

            Console.Write("Ingrese el nombre del cliente: ");
            string nombreCliente = Console.ReadLine();

            Console.Write("Ingrese la dirección del cliente: ");
            string direccionCliente = Console.ReadLine();

            Console.Write("Ingrese el teléfono del cliente: ");
            string telefonoCliente = Console.ReadLine();

            Console.Write("Ingrese los datos de referencia de la dirección del cliente: ");
            string datosReferencia = Console.ReadLine();

            // Crear cliente y pedido
            Cliente cliente = new Cliente(nombreCliente, direccionCliente, telefonoCliente, datosReferencia);
            Pedidos pedido = new Pedidos(nro, obs, cliente, Estado.Pendiente);

            // Listar cadetes disponibles
            Console.WriteLine("Seleccione un cadete para asignarle el pedido:");
            for (int i = 0; i < cadeteria.ListadoCadetes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {cadeteria.ListadoCadetes[i].Nombre1} (ID: {cadeteria.ListadoCadetes[i].Id1})");
            }

            int seleccionCadete;
            while (!int.TryParse(Console.ReadLine(), out seleccionCadete) || seleccionCadete < 1 || seleccionCadete > cadeteria.ListadoCadetes.Count)
            {
                Console.WriteLine("Selección inválida. Intente nuevamente.");
            }

            Cadete cadeteSeleccionado = cadeteria.ListadoCadetes[seleccionCadete - 1];
            cadeteSeleccionado.AgregarPedido(pedido); // Asignar el pedido al cadete
            Console.WriteLine($"El pedido {nro} ha sido asignado a {cadeteSeleccionado.Nombre1}.");

            Console.WriteLine("Pedido creado exitosamente. Presione Enter para continuar.");
            Console.ReadLine();
        }


        static void AsignarPedidoACadete(Cadeteria cadeteria)
        {
            Console.Write("Ingrese el número del pedido para asignar: ");
            int nroPedidoAsignar = int.Parse(Console.ReadLine());
            Pedidos pedidoParaAsignar = cadeteria.ListadoCadetes.SelectMany(c => c.ListadoPedidos1).FirstOrDefault(p => p.Nro1 == nroPedidoAsignar);

            if (pedidoParaAsignar == null)
            {
                Console.WriteLine("Pedido no encontrado. Presione Enter para continuar.");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("Seleccione un cadete para asignar el pedido:");
            for (int i = 0; i < cadeteria.ListadoCadetes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {cadeteria.ListadoCadetes[i].Nombre1}");
            }

            int indiceCadeteAsignar = int.Parse(Console.ReadLine()) - 1;
            if (indiceCadeteAsignar >= 0 && indiceCadeteAsignar < cadeteria.ListadoCadetes.Count)
            {
                Cadete cadeteAsignado = cadeteria.ListadoCadetes[indiceCadeteAsignar];
                cadeteria.AsignarPedido(cadeteAsignado, pedidoParaAsignar);
                Console.WriteLine("Pedido asignado exitosamente. Presione Enter para continuar.");
            }
            else
            {
                Console.WriteLine("Selección de cadete inválida. Presione Enter para continuar.");
            }
            Console.ReadLine();
        }

        static void CambiarEstadoPedido(Cadeteria cadeteria)
        {
            Console.Write("Ingrese el número del pedido para cambiar su estado: ");
            int nroPedido = int.Parse(Console.ReadLine());

            Console.WriteLine("Seleccione el nuevo estado del pedido:");
            Console.WriteLine("1. Pendiente");
            Console.WriteLine("2. En proceso");
            Console.WriteLine("3. Entregado");

            int estadoSeleccionado = int.Parse(Console.ReadLine());
            Estado nuevoEstado;

            switch (estadoSeleccionado)
            {
                case 1:
                    nuevoEstado = Estado.Pendiente;
                    break;
                case 2:
                    nuevoEstado = Estado.Proceso;
                    break;
                case 3:
                    nuevoEstado = Estado.Completado;
                    break;
                default:
                    Console.WriteLine("Estado no válido. Operación cancelada.");
                    return;
            }

            // Se debe buscar el pedido en los cadetes
            var pedido = cadeteria.ListadoCadetes.SelectMany(c => c.ListadoPedidos1).FirstOrDefault(p => p.Nro1 == nroPedido);
            if (pedido != null)
            {
                // Se debe buscar el cadete correspondiente para cambiar el estado
                var cadete = cadeteria.ListadoCadetes.First(c => c.ListadoPedidos1.Contains(pedido));
                cadete.CambiarEstadoPedido(nroPedido, nuevoEstado);
                Console.WriteLine($"Estado del pedido {nroPedido} cambiado a {nuevoEstado}. Presione Enter para continuar.");
            }
            else
            {
                Console.WriteLine("Pedido no encontrado. Presione Enter para continuar.");
            }
            Console.ReadLine();
        }

        static void ReasignarPedido(Cadeteria cadeteria)
        {
            Console.Write("Ingrese el número del pedido para reasignar: ");
            int nroPedidoReasignar = int.Parse(Console.ReadLine());
            Pedidos pedidoParaReasignar = cadeteria.ListadoCadetes.SelectMany(c => c.ListadoPedidos1).FirstOrDefault(p => p.Nro1 == nroPedidoReasignar);

            if (pedidoParaReasignar == null)
            {
                Console.WriteLine("Pedido no encontrado. Presione Enter para continuar.");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("Seleccione el cadete actual:");
            for (int i = 0; i < cadeteria.ListadoCadetes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {cadeteria.ListadoCadetes[i].Nombre1}");
            }

            int indiceCadeteActual = int.Parse(Console.ReadLine()) - 1;
            Cadete cadeteActual = cadeteria.ListadoCadetes[indiceCadeteActual];

            Console.WriteLine("Seleccione el nuevo cadete:");
            for (int i = 0; i < cadeteria.ListadoCadetes.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {cadeteria.ListadoCadetes[i].Nombre1}");
            }

            int indiceNuevoCadete = int.Parse(Console.ReadLine()) - 1;
            Cadete nuevoCadete = cadeteria.ListadoCadetes[indiceNuevoCadete];

            if (cadeteActual != null && nuevoCadete != null)
            {
                cadeteria.ReasignarPedido(cadeteActual, nuevoCadete, pedidoParaReasignar);
                Console.WriteLine("Pedido reasignado exitosamente. Presione Enter para continuar.");
            }
            else
            {
                Console.WriteLine("Cadete no válido. Presione Enter para continuar.");
            }
            Console.ReadLine();
        }

        static void MostrarInforme(Cadeteria cadeteria)
        {
            Console.WriteLine("Informe de Pedidos:");
            foreach (var cadete in cadeteria.ListadoCadetes)
            {
                Console.WriteLine($"Cadete: {cadete.Nombre1}");
                Console.WriteLine($"Jornal a cobrar: {cadete.JornalACobrar()}");
                Console.WriteLine("Pedidos asignados:");
                foreach (var pedido in cadete.ListadoPedidos1)
                {
                    Console.WriteLine($"- Pedido N° {pedido.Nro1}, Estado: {pedido.Estado}");
                }
                Console.WriteLine();
            }
            Console.WriteLine("Presione Enter para continuar.");
            Console.ReadLine();
        }
    }
}