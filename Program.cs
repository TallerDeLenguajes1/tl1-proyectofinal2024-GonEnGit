﻿
// un visualizador de json mucho mejor: https://jsonviewer.stack.hu/

using EspacioPersonajes;
using EspacioDialogos;
using Microsoft.VisualBasic;


// variables
List<personaje> listaDePersonajes = new List<personaje>();

// aquí tenes una salida creativa, duplicá las clases en el arreglo y no necesitas contarlas
string[] clases = { "Guerrero", "Monje", "Arquero", "Luchador", "Ladrón", "Guerrero", "Monje", "Arquero", "Luchador", "Ladrón",};


// desde acá la intro

/*
string fraseIntro = dialogosIntro.Intro();

Console.Write("\nNarrador: ");
foreach (var item in fraseIntro)
{
    Console.Write(item);
    //Thread.Sleep(150);
}

fin de la intro */

// generar los 9 enemigos
for (int i = 0; i < 10; i++)
{
    listaDePersonajes.Add(new personaje(clases[i]));
}
foreach (var item in listaDePersonajes)
{
    Console.WriteLine(item.devolverDatos());
    Console.WriteLine(item.DevolverEstadisticas());
}


// las partidas las vas a guardar en un json, como una lista
// podes usar el HP como bandera para ir haciendo los duelos