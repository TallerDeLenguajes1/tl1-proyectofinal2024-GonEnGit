namespace EspacioJuego;

using EspacioAPI;
using EspacioPersonajes;


public static class Textos
{
    public static void Introduccion()
    {
        string linea = "primera frase por ahora";
        string[] texto = linea.Split(';');

        Console.Write("\nNarrador: ");
        foreach (var renglon in texto)
        {
            foreach (var letra in renglon)
            {
                Console.Write(letra);
                Thread.Sleep(0);        // cambiar 0 por 150
            }
        }
        Console.Write("\n");
        Thread.Sleep(1500);
        Console.WriteLine("");
    }

    public static string CentrarRenglon(int anchoPantalla, string palabras)
    {
        int espaciosAntes = (anchoPantalla - palabras.Length) / 2;
        string renglon = palabras;

        for (int i = 0; i < espaciosAntes; i++)
        {
            renglon = " " + renglon;
        }

        return renglon;
    }
    public static string[] MenuPrincipal()
    {
        string[] menu = {  "  # ------------------------------- #\n",
                            "  |  # - #                  # - #   |\n",
                            "  |         MENU PRINCIPAL          |\n",
                            "  |                                 |\n",
                            "  |       1. Nueva Partida          |\n",
                            "  |       2. Cargar Partida         |\n",
                            "  |       3. Ganadores Anteriores   |\n",
                            "  |       4. Salir                  |\n",
                            "  |                                 |\n",
                            "  |  # - #                  # - #   |\n",
                            "  # ------------------------------- #\n",};
        return menu;
    }

    public static string MenuDeGuardado()
    {
        return  "  |                          | ;" +
                "--+--------------------------+--;" +
                "  |    Continuar Partida?    | ;" +
                "--+--------------------------+--;" +
                "  |  1. Guardar y Continuar  | ;" +
                "  |  2. Guardar y Salir      | ;" +
                "  |  3. Salir sin Guardar    | ;" +
                "--+--------------------------+--;" +
                "  |                          | ;" +
                "     Seleccione una opción: ";
    }

    public static string CrearTarjeta(Personaje instancia)
    {
        string renglon1, renglon2, renglon3, renglon4;
        string lineaDatos = instancia.DevolverDatos();
        string lineaEstadisticas = instancia.DevolverEstadisticas();

        string[] Datos = lineaDatos.Split(";");
        string[] Estadisticas = lineaEstadisticas.Split(";");

    // los renglones deben tener 39 caracteres
    // renglones de datos iguales
        renglon1 = "+-";
        if (Datos[0].Length == 1)
        {
            renglon1 += $"{Datos[0]}-------------------------------------+;";
        }
        else
        {
            renglon1 += $"{Datos[0]}------------------------------------+;";   // un - menos que arriba
        }
        renglon2 = Datos[1] + ", " + Datos[2];
        renglon3 = Datos[3] + ", " + Datos[4];
        if (Datos[6].Length == 2 )
        {
            renglon4 = Datos[6] + "años,  " + Datos[5]; // 2 espacios
        }
        else
        {
            renglon4 = Datos[6] + "años, " + Datos[5];
        }
        while (renglon2.Length < 39)    // no los podes juntar
        {
            renglon2 = " " + renglon2;
            if (renglon2.Length < 39)
            {
                renglon2 = renglon2 + " ";
            }
        }
        while (renglon3.Length < 39)    // tienen largos totales distintos
        {
            renglon3 = " " + renglon3;
            if (renglon3.Length < 39)
            {
                renglon3 = renglon3 + " ";
            }
        }
        while (renglon4.Length < 39)
        {
            renglon4 = " " + renglon4;
            if (renglon4.Length < 39)
            {
                renglon4 = renglon4 + " ";
            }
        }
    // digitos de las estadisticas
        if (Datos[6].Length < 3) // esto es para que todas las edades tengan 3 caracteres
        {
            Datos[6] = " " + Datos[6];
        }
        if (Estadisticas[0].Length == 2) // mientras salud disminuye, puede tener menos de 3 digitos
        {
            Estadisticas[0] = "0" + Estadisticas[0];
        }
        else if (Estadisticas[0].Length == 1)
        {
            Estadisticas[0] = "00" + Estadisticas[0];
        }
        for (int i = 1; i < 4; i++) // por los niveles, las demas estadisticas pueden tener hasta 2 digitos
        {
            if (Estadisticas[i].Length < 2)
            {
                Estadisticas[i] = "0" + Estadisticas[i];
            }
        }

        return  $"{renglon1}" +
                $"|{renglon2}|;" +
                $"|{renglon3}|;" +
                $"|         {Datos[6]} años, {Datos[5]}           |;" +
                "+---------------------------------------+;" +
                $"|              Salud: {Estadisticas[0]}               |;" +
                $"|              Armadura: {Estadisticas[1]}             |;" +
                $"|              Fuerza: {Estadisticas[2]}               |;" +
                $"|              Destreza: {Estadisticas[3]}             |;" +
                "+---------------------------------------+;";
    }

    public static void ArmarListaTarjetas(List<string[]> lista, List<Personaje> listaTemp)
    {
        string[] primeraTarjeta, segundaTarjeta, TerceraTarjeta;
        string[] primeraFila, segundaFila;

        primeraTarjeta = CrearTarjeta(listaTemp[0]).Split(";");
        segundaTarjeta = CrearTarjeta(listaTemp[1]).Split(";");
        TerceraTarjeta = CrearTarjeta(listaTemp[2]).Split(";");
        primeraFila = new string[primeraTarjeta.Length];       // C# puede decidir el tamño de una arreglo automaticamente

        for (int i = 0; i < primeraTarjeta.Length - 1; i++)
        {
            primeraFila[i] = " " + primeraTarjeta[i] + " " + segundaTarjeta[i] + " " + TerceraTarjeta[i]; // pero no de a 1
        }
        lista.Add(primeraFila);

        primeraTarjeta = CrearTarjeta(listaTemp[3]).Split(";");
        segundaTarjeta = CrearTarjeta(listaTemp[4]).Split(";");
        segundaFila = new string[primeraTarjeta.Length];
        for (int i = 0; i < primeraTarjeta.Length; i++)
        {
            segundaFila[i] = "                      " + primeraTarjeta[i] + " " + segundaTarjeta[i];
        }
        lista.Add(segundaFila);
    }

    public static string DevolverNombre(Personaje pers)
    {
        return pers.DatosGenerales.Nombre + ',' + pers.DatosGenerales.Apodo;
    }

    public static string TutorialCartas()
    {
        return  "Dev: En este juego, la iniciativa de cada personaje se va a decidir con cartas.;" +
                "Dev: Primero se muestran 2, una boca abajo, el jugador tiene que elegir una.\n;" +
                "\n\nNarrador: Si no puede ver la segunda, como se supone que elija?;" +
                "Dev: Esa es la apuesta, el color y el palo tambien cambian el resultado.;" +
                "Azul = x1.5, Amarillo = x3, ♠ = +2, ♣ = +3, ♦ = +4,♥ = +5;" +
                "Narrador: Ahora veo, una carta baja puede dar un resultado alto!;" +
                "Dev: O no, ahora que sabemos cuales son, sabemos quien mueve primero.";
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

    public static string ElegirColor()
    {
        Random rnd = new Random();
        string eleccion = "";
        switch (rnd.Next(1,3))
        {
            case 1:
                eleccion = "ConsoleColor.DarkYellow";
                break;
            case 2:
                eleccion = "Blue";
                break;
        }
        return eleccion;
    }
}