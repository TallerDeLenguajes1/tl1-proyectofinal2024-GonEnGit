namespace EspacioJuego;

using System.ComponentModel;
using EspacioAPI;
using EspacioPersonajes;


public static class Textos
{
/* --- Metodos generales --- */
    public static string ArmarRenglon(string datos, int cantEspacios)
    {
        string renglon = datos;
        while (renglon.Length < cantEspacios)
        {
            renglon = " " + renglon;
            if (renglon.Length < cantEspacios)
            {
                renglon = renglon + " ";
            }
        }
        return renglon;
    }

    public static string CentrarRenglon(int anchoPantalla, string palabras)
    {
        int espaciosAntes = (anchoPantalla - palabras.Length) / 2;
        string renglon = palabras;

        if (espaciosAntes <= 0) // parece que string muy largos rompen esto
        {
            espaciosAntes = 10;
        }

        for (int i = 0; i < espaciosAntes; i++)
        {
            renglon = " " + renglon;
        }

        return renglon;
    }

    public static void ArmarListaTarjetasPers(List<string[]> lista, List<Personaje> listaTemp)
    {
        string[] primeraTarjeta, segundaTarjeta, TerceraTarjeta;
        string[] primeraFila, segundaFila;

        primeraTarjeta = CrearTarjetaPers(listaTemp[0]).Split(";");
        segundaTarjeta = CrearTarjetaPers(listaTemp[1]).Split(";");
        TerceraTarjeta = CrearTarjetaPers(listaTemp[2]).Split(";");
        primeraFila = new string[primeraTarjeta.Length];       // C# puede decidir el tamño de una arreglo automaticamente

        for (int i = 0; i < primeraTarjeta.Length; i++)
        {
            primeraFila[i] = "     " + primeraTarjeta[i] + "     " + segundaTarjeta[i] + "     " + TerceraTarjeta[i]; // pero no de a 1
        }
        lista.Add(primeraFila);

        primeraTarjeta = CrearTarjetaPers(listaTemp[3]).Split(";");
        segundaTarjeta = CrearTarjetaPers(listaTemp[4]).Split(";");
        segundaFila = new string[primeraTarjeta.Length];
        for (int i = 0; i < primeraTarjeta.Length; i++)
        {
            segundaFila[i] = primeraTarjeta[i] + "     " + segundaTarjeta[i];
        }
        lista.Add(segundaFila);
    }

    public static string[] ArmarListaTarjetasArt(List<Artefacto> cofre)
    {
        string[] primeraTarjeta, segundaTarjeta, TerceraTarjeta, renglones;

        primeraTarjeta = CrearTarjetaArt(cofre[0], 1).Split(";");
        segundaTarjeta = CrearTarjetaArt(cofre[1], 2).Split(";");
        TerceraTarjeta = CrearTarjetaArt(cofre[2], 3).Split(";");

        renglones = new string[primeraTarjeta.Length];
        segundaTarjeta[0] = "  " + segundaTarjeta[0] + "  "; // esto se podria mejorar
        for (int i = 0; i < primeraTarjeta.Length; i++)
        {
            renglones[i] = primeraTarjeta[i] + "     " + segundaTarjeta[i] + "     " + TerceraTarjeta[i];
        }

        return renglones;
    }

    public static string DevolverNombre(Personaje pers)
    {
        return pers.DatosGenerales.Nombre + ", " + pers.DatosGenerales.Apodo;
    }

