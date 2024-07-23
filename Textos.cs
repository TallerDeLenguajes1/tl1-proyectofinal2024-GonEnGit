using EspacioPersonajes;

namespace EspacioTextos;

public static class Textos
{
    public static string Intro()
    {
        return "primera frase por ahora";
    }

    public static string Menu()
    {
        return "  # ------------------------------- #;" +
                "  |  # - #                  # - #   |;" +
                "  |         MENU PRINCIPAL          |;" +
                "  |                                 |;" +
                "  |       1. Nueva Partida          |;" +
                "  |       2. Cargar Partida         |;" +
                "  |       3. Ganadores Anteriores   |;" +
                "  |       4. Salir                  |;" +
                "  |                                 |;" +
                "  |  # - #                  # - #   |;" +
                "  # ------------------------------- #;" +
                "            Elija una opción:         ";
    }

    public static string Tarjetas(Personaje instancia)
    {
        string lineaDatos = instancia.DevolverDatos();
        string lineaEstadisticas = instancia.DevolverEstadisticas();

        string[] Datos = lineaDatos.Split(";");
        string[] Estadisticas = lineaEstadisticas.Split(";");

    // esto es para que todas las edades tengan 3 caractere
        if (Datos[6].Length < 3)
        {
            Datos[6] = " " + Datos[6];
        }

        return $"+-{Datos[0]}------------------------------------+;" +
                $"|         {Datos[1]}, {Datos[2]}                 |;" +
                $"|       {Datos[3]}, {Datos[4]}                 |;" +
                $"|           {Datos[6]} años, {Datos[5]}            |;" +
                "+--------------------------------------+;" +
                $"|              Salud: {Estadisticas[0]}              |;" +
                $"|              Armadura: {Estadisticas[1]}             |;" +
                $"|              Fuerza: {Estadisticas[2]}               |;" +
                $"|              Destreza: {Estadisticas[3]}             |;" +
                $"|              Velocidad: {Estadisticas[4]}            |;" +
                "+--------------------------------------+;";
    }

    public static string DevolverNombre(Personaje pers)
    {
        return pers.DatosGenerales.Nombre + ',' + pers.DatosGenerales.Apodo;
    }

    public static string MenuDeGuardado()
    {
        return  "+--------------------------+;" +
                "|    Continuar Partida?    |;" +
                "+--------------------------+;" +
                "|  1. Guardar y Continuar  |;" +
                "|  2. Guardar y Salir      |;" +
                "|  3. Salir sin Guardar    |;" +
                "+--------------------------+;" +
                "   Seleccione una opción: ";
    }
}