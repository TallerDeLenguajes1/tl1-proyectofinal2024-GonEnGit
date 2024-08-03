
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
int entradaDeUsuario = 99999, espaciosAntes = Console.WindowWidth;
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
    linea = Textos.CentrarRenglon(espaciosAntes, parte);
    Console.Write(linea);
}

do              /*--- inicio del control de opcion ---*/
{
    while (entradaDeUsuario == 99999 && continuar == 19)
    {
        linea = Textos.CentrarRenglon(espaciosAntes, "Ingresá una opción del menú: ");
        Console.Write("\n" + linea);
        entradaDeUsuario = Partida.ControlDeOpciones(Console.ReadLine(), 3, 0);
        if (entradaDeUsuario == 99999)
        {
            linea = Textos.CentrarRenglon(espaciosAntes, "Ingresá una opcion valida.");
            Console.Write(linea);
        }
    }
    Console.WriteLine("");
    switch (entradaDeUsuario)
    {
        case 1:
            List<Personaje> listaPersTemp = new List<Personaje>();  // listas provisorias para liberarlas despues

            listaPersTemp = HerramientaFabrica.CreadorDePersonajes(listaPersTemp);  // creas 10 personajes
            cofre = HerramientaFabricaArt.CreadorDeArtefactos(cofre);               // lista aleatoria de Artefactos

            linea = Textos.CentrarRenglon(espaciosAntes, "Ingresa un nombre para la partida: ");
            Console.Write(linea);   // creas carpetas
            nomPartida = Console.ReadLine();
            Partida.CrearCarpetas();
        
        /*--- esta parte se supone que sea otra historia ---*/ // ------------------------------------------------
            // historia
        /*--- fin de esta parte de historia---*/

            Console.WriteLine(" ");
            Textos.ArmarListaTarjetas(tarjetasAMostrar, listaPersTemp, cofre, 1);
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < tarjetasAMostrar[0].Length; j++)
                {
                    linea = Textos.CentrarRenglon(espaciosAntes, tarjetasAMostrar[i][j]);
                    Console.WriteLine(linea);
                }
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

            listaPersonajes = HerramientaFabrica.MezclarLista(entradaDeUsuario - 1, listaPersTemp); // armas lista mezclada

            Console.Write("\nIngresa el ataque de tu personaje: "); // cheat code
            linea = Duelo.CheatCode(listaPersonajes[0], listaPersonajes[1], Console.ReadLine());
            Console.WriteLine(linea + "\n");      /*--- Fin de seleccion de personaje ---*/

            continuar = 21;
            listaPersTemp.Clear();
            tarjetasAMostrar.Clear();  // '.Clear()' y/o '= null' habilita las listas para el garbage collector
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
                Console.WriteLine("/* --- Partidas Guardadas --- */\n");
                texto = linea.Split(";");
                for (int indice = 1; indice < texto.Length - 1; indice++)
                {
                    Console.WriteLine($"            {indice}. " + texto[indice]);
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
                Console.WriteLine("/*.... Salón de Ganadores ...*/\n");
                foreach (Historial entrada in historialCargado)
                {
                    linea = entrada.FechaVictoria.ToString("dd 'de' MMM 'del' yyyy 'a las' HH:mm");
                    Console.WriteLine($"{entrada.NombreGanador} ganó en {linea}");
                }
            }
            else
            {
                Console.WriteLine("Aun no hay gandores.\n");
            }
            Console.Write("\nContinuar con otra opcion? (S/N) ");
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
// dialogos antes de los duelos
    switch (ctrlDeFlujo)
    {
        case 2: // esto de las historias lo vas a tener que pensar mejor
            Console.WriteLine("primera historia antes");
            break;
    }
// desarrollo de los duelos
    if (ctrlDeFlujo == 1)   // tutorial antes del 1er duelo
    {
        linea = Textos.TutorialCartas();
        texto = linea.Split(";");
        for (int indice = 0; indice < 2; indice++)
        {
            Console.WriteLine(texto[indice]);
            Thread.Sleep(1500);
        }
    }
// muestra carta con reverso
    cartaIzq = Textos.ArmarCarta(cartasNuevas.cards[0]); 
    cartaDer = Textos.TraerReverso();
    partesCartaIzq = cartaIzq.Split(";");
    partesCartaDer = cartaDer.Split(";");
    for (int indice = 0; indice < partesCartaIzq.Length; indice++)
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.Write(partesCartaIzq[indice]);
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine(partesCartaDer[indice]);
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
        entradaDeUsuario = Partida.ControlDeOpciones(Console.ReadLine(), 1, 0);
        if (entradaDeUsuario == 99999)
        {
            Console.WriteLine("Solo podes elegir entre estas 2 cartas.");
        }
    } while (entradaDeUsuario == 99999);
