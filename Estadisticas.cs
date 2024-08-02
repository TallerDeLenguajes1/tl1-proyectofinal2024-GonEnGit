namespace  EspacioPersonajes;


public class Estadisticas
{
    private int nivel;
    private int salud;
    private int armadura;
    private int fuerza;
    private int destreza;

    public int Nivel { get => nivel; set => nivel = value; }
    public int Salud { get => salud; set => salud = value; }
    public int Armadura { get => armadura; set => armadura = value; }
    public int Fuerza { get => fuerza; set => fuerza = value; }
    public int Destreza { get => destreza; set => destreza = value; }

// los datos se generan primero, pode usar eso para 
    public void GenerarEstadisticas(string clase)
    {
        Nivel = 1;
        switch (clase)
        {
            case "Guerrero":
                Salud = 100;
                Armadura = 3;
                Fuerza = 3;
                Destreza = 3;
            break;

            case "Monje":
                Salud = 100;
                Armadura = 2;
                Fuerza = 2;
                Destreza = 4;
            break;

            case "Paladín":
                Salud = 125;
                Armadura = 5;
                Fuerza = 3;
                Destreza = 1;
            break;

            case "Berserker":
                Salud = 150;
                Armadura = 1;
                Fuerza = 5;
                Destreza = 2;
            break;

            case "Gladiador":
                Salud = 125;
                Armadura = 2;
                Fuerza = 4;
                Destreza = 4;
            break;
        }
    }
}