    public static string DatosDeSubaDeNivel(string clase)
    {
        string linea = "Salud +25;";
        switch (clase)
        {
            case "Guerrero":
                linea += "Armadura +2;Fuerza +2;Destreza +2";
                break;
            case "Monje":
                linea += "Armadura +1;Fuerza +1;Destreza +2";
                break;
            case "Paladín":
                linea += "Armadura +2;Fuerza +1;Destreza +1";
                break;
            case "Berserker":
                linea += "Armadura +1;Fuerza +2;Destreza +1";
                break;
            case "Gladiador":
                linea += "Armadura +1;Fuerza +1;Destreza +2";
                break;
        }
        return linea;
    }

/* --- Historias --- */
    public static string Introduccion()
    {
        return  "\n;" +
                "Narrador: ;"+
                "Las hojas caidas cuentan la historia... ;" +
                "El gran ani...\n;" +
                "Dev: Que estas haciendo?;" +
                "Narrador: Contando la historia del juego que mas?;" +
                "Dev: Nadie lee estas cosas... Ademas esa historia no es nueva!;" +
                "Dev: estas buscando que me desaprueben por copiar?;" +
                "Narrador: No pero...;" +
                "Dev: Traigan el titulo!;";
    }

    public static string IntroSeleccionPers()
    {
        return  "\nNarrador: y ahora, para nuestro primer duelo!;" +
                "Dev: ejem...;" +
                "Nerrador: Que pasa ahora...?;" +
                "Dev: Aun no eligen un personaje...;" +
                "Narrador: ah... me olvidé de eso... Elige tu personaje!;";
    }

    public static string TutorialCartas()
    {
        return  "Dev: En este juego, la iniciativa de cada personaje se va a decidir con cartas.;" +
                "Dev: Primero se muestran 2, una boca abajo, el jugador tiene que elegir una.\n;" +
                "\n\nNarrador: Si no puede ver la segunda, como se supone que elija?;" +
                "Dev: Esa es la apuesta, el color y el palo tambien cambian el resultado.;" +
                "Azul = x2, Amarillo = x1.5, ♠ = +2, ♣ = +3, ♦ = +4, ♥ = +5;" +
                "Narrador: Ahora veo, una carta baja puede dar un resultado alto!;" +
                "Dev: O no, ahora que sabemos cuales son, sabemos quien mueve primero.";
    }

/* --- Dibujos --- */
    public static string Titulo()
    {

        return  " _____         _                   _        ;" +
                "|_   _|       | |                 | |       ;" +
                  "| | ___   __| | ___     ___  ___| |_ ___;" +
                 @"| |/ _ \ / _` |/ _ \   / _ \/ __| __/ _ \;" +
                 @"  | | (_) | (_| | (_) | |  __/\__ \ || (_) |;" +
                 @"|_|\___/ \__,_|\___/   \___||___/\__\___/;" +
                 "                                  _                        ___;" +
                @"                                 | |                      |__ \;" +
                "  _ __   ___  _ __   _   _ _ __   | |_ _ __ ___  _ __   ___   ) |;" +
               @"| '_ \ / _ \| '__| | | | | '_ \  | __| '__/ _ \| '_ \ / _ \ / /;" +
                "| |_) | (_) | |    | |_| | | | | | |_| | | (_) | | | | (_) |_|;" +
               @"| .__/ \___/|_|     \__,_|_| |_|  \__|_|  \___/|_| |_|\___/(_);" +
                "| |                                                           ;" +
                "|_|                                                           ;";
    }

    public static string MenuPrincipal()
    {
        return  "# ------------------------------- #;" +
                "|  # - #                  # - #   |;" +
                "|         MENU PRINCIPAL          |;" +
                "|                                 |;" +
                "|       1. Nueva Partida          |;" +
                "|       2. Cargar Partida         |;" +
                "|       3. Ganadores Anteriores   |;" +
                "|       4. Salir                  |;" +
                "|                                 |;" +
                "|  # - #                  # - #   |;" +
                "# ------------------------------- #;";
    }

    public static string MenuDeGuardado()
    {
        return  " |                          | ;" +
                "--+--------------------------+--;" +
                " |    Continuar Partida?    | ;" +
                "--+--------------------------+--;" +
                " |  1. Guardar y Continuar  | ;" +
                " |  2. Guardar y Salir      | ;" +
                " |  3. Ver Inventario       | ;" +
                " |  4. Salir sin Guardar    | ;" +
                "--+--------------------------+--;" +
                " |                          |;";
    }

