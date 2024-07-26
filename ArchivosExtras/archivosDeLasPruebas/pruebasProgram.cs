﻿
using EspacioAPI;
using EspacioCartas;


List<Card> cartasAMostrar = new List<Card>();
Armador herramienta = new Armador();
API HerramientaAPI = new API();

Console.WriteLine("Pruebas para traer cartas desde una API.");

// estos await son necesarios, habren un hilo que sigue con todo
// lo que no dependa de la API, en tu caso se detiene porque todo
// depende de la API --------------------------------------------- anota esto en el proyecto

//var mazo = await API.ObtenerIdMazoAsync();

//Console.WriteLine(mazo.deck_id);
//Console.WriteLine(mazo.remaining);

cartasAMostrar = await API.ObtenerCartasAsync("0s5vj5pq5ujb");

foreach (var carta in cartasAMostrar)
{
    Console.WriteLine(carta.suit);
    Console.WriteLine(carta.value);
}

Console.WriteLine("Picas: ♠ " + "Corazones: ♥ " + "Diamantes: ♦ " + "Tréboles: ♣ ");


string cartasUnidas;
string primeraCarta = herramienta.ArmarCarta(cartasAMostrar[0]);
string segundaCarta = herramienta.ArmarCarta(cartasAMostrar[1]);
string reverso = herramienta.TraerReverso();

string[] partesIzq = primeraCarta.Split(";");
string[] partesDer = reverso.Split(";");

for (int indice = 0; indice < partesIzq.Length; indice++)
{
    if (cartasAMostrar[0].suit == "HEARTS" || cartasAMostrar[0].suit == "DIAMONDS")
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write(partesIzq[indice] + "  ");
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write(partesIzq[indice] + "  ");
    }

        if (cartasAMostrar[1].suit == "HEARTS" || cartasAMostrar[1].suit == "DIAMONDS")
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(partesDer[indice]);
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine(partesDer[indice]);
    }
}
Console.ForegroundColor = ConsoleColor.White;
{
    
}

partesDer = segundaCarta.Split(";");
for (int indice = 0; indice < partesIzq.Length; indice++)
{
    if (cartasAMostrar[0].suit == "HEARTS" || cartasAMostrar[0].suit == "DIAMONDS")
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write(partesIzq[indice] + "  ");
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write(partesIzq[indice] + "  ");
    }

        if (cartasAMostrar[1].suit == "HEARTS" || cartasAMostrar[1].suit == "DIAMONDS")
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(partesDer[indice]);
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine(partesDer[indice]);
    }
}
Console.ForegroundColor = ConsoleColor.White;


/*
priemeraCarta = herramienta.ArmarCarta(cartasAMostrar[0]);
primeraConReverso = herramienta.ConcatenarReverso(priemeraCarta);

string[] texto = primeraConReverso.Split(";");

Console.WriteLine("Carta con reverso: \n");
foreach (var linea in texto)
{
    Console.WriteLine(linea);
}

segundaCarta = herramienta.ArmarCarta(cartasAMostrar[1]);

cartasUnidas = herramienta.ConcatenarCartas(priemeraCarta, segundaCarta);

texto = cartasUnidas.Split(";");
foreach (var linea in texto)
{
    Console.WriteLine(linea);
}*/
