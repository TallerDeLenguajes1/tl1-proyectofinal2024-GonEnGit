
using System.Runtime.CompilerServices;

namespace  EspacioPersonajes;

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

    public void RecibirDanio(Personaje pers, int danio)
    {
        pers.estadisticas.Salud -= danio;
    }
}
