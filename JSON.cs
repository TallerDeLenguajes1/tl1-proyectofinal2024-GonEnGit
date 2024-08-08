// https://jsonviewer.stack.hu/
// https://json2csharp.com/


namespace EspacioArchivos;


using System.Diagnostics.Contracts;


public static class ClaseJson
{
    public static string LeerArchivo(string rutaArchivo)
    {
        string datosDeArchivo;

        using (FileStream archivo = new FileStream(rutaArchivo, FileMode.Open))
        {
            using (StreamReader lector = new StreamReader(archivo))
            {
                datosDeArchivo = lector.ReadToEnd();
                lector.Close();
            }
        }
        return datosDeArchivo;
    }

    public static void GuardarEnArchivoNuevo(string datos, string rutaFinal)
    {
        using (FileStream archivo = new FileStream(rutaFinal, FileMode.Create))
        {
            using (StreamWriter escritor = new StreamWriter(archivo))
            {
                escritor.WriteLine(datos);
                escritor.Close();
            }
        }
    }

    public static void GuardarEnArchivo(string datos, string rutaFinal)
    {
        using (FileStream archivo = new FileStream(rutaFinal, FileMode.Open))
        {
            using (StreamWriter escritor = new StreamWriter(archivo))
            {
                escritor.WriteLine(datos);
                escritor.Close();
            }
        }
    }
}