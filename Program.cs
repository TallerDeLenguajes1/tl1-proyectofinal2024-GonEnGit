
using EspacioPersonajes;
using EspacioDialogos;


// variables
List<personaje> listaDePersonajes = new List<personaje>();

// aquí tenes una salida creativa, duplicá las clases en el arreglo y no necesitas contarlas
string[] clases = { "Guerrero", "Mago", "Arquero", "Bárbaro", "Ladrón", "Guerrero", "Mago", "Arquero", "Bárbaro", "Ladrón",};


// desde acá la intro 
/*
string fraseIntro = dialogosIntro.Intro();

Console.Write("\nNarrador: ");
foreach (var item in fraseIntro)
{
    Console.Write(item);
    //Thread.Sleep(150);
}
*/

// generar los 9 enemigos
for (int i = 1; i < 10; i++)
{
    listaDePersonajes.Add(new personaje(clases[i]));
}
foreach (var item in listaDePersonajes)
{
    Console.WriteLine(item.ToString());
}