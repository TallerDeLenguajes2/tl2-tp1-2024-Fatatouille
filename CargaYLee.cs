using Cadeteria;
using Pedido;
using Clientes;
using Cadetes;

public class LecturaCsv
{
    public static Cadeterias TraerDatosDeCsv(string archivo1, string archivo2, Cadeterias miCadeteria)
    {
        // Leer datos de la cadetería desde el archivo CSV
        using (StreamReader archivo = new StreamReader(archivo2))
        {
            string separador = ",";
            string linea;
            while ((linea = archivo.ReadLine()) != null)
            {
                string[] fila = linea.Split(separador);
                miCadeteria.nombre = fila[0];
                miCadeteria.telefono = int.Parse(fila[1]);
            }
        }

        // Leer datos de los cadetes desde el archivo CSV
        using (StreamReader archivo = new StreamReader(archivo1))
        {
            string separador = ",";
            string linea;
            while ((linea = archivo.ReadLine()) != null)
            {
                string[] fila = linea.Split(separador);

                // Crear instancia de Cliente
                Cliente cliente = new Cliente(fila[6], fila[7], fila[8], fila[9]);

                // Crear instancia de Pedido con manejo seguro del estado
                Pedidos.estado estado;
                if (Enum.TryParse(fila[10], true, out estado))
                {
                    Pedidos pedido = new Pedidos(int.Parse(fila[4]), fila[5], cliente, estado);

                    // Crear o agregar pedido a la instancia de Cadete
                    int idCadete = int.Parse(fila[0]);
                    Cadete cadeteExistente = miCadeteria.listadoCadetes.FirstOrDefault(c => c.Id == idCadete);

                    if (cadeteExistente == null)
                    {
                        Cadete nuevoCadete = new Cadete(idCadete, fila[1], fila[2], fila[3]);
                        nuevoCadete.AgregarPedido(pedido);
                        miCadeteria.ListaCadete.Add(nuevoCadete);
                    }
                    else
                    {
                        cadeteExistente.AgregarPedido(pedido);
                    }
                }
                else
                {
                    Console.WriteLine($"Estado inválido en la línea: {linea}");
                }

            }
        }

        return miCadeteria;
    }

    public static void AgregarPedidoAlCSV(string rutaArchivo, Pedidos pedido)
{
    List<string> lineas = new List<string>();

    // Leer todas las líneas del archivo CSV
    using (StreamReader archivo = new StreamReader(rutaArchivo))
    {
        string linea;
        while ((linea = archivo.ReadLine()) != null)
        {
            lineas.Add(linea);
        }
    }

    // Buscar y modificar la línea correspondiente al pedido
    for (int i = 0; i < lineas.Count; i++)
    {
        string[] fila = lineas[i].Split(',');
        // Actualizar solo el estado del pedido en la línea
        fila[10] = pedido.Estado.ToString();
        lineas[i] = string.Join(",", fila);
        break; // Salir del bucle después de encontrar y modificar el pedido
    }

    // Sobrescribir el archivo CSV con las líneas modificadas
    using (StreamWriter archivo = new StreamWriter(rutaArchivo, false))
    {
        foreach (string linea in lineas)
        {
            archivo.WriteLine(linea);
        }
    }
}
  //Queda como ejemplo para cargar un csv

}