    public static string CrearTarjetaPers(Personaje instancia)
    {
        string renglon1, renglon2, renglon3, renglon4;
        string lineaDatos = instancia.DevolverDatos();
        string lineaEstadisticas = instancia.DevolverEstadisticas();

        string[] Datos = lineaDatos.Split(";");
        string[] Estadisticas = lineaEstadisticas.Split(";");

    // renglones de tarjetas de personjae tienen 39 caracteres + 2 bordes
        renglon1 = "+-";
        if (Datos[0].Length == 1)
        {
            renglon1 += $"{Datos[0]}-------------------------------------+;";
        }
        else
        {
            renglon1 += $"{Datos[0]}------------------------------------+;";   // un - menos que arriba
        }
        renglon1 = ArmarRenglon(renglon1, 39);
        renglon2 = ArmarRenglon(Datos[1] + ", " + Datos[2], 39);
        renglon3 = ArmarRenglon(Datos[3] + ", " + Datos[4], 39);
        if (Datos[6].Length == 2 )
        {
            renglon4 = Datos[6] + " años,  " + Datos[5]; // 2 espacios
        }
        else
        {
            renglon4 = Datos[6] + " años, " + Datos[5];
        }
        renglon4 = ArmarRenglon(renglon4, 39);
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
            if (Estadisticas[i].Length < 3)
            {
                Estadisticas[i] = " " + Estadisticas[i];
            }
        }

