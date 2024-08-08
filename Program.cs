
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
Random rnd = new Random();
FabricaDePersonajes HerramientaFabrica = new FabricaDePersonajes();
FabricaDeArtefactos HerramientaFabricaArt = new FabricaDeArtefactos();

// variables
bool gameOver = false, critico;
int entradaDeUsuario = 99999, espaciosAIzq = Console.WindowWidth;
int iniciativaJugador, iniciativaEnemigo, decisionEnemigo;
int danio, ctrlDeFlujo = 1, continuar = 19;
int saludGuardadaJu, saludGuardadaEn;
string linea = "", cartaIzq, cartaDer;
string nomJugador, nomEnemigo, nomPartida = "";
Mazo nuevoMazo = await API.ObtenerIdMazoAsync();
ListaCartas cartasNuevas = new ListaCartas();

// Arreglos
string[] texto, textoEnemigo;
string[] partesCartaIzq, partesCartaDer;

// Listas
List<Historial> historialCargado;
List<Personaje> listaPersonajes = new List<Personaje>();
List<Artefacto> cofre = new List<Artefacto>();
List<Artefacto> inventario = new List<Artefacto>();
List<string[]> tarjetasAMostrar = new List<string[]>();


/* ---- Desarrollo del juego ---- */

texto = Textos.Introduccion().Split(";");       /* --- Inicio de la intro --- */
for (int indice = 0; indice < texto.Length; indice++)
{
    if (indice == 1)
    {
        Console.Write(texto[indice]);
    }
    else if (indice <= 3)
    {
        foreach (char letra in texto[indice])
        {
            Console.Write(letra);
            Thread.Sleep(150);
        }
    }
    else
    {
        Console.WriteLine(texto[indice]);
        Thread.Sleep(1300);
    }
}
texto = Textos.Titulo().Split(";");
for (int indice = 0; indice < texto.Length; indice++)
{
    if (indice <= 6)
    {
        Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq, texto[indice]));
    }
    else
    {
        Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq, texto[indice]));
    }
}
Thread.Sleep(1500);
Console.WriteLine("");                         /* --- Fin de la intro --- */

texto = Textos.MenuPrincipal();     /* --- Inicio del menú principal --- */
foreach (string parte in texto)
{
        Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,parte));
}                                   /* --- Fin del menpu principal --- */

