namespace EspacioJuego;

using System.ComponentModel;
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
    public static string[] MenuPrincipal()
    {
        string[] menu = {   "# ------------------------------- #\n",
                            "|  # - #                  # - #   |\n",
                            "|         MENU PRINCIPAL          |\n",
                            "|                                 |\n",
                            "|       1. Nueva Partida          |\n",
                            "|       2. Cargar Partida         |\n",
                            "|       3. Ganadores Anteriores   |\n",
                            "|       4. Salir                  |\n",
                            "|                                 |\n",
                            "|  # - #                  # - #   |\n",
                            "# ------------------------------- #\n",};
        return menu;
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
                " |                          |\n;" +
                "Seleccione una opción:    ";
    }

    public static string CrearTarjeta(Personaje instancia)
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

    public static void ArmarListaTarjetas(List<string[]> lista, List<Personaje> listaTemp, List<Artefacto> cofre, int tipo)
    {
        string[] partes, primeraFila, segundaFila;
        List<string[]> tarjetas = new List<string[]>();

        if (tipo == 1)
        {
            for (int indiceTarjeta = 0; indiceTarjeta < 5; indiceTarjeta++)
            {
                partes = CrearTarjeta(listaTemp[indiceTarjeta]).Split(";");
                tarjetas.Add(partes);
            }
        }
        else
        {
            for (int indiceTarjeta = 0; indiceTarjeta < 3; indiceTarjeta++)
            {
                partes = CrearTarjetaArt(cofre[indiceTarjeta]).Split(";");
                tarjetas.Add(partes);
            }
        }

        primeraFila = new string[tarjetas[0].Length];
        for (int indiceFilas = 0; indiceFilas < tarjetas[0].Length; indiceFilas++)
        {
        // sin este control las tarjetas de artefacos se deforma
            if (indiceFilas == 0 && tipo == 2)
            {
                primeraFila[indiceFilas] = tarjetas[0][indiceFilas] + "     " + tarjetas[1][indiceFilas] + "     " + tarjetas[2][indiceFilas];
            }
            else
            {
                primeraFila[indiceFilas] = tarjetas[0][indiceFilas] + "   " + tarjetas[1][indiceFilas] + "   " + tarjetas[2][indiceFilas];
            }
        }
        lista.Add(primeraFila);

        if (tipo == 1)
        {
            segundaFila = new string[tarjetas[0].Length];
            for (int indiceFilas = 0; indiceFilas < tarjetas[0].Length; indiceFilas++)
            {
                segundaFila[indiceFilas] = tarjetas[3][indiceFilas] + "   " + tarjetas[4][indiceFilas];
            }
            lista.Add(segundaFila);
        }
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

    public static string TutorialCartas()
    {
        return  "Dev: En este juego, la iniciativa de cada personaje se va a decidir con cartas.;" +
                "Dev: Primero se muestran 2, una boca abajo, el jugador tiene que elegir una.\n;" +
                "\n\nNarrador: Si no puede ver la segunda, como se supone que elija?;" +
                "Dev: Esa es la apuesta, el color y el palo tambien cambian el resultado.;" +
                "Azul = x2, Amarillo = x1.5, ♠ = +2, ♣ = +3, ♦ = +4,♥ = +5;" +
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

    public static string CrearTarjetaArt(Artefacto artefacto)
    {
    // renglon de Artefacto: 45 caracteres + 2 bordes
        string lineaId = ArmarRenglon(artefacto.Id.ToString(), 40);
        string lineaNombre = ArmarRenglon(artefacto.Nombre, 40);
        string LineaDesc1 = ArmarRenglon(artefacto.Descripcion[0], 40);
        string LineaDesc2 = ArmarRenglon(artefacto.Descripcion[1], 40);
        string LineaDesc3;
        string LineaEfecto = ArmarRenglon(artefacto.Efecto + " +" + artefacto.Cantidad, 31); // sacas ' Efecto: '

        if (artefacto.Descripcion.Length == 3)
        {
            LineaDesc3 = ArmarRenglon(artefacto.Descripcion[2], 40);
        }
        else
        {
            LineaDesc3 = "                                        ";
        }
        return  @"/≒≒≒≒≒≒≒≒≒≒≒≒✠≑≑≑≑≑≑≑≑≑≑≑≑✠≓≓≓≓≓≓≓≓≓≓≓≓\;" +
                $@"/{lineaNombre}\;" +
                "|                                        |;" +
                "|<∻∻∻∻∻∻∻∻∻∻∻∻∻∻∻∻∻∰∻∻∰∻∻∻∻∻∻∻∻∻∻∻∻∻∻∻∻∻>|;" +
                "|                                        |;" +
                "|                                        |;" +
                $"|{LineaDesc1}|;" +
                $"|{LineaDesc2}|;" +
                $"|{LineaDesc3}|;" +
                "|                                        |;" +
                "|                                        |;" +
                $"| Efecto: {LineaEfecto}|;" +
                "✦≒≒≒≒≒≒≒≒≒≒≒≒≒✠≑≑≑≑≑≑≑≑≑≑≑≑✠≓≓≓≓≓≓≓≓≓≓≓≓≓✦;" +
                $"{lineaId}";
        
    }

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