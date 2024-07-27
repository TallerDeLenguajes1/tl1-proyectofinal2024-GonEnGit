namespace  EspacioPersonajes;


public class Caracteristicas
{
    private int nivel;
    private int salud;
    private int armadura;
    private int fuerza;
    private int destreza;
    private int velocidad;

    public int Nivel { get => nivel; set => nivel = value; }
    public int Salud { get => salud; set => salud = value; }
    public int Armadura { get => armadura; set => armadura = value; }
    public int Fuerza { get => fuerza; set => fuerza = value; }
    public int Destreza { get => destreza; set => destreza = value; }
    public int Velocidad { get => velocidad; set => velocidad = value; }

// los datos se generan primero, pode usar eso para 
    public void GenerarEstadisticas(string clase)
    {
        Nivel = 1;
        switch (clase)
        {
            case "Guerrero":
                Salud = 100;
                Armadura = 4;
                Fuerza = 2;
                Destreza = 2;
                Velocidad = 1;
            break;

            case "Monje":
                Salud = 100;
                Armadura = 2;
                Fuerza = 3;
                Destreza = 2;
                Velocidad = 1;
            break;

            case "Arquero":
                Salud = 100;
                Armadura = 2;
                Fuerza = 1;
                Destreza = 3;
                Velocidad = 1;
            break;

            case "Luchador":
                Salud = 125;
                Armadura = 1;
                Fuerza = 3;
                Destreza = 2;
                Velocidad = 1;
            break;

            case "Ladrón":
                Salud = 100;
                Armadura = 1;
                Fuerza = 3;
                Destreza = 3;
                Velocidad = 3;
            break;
        }
    }
}
