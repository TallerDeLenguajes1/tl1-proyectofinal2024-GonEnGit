namespace  EspacioPersonajes;


public class Datos
{
    // cuando tengas los nombres y apodos finales
    // cada uno tiene que llevar una serie de espacios antes y/o despues
    // para poder armar las tarjetas, no te olvides eso ----------------

    string[] nombresPosibles = {"Moe", "Larry", "Curly", "Moe", "Larry", "Curly", "Moe", "Larry", "Curly", "Moe", "Larry", "Curly",};
    string[] apodosPosibles = {"apodo1", "apodo2", " apodo3", "apodo11", "apodo52", " apodo34", "apodo81", "apodo29", " apodo32", "apodo31", "apodo21",};
    string[] razasPosibles = {"Humano", "Enano", "Elfo", "Orco",};

    private int id;
    private int edad;
    private string clase;
    private string nombre;
    private string raza;
    private string apodo;
    private string nacimiento;

    public int Id { get => id; set => id = value; }
    public int Edad { get => edad; set => edad = value; }
    public string Clase { get => clase; set => clase = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Raza { get => raza; set => raza = value; }
    public string Apodo { get => apodo; set => apodo = value; }
    public string Nacimiento { get => nacimiento; set => nacimiento = value; }

    public void GenerarDatos(string claseElegida, int id)
    {
    // semilla para los aleatorios
        var rand = new Random();

    // guardas el id
        Id = id;

    // la clase se genera en Program.cs para poder contarlas
        Clase = claseElegida;

    // nombre y apodo son aleatorios, como controlas que no se repitan ...?
        int indiceNombre = rand.Next(0, 12);
        Nombre = nombresPosibles[indiceNombre];
        int indiceApodos = rand.Next(0, 11);
        Apodo = apodosPosibles[indiceApodos];
        int indiceRaza = rand.Next(0, 4);
        Raza = razasPosibles[indiceRaza];

    // los nacimientos van a ser un lio, supongamos que el juego
    // se hace en algun calendario en el año 750
    // no queda otra que calcular los nacimientos posibles de cada raza
    // no podes usar DateTima para hacer fechas con años de 3 digitos, 
    // tenes que armarlas de otra forma... al final lo unico que pudiste
    // hacer fue lo viejo
        switch (Raza)
        {
            case "Humano":
                int anioHumano = rand.Next(710, 731);
                Nacimiento = $"{rand.Next(0,25)}/{rand.Next(0,12)}/{anioHumano}"; // 20 a 40 años
                Edad = 750 - anioHumano;
                break;
            case "Enano":
                int anioEnano = rand.Next(600, 671);
                Nacimiento = $"{rand.Next(0,25)}/{rand.Next(0,12)}/{anioEnano}";  // 70 a 150 años
                Edad = 750 - anioEnano;
                break;
            case "Elfo":
                int anioElfo = rand.Next(450, 651);
                Nacimiento = $"{rand.Next(0,25)}/{rand.Next(0,12)}/{anioElfo}";   // 100 a 300 años
                Edad = 750 - anioElfo;
                break;
            case "Orco":
                int anioOrco = rand.Next(710, 736);
                Nacimiento = $"{rand.Next(0,25)}/{rand.Next(0,12)}/{anioOrco}";   // 15 a 40 años
                Edad = 750 - anioOrco;
                break;
        }
    }
}