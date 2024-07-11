
// un visualizador de json mucho mejor: https://jsonviewer.stack.hu/

using Microsoft.VisualBasic;
using System.Runtime.InteropServices;

using EspacioTextos;
using EspacioPersonajes;


// variables
bool Aux = true;
bool pruebaOpciones = false;
int opcion = 0;
int contadorHistoria = 0;
string entradaDeUsuario;
string linea;
string[] texto;
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

/*--- FIN de la intro ---*/

/*--- Menú y control de opcion ---*/

// imprime el Menú
linea = Textos.Menu();
texto = linea.Split(';');
foreach (var renglon in texto)
{
    Console.WriteLine(renglon);
}
// controla que se ingrese lo correcto
while (!pruebaOpciones || opcion <= 0 || opcion >= 4)
{
    pruebaOpciones = int.TryParse(Console.ReadLine(), out opcion);
    if (pruebaOpciones == false || opcion <= 0 || opcion >= 4)
    {
        Console.WriteLine("Ingrese un numero valido.");
    }
}
// inicia el juego segun la opcion
if (opcion == 1)
{
    listaPersonajes = personaje.FabricaDePersonajes(listaPersonajes);
    foreach (var iter in listaPersonajes)
    {
        linea = Textos.Tarjetas(iter);
        texto = linea.Split(";");
        foreach (string frase in texto)
        {
            Console.WriteLine(frase);
        }
    }
}
else if (opcion == 2)
{
    /* cargas los datos desde un Json y contas los enemigos en HP = 0 */
    /* usa el contadorHistoria para esto */
}
else
{
    /* usas jugador.HP = 0 para cortar el juego */
}

/*--- FIN del menpu y control de opcion ---*/

/*--- Desde aquí funciona el juego en si ---*/

while (/* HP jugador != 0 */Aux)
{
// este switch controla los dialogos antes de cada duelo
    switch (contadorHistoria)
    {
        case 1:
            Console.WriteLine("primera historia antes");
            break;
    }

    // if? for?

// este switch es provisorio, pones mas dialogos?
    switch (contadorHistoria)
    {
        case 1:
            Console.WriteLine("primera historia despues");
            break;
    }

    Aux = false;
}


// generar los 9 enemigos
/*
for (int i = 0; i < 10; i++)
{
    listaDePersonajes.Add(new personaje(clases[i]));
}
foreach (var item in listaDePersonajes)
{
    Console.WriteLine(item.devolverDatos());
    Console.WriteLine(item.DevolverEstadisticas());
}
*/

// las partidas las vas a guardar en un json, como una lista
// podes usar el HP como bandera para ir haciendo los duelos