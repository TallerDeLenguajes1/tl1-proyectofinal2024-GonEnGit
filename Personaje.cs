namespace  EspacioPersonajes;


using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;


public class Personaje
{
    private Datos generales = new Datos();
    private Caracteristicas estadisticas = new Caracteristicas();

    public Datos DatosGenerales { get => generales; set => generales = value; }
    public Caracteristicas Estadisticas { get => estadisticas; set => estadisticas = value; }

    public string DevolverDatos()       // podrias sacar estos dos metodos de acá?
    {
        return  $"{DatosGenerales.Id};{DatosGenerales.Nombre};{DatosGenerales.Apodo};{DatosGenerales.Clase};" +
                $"{DatosGenerales.Raza};{DatosGenerales.Nacimiento};{DatosGenerales.Edad}";
    }

    public string DevolverEstadisticas()
    {
        return  $"{Estadisticas.Salud};{Estadisticas.Armadura};{Estadisticas.Fuerza};" +
                $"{Estadisticas.Destreza};{Estadisticas.Velocidad}";
    }

    public void RecibirDanio(int danio)
    {
        // este if es por el GodMode
        if (danio <= 0)
        {
            danio = 0;
        }

        Estadisticas.Salud -= danio;

        // para evitar Salud negativa y romper el bucle
        if (Estadisticas.Salud <= 0)
        {
            Estadisticas.Salud = 0;
        }
    }

    public void GodMode()
    {
        estadisticas.Salud = 9999;
        estadisticas.Armadura = 9999;
        estadisticas.Fuerza = 9999;
        estadisticas.Destreza = 9999;
        estadisticas.Velocidad = 9999;
    }
}
