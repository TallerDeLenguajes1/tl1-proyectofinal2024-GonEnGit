
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
string linea, linea2, linea3, cartaIzq, cartaDer;
string nomJugador, nomEnemigo, nomPartida = "";
Mazo nuevoMazo = await API.ObtenerIdMazoAsync();
ListaCartas cartasNuevas = new ListaCartas();

// Arreglos
string[] texto, texto2, texto3, textoEnemigo;
string[] partesCartaIzq, partesCartaDer;

// Listas
List<Historial> historialCargado;
List<Personaje> listaPersonajes = new List<Personaje>();
List<Artefacto> cofre = new List<Artefacto>();
List<Artefacto> inventario = new List<Artefacto>();
List<string[]> tarjetasAMostrar = new List<string[]>();


/* ---- Desarrollo del juego ---- */
Textos.Introduccion();
texto = Textos.MenuPrincipal();
foreach (string parte in texto)
{
    linea = Textos.CentrarRenglon(espaciosAIzq, parte);
    Console.Write(linea);
}

do              /*--- inicio del control de opcion ---*/
{
    while (entradaDeUsuario == 99999 && continuar == 19)
    {
        linea = Textos.CentrarRenglon(espaciosAIzq, "Ingresá una opción del menú: ");
        Console.Write("\n" + linea);
        entradaDeUsuario = Partida.ControlDeOpciones(Console.ReadLine(), 3, 0);
        if (entradaDeUsuario == 99999)
        {
            linea = Textos.CentrarRenglon(espaciosAIzq, "Ingresá una opcion valida.");
            Console.Write(linea);
        }
    }
    Console.WriteLine("");
    switch (entradaDeUsuario)
    {
        case 1:
            List<Personaje> listaPersTemp = HerramientaFabrica.CreadorDePersonajes();
            cofre = HerramientaFabricaArt.CreadorDeArtefactos(cofre);

            linea = Textos.CentrarRenglon(espaciosAIzq, "Ingresa un nombre para la partida: "); 
            Console.Write(linea);
            nomPartida = Console.ReadLine();
            Partida.CrearCarpetas();

            linea = Textos.IntroSeleccionPers(); /* --- ialogos seleccion --- */
            texto = linea.Split(";");
            foreach (string renglon in texto)
            {
                Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,renglon));
            }

            Console.WriteLine(" ");
            Textos.ArmarListaTarjetasPers(tarjetasAMostrar, listaPersTemp);
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < tarjetasAMostrar[0].Length; j++)
                {
                    linea = Textos.CentrarRenglon(espaciosAIzq, tarjetasAMostrar[i][j]);
                    Console.WriteLine(linea);
                }
                Console.WriteLine(" ");
            }

            Console.Write("Elige un personaje: ");   /*--- Seleccion de personaje ---*/
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
            linea = Duelo.CheatCode(listaPersonajes[0], listaPersonajes[1], Console.ReadLine());
            Console.WriteLine(linea + "\n");      /*--- Fin de seleccion de personaje ---*/

            continuar = 21;
            listaPersTemp.Clear();
            tarjetasAMostrar.Clear();
            HerramientaFabrica = null;
            break;
        case 2:
            linea = Partida.ObtenerNombresDePartidas();
            if (linea == "vacio;")
            {
                Console.WriteLine("Narrador: No hay partidas guardadas aún.");
                Console.WriteLine("Dev: así que te vamos a obligar a inicar un juego nuevo!");
                entradaDeUsuario = 1;
            }
            else
            {
                Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,"/* --- Partidas Guardadas --- */\n"));
                texto = linea.Split(";");
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
            Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,"\nContinuar con otra opcion? (S/N) "));
            do
            {
                continuar = Partida.ControlDeOpciones(Console.ReadLine(), 7, 0); // s = letra n° 19, n = letra n° 14
                if (continuar == 99999)
                {
                    Console.Write("Ingresa una opcion valida: ");
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
} while (continuar == 19 || continuar == 14);  /*--- FIN del control de opcion ---*/

while (gameOver == false && ctrlDeFlujo <= 9)          /*--- Desarrollo del juego ---*/
{
    ctrlDeFlujo = Duelo.ContarEnemigosVencidos(listaPersonajes); // cant de enemigos vencidos, sirve para controlar casi todo
    nomJugador = Textos.DevolverNombre(listaPersonajes[0]);
    nomEnemigo = Textos.DevolverNombre(listaPersonajes[ctrlDeFlujo]);
    saludGuardadaJu = listaPersonajes[0].Estadisticas.Salud;
    saludGuardadaEn = listaPersonajes[ctrlDeFlujo].Estadisticas.Salud;
    cartasNuevas.cards = await API.ObtenerCartasAsync(nuevoMazo.deck_id); // tomas 2 cartas del mazo
    switch (ctrlDeFlujo) // dialogos
    {
        case 2: // esto de las historias lo vas a tener que pensar mejor
            Console.WriteLine("primera historia antes");
            break;
    }
    ctrlDeFlujo = 9; // ---------------------------------------------
// desarrollo de los duelos
    if (ctrlDeFlujo == 1)   // 1ra parte del tutorial
    {
        linea = Textos.TutorialCartas();
        texto = linea.Split(";");
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
        Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,$"\n{nomJugador} mueve primero con {iniciativaJugador} de iniciativa!"));
    }
    else
    {
        Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,$"\n{nomEnemigo} mueve primero con {iniciativaEnemigo} de iniciativa!"));
    }
    do  // bucle del duelo
    {
        linea = Textos.CrearTarjetaPers(listaPersonajes[0]);    // tarjetas de los personajes que pelean
        texto = linea.Split(";");
        linea2 = Textos.CrearTarjetaPers(listaPersonajes[ctrlDeFlujo]);
        textoEnemigo = linea2.Split(";");
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
            Thread.Sleep(1000);
            if (entradaDeUsuario == 1)        // combate
            {
                if (decisionEnemigo == 2)   // 12 - jugador ataca, enemigo defiende
                {
                    critico = Duelo.DecidirCritico(listaPersonajes[0]);
                    danio = Duelo.CalcularDanio(listaPersonajes[0], listaPersonajes[ctrlDeFlujo], true, critico);
                    Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,$"\n{nomJugador} ataca pero {nomEnemigo} pudo defenderse!"));
                    if (critico)
                    {
                        Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,"Aun así fue un golpe critico!"));
                    }
                    Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,$"{nomEnemigo} recibe {danio} puntos de daño."));
                }
                else                        // 11 y 13 - jugador ataca, enemigo ataca o espera
                {
                    critico = Duelo.DecidirCritico(listaPersonajes[0]);
                    danio = Duelo.CalcularDanio(listaPersonajes[0], listaPersonajes[ctrlDeFlujo], false, critico);
                    if (critico)
                    {
                        Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,$"{nomJugador} atacá con todas sus fuerzas!"));
                        Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,$"{nomEnemigo} recibirá daño extra!"));
                    }
                    else
                    {
                        Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,$"{nomJugador} ataca!"));
                    }
                    Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,$"{nomEnemigo} recibe {danio} puntos de daño."));

                    if (listaPersonajes[ctrlDeFlujo].Estadisticas.Salud != 0)  // si el enemigo muere se corta el duelo
                    {
                        if (decisionEnemigo == 1)   // si no muere, ataca
                        {
                            Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,$"{nomEnemigo} puede seguir peleando y decidio atacar tambien!"));
                            Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,"Aun así no podrá hacer mas daño esta vez..."));
                            danio = Duelo.CalcularDanio(listaPersonajes[ctrlDeFlujo], listaPersonajes[0], false, false);
                            Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,$"{nomJugador} recibe {danio} puntos de daño."));
                        }
                        else                        // o aclaras que espera
                        {
                            Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,$"{nomEnemigo} está evaluando la situción... creo?"));
                            Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,$"{nomEnemigo} recuperó 10 puntos de salud!"));
                            listaPersonajes[ctrlDeFlujo].RecuperarSalud(saludGuardadaEn); // SaludG actua como limite, no es error
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
                        critico = Duelo.DecidirCritico(listaPersonajes[ctrlDeFlujo]);
                        if (critico)
                        {
                            Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,"Aun así fue un golpe critico!"));
                        }
                        danio = Duelo.CalcularDanio(listaPersonajes[ctrlDeFlujo], listaPersonajes[0], true, critico);
                        Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,$"{nomJugador} recibe {danio} puntos de daño."));
                    }
                    else                        // 31 - enemigo ataca, jugador espera
                    {
                        Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,$"{nomEnemigo} atacá ... y tu estas esperando...?"));
                        critico = Duelo.DecidirCritico(listaPersonajes[ctrlDeFlujo]);
                        if (critico)
                        {
                            Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,$"{nomEnemigo} estaba preparando un ataque especial, recibiras mas daño!"));
                        }
                        danio = Duelo.CalcularDanio(listaPersonajes[ctrlDeFlujo], listaPersonajes[0], false, critico);
                        Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,$"{nomJugador} recibe {danio} puntos de daño."));
                        if (listaPersonajes[0].Estadisticas.Salud != 0)
                        {
                            Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,$"{nomJugador} recupera 10 puntos de salud!"));
                            listaPersonajes[0].RecuperarSalud(saludGuardadaJu);
                        }
                    }
                }
                else    // 22, 23, 32, 33 - no pasa nada
                {
                    Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,"Narrador: ...Nadie está haciendo nada..."));
                    Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,"Dev: Se aburrieron antes que nosotros"));
                    if (entradaDeUsuario == 3)
                    {
                        Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,$"{nomJugador} recupera 10 puntos de salud!"));
                        listaPersonajes[0].RecuperarSalud(saludGuardadaJu);
                    }
                    if (decisionEnemigo == 3)
                    {
                        Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,$"{nomEnemigo} recupera 10 puntos de salud!"));
                        listaPersonajes[0].RecuperarSalud(saludGuardadaJu);
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
                    if (critico)
                    {
                        Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,"Aun así fue un golpe critico!"));
                    }
                    Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,$"{nomJugador} recibe {danio} puntos de daño."));
                }
                else                        // 11 y 13 - enemigo ataca, jugador ataca o espera
                {
                    critico = Duelo.DecidirCritico(listaPersonajes[ctrlDeFlujo]);
                    danio = Duelo.CalcularDanio(listaPersonajes[ctrlDeFlujo], listaPersonajes[0], false, critico);
                    if (critico)
                    {
                        Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,$"{nomEnemigo} atacá con todas sus fuerzas!"));
                        Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,$"{nomJugador} recibirá daño extra!"));
                    }
                    else
                    {
                        Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,$"{nomEnemigo} ataca!"));
                    }
                    Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,$"{nomJugador} recibe {danio} puntos de daño."));

                    if (listaPersonajes[0].Estadisticas.Salud != 0)
                    {
                        if (entradaDeUsuario == 1)
                        {
                            Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,$"{nomJugador} puede seguir peleando y decidio atacar tambien!"));
                            Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,"Aun así no podra hacer mas daño esta vez..."));

                            critico = false;
                            danio = Duelo.CalcularDanio(listaPersonajes[0], listaPersonajes[ctrlDeFlujo], false, critico);

                            Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,$"{nomEnemigo} recibe {danio} puntos de daño."));
                        }
                        else
                        {
                            Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,$"{nomJugador} está esperando...?"));
                            Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,$"{nomJugador} recupera 10 puntos de salud."));
                            listaPersonajes[0].RecuperarSalud(saludGuardadaJu);
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
                        if (critico)
                        {
                            Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,"Aun así fue un golpe critico!"));
                        }
                        Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,$"{nomEnemigo} recibe {danio} puntos de daño."));
                    }
                    else                        // 31 - jugador ataca, enemigo espera
                    {
                        critico = Duelo.DecidirCritico(listaPersonajes[0]);
                        danio = Duelo.CalcularDanio(listaPersonajes[0], listaPersonajes[ctrlDeFlujo], false, critico);
                        Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,$"{nomJugador} ataca pero no se que esta haciendo {nomEnemigo}..."));
                        if (critico)
                        {
                            Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,"Incluso recibirá un golpe critico!"));
                        }
                        Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,$"{nomEnemigo} recibe {danio} puntos de daño."));
                        if (listaPersonajes[ctrlDeFlujo].Estadisticas.Salud != 0)
                        {
                            Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,$"{nomEnemigo} recuper 10 puntos de salud."));
                            listaPersonajes[ctrlDeFlujo].RecuperarSalud(saludGuardadaEn);
                        }
                    }
                }
                else                            // 22, 23, 32, 33 - no pasa nada
                {
                    Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,"Narrador: ...Nadie está haciendo nada... "));
                    Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,"Dev: se aburrieron antes que nosotros?"));
                    if (entradaDeUsuario == 3)
                    {
                        Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,$"{nomJugador} recuper 10 puntos de salud."));
                        listaPersonajes[0].RecuperarSalud(saludGuardadaJu);
                    }
                    if (decisionEnemigo == 3)
                    {
                        Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,$"{nomEnemigo} recuper 10 puntos de salud."));
                        listaPersonajes[ctrlDeFlujo].RecuperarSalud(saludGuardadaEn);
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
        ctrlDeFlujo += 1;
        if (ctrlDeFlujo <= 9)
        {
            listaPersonajes[0].SubirDeNivel(); // el nivel devuelve la vida al max, artefactos despues
            listaPersonajes[ctrlDeFlujo].SubirDeNivel();
            linea = Textos.DatosDeSubaDeNivel(listaPersonajes[0].DatosGenerales.Clase);
            texto = linea.Split(";");   // la verdad estos pares linea - texto podrian ser un metodo tambien -------------
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
                Console.WriteLine("Narrador: Felicitaciones en ganar ti primer duelo.");
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

            linea = Textos.MenuDeGuardado();                        /* --- Menú de guardado --- */
            texto = linea.Split(";");
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
                    linea = Textos.ArmarItemAMOstrar(art);
                    texto = linea.Split(";");
                    foreach (string renglon in texto)
                    {
                        Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,renglon));
                    }
                }

                Thread.Sleep(1500);
            }
            if (entradaDeUsuario == 1 || entradaDeUsuario == 2)
            {
                Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,"Partida guardada.")); // esto lo podrias mejorar con algun control
                Thread.Sleep(1500);
            }
        }                                                              /* --- Fin del menu de guardado --- */
        else
        {
            linea = Textos.CierreYTrono();
            texto = linea.Split(";");
            for (int indice = 0; indice < texto.Length; indice++)
            {
                Console.WriteLine(Textos.CentrarRenglon(espaciosAIzq,texto[indice]));
                if (indice <= 4 || indice >= texto.Length - 3)
                {
                    Thread.Sleep(500);
                }
            }
        }
    }
    else // si el jugador pierde
    {
        Console.WriteLine($"Narrador: {nomJugador} acaba de morir...");
        Console.WriteLine("Dev: No me hechen la culpa del mal balance...");
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