namespace EspacioArchivos;


using System;
using System.Runtime.CompilerServices;
using System.Reflection.Metadata.Ecma335;

using System.Text.Json;
using EspacioPersonajes;
using EspacioJuego;

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

    public static int ControlDeOpciones(string entrada, int tipoControl, int limDesconocido)
    {
        char letra;
        bool pruebaOpciones;
        int valor, limiteSup = 0;

        if (tipoControl == 7)   // control de letras
        {
            pruebaOpciones = char.TryParse(entrada, out letra);
            letra = char.ToUpper(letra);
            if (pruebaOpciones == false || letra != 'S' && letra != 'N')
            {
                valor = 99999;
            }
            else
            {
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
                case 2:             // opciones de duelo, menu de guardado y control de artefactos
                    limiteSup = 4;
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
            if (pruebaOpciones == false || valor <= 0 || valor >= limiteSup)
            {
                valor = 99999;
            }
        }
        return valor;
    }

    public static bool EjecutarOpcion(List<Personaje> lista, List<Artefacto> cofre, List<Artefacto> inv, string nombre, int opcion)
    {
        if (opcion == 1 || opcion == 3)
        {
            if (opcion == 1)
            {
                GuardarPartida(lista, cofre, inv, nombre);
            }
            return false;
        }
        else
        {
            if (opcion == 2)
            {
                GuardarPartida(lista, cofre, inv, nombre);
            }
            return true;
        }
    }

    public static void GuardarPartida(List<Personaje> lista, List<Artefacto> cofre, List<Artefacto> inv, string nombrePartida)
    {
        // PartidasGuardadas/nombrePartida
        string rutaFinal = "PartidasGuardadas/" + nombrePartida;
        if (!Directory.Exists(rutaFinal))
        {
            Directory.CreateDirectory(rutaFinal);
        }

        // PartidasGuardadas/nombrePartida/archivo.json
        string rutaPersonajes = rutaFinal + "/personajes.json";
        string persSerializados = JsonSerializer.Serialize(lista);
        string rutaCofre = rutaFinal + "/cofre.json";
        string cofreSerializado = JsonSerializer.Serialize(cofre);
        string rutaInventario = rutaFinal + "/inventario.json";
        string invSerializado = JsonSerializer.Serialize(inv);

        ClaseJson.GuardarEnArchivoNuevo(persSerializados, rutaPersonajes);
        ClaseJson.GuardarEnArchivoNuevo(cofreSerializado, rutaCofre);
        ClaseJson.GuardarEnArchivoNuevo(invSerializado, rutaInventario);
    }

    public static List<T> CargarPartida<T>(string partidaElegida, string tipoDeLista)
    {
        string datosDeArchivo, rutaFinal = $"PartidasGuardadas/{partidaElegida}/{tipoDeLista}.json";

        datosDeArchivo = ClaseJson.LeerArchivo(rutaFinal);
        List<T> listaCargada = JsonSerializer.Deserialize<List<T>>(datosDeArchivo);
        // parece que en C#, '= new List<>' sobra si acto seguido asignas una lista

        return listaCargada;
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

    // un borrar partida? tambien podes
}