// ambas cartas boca arriba
    cartaDer = Textos.ArmarCarta(cartasNuevas.cards[1]);
    partesCartaDer = cartaDer.Split(";");
    Console.WriteLine("");
    for (int indice = 0; indice < partesCartaDer.Length; indice++)
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.Write(partesCartaIzq[indice]);
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine(partesCartaDer[indice]);
    }
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("");
    if (ctrlDeFlujo == 1)
    {
        for (int indice = 5; indice < texto.Length; indice++)
        {
            Console.WriteLine(texto[indice]);
            Thread.Sleep(1500);
        }
        Console.WriteLine("\nPresiona una tecla para continuar.");
        Console.ReadKey();
    }
// estas 2 dependen de la eleccion
    if (entradaDeUsuario == 1)
    {
        iniciativaJugador = Duelo.CalcularIniciativa(cartasNuevas.cards[0], 1);
        iniciativaEnemigo = Duelo.CalcularIniciativa(cartasNuevas.cards[1], 2);
    }
    else
    {
        iniciativaJugador = Duelo.CalcularIniciativa(cartasNuevas.cards[1], 2);
        iniciativaEnemigo = Duelo.CalcularIniciativa(cartasNuevas.cards[0], 1);
    }
// por ultimo anuncia quien va primero
    if (iniciativaJugador >= iniciativaEnemigo)
    {
        Console.WriteLine($"\n{nomJugador} mueve primero con {iniciativaJugador} de iniciativa!");
    }
    else
    {
        Console.WriteLine($"\n{nomEnemigo} mueve primero con {iniciativaEnemigo} de iniciativa!");
    }
