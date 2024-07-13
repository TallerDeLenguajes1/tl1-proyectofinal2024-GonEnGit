﻿
// un visualizador de json mucho mejor: https://jsonviewer.stack.hu/

using System.Data;
using Microsoft.VisualBasic;
using System.Runtime.InteropServices;

using EspacioTextos;
using EspacioDuelos;
using EspacioPersonajes;


// variables
bool gameOver = false;
bool pruebaOpciones = false;
int numeroAleatorio;
int contadorHistoria = 1;
int entradaDeUsuario = 0;

// por la carga de partida estos 2 tendrian que ser metodos... como? personaje?
// instancias un duelo de clase separada y usas eso?
int cantEnemigosVivos;
int enemigoActivo;

int[] orden = new int[9];
string linea;
string[] texto;
Random rnd = new Random();
personaje auxiliar;
duelo controlDuelo = new duelo();
List<personaje> listaPersonajes = new List<personaje>();



/*--- Desde acá la intro ---*/
linea = Textos.Intro();
texto = linea.Split(';');

Console.Write("\nNarrador: ");
foreach (var renglon in texto)
{
    foreach (var letra in renglon)
    {
        Console.Write(letra);
        Thread.Sleep(0);        // cambiar 0 por 150
    }
}
contadorHistoria += 1;
Console.Write("\n");
Thread.Sleep(1500);         // son milisegundos, 2s es mucho
Console.WriteLine("");

/*--- FIN de la intro ---*/



/*--- Menú ---*/

// imprime el Menú
linea = Textos.Menu();
texto = linea.Split(';');
for (int indice = 0; indice < texto.Length; indice++)
{
    if (indice == texto.Length - 1)
    {
        Console.Write(texto[indice].TrimEnd() + " ");
    }
    else
    {
        Console.WriteLine(texto[indice]);
    }
}
// controla que se ingrese lo correcto
while (pruebaOpciones == false || entradaDeUsuario <= 0 || entradaDeUsuario >= 4)
{
    pruebaOpciones = int.TryParse(Console.ReadLine(), out entradaDeUsuario);
    if (pruebaOpciones == false || entradaDeUsuario <= 0 || entradaDeUsuario >= 4)
    {
        Console.WriteLine("");
        Console.Write("Ingrese un numero valido: ");
    }
}
Console.WriteLine("");

/*--- FIN del menu ---*/



/*--- inicio del control de opcion ---*/

