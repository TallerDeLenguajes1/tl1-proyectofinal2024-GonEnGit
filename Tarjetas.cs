
using EspacioPersonajes;

namespace EspacioTarjetas;

public class EspacioTarjetas()
{
    public string GenerarTarjeta(personaje aMostrar)
    {
        string frase = "Apodo: " + aMostrar.Generales.Apodo;
        int largo = frase.Length;
        return "esto por ahora";
    }
}
