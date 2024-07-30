namespace  EspacioPersonajes;


using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;


public class Personaje
{
    private Datos generales = new Datos();
    private Estadisticas estadisticas = new Estadisticas();

    public Datos DatosGenerales { get => generales; set => generales = value; }
    public Estadisticas Estadisticas { get => estadisticas; set => estadisticas = value; }

    public string DevolverDatos()       // podrias sacar estos dos metodos de acá?
    {
        return  $"{DatosGenerales.Id};{DatosGenerales.Nombre};{DatosGenerales.Apodo};{DatosGenerales.Clase};" +
                $"{DatosGenerales.Raza};{DatosGenerales.Nacimiento};{DatosGenerales.Edad}";
    }

    public string DevolverEstadisticas()
    {
        return  $"{Estadisticas.Salud};{Estadisticas.Armadura};{Estadisticas.Fuerza};{Estadisticas.Destreza}";
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

    public void subirDeNivel()
    {
        if (estadisticas.Fuerza != 999) // GodMode pone todo en 999, cualquiera sirve
        {
            if (generales.Id <= 3)      //jugador, y enemigos 1 y 2
            {
                sumarStatsSegunClase(1);
            }
            else if(generales.Id <= 6)
            {
                if (generales.Id == 4)  // tercer enemigo, cuarto personaje
                {
                    sumarStatsSegunClase(3);
                }
                else
                {
                    sumarStatsSegunClase(5);
                }
            }
            else if (generales.Id <= 9)
            {
                if (generales.Id == 9)  // octavo enemigo, noveno personaje
                {
                    sumarStatsSegunClase(9);
                }
                else
                {
                    sumarStatsSegunClase(7);
                }
            }
            else
            {
                sumarStatsSegunClase(11);
            }
        }
    }

    public void sumarStatsSegunClase(int multiplicador)
    {
        switch (generales.Clase)
        {
            case "Guerrero":
                estadisticas.Nivel += multiplicador;
                estadisticas.Salud = 100 + (25 * estadisticas.Nivel);// el jugador tiene que recupara salud, podes usar esto
                estadisticas.Armadura += 2 * multiplicador;
                estadisticas.Fuerza += 1 * multiplicador;
                estadisticas.Destreza += 1 * multiplicador;
                break;
            case "Monje":
                estadisticas.Nivel += multiplicador;
                estadisticas.Salud = 100 + (25 * estadisticas.Nivel);
                estadisticas.Armadura += 1 * multiplicador;
                estadisticas.Fuerza += 2 * multiplicador;
                estadisticas.Destreza += 1 * multiplicador;
                break;
            case "Arquero":
                estadisticas.Nivel += multiplicador;
                estadisticas.Salud = 100 + (25 * estadisticas.Nivel);
                estadisticas.Armadura += 1 * multiplicador;
                estadisticas.Fuerza += 1 * multiplicador;
                estadisticas.Destreza += 2 * multiplicador;
                break;
            case "Luchador":
                estadisticas.Nivel += multiplicador;
                estadisticas.Salud = 125 + (25 * estadisticas.Nivel);
                estadisticas.Armadura += 1 * multiplicador;
                estadisticas.Fuerza += 2 * multiplicador;
                estadisticas.Destreza += 1 * multiplicador;
                break;
            case "Ladrón":
                estadisticas.Nivel += multiplicador;
                estadisticas.Salud = 100 + (25 * estadisticas.Nivel);
                estadisticas.Armadura += 1 * multiplicador;
                estadisticas.Fuerza += 2 * multiplicador;
                estadisticas.Destreza += 2 * multiplicador;
                break;
        }
    }

    public void GodMode()
    {
        estadisticas.Salud = 999;
        estadisticas.Armadura = 999;
        estadisticas.Fuerza = 999;
        estadisticas.Destreza = 999;
    }
}
