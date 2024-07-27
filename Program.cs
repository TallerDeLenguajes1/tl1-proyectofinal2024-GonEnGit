
// un visualizador de json mucho mejor: https://jsonviewer.stack.hu/

using System.Data;
using Microsoft.VisualBasic;
using System.Runtime.InteropServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.CompilerServices;

using EspacioAPI;
using EspacioJuego;
using EspacioArchivos;
using EspacioPersonajes;

// Instancias de clases y herramientas
Personaje auxiliar;
Partida HerramientaPartida = new Partida();
Historial HerramientaHistorial = new Historial();
FabricaDePersonajes HerramientaFabrica = new FabricaDePersonajes();



// variables
bool gameOver = false, pruebaOpciones = false, critico;
int entradaDeUsuario = 0, numeroAleatorio;
int iniciativaJugador, iniciativaEnemigo, decisionEnemigo;
int danio, ctrlDeFlujo;
char continuar = 'S';
string linea, linea2, cartaIzq, cartaDer;
string nomJugador, nomEnemigo, nomPartida = "";
Mazo nuevoMazo = await API.ObtenerIdMazoAsync();
ListaCartas cartasNuevas = new ListaCartas();

// Arreglos
string[] texto, texto2, textoEnemigo;
string[] partesCartaIzq, partesCartaDer;

// Listas
List<Historial> historialCargado;
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
Thread.Sleep(1500);
Console.WriteLine("");

/*--- FIN de la intro ---*/


/*--- Impresion del Menú ---*/
linea = Textos.MenuPrincipal();
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
/*--- FIN del menu ---*/