// bucle del duelo
    do
    {
        linea = Textos.CrearTarjeta(listaPersonajes[0]);    // tarjetas de los personajes que pelean
        texto = linea.Split(";");
        linea2 = Textos.CrearTarjeta(listaPersonajes[ctrlDeFlujo]);
        textoEnemigo = linea2.Split(";");
        for (int indice = 0; indice < texto.Length; indice++)
        {
            if (indice != 3)
            {
                Console.WriteLine("  " + texto[indice] + " " + textoEnemigo[indice]);
            }
        }

        if (iniciativaJugador >= iniciativaEnemigo) // primer turno jugador, segundo turno enemigo
        {
        // decisiones
            Console.WriteLine("Elige una acción:");
            Console.Write("1.Atacar 2.Defender 3.Esperar: ");
            entradaDeUsuario = Partida.ControlDeOpciones(Console.ReadLine(), 2, 0);
            if (entradaDeUsuario == 99999)
            {
                entradaDeUsuario = 2;   // jugador defiende por defecto
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
                    critico = Duelo.DecidirCritico(listaPersonajes[0]);
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
                        if (decisionEnemigo == 1)   // si no muere, ataca
                        {
                            Console.WriteLine($"{nomEnemigo} puede seguir peleando y decidio atacar tambien!");
                            Console.WriteLine("Aun así no podrá hacer mas daño esta vez...");
                            danio = Duelo.CalcularDanio(listaPersonajes[ctrlDeFlujo], listaPersonajes[0], false, false);
                            Console.WriteLine($"{nomJugador} recibe {danio} puntos de daño.");
                        }
                        else                        // o aclaras que espera
                        {
                            Console.WriteLine($"{nomEnemigo} está evaluando la situción... creo?");
                            Console.WriteLine($"{nomEnemigo} recuperó 10 puntos de salud!");
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
                        if (listaPersonajes[0].Estadisticas.Salud != 0)
                        {
                            Console.WriteLine($"{nomJugador} recupera 10 puntos de salud!");
                            listaPersonajes[0].RecuperarSalud(saludGuardadaJu);
                        }
                    }
                }
                else    // 22, 23, 32, 33 - no pasa nada
                {
                    Console.WriteLine("Narrador: ...Nadie está haciendo nada...");
                    Console.WriteLine("Dev: Se aburrieron antes que nosotros");
                    if (entradaDeUsuario == 3)
                    {
                        Console.WriteLine($"{nomJugador} recupera 10 puntos de salud!");
                        listaPersonajes[0].RecuperarSalud(saludGuardadaJu);
                    }
                    if (decisionEnemigo == 3)
                    {
                        Console.WriteLine($"{nomEnemigo} recupera 10 puntos de salud!");
                        listaPersonajes[0].RecuperarSalud(saludGuardadaJu);
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
                    Console.WriteLine($"{nomEnemigo} ataca pero {nomJugador} pudo defenderse!");
                    if (critico)
                    {
                        Console.WriteLine("Aun así fue un golpe critico!");
                    }
                    Console.WriteLine($"{nomJugador} recibe {danio} puntos de daño.");
                }
                else                        // 11 y 13 - enemigo ataca, jugador ataca o espera
                {
                    critico = Duelo.DecidirCritico(listaPersonajes[ctrlDeFlujo]);
                    danio = Duelo.CalcularDanio(listaPersonajes[ctrlDeFlujo], listaPersonajes[0], false, critico);
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

                            critico = false;
                            danio = Duelo.CalcularDanio(listaPersonajes[0], listaPersonajes[ctrlDeFlujo], false, critico);

                            Console.WriteLine($"{nomEnemigo} recibe {danio} puntos de daño.");
                        }
                        else
                        {
                            Console.WriteLine($"{nomJugador} está esperando...?");
                            Console.WriteLine($"{nomJugador} recupera 10 puntos de salud.");
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
                        Console.WriteLine($"{nomJugador} ataca pero {nomEnemigo} pudo defenderse!");
                        if (critico)
                        {
                            Console.WriteLine("Aun así fue un golpe critico!");
                        }
                        Console.WriteLine($"{nomEnemigo} recibe {danio} puntos de daño.");
                    }
                    else                        // 31 - jugador ataca, enemigo espera
                    {
                        critico = Duelo.DecidirCritico(listaPersonajes[0]);
                        danio = Duelo.CalcularDanio(listaPersonajes[0], listaPersonajes[ctrlDeFlujo], false, critico);
                        Console.WriteLine($"{nomJugador} ataca pero no se que esta haciendo {nomEnemigo}...");
                        if (critico)
                        {
                            Console.WriteLine("Incluso recibirá un golpe critico!");
                        }
                        Console.WriteLine($"{nomEnemigo} recibe {danio} puntos de daño.");
                        if (listaPersonajes[ctrlDeFlujo].Estadisticas.Salud != 0)
                        {
                            Console.WriteLine($"{nomEnemigo} recuper 10 puntos de salud.");
                            listaPersonajes[ctrlDeFlujo].RecuperarSalud(saludGuardadaEn);
                        }
                    }
                }
                else                            // 22, 23, 32, 33 - no pasa nada
                {
                    Console.WriteLine("Narrador: ...Nadie está haciendo nada... ");
                    Console.WriteLine("Dev: se aburrieron antes que nosotros?");
                    if (entradaDeUsuario == 3)
                    {
                        Console.WriteLine($"{nomJugador} recuper 10 puntos de salud.");
                        listaPersonajes[0].RecuperarSalud(saludGuardadaJu);
                    }
                    if (decisionEnemigo == 3)
                    {
                        Console.WriteLine($"{nomEnemigo} recuper 10 puntos de salud.");
                        listaPersonajes[ctrlDeFlujo].RecuperarSalud(saludGuardadaEn);
                    }
                }
            }
        }
    } while (listaPersonajes[0].Estadisticas.Salud != 0 && listaPersonajes[ctrlDeFlujo].Estadisticas.Salud != 0);
    /* ---------------------- Fin del bucle del duelo ----------------------*/

    if (listaPersonajes[ctrlDeFlujo].Estadisticas.Salud == 0)   // si el jugador gana
    {
        Console.WriteLine($"{nomEnemigo} sufrió demasiado daño... ");
        Console.WriteLine("Dev: habrá que buscar a alguien mas...\n\n");
        ctrlDeFlujo += 1;
        listaPersonajes[0].SubirDeNivel(); // el nivel devuelve la vida al max, artefactos despues
        listaPersonajes[ctrlDeFlujo].SubirDeNivel();
        linea = Textos.DatosDeSubaDeNivel(listaPersonajes[0].DatosGenerales.Clase);
        texto = linea.Split(";");   // la verdad estos pares linea - texto podrian ser un metodo tambien -------------
        Console.WriteLine($"{nomJugador} subio de nivel: \n");
        for (int indice = 0; indice < texto.Length; indice++)
        {
            Console.WriteLine("             " + texto[indice]);
        }

        Textos.ArmarListaTarjetas(tarjetasAMostrar, listaPersonajes, cofre, 2);    /* --- Eleccion de artefactos --- */
        for (int indice = 0; indice < tarjetasAMostrar[0].Length; indice++)
        {
            Console.WriteLine(Textos.CentrarRenglon(espaciosAntes, tarjetasAMostrar[0][indice]));
        }
        if (ctrlDeFlujo == 2)
        {
            Console.WriteLine("Narrador: El ganador puede elegir una mejora.");
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
        inventario.Add(cofre[entradaDeUsuario]);
        listaPersonajes[0].MejorasPorItem(cofre[entradaDeUsuario]);
        cofre.RemoveAt(entradaDeUsuario);                                           /* --- Fin artefactos --- */
        if (ctrlDeFlujo != 9)
        {
            // guardado de partida en json
            linea = Textos.MenuDeGuardado();
            texto = linea.Split(";");
            for (int indice = 0; indice < texto.Length; indice++)
            {
                if (indice == (texto.Length - 1))
                {
                    Console.Write("  " + texto[indice]);
                }
                else
                {
                    Console.WriteLine("  " + texto[indice]);
                }
            }
            do
            {
                entradaDeUsuario = Partida.ControlDeOpciones(Console.ReadLine(), 2, 0);
                if (entradaDeUsuario == 99999)
                {
                    Console.WriteLine("Dev: no puedo dejar que pongas cualquier cosa siempre...");
                    Console.Write("Ingresa un opcion valida: ");
                }
            } while (entradaDeUsuario == 99999);
            gameOver = Partida.EjecutarOpcion(listaPersonajes, cofre, inventario, nomPartida, entradaDeUsuario); //-----------------------------------
            if (entradaDeUsuario == 1 || entradaDeUsuario == 2)
            {
                Console.WriteLine("Partida guardada."); // esto lo podrias mejorar con algun control
            }
        }
        else    // esta sería la ultima rama, cuando ganas todos los duelos
        {
            Console.WriteLine("Dev: Parece que el jugador pudo ganar todos los duelos...");
            Console.WriteLine("Narrador: y eso no es bueno?");
            Console.WriteLine("Dev: si pero ahora hay que cerrar el juego...");
            Console.Write("Ingresa un nombre para agregar a los ganadores: ");
            linea = Console.ReadLine();
            Historial.AgregarAlHistorial(linea);
            gameOver = true;
        }
    }
    else
    {
        Console.WriteLine($"Narrador: {nomJugador} acaba de morir...");
        Console.WriteLine($"Dev: No me hechen la culpa del mal balance...");
        Console.WriteLine("Dev: queres intentar de nuevo? 1. Si 2. No");
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
            gameOver = false;
        }
        else
        {
            gameOver = true;
        }
    }
}
Console.WriteLine("Hasta la proxima!");