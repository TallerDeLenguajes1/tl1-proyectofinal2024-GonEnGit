
using System.Runtime.CompilerServices;

namespace  EspacioPersonajes;

public class Personaje
{
    private bool vencido = false;
    private Datos generales = new Datos();
    private Caracteristicas estadisticas = new Caracteristicas();

    public bool Vencido { get => vencido; set => vencido = value; }
    public Datos DatosGenerales { get => generales; set => generales = value; }
    public Caracteristicas Estadisticas { get => estadisticas; set => estadisticas = value; }

    public string DevolverDatos()
    {
        return  $"{DatosGenerales.Id};{DatosGenerales.Nombre};{DatosGenerales.Apodo};{DatosGenerales.Clase};" +
                $"{DatosGenerales.Raza};{DatosGenerales.Nacimiento};{DatosGenerales.Edad}";
    }

    public string DevolverEstadisticas()
    {
        return  $"{Estadisticas.Salud};{Estadisticas.Armadura};{Estadisticas.Fuerza};" +
                $"{Estadisticas.Destreza};{Estadisticas.Velocidad}";
    }
}
