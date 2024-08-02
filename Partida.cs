namespace EspacioArchivos;


using System;
using System.Runtime.CompilerServices;
using System.Reflection.Metadata.Ecma335;

using System.Text.Json;
using EspacioPersonajes;


public static class Partida
{
    public static void CrearCarpetas()
    {
        string nombreCarpeta = "PartidasGuardadas/";
        if (!Directory.Exists(nombreCarpeta))
        {
            Directory.CreateDirectory(nombreCarpeta);
        }
    }

    public static void GuardarPartida(List<Personaje> lista, string nombrePartida)
    {
        // PartidasGuardadas/nombrePartida
        string rutaFinal = "PartidasGuardadas/" + nombrePartida;
        if (!Directory.Exists(rutaFinal))
        {
            Directory.CreateDirectory(rutaFinal);
        }

        // PartidasGuardadas/nombrePartida/personajes.json
        string rutaPersonajes = rutaFinal + "/personajes.json";
        string persSerializados = JsonSerializer.Serialize(lista);

        ClaseJson.GuardarEnArchivoNuevo(persSerializados, rutaPersonajes);
    }

    public static string ObtenerNombresDePartidas()
    {
        string nombreCarpeta = "PartidasGuardadas/";
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
        //nomConcatenados = "vacio;"; // para pruebas
        return nomConcatenados;
    }

    public static List<Personaje> CargarPartida(string partidaElegida)
    {
        string datosDeArchivo;
        string rutaFinal = "PartidasGuardadas/" + partidaElegida + "/personajes.json";

        datosDeArchivo = ClaseJson.LeerArchivo(rutaFinal);
        List<Personaje> listaCargada = JsonSerializer.Deserialize<List<Personaje>>(datosDeArchivo);
        // parece que en C#, '= new List<>' sobra si acto seguido asignas una lista

        return listaCargada;
    }

    public static int ControlDeOpciones(string entrada, int tipoControl, int limDesconocido)
    {
        int valor, limiteSup = 0;
        char letra;
        bool pruebaOpciones;

        if (tipoControl == 7)   // control de letras
        {
            pruebaOpciones = char.TryParse(entrada, out letra);
            if (pruebaOpciones == false)
            {
                valor = 99999;
            }
            else
            {
                letra = char.ToUpper(letra);
                if (letra == 'S')
                {
                    valor = 19;
                }
                else
                {
                    valor = 14;
                }
            }
        }
        else                    // control de numeros segun limite
        {
            switch (tipoControl)
            {
                case 1:             // continuar despues de perder y seleccion de carta
                    limiteSup = 3;
                    break;
                case 2:
                    limiteSup = 4;  // opciones de duelo y menu de guardado
                    break;
                case 3:             // opciones menu principal
                    limiteSup = 5;
                    break;
                case 4:             // seleccion de personaje
                    limiteSup = 6;
                    break;
                case 5:             // n° de partida a cargar
                    limiteSup = limDesconocido;
                    break;
            }
            pruebaOpciones = int.TryParse(entrada, out valor);
            if (pruebaOpciones == false || valor <= 0 || valor >= limiteSup) // no permite el 0, no necesitas cambiar el lim inferior
            {
                valor = 99999;
            }
        }
        return valor;
    }

    public static bool EjecutarOpcion(List<Personaje> lista, string nombrePartida, int opcion)
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