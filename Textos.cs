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
        return  "# ------------------------------- #;" +
                "|  # - #                  # - #   |;" +
                "|         MENU PRINCIPAL          |;" +
                "|                                 |;" +
                "|       1. Nueva Partida          |;" +
                "|       2. Cargar Partida         |;" +
                "|       3. Salir                  |;" +
                "|                                 |;" +
                "|  # - #                  # - #   |;" +
                "# ------------------------------- #;" +
                "          Elija una opción:         ";
    }

    public static string Tarjetas(personaje instancia)
    {
        string lineaDatos = instancia.DevolverDatos();
        string lineaEstadisticas = instancia.DevolverEstadisticas();

        string[] Datos = lineaDatos.Split(";");
        string[] Estadisticas = lineaEstadisticas.Split(";");

        return  $"+-{Datos[0]}---------------------------------------------+;" +
                $"|                Nombre: {Datos[1]}, {Datos[2]}          |;" +
                $"|          Raza: {Datos[3]}           Clase: {Datos[4]}  |;" +
                $"|    Nacimiento: {Datos[5]}     Edad: {Datos[6]} años    |;" +
                "+---------------------------------------------------------+" +
                $"|                       Salud: {Estadisticas[0]}         |;" +
                $"|                       Armadura: {Estadisticas[1]}      |;" +
                $"|                       Fuerza: {Estadisticas[2]}        |;" +
                $"|                       Destreza: {Estadisticas[3]}      |;" +
                $"|                       Velocidad: {Estadisticas[4]}     |;" +
                "+---------------------------------------------------------+;";
    }
}