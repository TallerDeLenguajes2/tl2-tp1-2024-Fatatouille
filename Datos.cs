namespace datos;
using Cadeteria;
using Cadetes;
using System.Text.Json;

public abstract class AccesoADatos{
    public abstract Cadeterias CargarCadeteria(string archivo1, string archivo2, Cadeterias miCadeteria);

}

public class AccesoCSV : AccesoADatos
{
    public override Cadeterias CargarCadeteria(string archivo1, string archivo2, Cadeterias miCadeteria)
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

                // Crear o agregar pedido a la instancia de Cadete
                int idCadete = int.Parse(fila[0]);
                Cadete cadeteExistente = miCadeteria.listadoCadetes.FirstOrDefault(c => c.id == idCadete);
                Cadete nuevoCadete = new Cadete(idCadete, fila[1], fila[2], Convert.ToInt32(fila[3]));
                miCadeteria.listadoCadetes.Add(nuevoCadete);
            }
        }

        return miCadeteria;
}
}


//para el json

public class AccesoJSON : AccesoADatos
{
    public override Cadeterias CargarCadeteria(string archivo1, string archivo2, Cadeterias miCadeteria)
    {

        try
        {
            string contenidoCadeteriaJson = File.ReadAllText(archivo2);
            miCadeteria = JsonSerializer.Deserialize<Cadeterias>(contenidoCadeteriaJson) ?? miCadeteria;

            string contenidoCadetesJson = File.ReadAllText(archivo1);
            List<Cadete> cadetes = JsonSerializer.Deserialize<List<Cadete>>(contenidoCadetesJson) ?? new List<Cadete>();

            // Asigno los cadetes a la cadetería
            miCadeteria.listadoCadetes = cadetes;
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Error al leer o deserializar los archivos JSON: {ex.Message}");
        }

        return miCadeteria;
    }
}