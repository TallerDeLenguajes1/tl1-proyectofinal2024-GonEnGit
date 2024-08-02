namespace EspacioArchivos;


using System.Net;
using System.Security;


using System.Text.Json;


public class Historial  // no puede ser estatica por los campos de Historial
{
    private string nombreGanador;
    private DateTime fechaVictoria;

    public string NombreGanador { get => nombreGanador; set => nombreGanador = value; }
    public DateTime FechaVictoria { get => fechaVictoria; set => fechaVictoria = value; }

    public Historial(){}

    public Historial(string nombre){
        NombreGanador = nombre;
        FechaVictoria = DateTime.Now;
    }

    public static void AgregarAlHistorial(string nombre)
    {
        string rutaHistorial = "Historial/historial.json";
        string respuesta;
        string datosHistorial;
        List<Historial> lista = new List<Historial>();

        if (!Directory.Exists("Historial/"))
        {
            Directory.CreateDirectory("Historial/");
        }

        Historial entrada = new Historial(nombre);

        if (File.Exists(rutaHistorial)) // si existe un historial, lees, añadis y guardas
        {
            datosHistorial = ClaseJson.LeerArchivo(rutaHistorial);
            lista = JsonSerializer.Deserialize<List<Historial>>(datosHistorial);
            lista.Add(entrada);
            datosHistorial = JsonSerializer.Serialize(lista);
            respuesta = ClaseJson.GuardarEnArchivo(datosHistorial, rutaHistorial);
            Console.WriteLine("El historial" + respuesta);
        }
        else    // si no, lo creas añadis y guardas
        {
            lista.Add(entrada);
            datosHistorial = JsonSerializer.Serialize(lista);
            ClaseJson.GuardarEnArchivoNuevo(datosHistorial, rutaHistorial);
        }
    }

    public static List<Historial> LeerHistorial()
    {
        string rutaHistorial = "Historial/historial.json";
        string datosHistorial;
        List<Historial> historialCargado = new List<Historial>(); // ojo, esta lista no esta apuntada a null, solo está vacia

        if (File.Exists(rutaHistorial))
        {
            datosHistorial = ClaseJson.LeerArchivo(rutaHistorial);
            historialCargado = JsonSerializer.Deserialize<List<Historial>>(datosHistorial);
        }

        return historialCargado;
    }
}