do              /*--- inicio del control de opcion ---*/
{
    while (entradaDeUsuario == 99999 && continuar == 19)
    {
        Console.Write("\n" + Textos.CentrarRenglon(espaciosAIzq, "Ingresá una opción del menú: "));
        entradaDeUsuario = Partida.ControlDeOpciones(Console.ReadLine(), 3, 0);
        if (entradaDeUsuario == 99999)
        {
            Console.Write(Textos.CentrarRenglon(espaciosAIzq, "Ingresá una opcion valida."));
        }
    }
    Console.WriteLine("");
    switch (entradaDeUsuario)
    {
        case 1:
            List<Personaje> listaPersTemp = HerramientaFabrica.CreadorDePersonajes();
            cofre = HerramientaFabricaArt.CreadorDeArtefactos(cofre);

            Console.Write(Textos.CentrarRenglon(espaciosAIzq, "Ingresa un nombre para la partida: "));
            nomPartida = Console.ReadLine();
            Partida.CrearCarpetas();

            texto = Textos.IntroSeleccionPers().Split(";");  /* --- Dialogos seleccion --- */
            foreach (string renglon in texto)
            {
                Console.WriteLine(renglon);
                Thread.Sleep(1300);
            }

            Console.WriteLine(" ");
            Textos.ArmarListaTarjetasPers(tarjetasAMostrar, listaPersTemp);
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < tarjetasAMostrar[0].Length; j++)
                {
                    Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq, tarjetasAMostrar[i][j]));
                }
                Console.WriteLine(" ");
            }

            Console.Write("Numero: ");/*--- Seleccion de personaje ---*/
            do
            {
                entradaDeUsuario = Partida.ControlDeOpciones(Console.ReadLine(), 4, 0);
                if (entradaDeUsuario == 99999)
                {
                    Console.Write("Elige uno de estos 5 personajes: ");
                }
            } while (entradaDeUsuario == 99999);

            listaPersonajes = HerramientaFabrica.MezclarLista(entradaDeUsuario - 1, listaPersTemp);

            Console.Write("\nIngresa el ataque de tu personaje: "); // cheat code
            Console.WriteLine(Duelo.CheatCode(listaPersonajes[0], listaPersonajes[1], Console.ReadLine()) + "\n");
            /*--- Fin de seleccion de personaje ---*/

            continuar = 21;
            listaPersTemp.Clear();
            tarjetasAMostrar.Clear();
            HerramientaFabrica = null;
            break;
        case 2:
            texto = Partida.ObtenerNombresDePartidas().Split(";");
            if (texto[0] == "vacio" && texto[1] == "")
            {
                Console.WriteLine("Narrador: No hay partidas guardadas aún.");
                Console.WriteLine("Dev: así que te vamos a obligar a inicar un juego nuevo!");
                entradaDeUsuario = 1;
            }
            else
            {
                Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,"/* --- Partidas Guardadas --- */\n"));
                for (int indice = 1; indice < texto.Length - 1; indice++)
                {
                    Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,$"{indice}. " + texto[indice]));
                }

                do
                {
                    Console.Write("\nIngresá el numero de partida: ");
                    entradaDeUsuario = Partida.ControlDeOpciones(Console.ReadLine(), 5, texto.Length - 1);
                    if (entradaDeUsuario == 99999)
                    {
                        Console.WriteLine("No ingresaste un numero que corresponda a alguna partida.");
                    }
                } while (entradaDeUsuario == 99999);

                nomPartida = texto[entradaDeUsuario]; continuar = 21;   // el 21 es arbitrario, solo importa que rompe el while
                listaPersonajes = Partida.CargarPartida<Personaje>(nomPartida, "personajes");
                cofre = Partida.CargarPartida<Artefacto>(nomPartida, "cofre");
                inventario = Partida.CargarPartida<Artefacto>(nomPartida, "inventario");
            }
            break;
        case 3:
            historialCargado = Historial.LeerHistorial();
            if (historialCargado.Count() != 0)
            {
                Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,"/*.... Salón de Ganadores ...*/\n"));
                foreach (Historial entrada in historialCargado)
                {
                    linea = entrada.FechaVictoria.ToString("dd 'de' MMM 'del' yyyy 'a las' HH:mm");
                    Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,$"{entrada.NombreGanador} ganó en {linea}"));
                }
            }
            else
            {
                Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,"Aun no hay gandores.\n"));
            }
            do
            {
                Console.Write("\n" + Textos.CentrarRenglon(espaciosAIzq,"Continuar con otra opcion? (S/N) "));
                continuar = Partida.ControlDeOpciones(Console.ReadLine(), 7, 0); // s = letra n° 19, n = letra n° 14
                if (continuar == 99999)
                {
                    Console.WriteLine("\n" + Textos.CentrarRenglon(espaciosAIzq,"Tengo que tomar eso como un no?"));
                }
            } while (continuar == 99999);
            if (continuar == 19)
            {
                entradaDeUsuario = 99999;
            }
            else
            {
                entradaDeUsuario = 4;
            }
            historialCargado.Clear();
            break;
        case 4:
            gameOver = true; continuar = 21;
            break;
    }
} while (continuar == 19 || continuar == 14);    /*--- FIN del control de opcion ---*/

