namespace EspacioPartida;


using System;
using System.Runtime.CompilerServices;
using System.Reflection.Metadata.Ecma335;

using System.Text.Json;
using EspacioJson;
using EspacioPersonajes;


public class Partida
{
    private string nombreCarpeta = "PartidasGuardadas/";
    private ClaseJson HerramientaJson = new ClaseJson();

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

        HerramientaJson.GuardarEnArchivoNuevo(persSerializados, rutaPersonajes);
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
        ClaseJson HerramientaJson = new ClaseJson();
        
        datosDeArchivo = HerramientaJson.LeerArchivo(rutaFinal);
        List<Personaje> listaCargada = JsonSerializer.Deserialize<List<Personaje>>(datosDeArchivo);
        // parece que en C#, '= new List<>' sobra si acto seguido asignas una lista

        return listaCargada;
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