// inicia el juego segun la opcion
if (entradaDeUsuario == 1)
{
    // declaras listas provisorias para liberarlas despues
    List<personaje> listaPersTemp = new List<personaje>();
    List<string[]> tarjetasAMostrar = new List<string[]>();

    // inicializas variables
    listaPersTemp = personaje.FabricaDePersonajes(listaPersTemp);

/*--- esta parte se supone que sea otra historia ---*/ // ------------------------------------------------
    // historia
/*--- fin de esta parte de historia---*/

// la consola no puede mostrar las tarjetas una al lado de la otra
// lo que podes hacer es una matriz, un vector de 5 vectores
// donde cada uno de los 5 tiene la tarjeta separada linea por linea

    // llenas esa lista con los primeros 5 personajes
    for (int indice = 0; indice <= 5; indice++)
    {
        linea = Textos.Tarjetas(listaPersTemp[indice]);
        texto = linea.Split(";");
        tarjetasAMostrar.Add(texto);
    }
    // si no limpias 'linea' esto es un desastre
    linea = "";
    // ahora con esa lista podes concatenar cada linea e imprimirlas
    for (int indiceLinea = 0; indiceLinea < 12; indiceLinea++)
    {
        for (int indiceTarjeta = 0; indiceTarjeta < 3; indiceTarjeta++)
        {
            linea = linea + "   " + tarjetasAMostrar[indiceTarjeta][indiceLinea];
        }
        Console.WriteLine(linea);
        linea = "";
    }
    // no es lo mas lindo pero tenes que mostrar 5 personajes, 2 quedan abajo
    for (int indiceLinea = 0; indiceLinea < 12; indiceLinea++)
    {
        for (int indiceTarjeta = 3; indiceTarjeta < 5; indiceTarjeta++)
        {
            if (indiceTarjeta == 3)
            {
                linea = "                      ";
            } 
            linea = linea + "   " + tarjetasAMostrar[indiceTarjeta][indiceLinea];
        }
        Console.WriteLine(linea);
        linea = " ";
    }

    /*--- seleccion de personaje ---*/
    entradaDeUsuario = 0;
    Console.Write("Elige un personaje: ");
    while (pruebaOpciones == false || entradaDeUsuario <= 0 || entradaDeUsuario >= 6)
    {
        pruebaOpciones = int.TryParse(Console.ReadLine(), out entradaDeUsuario);
        if (pruebaOpciones == false || entradaDeUsuario <= 0 || entradaDeUsuario >= 6)
        {
            Console.WriteLine("");
            Console.Write("Ingrese un numero valido: ");
        }
    }
    entradaDeUsuario -= 1;      // recorda que el orden de la lista no es el mismo que los id
    Console.WriteLine("");

    // mezclas la lista para que sea aleatoria siempre
    listaPersTemp[entradaDeUsuario].DatosGenerales.Id = 1;  // cambias el id
    auxiliar = listaPersTemp[entradaDeUsuario];
    listaPersTemp.RemoveAt(entradaDeUsuario);               // lo sacas de la lista Temporal
    listaPersonajes.Add(auxiliar);   // guardas el personaje elegido

    for (int indice = 0; indice < 9; indice++)     // necesitas el 0 pero inicializa con 0 por defecto
    {
        orden[indice] = 721;                       // cambias los valores a otra cosa
    }
    for (int indice = 0; indice < 9; indice++)     // generas el nuevo orden
    {
        do
        {
            numeroAleatorio = rnd.Next(0, 9);
        } while (orden.Contains(numeroAleatorio));
        orden[indice] = numeroAleatorio;
    }
    listaPersonajes = personaje.MezclarLista(listaPersonajes, orden, listaPersTemp);

    foreach (var item in listaPersonajes)
    {
        linea = Textos.Tarjetas(item);
        texto = linea.Split(";");
        foreach (string str in texto)
        {
            Console.WriteLine(str);
        }
    }
    /*--- Fin de seleccion de personaje ---*/

    // si limpias la lista y no hay mas referencias
    // el garbage collector se encarga solo de liberar la memoria
    // tambien podrias igualarla a null
    tarjetasAMostrar.Clear();
    listaPersTemp.Clear();
}
else if (entradaDeUsuario == 2)
{
    /* cargas los datos desde un Json y contas los enemigos en HP = 0 */
    /* usa el contadorHistoria para esto */
}
else
{
    /* usas jugador.HP = 0 para cortar el juego */
}

/*--- FIN del control de opcion ---*/



/*--- Desde aquí funciona el juego en si ---*/


    // while( jugador.hp != 0 && gameOver == false)
    //  while( cantidadEnemigosVivos != 0 )         // un metodo que cuente un bool en personaje?
    //    do
    //      metodos
    //    while ( jugador.hp && enemigo.hp != 0 )
    // contadores y bools
while (listaPersonajes[0].Estadisticas.Salud != 0 && gameOver == false)
{
    cantEnemigosVivos = controlDuelo.ContarEnemigosActivos(listaPersonajes); // esto quiza no sirva, otra manera?
    while (cantEnemigosVivos != 0)
    {
    // este switch controla los dialogos antes de cada duelo
        switch (contadorHistoria)
        {
            case 1:
                Console.WriteLine("primera historia antes");
                break;
        }
    
        do
        {
            
        } while (listaPersonajes[0].Estadisticas.Salud != 0 && listaPersonajes[enemigoActivo].Estadisticas.Salud != 0);

    // este switch es provisorio, pones mas dialogos?
        switch (contadorHistoria)
        {
            case 1:
                Console.WriteLine("primera historia despues");
                break;
        }
    }
    // esta variable tendria que estar en un if
    gameOver = true;
}


// las partidas las vas a guardar en un json, como una lista
// podes usar el HP como bandera para ir haciendo los duelos