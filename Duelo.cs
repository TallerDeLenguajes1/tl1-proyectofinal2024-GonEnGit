namespace EspacioJuego;


using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


using EspacioAPI;
using EspacioPersonajes;


public static class Duelo
{
    public static string CheatCode(Personaje jugador, Personaje enemigo, string codigo)
    {
        string respuesta;
        if (codigo.Trim() == "8855464621")
        {
            jugador.GodMode();
            respuesta = "Dev: espero que estes rindiendo y no jugando en serio...";
        }
        else if (codigo.Trim() == "1264645588")
        {
            enemigo.GodMode();
            respuesta = "Narrador: Espero que sepas lo que estas haciendo...";
        }
        else
        {
            respuesta = "Dev: En serio pensabas que podias usar esto?";
        }

        return respuesta;
    }

    public static int ContarEnemigosVencidos(List<Personaje> lista)
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

// la idea es hacer un calculo segun palo, color y valor
    public static int CalcularIniciativa(Carta cartaRecibida, int color)
    {
        int valorFinal;

        switch (cartaRecibida.value)
        {
            case "ACE":
                valorFinal = 1;
                break;
            case "JACK":
                valorFinal = 11;
                break;
            case "QUEEN":
                valorFinal = 12;
                break;
            case "KING":
                valorFinal = 13;
                break;
            default:
                valorFinal = int.Parse(cartaRecibida.value);
                break;
        }
    // multiplicas segun el color
        if (color == 1)
        {
            valorFinal = (int)(valorFinal* 1.5);
        }
        else
        {
            valorFinal *= 2;
        }
    // a estos calculos tambien los tenes que cambiar
        switch (cartaRecibida.suit)
        {
            case "SPADES":
                valorFinal += 2;
                break;
            case "CLUBS":
                valorFinal += 3;
                break;
            case "DIAMONDS":
                valorFinal += 4;
                break;
            case "HEARTS":
                valorFinal += 5;
                break;
        }

        return valorFinal;
    }

    public static int AccionEnemigo()
    {
        Random rnd = new Random();
        int numero = rnd.Next(0, 101);
        int accion;
        if (numero % 10 == 9)   // numero terminados en 9
        {
            accion = 3;     // el enemigo Espera, ~ 10%
        }
        else if (numero % 10 == 3 || numero % 10 == 5 || numero % 10 == 7)
        {
            accion = 2;     // el enemigo Defiende, ~ 30%
        }
        else
        {
            accion = 1;     // el enemigo Ataca, ~ 60%
        }

        return accion;
    }

    public static int CalcularDefensa(Personaje pers)
    {
        return pers.Estadisticas.Armadura + (10 * (pers.Estadisticas.Destreza / 10));
    }

    public static int CalcularAtaque(Personaje pers, bool critico)
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

    public static bool DecidirCritico(Personaje pers)
    {
        Random rnd = new Random();
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

// devolves el daño para mostrarlo
// y usas un metodo de personje para cambiar la vida
    public static int CalcularDanio(Personaje atacante, Personaje objetivo, bool objetivoDefiende, bool critico)
    {
        int ataqueAtacante = CalcularAtaque(atacante, critico);
        int defensaObjetivo = CalcularDefensa(objetivo);
        int danio;

        if (objetivoDefiende)
        {
            danio = (ataqueAtacante * 5) - ((defensaObjetivo * 3) + 10);
        }
        else
        {
            danio = (ataqueAtacante * 5) - (defensaObjetivo * 3);
        }


        if (danio <= 0) // GodMode hace que, por la defensa, de negativo
        {
            danio = 0;
        }
        objetivo.RecibirDanio(danio);

        return danio;
    }
}

// combinaciones posibles de acciones
    // jugador ataca, enemigo ataca         - 11 - CalcDanioAtaqueSinDefensa
    // jugador ataca, enemigo defiende      - 12 - CalcDanioAtaqueConDefensa
    // jugador ataca, enemigo espera        - 13 - CalcDanioAtaqueSinDefensa

    // jugador defiende, enemigo ataca      - 21 - CalcDanioAtaqueConDefensa
    // jugador espera, enemigo ataca        - 31 - CalcDanioAtaqueSinDefensa

    // jugador defiende, enemigo defiende   - 22 - no pasa nada
    // jugador defiende, enemigo espera     - 23 - no pasa nada
    // jugador espera, enemigo defiende     - 32 - no pasa nada
    // jugador espera, enemigo espera       - 33 - no pasa nada