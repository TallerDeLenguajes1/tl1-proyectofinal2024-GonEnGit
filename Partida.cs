namespace EspacioArchivos;


using System;
using System.Runtime.CompilerServices;
using System.Reflection.Metadata.Ecma335;

using System.Text.Json;
using EspacioPersonajes;


public class Partida
{
    private string nombreCarpeta = "PartidasGuardadas/";

    public void CrearCarpetas()
    {
        string rutaFinal = nombreCarpeta;
        if (!Directory.Exists(rutaFinal))
        {
            Directory.CreateDirectory(rutaFinal);
        }
    }

    public void GuardarPartida(List<Personaje> lista, string nombrePartida)
    {
        // PartidasGuardadas/nombrePartida
        string rutaFinal = nombreCarpeta + nombrePartida;
        if (!Directory.Exists(rutaFinal))
        {
            Directory.CreateDirectory(rutaFinal);
        }

        // PartidasGuardadas/nombrePartida/personajes.json
        string rutaPersonajes = rutaFinal + "/personajes.json";
        string persSerializados = JsonSerializer.Serialize(lista);

        ClaseJson.GuardarEnArchivoNuevo(persSerializados, rutaPersonajes);
    }

    public string ObtenerNombresDePartidas()
    {
        string nomConcatenados = "vacio;";    // para anular el espacio 0
        if (Directory.Exists(nombreCarpeta))
        {
            string[] partes;
            string[] archivos = Directory.GetDirectories(nombreCarpeta);
            if (archivos.Length != 0)
            {
                foreach (string ruta in archivos)
                {
                    partes = ruta.Split("/");
                    nomConcatenados += partes[partes.Length - 1] + ";";
                }
            }
        }
        return nomConcatenados;
    }

    public List<Personaje> CargarPartida(string partidaElegida)
    {
        string datosDeArchivo;
        string rutaFinal = nombreCarpeta + partidaElegida + "/personajes.json";

        datosDeArchivo = ClaseJson.LeerArchivo(rutaFinal);
        List<Personaje> listaCargada = JsonSerializer.Deserialize<List<Personaje>>(datosDeArchivo);
        // parece que en C#, '= new List<>' sobra si acto seguido asignas una lista

        return listaCargada;
    }

    public static int ControlDeOpciones(string entrada, int tipoControl)
    {
        bool pruebaOpciones = false;
        int valor = 0, limiteSup = 0;

        switch (tipoControl)
        {
            case 1:
                limiteSup = 5;
                break;
            case 2:
                limiteSup = 6;
                break;
            case 3:
                limiteSup = 2;
                break;
        }

        while (pruebaOpciones == false || valor <= 0 || valor >= limiteSup)
        {
            pruebaOpciones = int.TryParse(entrada, out valor);
            if (pruebaOpciones == false || valor <= 0 || valor >= 5)
            {
                valor = 99999;
            }
        }
        return valor;
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