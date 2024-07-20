
using System.Data.Common;
using System.Runtime.CompilerServices;

using EspacioPersonajes;

namespace EspacioPartida;

public class Partida
{
    private DateTime fecha = DateTime.Now; // .ToString("dd-MM-yy_hh-mm");
    private string nombreCarpeta = "PartidasGuardadas/";

    public void GuardarPartida(List<Personaje> lista, string nombrePartida)
    {
        // PartidasGuardadas/nombrePartida/
        string nombreSubCarpeta = nombreCarpeta + nombrePartida + "/";

        // nombrePartida_dd-MM-yy_hh-mm
        string nombreArchivo = nombrePartida + "_" + fecha.ToString("dd-MM-yy_hh-mm");

        // PartidasGuardadas/nombrePartida/nombrePartida_dd-MM-yy_hh-mm.json
        string rutaFinal = nombreSubCarpeta + nombreArchivo + ".json";

        if (!Directory.Exists(nombreSubCarpeta))
        {
            Directory.CreateDirectory(nombreSubCarpeta);
        }

        File.Create(rutaFinal).Close(); // esto no va realmente, tenes que hacer bien el guardado
    }

    public void CargarPartida(List<Personaje> lista)
    {

    }

    public bool EjecutarOpcion(List<Personaje> lista, string nombrePartida, int opcion)
    {
        if (opcion == 1)
        {
            GuardarPartida(lista, nombrePartida);
            return false;
        }
        else
        {
            if (opcion == 2)
            {
                GuardarPartida(lista, nombrePartida);
            }
            return true;
        }
    }

    // un borrar partida? tambien podes, varias partidas?
}