        return  $"{renglon1}" +
                $"|{renglon2}|;" +
                $"|{renglon3}|;" +
                $"|{renglon4}|;" +
                "+---------------------------------------+;" +
                $"|              Salud: {Estadisticas[0]}               |;" +
                $"|              Armadura: {Estadisticas[1]}            |;" +
                $"|              Fuerza: {Estadisticas[2]}              |;" +
                $"|              Destreza: {Estadisticas[3]}            |;" +
                "+---------------------------------------+";
    }

    public static string CrearTarjetaArt(Artefacto artefacto, int numOpcion)
    {
    // renglon de Artefacto: 45 caracteres + 2 bordes
        string lineaId = ArmarRenglon(numOpcion.ToString(), 40);
        string lineaNombre = ArmarRenglon(artefacto.Nombre, 40);
        string lineaDesc1 = ArmarRenglon(artefacto.Descripcion[0], 40);
        string lineaDesc2 = ArmarRenglon(artefacto.Descripcion[1], 40);
        string lineaDesc3;
        string lineaEfecto = ArmarRenglon(artefacto.Efecto + " +" + artefacto.Cantidad, 31); // sacas ' Efecto: '

        if (artefacto.Descripcion.Length == 3)
        {
            lineaDesc3 = ArmarRenglon(artefacto.Descripcion[2], 40);
        }
        else
        {
            lineaDesc3 = "                                        ";
        }
        return  @"/≒≒≒≒≒≒≒≒≒≒≒✠≑≑≑≑≑≑≑≑≑≑≑≑≑≑✠≓≓≓≓≓≓≓≓≓≓≓\;" +
                $@"/{lineaNombre}\;" +
                "|                                        |;" +
                "|<∻∻∻∻∻∻∻∻∻∻∻∻∻∻∻∻∻∰∻∻∰∻∻∻∻∻∻∻∻∻∻∻∻∻∻∻∻∻>|;" +
                "|                                        |;" +
                "|                                        |;" +
                $"|{lineaDesc1}|;" +
                $"|{lineaDesc2}|;" +
                $"|{lineaDesc3}|;" +
                "|                                        |;" +
                "|                                        |;" +
                $"| Efecto: {lineaEfecto}|;" +
                "✦≒≒≒≒≒≒≒≒≒≒≒≒≒✠≑≑≑≑≑≑≑≑≑≑≑≑✠≓≓≓≓≓≓≓≓≓≓≓≓≓✦;" +
                $"{lineaId}";
    }

    public static string ArmarItemAMostrar(Artefacto artefacto)
    {
        string lineaNombre = ArmarRenglon(artefacto.Nombre, 36);
        string lineaDesc1 = ArmarRenglon(artefacto.Descripcion[0], 40);
        string lineaDesc2 = ArmarRenglon(artefacto.Descripcion[1], 40);
        string lineaDesc3;
        string lineaEfecto = ArmarRenglon(artefacto.Efecto + " +" + artefacto.Cantidad, 40); // sacas ' Efecto: '

        if (artefacto.Descripcion.Length == 3)
        {
            lineaDesc3 = ArmarRenglon(artefacto.Descripcion[2], 40);
        }
        else
        {
            lineaDesc3 = "                                        ";
        }
        return   "__ _____________________________________;" + 
                @"/  \ ____________________________________\;" +
                @"| \ |                                    |;" +
                $@"\_| |{lineaNombre}|;" +
                    "|                                        |;" +
                    "|                                        |;" +
                    $"|{lineaDesc1}|;" +
                    $"|{lineaDesc2}|;" +
                    $"|{lineaDesc3}|;" +
                    "|                                        |;" +
                    "|                                        |;" +
                    $"|{lineaEfecto}|;" + 
                    @"|   \ _____________________________  __  |;" +
                    @"\__/________________________________ _ /;";
    }

    public static string CierreYTrono()
    {
        return  "Narrador: ... Ya no queda nadie mas... solo queda entregar el trono...;" +
                "Dev: Trono? tenia que haber un trono?!;" +
                "Narrador: esa era la idea desde el principio... no leiste la historia...;" +
                "Dev: uh... a ver... aquí hay uno!\n;" +
                                "⢀⣀  ⣀⡀;" +
                        "⢸⣿⡇⢠⣤⣶⣾⣿⣿⣿⣿⣷⣶⣤⡄⢸⣿⡇;" +
                        "⢸⣿⡇⢸⣿⣿⣿⠛   ⠛⣿⣿⣿⡇⢸⣿⡇;" +
                        "⢸⣿⡇⢸⣿⣿⣇⣀⣀⣀⣀⣸⣿⣿⡇⢸⣿⡇;" +
                        "⢸⣿⡇⢸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⢸⣿⡇;" +
                        "⢸⣿⡇⢸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⢸⣿⡇;" +
                        "⢸⣿⡇⢸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⢸⣿⡇;" +
                        "⢸⣿⡇⢸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⢸⣿⡇;" +
                        " ⢠⣤⣤⣤⣤⡄⠸⣿⣿⣿⣿⣿⣿⣿⣿⡇⢠⣤⣤⣤⣤⡄;" +
                        " ⠈⠉⠉⠉⠉⠁⢠⣤⣤⣤⣤⣤⣤⣤⣤⡄⠈⠉⠉⠉⠉⠁;" +
                        "⠀⢸⣿⣿⡇⠘⠛⠛⠛⠛⠛⠛⠛⠛⠛⠛⠃⢸⣿⣿⡇;" +
                        "⠀⢸⣿⣿⡇⢸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⢸⣿⣿⡇;" +
                        "⠀⢸⣿⣿⡇⢸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡇⢸⣿⣿⡇;" +
                        "⠀⢸⣿⣿⡇⠸⠿⠿⠿⠿⠿⠿⠿⠿⠿⠿⠇⢸⣿⣿⡇;" +
                        "⠀⠈⠛⠛⠃⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀  ⠘⠛⠛⠁;" +
                "Narrador: Eso... es todo...? Hasta está torcido!;" +
                "Dev: Es el unico que teniamos...;" +
                "Dev: Felicidades por ganar nuestro juego!;";
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
}