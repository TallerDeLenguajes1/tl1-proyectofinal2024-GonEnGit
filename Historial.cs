using System.Net;
using System.Security;
using System.Text.Json;

using EspacioJson;
namespace EspacioHistorial;

public class Historial
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

    public void AgregarAlHistorial(string nombre)
    {
        string respuesta;
        string datosHistorial;
        string rutaFinal = "Historial/historial.json";
        ClaseJson HerramientaJson = new ClaseJson();
        List<Historial> lista = new List<Historial>();

        if (!Directory.Exists("Historial/"))
        {
            Directory.CreateDirectory("Historial/");
        }

        Historial entrada = new Historial(nombre);

        if (File.Exists(rutaFinal)) // si existe un historial, lees, añadis y guardas
        {
            datosHistorial = HerramientaJson.LeerArchivo(rutaFinal);
            lista = JsonSerializer.Deserialize<List<Historial>>(datosHistorial);
            lista.Add(entrada);
            datosHistorial = JsonSerializer.Serialize(lista);
            respuesta = HerramientaJson.GuardarEnArchivo(datosHistorial, rutaFinal);
            Console.WriteLine("El historial" + respuesta);
        }
        else    // si no, lo creas añadis y guardas
        {
            lista.Add(entrada);
            datosHistorial = JsonSerializer.Serialize(lista);
            HerramientaJson.GuardarEnArchivoNuevo(datosHistorial, rutaFinal);
        }
    }
}