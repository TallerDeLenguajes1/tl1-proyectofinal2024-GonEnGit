namespace EspacioTextos;


using EspacioCartas;
using EspacioPersonajes;


public static class Textos
{
    public static string Intro()
    {
        return "primera frase por ahora";
    }

    public static string Menu()
    {
        return "  # ------------------------------- #;" +
                "  |  # - #                  # - #   |;" +
                "  |         MENU PRINCIPAL          |;" +
                "  |                                 |;" +
                "  |       1. Nueva Partida          |;" +
                "  |       2. Cargar Partida         |;" +
                "  |       3. Ganadores Anteriores   |;" +
                "  |       4. Salir                  |;" +
                "  |                                 |;" +
                "  |  # - #                  # - #   |;" +
                "  # ------------------------------- #;" +
                "            Elija una opción:         ";
    }

    public static string Tarjetas(Personaje instancia)
    {
        string lineaDatos = instancia.DevolverDatos();
        string lineaEstadisticas = instancia.DevolverEstadisticas();

        string[] Datos = lineaDatos.Split(";");
        string[] Estadisticas = lineaEstadisticas.Split(";");

    // esto es para que todas las edades tengan 3 caractere
        if (Datos[6].Length < 3)
        {
            Datos[6] = " " + Datos[6];
        }

        return $"+-{Datos[0]}------------------------------------+;" +
                $"|         {Datos[1]}, {Datos[2]}                 |;" +
                $"|       {Datos[3]}, {Datos[4]}                 |;" +
                $"|           {Datos[6]} años, {Datos[5]}            |;" +
                "+--------------------------------------+;" +
                $"|              Salud: {Estadisticas[0]}              |;" +
                $"|              Armadura: {Estadisticas[1]}             |;" +
                $"|              Fuerza: {Estadisticas[2]}               |;" +
                $"|              Destreza: {Estadisticas[3]}             |;" +
                $"|              Velocidad: {Estadisticas[4]}            |;" +
                "+--------------------------------------+;";
    }

    public static string DevolverNombre(Personaje pers)
    {
        return pers.DatosGenerales.Nombre + ',' + pers.DatosGenerales.Apodo;
    }

    public static string MenuDeGuardado()
    {
        return  " |                          | ;" +
                "-+--------------------------+-;" +
                " |    Continuar Partida?    | ;" +
                "-+--------------------------+-;" +
                " |  1. Guardar y Continuar  | ;" +
                " |  2. Guardar y Salir      | ;" +
                " |  3. Salir sin Guardar    | ;" +
                "-+--------------------------+-;" +
                " |                          | ;" +
                "    Seleccione una opción: ";
    }

    public static string TraerReverso()
    {
        return  " ------------- ;" +
                "| []  []  []  |;" +
                "|   []  []  []|;" +
                "| []  []  []  |;" +
                "|   []  []  []|;" +
                "| []  []  []  |;" +
                "|   []  []  []|;" +
                "| []  []  []  |;" +
                "|   []  []  []|;" +
                "| []  []  []  |;" +
                "|   []  []  []|;" +
                "| []  []  []  |;" +
                " ------------- ";
    }

