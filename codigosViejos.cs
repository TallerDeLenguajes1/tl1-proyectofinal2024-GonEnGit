
decisionCombinada = entradaDeUsuario + (decisionEnemigo * 10);
switch (decisionCombinada)
{
    case 11:    // jugador ataca, enemigo ataca         - 11
        danio = HerramientaDuelos.CalcularDanio(ataqueJugador, defensaEnemigo);

        Console.WriteLine($"{listaPersonajes[0].DatosGenerales.Nombre} ataca primero!");
        Console.WriteLine($"{listaPersonajes[ctrlDeFlujo].DatosGenerales.Nombre} recibe {danio} puntos de daño!");

        if (listaPersonajes[ctrlDeFlujo].Estadisticas.Salud > 0)
        {
            danio = HerramientaDuelos.CalcularDanio(ataqueEnemigo, defensaJugador);

            Console.WriteLine($"{listaPersonajes[ctrlDeFlujo].DatosGenerales.Nombre} ataca tambien!");
            Console.WriteLine($"{listaPersonajes[0].DatosGenerales.Nombre} recibe {danio} puntos de daño!");
        }
        else
        {
            Console.WriteLine($"{listaPersonajes[ctrlDeFlujo].DatosGenerales.Nombre} no puede seguir peleando...");
        }

        if (listaPersonajes[0].Estadisticas.Salud <= 0)
        {
            gameOver = true;
            ctrlDeFlujo = 9;
            // estos dialogos a otra parte...?
            Console.WriteLine("Narrador: uff eso debió doler...");
            Console.WriteLine("Dev: hice esto muy dificil?");
            Console.WriteLine("Narrador: ...probablemente sea el jugador...");
        }
        break;

    case 12:    // jugador ataca, enemigo defiende      - 12
        danio = HerramientaDuelos.CalcularDanio(ataqueJugador, defensaEnemigo);
        listaPersonajes[ctrlDeFlujo].Estadisticas.Salud -= danio;

        Console.WriteLine($"{listaPersonajes[0].DatosGenerales.Nombre} ataca primero!");
        Console.WriteLine($"{listaPersonajes[ctrlDeFlujo].DatosGenerales.Nombre} decidio defenderse...!");
        Console.WriteLine($"{listaPersonajes[ctrlDeFlujo].DatosGenerales.Nombre} recibe {danio} puntos de daño!");
        break;

    case 13:    // jugador ataca, enemigo espera        - 13

        break;

    case 21:    // jugador defiende, enemigo ataca      - 21

        break;

    case 31:    // jugador espera, enemigo ataca        - 31

        break;

    default:    // casos 22, 23, 32, 33 - no pasa nada
        Console.WriteLine("Narrador: nadie está haciendo nada... se olvidaron de la pelea?");
        break;
}