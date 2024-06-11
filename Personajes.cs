
using Microsoft.VisualBasic;

namespace  EspacioPersonajes;


public class personaje
{
// la verdad no hace falta un diccionario, podrias usar 2 arreglos
    private Dictionary<int, string> clases = new Dictionary<int, string>()
    {
        {1, "Guerrero"},
        {2, "Mago"},
        {3, "Arquero"},
    };
    private string[] apodos = {"apodo1", "apodo2", " apodo3"};

    private datos generales;
    private characteristicas estadisticas;

    internal datos Generales { get => generales; set => generales = value; }
    internal characteristicas Estadisticas { get => estadisticas; set => estadisticas = value; }

    public personaje(string nom)
    {
    // a estos 2 los podes usar para cambiar stats segun la clase
        int indiceApodos = new Random().Next(1,3);
        int indiceClases = new Random().Next(1,3);

        Generales.GenerarDatos(nom, clases[indiceClases], apodos[indiceApodos]);
       //estadisticas.GenerarEstadisticas();
    }
}


internal class datos
{

    private string tipo;
    private string nombre;
    private string apodo;
    private DateTime nacimiento;
    private int edad;

    public string Tipo { get => tipo; set => tipo = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Apodo { get => apodo; set => apodo = value; }
    public DateTime Nacimiento { get => nacimiento; set => nacimiento = value; }
    public int Edad { get => edad; set => edad = value; }

    public void GenerarDatos(string nom, string clase, string apodo)
    {
        Tipo = clase;
        Nombre = nom;
        Apodo = apodo;
    // podrias intentar hacer una fecha aleatoria
        Nacimiento = new DateTime(1523, 5, 21);
    // y en base a eso la edad, o podrias incluir razas y
    // decidir edad y nacimiento segun eso
        Edad = 150;
    }
}

internal class characteristicas
{

    private int velocidad;
    private int destreza;
    private int fuerza;
    private int nivel;
    private int armadura;
    private int salud;

    public int Velocidad { get => velocidad; set => velocidad = value; }
    public int Destreza { get => destreza; set => destreza = value; }
    public int Fuerza { get => fuerza; set => fuerza = value; }
    public int Nivel { get => nivel; set => nivel = value; }
    public int Armadura { get => armadura; set => armadura = value; }
    public int Salud { get => salud; set => salud = value; }

// los datos se generan primero, pode usar eso para 
    public void GenerarEstadisticas()
    {

    }
}