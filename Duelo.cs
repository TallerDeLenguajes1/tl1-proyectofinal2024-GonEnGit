namespace EspacioDuelos;

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

using EspacioPersonajes;

public class Duelo
{
    private Random rnd = new Random();

    public int ContarEnemigosActivos(List<Personaje> lista)
    {
        int contador = 1;   // no en 0, tenes que tener en cuenta el jugador
        if (lista[0].Estadisticas.Salud == 0)
        {
            contador = -1;
        }
        else
        {
            foreach (Personaje instancia in lista)
            {
                if (instancia.Estadisticas.Salud == 0)
                {
                    contador += 1;
                }
            }
        }
        return contador;
    }

    public double CalcularIniciativa(Personaje pers)
    {
        return pers.Estadisticas.Velocidad * 0.5 + 2;   // esta ecuacion cambiala, es cualquier cosa
    }

    public int AccionEnemigo()
    {
        int numero = rnd.Next(1, 13);
        int accion;
        if (numero == 1 || numero == 11)
        {
            accion = 3;     // el enemigo Espera, 16,7%
        }
        else if (numero % 2 != 0)
        {
            accion = 2;     // el enemigo Defiende, 33,3%
        }
        else
        {
            accion = 1;     // el enemigo Ataca, 50%
        }

        return accion;
    }

    public int CalcularDefensa(Personaje pers)
    {
        return pers.Estadisticas.Armadura + (10 * (pers.Estadisticas.Destreza / 10));
    }

    public int CalcularAtaque(Personaje pers, bool critico)
    {
        if (critico)
        {
            return (int)((pers.Estadisticas.Fuerza * 2) + (pers.Estadisticas.Fuerza * 0.25));
        }
        else
        {
            return pers.Estadisticas.Fuerza * 2;
        }
    }

    public bool DecidirCritico(Personaje pers)
    {
        int numero = rnd.Next(0, 100);
        if (numero <= pers.Estadisticas.Destreza * 10)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // suplanta AtaqueEspera
    public int CalcDanioAtaqueAtaque(Personaje atacante, Personaje objetivo)
    {
        bool citricoAtacante = DecidirCritico(atacante);
        int ataqueAtacante = CalcularAtaque(atacante, citricoAtacante);
        int defensaObjetivo = CalcularDefensa(objetivo);
        int danio = (ataqueAtacante * 10) - (defensaObjetivo / 10);

        objetivo.Estadisticas.Salud -= danio;

        return danio;
    }

    public int CalcDanioAtaqueDefensa(Personaje atacante, Personaje objetivo)
    {
        bool citricoAtacante = DecidirCritico(atacante);
        int ataqueAtacante = CalcularAtaque(atacante, citricoAtacante);
        int defensaObjetivo = CalcularDefensa(objetivo);
        int danio = (ataqueAtacante * 10) - ((defensaObjetivo / 10) + 25);

        objetivo.Estadisticas.Salud -= danio;

        return danio;
    }
}

// combinaciones posibles de acciones
    // jugador ataca, enemigo ataca         - 11
    // jugador ataca, enemigo defiende      - 12
    // jugador ataca, enemigo espera        - 13
    // jugador defiende, enemigo ataca      - 21
    // jugador defiende, enemigo defiende   - 22 - no pasa nada
    // jugador defiende, enemigo espera     - 23 - no pasa nada
    // jugador espera, enemigo ataca        - 31
    // jugador espera, enemigo defiende     - 32 - no pasa nada
    // jugador espera, enemigo espera       - 33 - no pasa nada