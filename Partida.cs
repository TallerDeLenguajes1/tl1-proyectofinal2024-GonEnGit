

using System;
using System.Runtime.CompilerServices;
using System.Text.Json;
using EspacioDuelos;
using EspacioPersonajes;

namespace EspacioPartida;

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
        string persSerialozados = JsonSerializer.Serialize(lista);

        using (FileStream archivo = new FileStream(rutaPersonajes, FileMode.Create))
        {
            using (StreamWriter esctritor = new StreamWriter(archivo))
            {
                esctritor.WriteLine(persSerialozados);
                esctritor.Close();
            }
        }
    }

    public string ObtenerNombresDePartidas(List<Personaje> lista)
    {
        string nomConcatenados = "anulado;";    // para anular el espacio 0
        if (Directory.Exists(nombreCarpeta))
        {
            string[] partes;
            string[] archivos = Directory.GetDirectories(nombreCarpeta);
            if (archivos.Length == 0)
            {
                return "";
            }
            else
            {
                foreach (string nombre in archivos)
                {
                    partes = nombre.Split("/");
                    nomConcatenados += partes[partes.Length - 1] + ";";
                }
                return nomConcatenados;
            }
        }
        else
        {
            return "";
        }
    }

    public List<Personaje> CargarPartida(string partidaElegida)
    {
        string datosDeArchivo;
        string rutaFinal = nombreCarpeta + partidaElegida + "/personajes.json";
        List<Personaje> listaCargada = new List<Personaje>();

        using (FileStream archivo = new FileStream(rutaFinal, FileMode.Open))
        {
            using (StreamReader lector = new StreamReader(archivo))
            {
                datosDeArchivo = lector.ReadToEnd();
                listaCargada = JsonSerializer.Deserialize<List<Personaje>>(datosDeArchivo);
                archivo.Close();
            }
        }

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