/*--- inicio del control de opcion ---*/
do
{
    // controla que se ingrese lo correcto
    while (pruebaOpciones == false || entradaDeUsuario <= 0 || entradaDeUsuario >= 5)
    {
        pruebaOpciones = int.TryParse(Console.ReadLine(), out entradaDeUsuario);
        if (pruebaOpciones == false || entradaDeUsuario <= 0 || entradaDeUsuario >= 5)
        {
            Console.WriteLine("");
            Console.Write("Ingresa una opcion del menú: ");
        }
    }
    Console.WriteLine("");

    switch (entradaDeUsuario)
    {
        case 1:
            // declaras listas provisorias para liberarlas despues
            List<Personaje> listaPersTemp = new List<Personaje>();
            List<string[]> tarjetasAMostrar = new List<string[]>();
        
            // inicializas variables
            listaPersTemp = HerramientaFabrica.CreadorDePersonajes(listaPersTemp);
        
            // crea carpetas
            Console.Write("Ingresa un nombre para la partida: ");
            nomPartida = Console.ReadLine();
            HerramientaPartida.CrearCarpetas();
        
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
                linea = ""; // si no limpias 'linea' las tarjetas se rompen
            }

            // ahora con esa lista podes concatenar cada linea e imprimirlas
            for (int indiceLinea = 0; indiceLinea < texto.Length; indiceLinea++)
            {
                for (int indiceTarjeta = 0; indiceTarjeta < 3; indiceTarjeta++)
                {
                    linea = linea + "   " + tarjetasAMostrar[indiceTarjeta][indiceLinea];
                }
                Console.WriteLine(linea);
                linea = "";
            }
            // no es lo mas lindo pero tenes que mostrar 5 personajes, 2 quedan abajo
            for (int indiceLinea = 0; indiceLinea < texto.Length; indiceLinea++)
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

            // armas la lista mezclada
            listaPersonajes = HerramientaFabrica.MezclarLista(entradaDeUsuario, listaPersTemp);

            // cheat code para las pruebas
            Console.WriteLine("Ingresa el ataque de tu personaje:");
            linea = Console.ReadLine();
            linea = Duelo.CheatCode(listaPersonajes[0], listaPersonajes[1], linea);
            if (linea == "GodMode activo")
            {
                Console.WriteLine(linea);
            }
            else
            {
                texto = linea.Split(";");
                foreach (string frase in texto)
                {
                    Console.WriteLine(frase);
                }
            }
            Console.WriteLine("");
            /*--- Fin de seleccion de personaje ---*/

            continuar = 'N';

            // '.Clear()' y/o '= null' habilita las listas para el garbage collector
            tarjetasAMostrar.Clear();
            listaPersTemp.Clear();
            break;

        case 2:
            linea = HerramientaPartida.ObtenerNombresDePartidas();
            if (linea == "vacio;")
            {
                Console.WriteLine("Narrador: No hay partidas guardadas aún.");
                Console.WriteLine("Dev: así que te vamos a obligar a inicar un juego nuevo!");
                entradaDeUsuario = 1; continuar = 'S'; pruebaOpciones = true;
            }
            else
            {
                Console.WriteLine("/* --- Partidas Guardadas --- */\n");
                texto = linea.Split(";");
                for (int indice = 1; indice < texto.Length - 1; indice++)
                {
                    Console.WriteLine($"            {indice}. " + texto[indice]);
                }

                do
                {
                    Console.Write("Ingresá el numero de partida: ");
                    pruebaOpciones = int.TryParse(Console.ReadLine(), out entradaDeUsuario);
                    if (!pruebaOpciones || entradaDeUsuario <= 0 || entradaDeUsuario >= texto.Length - 1)
                    {
                        Console.WriteLine("No ingresaste un numero que corresponda a alguna partida.");
                    }
                } while (!pruebaOpciones || entradaDeUsuario <= 0 || entradaDeUsuario >= texto.Length - 1);

                nomPartida = texto[entradaDeUsuario]; continuar = 'N';
                listaPersonajes = HerramientaPartida.CargarPartida(nomPartida);
            }
            break;

        case 3:
            historialCargado = HerramientaHistorial.LeerHistorial();
            Console.WriteLine("/*.... Salón de Ganadores ...*/\n");
            foreach (Historial entrada in historialCargado)
            {
                linea = entrada.FechaVictoria.ToString("dd 'de' MMM 'del' yyyy 'a las' HH:mm");
                Console.WriteLine($"{entrada.NombreGanador} ganó en {linea}");
            }
            Console.Write("\nContinuar con otra opcion? (S/N) ");
            do
            {
                pruebaOpciones = char.TryParse(Console.ReadLine(), out continuar);
                continuar = char.ToUpper(continuar);
                if (!pruebaOpciones || continuar != 'S' && continuar != 'N')
                {
                    Console.WriteLine("Ingresa 'S' para elegir otra opcion.");
                    Console.Write("O 'N' para salir del juego: ");
                }
            } while (!pruebaOpciones || continuar != 'S' && continuar != 'N');
            if (continuar == 'S')
            {
                pruebaOpciones = false; // sin esto, la esntrada de opcion se rompe
            }
            else
            {
                entradaDeUsuario = 4;
            }
            historialCargado.Clear();
            break;

        case 4:
            Console.WriteLine("Hasta la proxima!");
            gameOver = true; continuar = 'N';
            break;
    }
} while (continuar == 'S');
/*--- FIN del control de opcion ---*/


/*--- desarrollo del juego ---*/
while (gameOver == false) // podrias sacar el .Salud != 0?
{
    // si contas los enemigos con HP en 0, podes usar la misma variable para 
    // la historia, el enemigo activo e incluso el fin del juego
    ctrlDeFlujo = Duelo.ContarEnemigosVencidos(listaPersonajes);    // probablemente algo esta de mas con ctrDeFlujo
    nomJugador = Textos.DevolverNombre(listaPersonajes[0]);
    nomEnemigo = Textos.DevolverNombre(listaPersonajes[ctrlDeFlujo]);
    cartasNuevas.cards = await API.ObtenerCartasAsync(nuevoMazo.deck_id); // tomas 2 cartas del mazo
    while (ctrlDeFlujo <= 9)
    {
        // dialogos antes de los duelos
        switch (ctrlDeFlujo)
        {
            case 2: // esto de las historias lo va a tener que pensar mejor
                Console.WriteLine("primera historia antes");
                break;
        }

// si queres poner un tutorial, cuando ctrDeFlujo = 1 tenes que escribir un par de cosas
// mostrar cartas, seguir explicacion, mostrarlas dadas vuelta y el resultado

    // desarrollo de los duelos
        if (ctrlDeFlujo == 1)   // tutorial antes del 1er duelo
        {
            linea = Textos.TutorialCartas();
            texto = linea.Split(";");
            for (int indice = 0; indice < 2; indice++)
            {
                Console.WriteLine(texto[indice]);
            }
        }

// muestra carta con reverso
        cartaIzq = Textos.ArmarCarta(cartasNuevas.cards[0]);
        partesCartaIzq = cartaIzq.Split(";");
        cartaDer = Textos.TraerReverso();
        partesCartaDer = cartaDer.Split(";");
        for (int indice = 0; indice < partesCartaDer.Length; indice++)
        {
            if (cartasNuevas.cards[0].suit == "HEARTS" || cartasNuevas.cards[0].suit == "DIAMONDS")
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;      // reemplazas el rojo acá o los duelos?
                Console.Write(partesCartaIzq[indice]);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(partesCartaIzq[indice]);
            }
            if (cartasNuevas.cards[1].suit == "HEARTS" || cartasNuevas.cards[1].suit == "DIAMONDS")
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;      // reemplazas el rojo acá o los duelos?
                Console.WriteLine(partesCartaDer[indice]);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Blue;      // reemplazas el rojo acá o los duelos?
                Console.WriteLine(partesCartaDer[indice]);
            }
        }
        Console.ForegroundColor = ConsoleColor.White;

        if (ctrlDeFlujo == 1)
        {
            Console.Write("\nPresiona una tecla para continuar.");
            Console.ReadKey();
            for (int indice = 2; indice < 5; indice++)
            {
                Console.WriteLine(texto[indice]);
                Thread.Sleep(1000);
            }
        }