while (gameOver == false && ctrlDeFlujo <= 9)    /*--- Desarrollo del juego ---*/
{
    ctrlDeFlujo = Duelo.ContarEnemigosVencidos(listaPersonajes); // cant de enemigos vencidos, sirve para controlar casi todo
    nomJugador = Textos.DevolverNombre(listaPersonajes[0]);
    nomEnemigo = Textos.DevolverNombre(listaPersonajes[ctrlDeFlujo]);
    saludGuardadaJu = listaPersonajes[0].Estadisticas.Salud;
    saludGuardadaEn = listaPersonajes[ctrlDeFlujo].Estadisticas.Salud;
    cartasNuevas.cards = await API.ObtenerCartasAsync(nuevoMazo.deck_id);

    switch (ctrlDeFlujo) // dialogos
    {
        case 2:
            Console.WriteLine("segunda historia");
            break;
    }
    //ctrlDeFlujo = 9; // para pruebas

// desarrollo de los duelos
    if (ctrlDeFlujo == 1)   // 1ra parte del tutorial
    {
        texto = Textos.TutorialCartas().Split(";");
        for (int indice = 0; indice < 2; indice++)
        {
            Console.WriteLine(texto[indice]);
            Thread.Sleep(1500);
        }
    }
    cartaIzq = Textos.ArmarCarta(cartasNuevas.cards[0]); // muestra carta con reverso
    cartaDer = Textos.TraerReverso();
    partesCartaIzq = cartaIzq.Split(";");
    partesCartaDer = cartaDer.Split(";");
    for (int indice = 0; indice < partesCartaIzq.Length; indice++)
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.Write(Textos.CentrarRenglon(espaciosAIzq - 19, partesCartaIzq[indice]));
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("     " + partesCartaDer[indice]);
    }
    Console.ForegroundColor = ConsoleColor.White;

    if (ctrlDeFlujo == 1) // 2da parte del tutorial
    {
        Console.Write("\nPresiona una tecla para continuar.");
        Console.ReadKey();
        for (int indice = 2; indice < 5; indice++)
        {
            Console.WriteLine(texto[indice]);
            Thread.Sleep(1000);
        }
    }
    do  // seleccion de carta
    {
        if (ctrlDeFlujo == 1)
        {
            Console.Write("Elije una carta para continuar (1/2): ");
        }
        else
        {
            Console.Write("Elije una carta: ");
        }
        entradaDeUsuario = Partida.ControlDeOpciones(Console.ReadLine(), 1, 0);
        if (entradaDeUsuario == 99999)
        {
            Console.WriteLine("Solo podes elegir entre estas 2 cartas.");
        }
    } while (entradaDeUsuario == 99999);
    cartaDer = Textos.ArmarCarta(cartasNuevas.cards[1]); // muestra cartas boca arriba
    partesCartaDer = cartaDer.Split(";");
    Console.WriteLine("");
    for (int indice = 0; indice < partesCartaDer.Length; indice++)
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.Write(Textos.CentrarRenglon(espaciosAIzq - 19, partesCartaIzq[indice]));
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("     " + partesCartaDer[indice]);
    }
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("");
    if (ctrlDeFlujo == 1) // 3ra parte del tutorial
    {
        for (int indice = 5; indice < texto.Length; indice++)
        {
            Console.WriteLine(texto[indice]);
            Thread.Sleep(1500);
        }
        Console.WriteLine("\nPresiona una tecla para continuar.");
        Console.ReadKey();
    }
    if (entradaDeUsuario == 1) // calculo de iniciativa
    {
        iniciativaJugador = Duelo.CalcularIniciativa(cartasNuevas.cards[0], 1);
        iniciativaEnemigo = Duelo.CalcularIniciativa(cartasNuevas.cards[1], 2);
    }
    else
    {
        iniciativaJugador = Duelo.CalcularIniciativa(cartasNuevas.cards[1], 2);
        iniciativaEnemigo = Duelo.CalcularIniciativa(cartasNuevas.cards[0], 1);
    }
    if (iniciativaJugador >= iniciativaEnemigo) // anuncio de iniciativa
    {
        Console.WriteLine("\n" + Textos.CentrarRenglon(espaciosAIzq,$"{nomJugador} mueve primero con {iniciativaJugador} de iniciativa!"));
    }
    else
    {
        Console.WriteLine("\n" + Textos.CentrarRenglon(espaciosAIzq,$"{nomEnemigo} mueve primero con {iniciativaEnemigo} de iniciativa!"));
    }
    Thread.Sleep(1000);
    do  // bucle del duelo
    {
        texto = Textos.CrearTarjetaPers(listaPersonajes[0]).Split(";");  // tarjetas de los personajes que pelean
        textoEnemigo = Textos.CrearTarjetaPers(listaPersonajes[ctrlDeFlujo]).Split(";");
        for (int indice = 0; indice < texto.Length; indice++)
        {
            if (indice != 3)
            {
                Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq, texto[indice] + "      " + textoEnemigo[indice]));
            }
        }

        if (iniciativaJugador >= iniciativaEnemigo) // primer turno jugador, segundo turno enemigo
        {
            Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,"Elige una acción:"));          // decisiones
            Console.Write(Textos.CentrarRenglon(espaciosAIzq,"1.Atacar 2.Defender 3.Esperar: "));
            entradaDeUsuario = Partida.ControlDeOpciones(Console.ReadLine(), 2, 0);
            if (entradaDeUsuario == 99999)
            {
                entradaDeUsuario = 2;   // jugador defiende por defecto
            }
            decisionEnemigo = Duelo.AccionEnemigo();
            Console.WriteLine(" ");
            Thread.Sleep(1000);
            if (entradaDeUsuario == 1)        // combate
            {
                if (decisionEnemigo == 2)   // 12 - jugador ataca, enemigo defiende
                {
                    critico = Duelo.DecidirCritico(listaPersonajes[0]);
                    danio = Duelo.CalcularDanio(listaPersonajes[0], listaPersonajes[ctrlDeFlujo], true, critico);
                    Console.WriteLine("\n" + Textos.CentrarRenglon(espaciosAIzq,$"{nomJugador} ataca pero {nomEnemigo} pudo defenderse!"));
                    Thread.Sleep(1000);
                    if (critico)
                    {
                        Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,"Aun así fue un golpe critico!"));
                        Thread.Sleep(1000);
                    }
                    Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,$"{nomEnemigo} recibe {danio} puntos de daño."));
                    Thread.Sleep(1000);
                }
                else                        // 11 y 13 - jugador ataca, enemigo ataca o espera
                {
                    critico = Duelo.DecidirCritico(listaPersonajes[0]);
                    danio = Duelo.CalcularDanio(listaPersonajes[0], listaPersonajes[ctrlDeFlujo], false, critico);
                    if (critico)
                    {
                        Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,$"{nomJugador} atacá con todas sus fuerzas!"));
                        Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,$"{nomEnemigo} recibirá daño extra!"));
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,$"{nomJugador} ataca!"));
                        Thread.Sleep(1000);
                    }
                    Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,$"{nomEnemigo} recibe {danio} puntos de daño."));
                    Thread.Sleep(1000);

                    if (listaPersonajes[ctrlDeFlujo].Estadisticas.Salud != 0)  // si el enemigo muere se corta el duelo
                    {
                        if (decisionEnemigo == 1)   // si no muere, ataca
                        {
                            Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,$"{nomEnemigo} puede seguir peleando y decidio atacar tambien!"));
                            Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,"Aun así no podrá hacer mas daño esta vez..."));
                            Thread.Sleep(1000);
                            danio = Duelo.CalcularDanio(listaPersonajes[ctrlDeFlujo], listaPersonajes[0], false, false);
                            Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,$"{nomJugador} recibe {danio} puntos de daño."));
                            Thread.Sleep(1000);
                        }
                        else                        // o aclaras que espera
                        {
                            Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,$"{nomEnemigo} está evaluando la situción... creo?"));
                            Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,$"{nomEnemigo} recuperó 10 puntos de salud!"));
                            listaPersonajes[ctrlDeFlujo].RecuperarSalud(saludGuardadaEn); // SaludG actua como limite, no es error
                            Thread.Sleep(1000);
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
                        Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,$"{nomEnemigo} ataca pero {nomJugador} pudo defenderse!"));
                        Thread.Sleep(1000);
                        critico = Duelo.DecidirCritico(listaPersonajes[ctrlDeFlujo]);
                        if (critico)
                        {
                            Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,"Aun así fue un golpe critico!"));
                            Thread.Sleep(1000);
                        }
                        danio = Duelo.CalcularDanio(listaPersonajes[ctrlDeFlujo], listaPersonajes[0], true, critico);
                        Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,$"{nomJugador} recibe {danio} puntos de daño."));
                        Thread.Sleep(1000);
                    }
                    else                        // 31 - enemigo ataca, jugador espera
                    {
                        Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,$"{nomEnemigo} atacá ... y tu estas esperando...?"));
                        Thread.Sleep(1000);
                        critico = Duelo.DecidirCritico(listaPersonajes[ctrlDeFlujo]);
                        if (critico)
                        {
                            Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,$"{nomEnemigo} estaba preparando un ataque especial, recibiras mas daño!"));
                            Thread.Sleep(1000);
                        }
                        danio = Duelo.CalcularDanio(listaPersonajes[ctrlDeFlujo], listaPersonajes[0], false, critico);
                        Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,$"{nomJugador} recibe {danio} puntos de daño."));
                        Thread.Sleep(1000);
                        if (listaPersonajes[0].Estadisticas.Salud != 0)
                        {
                            Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,$"{nomJugador} recupera 10 puntos de salud!"));
                            listaPersonajes[0].RecuperarSalud(saludGuardadaJu);
                            Thread.Sleep(1000);
                        }
                    }
                }
                else    // 22, 23, 32, 33 - no pasa nada
                {
                    Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,"Narrador: ...Nadie está haciendo nada..."));
                    Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,"Dev: Se aburrieron antes que nosotros"));
                    Thread.Sleep(1000);
                    if (entradaDeUsuario == 3)
                    {
                        Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,$"{nomJugador} recupera 10 puntos de salud!"));
                        listaPersonajes[0].RecuperarSalud(saludGuardadaJu);
                        Thread.Sleep(1000);
                    }
                    if (decisionEnemigo == 3)
                    {
                        Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,$"{nomEnemigo} recupera 10 puntos de salud!"));
                        listaPersonajes[0].RecuperarSalud(saludGuardadaJu);
                        Thread.Sleep(1000);
                    }
                }
            }
        }
        else        // primer turno enemigo, segundo turno jugador
        {
            decisionEnemigo = Duelo.AccionEnemigo();  // decisiones
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
            Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,$"{nomEnemigo} va a {linea}! Elige una accion:"));
            Console.Write(Textos.CentrarRenglon(espaciosAIzq,"1.Atacar 2.Defender 3.Esperar: "));
            entradaDeUsuario = Partida.ControlDeOpciones(Console.ReadLine(), 2, 0);
            if (entradaDeUsuario == 99999) // jugador defiende por defecto
            {
                entradaDeUsuario = 2;
            }

            if (decisionEnemigo == 1)
            {
                if (entradaDeUsuario == 2)   // 12 - enemigo ataca, jugador defiende
                {
                    critico = Duelo.DecidirCritico(listaPersonajes[ctrlDeFlujo]);
                    danio = Duelo.CalcularDanio(listaPersonajes[ctrlDeFlujo], listaPersonajes[0], true, critico);
                    Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,$"{nomEnemigo} ataca pero {nomJugador} pudo defenderse!"));
                    Thread.Sleep(1000);
                    if (critico)
                    {
                        Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,"Aun así fue un golpe critico!"));
                        Thread.Sleep(1000);
                    }
                    Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,$"{nomJugador} recibe {danio} puntos de daño."));
                    Thread.Sleep(1000);
                }
                else                        // 11 y 13 - enemigo ataca, jugador ataca o espera
                {
                    critico = Duelo.DecidirCritico(listaPersonajes[ctrlDeFlujo]);
                    danio = Duelo.CalcularDanio(listaPersonajes[ctrlDeFlujo], listaPersonajes[0], false, critico);
                    if (critico)
                    {
                        Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,$"{nomEnemigo} atacá con todas sus fuerzas!"));
                        Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,$"{nomJugador} recibirá daño extra!"));
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,$"{nomEnemigo} ataca!"));
                        Thread.Sleep(1000);
                    }
                    Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,$"{nomJugador} recibe {danio} puntos de daño."));
                    Thread.Sleep(1000);

                    if (listaPersonajes[0].Estadisticas.Salud != 0)
                    {
                        if (entradaDeUsuario == 1)
                        {
                            Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,$"{nomJugador} puede seguir peleando y decidio atacar tambien!"));
                            Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,"Aun así no podra hacer mas daño esta vez..."));
                            Thread.Sleep(1000);

                            critico = false;
                            danio = Duelo.CalcularDanio(listaPersonajes[0], listaPersonajes[ctrlDeFlujo], false, critico);

                            Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,$"{nomEnemigo} recibe {danio} puntos de daño."));
                            Thread.Sleep(1000);
                        }
                        else
                        {
                            Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,$"{nomJugador} está esperando...?"));
                            Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,$"{nomJugador} recupera 10 puntos de salud."));
                            listaPersonajes[0].RecuperarSalud(saludGuardadaJu);
                            Thread.Sleep(1000);
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
                        critico = Duelo.DecidirCritico(listaPersonajes[0]);
                        danio = Duelo.CalcularDanio(listaPersonajes[0], listaPersonajes[ctrlDeFlujo], true, critico);
                        Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,$"{nomJugador} ataca pero {nomEnemigo} pudo defenderse!"));
                        Thread.Sleep(1000);
                        if (critico)
                        {
                            Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,"Aun así fue un golpe critico!"));
                            Thread.Sleep(1000);
                        }
                        Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,$"{nomEnemigo} recibe {danio} puntos de daño."));
                        Thread.Sleep(1000);
                    }
                    else                        // 31 - jugador ataca, enemigo espera
                    {
                        critico = Duelo.DecidirCritico(listaPersonajes[0]);
                        danio = Duelo.CalcularDanio(listaPersonajes[0], listaPersonajes[ctrlDeFlujo], false, critico);
                        Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,$"{nomJugador} ataca pero no se que esta haciendo {nomEnemigo}..."));
                        Thread.Sleep(1000);
                        if (critico)
                        {
                            Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,"Incluso recibirá un golpe critico!"));
                            Thread.Sleep(1000);
                        }
                        Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,$"{nomEnemigo} recibe {danio} puntos de daño."));
                        Thread.Sleep(1000);
                        if (listaPersonajes[ctrlDeFlujo].Estadisticas.Salud != 0)
                        {
                            Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,$"{nomEnemigo} recuper 10 puntos de salud."));
                            listaPersonajes[ctrlDeFlujo].RecuperarSalud(saludGuardadaEn);
                            Thread.Sleep(1000);
                        }
                    }
                }
                else                            // 22, 23, 32, 33 - no pasa nada
                {
                    Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,"Narrador: ...Nadie está haciendo nada... "));
                    Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,"Dev: se aburrieron antes que nosotros?"));
                    Thread.Sleep(1000);
                    if (entradaDeUsuario == 3)
                    {
                        Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,$"{nomJugador} recuper 10 puntos de salud."));
                        listaPersonajes[0].RecuperarSalud(saludGuardadaJu);
                        Thread.Sleep(1000);
                    }
                    if (decisionEnemigo == 3)
                    {
                        Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,$"{nomEnemigo} recuper 10 puntos de salud."));
                        listaPersonajes[ctrlDeFlujo].RecuperarSalud(saludGuardadaEn);
                        Thread.Sleep(1000);
                    }
                }
            }
        }
    } while (listaPersonajes[0].Estadisticas.Salud != 0 && listaPersonajes[ctrlDeFlujo].Estadisticas.Salud != 0);
    /* ---------------------- Fin del bucle del duelo ----------------------*/

    if (listaPersonajes[0].Estadisticas.Salud != 0)    /* ------- Control despues del duelo ------- */
    {
        Console.WriteLine($"\n{nomEnemigo} sufrió demasiado daño... ");
        Console.WriteLine("Dev: habrá que buscar a alguien mas...\n\n");
        Thread.Sleep(1000);
        ctrlDeFlujo += 1;
        if (ctrlDeFlujo <= 9)
        {
            listaPersonajes[0].SubirDeNivel(); // el nivel devuelve la vida al max, artefactos despues
            listaPersonajes[ctrlDeFlujo].SubirDeNivel();
            texto = Textos.DatosDeSubaDeNivel(listaPersonajes[0].DatosGenerales.Clase).Split(";");
            Console.WriteLine($"{nomJugador} subio de nivel: \n");
            for (int indice = 0; indice < texto.Length; indice++)
            {
                Console.WriteLine("             " + texto[indice]);
            }

            texto = Textos.ArmarListaTarjetasArt(cofre);                                 /* --- Eleccion de artefactos --- */
            for (int indice = 0; indice < texto.Length; indice++)
            {
                Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq, texto[indice]));
            }
            if (ctrlDeFlujo == 2)
            {
                Console.WriteLine(" ");
                Console.WriteLine("Narrador: Felicitaciones en ganar tu primer duelo.");
                Thread.Sleep(1000);
                Console.WriteLine("Narrador: Ahora puedes elegir una mejora para el siguiente.");
                Console.WriteLine(" ");
                Thread.Sleep(1000);
            }
            do
            {
                Console.Write("Elige un artefacto: ");
                entradaDeUsuario = Partida.ControlDeOpciones(Console.ReadLine(), 2, 0);
                if (entradaDeUsuario == 99999)
                {
                    Console.WriteLine("Narrador: No, solo están estos 3...");
                }
            } while (entradaDeUsuario == 99999);

            entradaDeUsuario -= 1;
            listaPersonajes[0].MejorasPorItem(cofre[entradaDeUsuario]);
            HerramientaFabricaArt.PasarAInventario(entradaDeUsuario, cofre, inventario);    /* --- Fin eleccion de artefactos --- */

            texto = Textos.MenuDeGuardado().Split(";");                        /* --- Menú de guardado --- */
            for (int indice = 0; indice < texto.Length; indice++)
            {
                Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,texto[indice]));
            }
            Console.Write(Textos.CentrarRenglon(espaciosAIzq,"Selecciona una opción:  "));
            do
            {
                entradaDeUsuario = Partida.ControlDeOpciones(Console.ReadLine(), 2, 0);
                if (entradaDeUsuario == 99999)
                {
                    Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,"Dev: no puedo dejar que pongas cualquier cosa siempre..."));
                    Console.Write(Textos.CentrarRenglon(espaciosAIzq,"Ingresa un opcion valida: "));
                }
            } while (entradaDeUsuario == 99999);
            gameOver = Partida.EjecutarOpcion(listaPersonajes, cofre, inventario, nomPartida, entradaDeUsuario);
            if (entradaDeUsuario == 3)  // muestra inventario, esto seguro puede mejorar
            {
                Console.WriteLine("\n" + Textos.CentrarRenglon(espaciosAIzq, "Inventario:"));

                foreach (Artefacto art in inventario)
                {
                    texto = Textos.ArmarItemAMostrar(art).Split(";");
                    foreach (string renglon in texto)
                    {
                        Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,renglon));
                    }
                }

                Thread.Sleep(1500);
            }
            if (entradaDeUsuario == 1 || entradaDeUsuario == 2)
            {
                Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,"Partida guardada.\n"));
                Thread.Sleep(1000);
            }
        }                                                              /* --- Fin del menu de guardado --- */
        else    /* --- cierre e historial --- */
        {
            texto = Textos.CierreYTrono().Split(";");
            for (int indice = 0; indice < texto.Length; indice++)
            {
                Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,texto[indice]));
                if (indice <= 4 || indice >= texto.Length - 3)
                {
                    Thread.Sleep(500);
                }
            }
            Console.Write(Textos.CentrarRenglon(espaciosAIzq, "Ingresa un nombre para agregar a los ganadores: "));
            Historial.AgregarAlHistorial(Console.ReadLine());
            Console.Write(Textos.CentrarRenglon(espaciosAIzq, "Historial guardado."));
            gameOver = true;
        }
    }
    else // si el jugador pierde
    {
        Console.WriteLine($"Narrador: {nomJugador} acaba de morir...");
        Thread.Sleep(1000);
        Console.WriteLine("Dev: No me hechen la culpa del mal balance...");
        Thread.Sleep(1000);
        Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq, "Dev: queres intentar de nuevo? 1. Si 2. No"));
        do
        {
            entradaDeUsuario = Partida.ControlDeOpciones(Console.ReadLine(), 1, 0);
            if (entradaDeUsuario == 99999)
            {
                Console.WriteLine("Ingresa una opcion valida.");
            }
        } while (entradaDeUsuario == 99999);
        if (entradaDeUsuario == 1)
        {
            listaPersonajes[0].Estadisticas.Salud = saludGuardadaJu;
            listaPersonajes[ctrlDeFlujo].Estadisticas.Salud = saludGuardadaEn;
            gameOver = false;
        }
        else
        {
            gameOver = true;
        }
    }
}
Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,"Nos vemos!"));