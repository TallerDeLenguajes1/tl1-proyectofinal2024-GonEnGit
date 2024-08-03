namespace  EspacioPersonajes;


using EspacioJuego;


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
        Estadisticas.Salud -= danio;

        if (Estadisticas.Salud <= 0) // para evitar Salud negativa y romper el bucle
        {
            Estadisticas.Salud = 0;
        }
    }

    public void RecuperarSalud(int maximo)
    {
        estadisticas.Salud += 10;
        if (estadisticas.Salud > maximo) // si no, alguien va a tener salud infinita
        {
            estadisticas.Salud = maximo;
        }
    }

    public void SubirDeNivel()
    {
        if (estadisticas.Fuerza != 999) // GodMode pone todo en 999, cualquiera sirve
        {
            if (generales.Id <= 3)      //jugador, y enemigos 1 y 2
            {
                SumarStatsSegunClase(1);
            }
            else if(generales.Id <= 6)
            {
                if (generales.Id == 4)  // tercer enemigo, cuarto personaje
                {
                    SumarStatsSegunClase(3);
                }
                else
                {
                    SumarStatsSegunClase(5);
                }
            }
            else if (generales.Id <= 9)
            {
                if (generales.Id == 9)  // octavo enemigo, noveno personaje
                {
                    SumarStatsSegunClase(9);
                }
                else
                {
                    SumarStatsSegunClase(7);
                }
            }
            else
            {
                SumarStatsSegunClase(11);
            }
        }
    }

    public void SumarStatsSegunClase(int multiplicador)
    {
        switch (generales.Clase)
        {
            case "Guerrero":
                estadisticas.Nivel += multiplicador;
                estadisticas.Salud = 100 + (25 * multiplicador);// el jugador tiene que recupara salud, podes usar esto
                estadisticas.Armadura += 2 * multiplicador;
                estadisticas.Fuerza += 2 * multiplicador;
                estadisticas.Destreza += 2 * multiplicador;
                break;
            case "Monje":
                estadisticas.Nivel += multiplicador;
                estadisticas.Salud = 100 + (25 * multiplicador);
                estadisticas.Armadura += 1 * multiplicador;
                estadisticas.Fuerza += 1 * multiplicador;
                estadisticas.Destreza += 2 * multiplicador;
                break;
            case "Paladín":
                estadisticas.Nivel += multiplicador;
                estadisticas.Salud = 100 + (25 * multiplicador);
                estadisticas.Armadura += 2 * multiplicador;
                estadisticas.Fuerza += 1 * multiplicador;
                estadisticas.Destreza += 1 * multiplicador;
                break;
            case "Berserker":
                estadisticas.Nivel += multiplicador;
                estadisticas.Salud = 150 + (25 * multiplicador);
                estadisticas.Armadura += 1 * multiplicador;
                estadisticas.Fuerza += 2 * multiplicador;
                estadisticas.Destreza += 1 * multiplicador;
                break;
            case "Gladiador":
                estadisticas.Nivel += multiplicador;
                estadisticas.Salud = 125 + (25 * multiplicador);
                estadisticas.Armadura += 1 * multiplicador;
                estadisticas.Fuerza += 1 * multiplicador;
                estadisticas.Destreza += 2 * multiplicador;
                break;
        }
    }

// así la podrias usar con enemigos tambien
// pero no hay nesecidad de traer un personaje tambien
    public void MejorasPorItem(Artefacto artefacto)
    {
        switch (artefacto.Efecto)
        {
            case "Salud":
                estadisticas.Salud += artefacto.Cantidad;
                break;
            case "Armadura":
                estadisticas.Armadura += artefacto.Cantidad;
                break;
            case "Fuerza":
                estadisticas.Fuerza += artefacto.Cantidad;
                break;
            case "Destreza":
                estadisticas.Destreza += artefacto.Cantidad;
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