// se elige una carta
        do
        {
            if (ctrlDeFlujo == 1)
            {
                Console.Write("Elije una carta para continuar (1/2): ");
            }
            else
            {
                Console.Write("Elije una carta: ");
            }
            pruebaOpciones = int.TryParse(Console.ReadLine(), out entradaDeUsuario);
            if (!pruebaOpciones || entradaDeUsuario != 1 && entradaDeUsuario != 2)
            {
                Console.WriteLine("Solo podes elegir entre estas 2 cartas.");
            }
        } while (!pruebaOpciones || entradaDeUsuario != 1 && entradaDeUsuario != 2);

// ambas cartas boca arriba
        cartaDer = Textos.ArmarCarta(cartasNuevas.cards[1]);
        partesCartaDer = cartaDer.Split(";");
        Console.WriteLine("");
        for (int indice = 0; indice < partesCartaDer.Length; indice++)
        {
            if (cartasNuevas.cards[0].suit == "HEARTS" || cartasNuevas.cards[0].suit == "DIAMONDS")
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;      // reemplazas el rojo acá o los duelos?
                Console.Write(partesCartaIzq[indice]);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write(partesCartaIzq[indice]);
            }
            if (cartasNuevas.cards[1].suit == "HEARTS" || cartasNuevas.cards[1].suit == "DIAMONDS")
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;      // reemplazas el rojo acá o los duelos?
                Console.WriteLine(partesCartaDer[indice]);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Blue;      // reemplazas el rojo acá o los duelos?
                Console.WriteLine(partesCartaDer[indice]);
            }
        }
        Console.WriteLine("");
        Console.ForegroundColor = ConsoleColor.White;
        if (ctrlDeFlujo == 1)
        {
            for (int indice = 5; indice < texto.Length; indice++)
            {
                Console.WriteLine(texto[indice]);
            }
            Console.Write("\nPresiona una tecla para continuar.");
            Console.ReadKey();
        }
    // estas 2 dependen de la eleccion
        if (entradaDeUsuario == 1)
        {
            iniciativaJugador = Duelo.CalcularIniciativa(cartasNuevas.cards[0]); // como incluis el color, otra variable?
            iniciativaEnemigo = Duelo.CalcularIniciativa(cartasNuevas.cards[1]);
        }
        else
        {
            iniciativaJugador = Duelo.CalcularIniciativa(cartasNuevas.cards[1]); // como incluis el color, otra variable?
            iniciativaEnemigo = Duelo.CalcularIniciativa(cartasNuevas.cards[0]);
        }
    // por ultimo anuncia quien va primero
        if (iniciativaJugador >= iniciativaEnemigo)
        {
            Console.WriteLine($"{nomJugador} mueve primero!");
        }
        else
        {
            Console.WriteLine($"{nomEnemigo} mueve primero!");
        }

        // bucle del duelo
        do
        {
            linea = Textos.Tarjetas(listaPersonajes[0]);
            texto = linea.Split(";");
            linea2 = Textos.Tarjetas(listaPersonajes[ctrlDeFlujo]);
            textoEnemigo = linea2.Split(";");
            for (int indice = 0; indice < 11; indice++)
            {
                if (indice != 3)
                {
                    Console.WriteLine(texto[indice] + " " + textoEnemigo[indice]); // aqui hay un index out of range ahora
                }
            }

            if (iniciativaJugador >= iniciativaEnemigo) // primer turno jugador, segundo turno enemigo
            {
                // decisiones
                Console.WriteLine("Elige una acción:");
                Console.Write("1.Atacar 2.Defender 3.Esperar: ");
                pruebaOpciones = int.TryParse(Console.ReadLine(), out entradaDeUsuario);
                if (pruebaOpciones == false || entradaDeUsuario <= 0 || entradaDeUsuario >= 4) // jugador defiende por defecto
                {
                    entradaDeUsuario = 2;
                }
                decisionEnemigo = Duelo.AccionEnemigo();

                // combate
                if (entradaDeUsuario == 1)
                {
                    if (decisionEnemigo == 2)   // 12 - jugador ataca, enemigo defiende
                    {
                        critico = Duelo.DecidirCritico(listaPersonajes[0]);
                        danio = Duelo.CalcularDanio(listaPersonajes[0], listaPersonajes[ctrlDeFlujo], true, critico);
                        Console.WriteLine($"{nomJugador} ataca pero {nomEnemigo} pudo defenderse!");
                        if (critico)
                        {
                            Console.WriteLine("Aun así fue un golpe critico!");
                        }
                        Console.WriteLine($"{nomEnemigo} recibe {danio} puntos de daño.");
                    }
                    else                        // 11 y 13 - jugador ataca, enemigo ataca o espera
                    {
                        critico = Duelo.DecidirCritico(listaPersonajes[ctrlDeFlujo]);
                        danio = Duelo.CalcularDanio(listaPersonajes[0], listaPersonajes[ctrlDeFlujo], false, critico);
                        if (critico)
                        {
                            Console.WriteLine($"{nomJugador} atacá con todas sus fuerzas!");
                            Console.WriteLine($"{nomEnemigo} recibirá daño extra!");
                        }
                        else
                        {
                            Console.WriteLine($"{nomJugador} ataca!");
                        }
                        Console.WriteLine($"{nomEnemigo} recibe {danio} puntos de daño.");

                        // si el enemigo muere se corta el duelo
                        if (listaPersonajes[ctrlDeFlujo].Estadisticas.Salud != 0)
                        {
                            if (decisionEnemigo == 2)   // si no muere ataca
                            {
                                Console.WriteLine($"{nomEnemigo} puede seguir peleando y decidio atacar tambien!");
                                Console.WriteLine("Aun así no podra hacer mas daño esta vez...");
                                Console.WriteLine($"{nomJugador} recibe {danio} puntos de daño.");
                            }
                            else                        // o aclaras que espera
                            {
                                Console.WriteLine($"{nomEnemigo} está evaluando la situción... creo?");
                            }
                        }
                    }
                }
                else    // jugador espera o defiende
                {
                    if (decisionEnemigo == 1)
                    {
                        if (entradaDeUsuario == 2)  // 21 - enemigo ataca, jugador defiende
                        {
                            Console.WriteLine($"{nomEnemigo} ataca pero {nomJugador} pudo defenderse!");
                            critico = Duelo.DecidirCritico(listaPersonajes[ctrlDeFlujo]);
                            if (critico)
                            {
                                Console.WriteLine("Aun así fue un golpe critico!");
                            }
                            danio = Duelo.CalcularDanio(listaPersonajes[ctrlDeFlujo], listaPersonajes[0], true, critico);
                            Console.WriteLine($"{nomJugador} recibe {danio} puntos de daño.");
                        }
                        else                        // 31 - enemigo ataca, jugador espera
                        {
                            Console.WriteLine($"{nomEnemigo} atacá ... y tu estas esperando...?");
                            critico = Duelo.DecidirCritico(listaPersonajes[ctrlDeFlujo]);
                            if (critico)
                            {
                                Console.WriteLine($"{nomEnemigo} estaba preparando un ataque especial, recibiras mas daño!");
                            }
                            danio = Duelo.CalcularDanio(listaPersonajes[ctrlDeFlujo], listaPersonajes[0], false, critico);
                            Console.WriteLine($"{nomJugador} recibe {danio} puntos de daño.");
                        }
                    }
                    else    // 22, 23, 32, 33 - no pasa nada
                    {
                        if (entradaDeUsuario == 2 && decisionEnemigo == 2)
                        {
                            Console.WriteLine("Narrador: ... Nadie está haciendo nada...");
                            Console.WriteLine("Dev: quizá son metapods...?");
                        }
                        else
                        {
                            Console.WriteLine("Narrador: ...Nadie está haciendo nada...");
                            Console.WriteLine("Dev: Se aburrieron antes que nosotros");
                        }
                    }
                }
            }
            else        // primer turno enemigo, segundo turno jugador
            {
                // decisiones
                decisionEnemigo = Duelo.AccionEnemigo();
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
                        critico = Duelo.DecidirCritico(listaPersonajes[0]);
                        danio = Duelo.CalcularDanio(listaPersonajes[0], listaPersonajes[ctrlDeFlujo], true, critico);
                        Console.WriteLine($"{nomEnemigo} ataca pero {nomJugador} pudo defenderse!");
                        if (critico)
                        {
                            Console.WriteLine("Aun así fue un golpe critico!");
                        }
                        Console.WriteLine($"{nomJugador} recibe {danio} puntos de daño.");
                    }
                    else                        // 11 y 13 - enemigo ataca, jugador ataca o espera
                    {
                        critico = Duelo.DecidirCritico(listaPersonajes[0]);
                        danio = Duelo.CalcularDanio(listaPersonajes[0], listaPersonajes[ctrlDeFlujo], false, critico);
                        if (critico)
                        {
                            Console.WriteLine($"{nomEnemigo} atacá con todas sus fuerzas!");
                            Console.WriteLine($"{nomJugador} recibirá daño extra!");
                        }
                        else
                        {
                            Console.WriteLine($"{nomEnemigo} ataca!");
                        }
                        Console.WriteLine($"{nomJugador} recibe {danio} puntos de daño.");

                        if (listaPersonajes[0].Estadisticas.Salud != 0)
                        {
                            if (entradaDeUsuario == 1)
                            {
                                Console.WriteLine($"{nomJugador} puede seguir peleando y decidio atacar tambien!");
                                Console.WriteLine("Aun así no podra hacer mas daño esta vez...");
                                Console.WriteLine($"{nomEnemigo} recibe {danio} puntos de daño.");
                            }
                            else
                            {
                                Console.WriteLine($"{nomJugador} está esperando...");  /* esperar podria recuperar hp o algo */
                            }
                        }
                    }
                }
                else    // enemigo defiende o espera
                {
                    if (entradaDeUsuario == 1)
                    {
                        if (decisionEnemigo == 2)  // 21 - jugador ataca, enemigo defiende
                        {
                            critico = Duelo.DecidirCritico(listaPersonajes[ctrlDeFlujo]);
                            danio = Duelo.CalcularDanio(listaPersonajes[ctrlDeFlujo], listaPersonajes[0], true, critico);
                            Console.WriteLine($"{nomJugador} ataca pero {nomEnemigo} pudo defenderse!");
                            if (critico)
                            {
                                Console.WriteLine("Aun así fue un golpe critico!");
                            }
                            Console.WriteLine($"{nomEnemigo} recibe {danio} puntos de daño.");
                        }
                        else                        // 31 - jugador ataca, enemigo espera
                        {
                            critico = Duelo.DecidirCritico(listaPersonajes[ctrlDeFlujo]);
                            danio = Duelo.CalcularDanio(listaPersonajes[ctrlDeFlujo], listaPersonajes[0], false, critico);
                            Console.WriteLine($"{nomJugador} ataca pero no se que esta haciendo {nomEnemigo}...");
                            if (critico)
                            {
                                Console.WriteLine("Incluso recibirá un golpe critico!");
                            }
                            Console.WriteLine($"{nomEnemigo} recibe {danio} puntos de daño.");
                        }
                    }
                    else    // 22, 23, 32, 33 - no pasa nada
                    {
                        if (entradaDeUsuario == 2 && decisionEnemigo == 2)
                        {
                            Console.WriteLine("Narrador: ... Nadie está haciendo nada...");
                            Console.WriteLine("Dev: quizá son metapods...?");
                        }
                        else
                        {
                            Console.WriteLine("Narrador: ... Nadie está haciendo nada...");
                            Console.WriteLine("Dev: se aburrieron antes que nosotros?");
                        }
                    }
                }
            }
        } while (listaPersonajes[0].Estadisticas.Salud != 0 && listaPersonajes[ctrlDeFlujo].Estadisticas.Salud != 0);

    /*---- terminado el duelo controlas quien perdió y como seguir ----*/
        if (listaPersonajes[ctrlDeFlujo].Estadisticas.Salud == 0)   // si el jugador gana
        {
            Console.WriteLine($"{nomEnemigo} sufrió demasiado daño... ");
            Console.WriteLine("Dev: habrá que buscar a alguien mas...\n");
            ctrlDeFlujo += 1;

            if (ctrlDeFlujo != 9)
            {
                // guardado de partida en json
                linea = Textos.MenuDeGuardado();
                texto = linea.Split(";");
                for (int indice = 0; indice < texto.Count(); indice++)
                {
                    if (indice == (texto.Count() - 1))
                    {
                        Console.Write(texto[indice]);
                    }
                    else
                    {
                        Console.WriteLine(texto[indice]);
                    }
                }
                do
                {
                    pruebaOpciones = int.TryParse(Console.ReadLine(), out entradaDeUsuario);
                    if (!pruebaOpciones || entradaDeUsuario <= 0 || entradaDeUsuario >= 4)
                    {
                        Console.WriteLine("Dev: no puedo dejar que pongas cualquier cosa siempre...");
                        Console.Write("Ingresa un opcion valida: ");
                    }
                } while (!pruebaOpciones || entradaDeUsuario <= 0 || entradaDeUsuario >= 4);
                gameOver = HerramientaPartida.EjecutarOpcion(listaPersonajes, nomPartida, entradaDeUsuario);
                if (gameOver == true)
                {
                    ctrlDeFlujo = 21;   // por que 21? si
                }
                Console.WriteLine("Partida guardada."); // esto lo podrias mejorar con algun control

                // dialogos despues de los duelos, quiza no?
                // podrias poner esto arriba, del 8 al 9 en el switch
                // los dialogos para despues del duelo anterior
                // se imprimen antes de los dialogos que preceden al duelo siguiente
                switch (ctrlDeFlujo)
                {
                    case 9:
                        Console.WriteLine("primera historia despues");
                        break;
                }
            }
            else    // esta sería la ultima rama, cuando ganas todos los duelos
            {
                Console.WriteLine("Dev: Parece que el jugador pudo ganar todos los duelos...");
                Console.WriteLine("Narrador: y eso no es bueno?");
                Console.WriteLine("Dev: si pero ahora hay que cerrar el juego...");
                Console.Write("Ingresa un nombre para agregar a los ganadores: ");
                linea = Console.ReadLine();
                HerramientaHistorial.AgregarAlHistorial(linea);
                gameOver = true;
            }
        }
        else
        {
            Console.WriteLine($"Narrador: {nomJugador} acaba de morir...");
            Console.WriteLine($"Dev: Supongo que estoy haciendo esto demaciado dificil...");

            do
            {
                Console.WriteLine("Dev: queres intentar de nuevo? (S/N) ");
                pruebaOpciones = char.TryParse(Console.ReadLine(), out continuar);
                if (pruebaOpciones)
                {
                    continuar = char.ToUpper(continuar);
                    if (continuar != 'S' && continuar != 'N')
                    {
                        // -------- falta completar ---------
                    }
                    else
                    {
                        Console.WriteLine("Opcion incorrecta.");
                    }
                }
                else
                {
                    Console.WriteLine("Opcion incorrecta.");
                }
            } while (!pruebaOpciones && continuar != 'S' && continuar != 'N');

            if (continuar == 'S')
            {
                listaPersonajes[0].Estadisticas.Salud = 100;// esto lo vas a tener que registrar de alguna forma, podrias cargar la ultima partida tambien...
                gameOver = false;
            }
            else
            {
                gameOver = true;
            }
        }
    }
}