    public static string ArmarCarta(Carta carta)
    {
        string cabeza = " ------------- ;" + "| G          |;" + "|   -------   |;";    // el espacio que falta está a proposito
        string pie ="|   -------   |;" + "|          G |;" + " ------------- ";         // igual en este caso
        string cartaString = "";

// los valores que trae la API son todos strings
// es menos lio compararlos como tales que pasar a int
// 4 casos "diferentes" los tres nombres y el 10
        if (carta.value.Length >= 2)
        {
            switch (carta.value)
            {
                case "ACE":
                    cartaString = (cabeza  + 
                            "|  |X      |  |;" +
                            "|  |       |  |;" +
                            "|  |       |  |;" +
                            "|  |   A   |  |;" +
                            "|  |       |  |;" +
                            "|  |       |  |;" +
                            "|  |      X|  |;" 
                                    + pie).Replace("G", "01");
                    break;
                case "10":
                    cartaString = (cabeza  + 
                            "|  |X     X|  |;" +
                            "|  |   X   |  |;" +
                            "|  |X     X|  |;" +
                            "|  |X     X|  |;" +
                            "|  |X     X|  |;" +
                            "|  |   X   |  |;" +
                            "|  |X     X|  |;" 
                                    + pie).Replace("G", "10");
                    break;
                case "JACK":
                    cartaString = (cabeza  + 
                            "|  |X      |  |;" +
                            "|  |       |  |;" +
                            "|  |       |  |;" +
                            "|  |   J   |  |;" +
                            "|  |       |  |;" +
                            "|  |       |  |;" +
                            "|  |      X|  |;" 
                                    + pie).Replace("G", "11");
                    break;
                case "QUEEN":
                    cartaString = (cabeza + 
                            "|  |X      |  |;" +
                            "|  |       |  |;" +
                            "|  |       |  |;" +
                            "|  |   Q   |  |;" +
                            "|  |       |  |;" +
                            "|  |       |  |;" +
                            "|  |      X|  |;" 
                                    + pie).Replace("G", "12");
                    break;
                case "KING":
                    cartaString = (cabeza + 
                            "|  |X      |  |;" +
                            "|  |       |  |;" +
                            "|  |       |  |;" +
                            "|  |   K   |  |;" +
                            "|  |       |  |;" +
                            "|  |       |  |;" +
                            "|  |      X|  |;" 
                                    + pie).Replace("G", "13");
                    break;
            }
        }
        else    // los demas valores tienen un solo digito
        {
            switch (carta.value)
            {
                case "2":
                    cartaString = cabeza  + 
                            "|  |       |  |;" +
                            "|  |   X   |  |;" +
                            "|  |       |  |;" +
                            "|  |       |  |;" +
                            "|  |       |  |;" +
                            "|  |   X   |  |;" +
                            "|  |       |  |;" 
                                    + pie;
                    break;
                case "3":
                    cartaString = cabeza  + 
                            "|  |       |  |;" +
                            "|  |   X   |  |;" +
                            "|  |       |  |;" +
                            "|  |   X   |  |;" +
                            "|  |       |  |;" +
                            "|  |   X   |  |;" +
                            "|  |       |  |;" 
                                    + pie;
                    break;
                case "4":
                    cartaString = cabeza  + 
                            "|  |X     X|  |;" +
                            "|  |       |  |;" +
                            "|  |       |  |;" +
                            "|  |       |  |;" +
                            "|  |       |  |;" +
                            "|  |       |  |;" +
                            "|  |X     X|  |;" 
                                    + pie;
                    break;
                case "5":
                    cartaString = cabeza  + 
                            "|  |X     X|  |;" +
                            "|  |       |  |;" +
                            "|  |   X   |  |;" +
                            "|  |       |  |;" +
                            "|  |       |  |;" +
                            "|  |       |  |;" +
                            "|  |X     X|  |;" 
                                    + pie;
                    break;
                case "6":
                    cartaString = cabeza  + 
                            "|  |X     X|  |;" +
                            "|  |       |  |;" +
                            "|  |       |  |;" +
                            "|  |   X   |  |;" +
                            "|  |       |  |;" +
                            "|  |       |  |;" +
                            "|  |X     X|  |;" 
                                    + pie;
                    break;
                case "7":
                    cartaString = cabeza  + 
                            "|  |X     X|  |;" +
                            "|  |   X   |  |;" +
                            "|  |       |  |;" +
                            "|  |X     X|  |;" +
                            "|  |       |  |;" +
                            "|  |   X   |  |;" +
                            "|  |X     X|  |;" 
                                    + pie;
                    break;
                case "8":
                    cartaString = cabeza  + 
                            "|  |X     X|  |;" +
                            "|  |   X   |  |;" +
                            "|  |X     X|  |;" +
                            "|  |       |  |;" +
                            "|  |X     X|  |;" +
                            "|  |   X   |  |;" +
                            "|  |X     X|  |;" 
                                    + pie;
                    break;
                case "9":
                    cartaString = cabeza  + 
                            "|  |X     X|  |;" +
                            "|  |   X   |  |;" +
                            "|  |X     X|  |;" +
                            "|  |X     X|  |;" +
                            "|  |X     X|  |;" +
                            "|  |   X   |  |;" +
                            "|  |X     X|  |;" 
                                    + pie;
                    break;
            }
            cartaString = cartaString.Replace("G", "0" + carta.value);
        }

// caracteres unicode, estan en UTF8: ♠, ♥, ♦, ♣
// si cmd no los imprime usas chcp 65001
        switch (carta.suit)
        {
            case "DIAMONDS":
                cartaString = cartaString.Replace("X", "♦");
                break;
            case "HEARTS":
                cartaString = cartaString.Replace("X", "♥");
                break;
            case "CLUBS":
                cartaString = cartaString.Replace("X", "♣");
                break;
            case "SPADES":
                cartaString = cartaString.Replace("X", "♠");
                break;
        }

        return cartaString;
    }

/*
    public static string ConcatenarCartas(string primera, string segunda)
    {
        string[] partesPrimera = primera.Split(";");
        string[] partesSegunda = segunda.Split(";");

        string cartasUnidas = "";
        string linea;

        for (int i = 0; i < partesPrimera.Length; i++)
        {
            linea = partesPrimera[i] + " " + partesSegunda[i] + ";";
            cartasUnidas += linea;
        }

        return cartasUnidas;
    }
*/
}