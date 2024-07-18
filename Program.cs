
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
int entradaDeUsuario = 0;
int numeroAleatorio;
int ctrlDeFlujo;
int iniciativaJugador;
int iniciativaEnemigo;
int decisionEnemigo;
int danio;
string linea;
string nomJugador;
string nomEnemigo;

int[] orden = new int[9];
string[] texto;
string[] textoEnemigo;
Random rnd = new Random();
Personaje auxiliar;
Duelo HerramientaDuelos = new Duelo();
FabricaDePersonajes HerramientaFabrica = new FabricaDePersonajes();
List<Personaje> listaPersonajes = new List<Personaje>();



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
    List<Personaje> listaPersTemp = new List<Personaje>();
    List<string[]> tarjetasAMostrar = new List<string[]>();

    // inicializas variables
    listaPersTemp = HerramientaFabrica.CreadorDePersonajes(listaPersTemp);

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
    listaPersonajes = HerramientaFabrica.MezclarLista(listaPersonajes, orden, listaPersTemp);

    // cheat code para las pruebas
    Console.WriteLine("Ingresa el ataque de tu personaje:");
    linea = Console.ReadLine();
    linea = HerramientaDuelos.CheatCode(listaPersonajes[0], listaPersonajes[1], linea);
    Console.WriteLine(linea);

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
    /* usas gameOver para cerrar el juego*/
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
    // si contas los enemigos con HP en 0
    // podes usar la misma variable para 
    // la historia, el enemigo activo e incluso el fin del juego
    ctrlDeFlujo = HerramientaDuelos.ContarEnemigosActivos(listaPersonajes); // esto quiza no sirva, otra manera?
    while (ctrlDeFlujo != 9)
    {
    // este switch controla los dialogos antes de cada duelo
        switch (ctrlDeFlujo)
        {
            case 1:
                Console.WriteLine("primera historia antes");
                break;
        }
    
    // desarrollo de los duelos

        // anuncion primer movimiento
        nomJugador = Textos.DevolverNombre(listaPersonajes[0]);
        nomEnemigo = Textos.DevolverNombre(listaPersonajes[ctrlDeFlujo]);
        iniciativaJugador = (int)HerramientaDuelos.CalcularIniciativa(listaPersonajes[0]);
        iniciativaEnemigo = (int)HerramientaDuelos.CalcularIniciativa(listaPersonajes[ctrlDeFlujo]);
        if (iniciativaJugador >= iniciativaEnemigo)
        {
            Console.WriteLine($"{nomJugador} es mas rapido!");
        }
        else
        {
            Console.WriteLine($"{nomEnemigo} es mas rapido!");
        }

        // bucle del duelo
        do
        {
            linea = Textos.Tarjetas(listaPersonajes[0]);
            texto = linea.Split(";");
            linea = Textos.Tarjetas(listaPersonajes[ctrlDeFlujo]);
            textoEnemigo = linea.Split(";");
            for (int indice = 0; indice < 12; indice++)
            {
                if (indice != 3)
                {
                    Console.WriteLine(texto[indice] + " " + textoEnemigo[indice]);
                }
            }

            if (iniciativaJugador >= iniciativaEnemigo)
            {
            // primer turno jugador, segundo turno enemigo

                // decisiones
                Console.WriteLine("Elige una acción:");
                Console.Write("1.Atacar 2.Defender 3.Esperar: ");
                pruebaOpciones = int.TryParse(Console.ReadLine(), out entradaDeUsuario);
                if (pruebaOpciones == false || entradaDeUsuario <= 0 || entradaDeUsuario >= 4) // jugador defiende por defecto
                {
                    entradaDeUsuario = 2;
                }
                decisionEnemigo = HerramientaDuelos.AccionEnemigo();

                // combate
                if (entradaDeUsuario == 1)
                {
                    if (decisionEnemigo == 2)   // 12 - jugador ataca, enemigo defiende
                    {
                        danio = HerramientaDuelos.CalcularDanio(listaPersonajes[0], listaPersonajes[ctrlDeFlujo], true);
                        Console.WriteLine($"{nomJugador} ataca pero {nomEnemigo} pudo defenderse!");
                        Console.WriteLine($"{nomEnemigo} recibe {danio} puntos de daño.");
                    }
                    else                        // 11 y 13 - jugador ataca, enemigo ataca o espera
                    {
                        danio = HerramientaDuelos.CalcularDanio(listaPersonajes[0], listaPersonajes[ctrlDeFlujo], false);
                        Console.WriteLine($"{nomJugador} atacó primero!");
                        Console.WriteLine($"{nomEnemigo} recibe {danio} puntos de daño.");
                        if (listaPersonajes[ctrlDeFlujo].Estadisticas.Salud != 0)
                        {
                            Console.WriteLine($"{nomEnemigo} puede seguir peleando y decidio atacar tambien!");
                            Console.WriteLine($"{nomJugador} recibe {danio} puntos de daño.");
                        }
                        else
                        {
                            Console.WriteLine($"{nomEnemigo} está evaluando la situción...");
                        }
                    }
                }
                else
                {
                    if (decisionEnemigo == 1)
                    {
                        if (entradaDeUsuario == 2)  // 21 - enemigo ataca, jugador defiende
                        {
                            danio = HerramientaDuelos.CalcularDanio(listaPersonajes[ctrlDeFlujo], listaPersonajes[0], true);
                            Console.WriteLine($"{nomEnemigo} ataca pero {nomJugador} pudo defenderse!");
                            Console.WriteLine($"{nomJugador} recibe {danio} puntos de daño.");
                        }
                        else                        // 31 - enemigo ataca, jugador espera
                        {
                            danio = HerramientaDuelos.CalcularDanio(listaPersonajes[ctrlDeFlujo], listaPersonajes[0], false);
                            Console.WriteLine($"{nomEnemigo} ataca pero no se que esta haciendo {nomJugador}...");
                            Console.WriteLine($"{nomJugador} recibe {danio} puntos de daño.");
                        }
                    }
                    else    // 22, 23, 32, 33 - no pasa nada
                    {
                        Console.WriteLine("Narrador: ... Nadie está haciendo nada...");
                        Console.WriteLine("Dev: quizá son metapods...?");
                    }
                }
            }
            else
            {
            //primer turno enemigo, segundo turno jugador
                // decisiones
                decisionEnemigo = HerramientaDuelos.AccionEnemigo();
                Console.WriteLine($"{nomEnemigo} es mas rapido!");
                switch (decisionEnemigo)
                {
                    case 1:
                        linea = "atacar";
                        break;
                    case 2:
                        linea = "defender";
                        break;
                    case 3:
                        linea = "esperar";
                        break;
                }
                Console.WriteLine($"{nomEnemigo} va a {linea}! Elige una accion:");
                Console.Write("1.Atacar 2.Defender 3.Esperar: ");
                pruebaOpciones = int.TryParse(Console.ReadLine(), out entradaDeUsuario);
                if (pruebaOpciones == false || entradaDeUsuario <= 0 || entradaDeUsuario >= 4) // jugador defiende por defecto
                {
                    entradaDeUsuario = 2;
                }

                if (decisionEnemigo == 1)
                {
                    if (decisionEnemigo == 2)   // 12 - enemigo ataca, jugador defiende
                    {
                        danio = HerramientaDuelos.CalcularDanio(listaPersonajes[0], listaPersonajes[ctrlDeFlujo], true);
                        Console.WriteLine($"{nomEnemigo} ataca pero {nomJugador} pudo defenderse!");
                        Console.WriteLine($"{nomJugador} recibe {danio} puntos de daño.");
                    }
                    else                        // 11 y 13 - enemigo ataca, jugador ataca o espera
                    {
                        danio = HerramientaDuelos.CalcularDanio(listaPersonajes[0], listaPersonajes[ctrlDeFlujo], false);
                        Console.WriteLine($"{nomEnemigo} atacó primero!");
                        Console.WriteLine($"{nomJugador} recibe {danio} puntos de daño.");
                        if (listaPersonajes[0].Estadisticas.Salud != 0)
                        {
                            Console.WriteLine($"{nomJugador} puede seguir peleando y decidio atacar tambien!");
                            Console.WriteLine($"{nomEnemigo} recibe {danio} puntos de daño.");
                        }
                        else
                        {
                            Console.WriteLine($"{nomJugador} está esperando...");  /* esperar podria recuperar hp o algo */
                        }
                    }
                }
                else
                {
                    if (entradaDeUsuario == 1)
                    {
                        if (decisionEnemigo == 2)  // 21 - jugador ataca, enemigo defiende
                        {
                            danio = HerramientaDuelos.CalcularDanio(listaPersonajes[ctrlDeFlujo], listaPersonajes[0], true);
                            Console.WriteLine($"{nomJugador} ataca pero {nomEnemigo} pudo defenderse!");
                            Console.WriteLine($"{nomEnemigo} recibe {danio} puntos de daño.");
                        }
                        else                        // 31 - jugador ataca, enemigo espera
                        {
                            danio = HerramientaDuelos.CalcularDanio(listaPersonajes[ctrlDeFlujo], listaPersonajes[0], false);
                            Console.WriteLine($"{nomJugador} ataca pero no se que esta haciendo {nomEnemigo}...");
                            Console.WriteLine($"{nomEnemigo} recibe {danio} puntos de daño.");
                        }
                    }
                    else    // 22, 23, 32, 33 - no pasa nada
                    {
                        Console.WriteLine("Narrador: ... Nadie está haciendo nada...");
                        Console.WriteLine("Dev: quizá son metapods...?");
                    }
                }
            }

        /* --------------------------------hay un error en los daños controlá--------------------------------- */

        /* --------------------------------------- para pruebas --------------------------------------- */
            //listaPersonajes[0].Estadisticas.Salud = 0;
            //ctrlDeFlujo = 9;
            //gameOver = true;
        /* ------------------------------------------ borrar ------------------------------------------ */

        } while (listaPersonajes[0].Estadisticas.Salud != 0 && listaPersonajes[ctrlDeFlujo].Estadisticas.Salud != 0);

    // este switch es provisorio, pones mas dialogos?
        switch (ctrlDeFlujo)
        {
            case 9:
                Console.WriteLine("primera historia despues");
                break;
        }
    }
    // controla si el juego termina
    if (listaPersonajes[0].Estadisticas.Salud == 0 || ctrlDeFlujo == 9)
    {
        gameOver = true;
    }

    // lo unico que falta es dar opciones para guardar partida
}


// las partidas las vas a guardar en un json, como una lista
// podes usar el HP como bandera para ir haciendo los duelos