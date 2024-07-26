
using System.Runtime.CompilerServices;
using EspacioCartas;

public class Armador
{
    public string ArmarCarta(Card carta)
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

    public string TraerReverso()
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

    public string ConcatenarReverso(string carta)
    {
        string[] reverso = {" ------------- ",
                            "| []  []  []  |",
                            "|   []  []  []|",
                            "| []  []  []  |",
                            "|   []  []  []|",
                            "| []  []  []  |",
                            "|   []  []  []|",
                            "| []  []  []  |",
                            "|   []  []  []|",
                            "| []  []  []  |",
                            "|   []  []  []|",
                            "| []  []  []  |",
                            " ------------- "};
        string[] partesCarta = carta.Split(";");
        string cartasUnidas = "";
        string linea;

        for (int i = 0; i < reverso.Length; i++)
        {
            linea = partesCarta[i] + " " + reverso[i] + ";";
            cartasUnidas += linea;
        }

        return cartasUnidas;       // creo que esto está, falta la aprte donde las mostras y das a elegir
    }

    public string ConcatenarCartas(string primera, string segunda)
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
}