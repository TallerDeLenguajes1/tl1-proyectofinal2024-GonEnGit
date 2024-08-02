namespace  EspacioPersonajes;


public class Datos
{
    // cuando tengas los nombres y apodos finales
    // cada uno tiene que llevar una serie de espacios antes y/o despues
    // para poder armar las tarjetas, no te olvides eso ----------------

    string[] nombresHumanos = {"Aelric", "Thalorin", "Branthor", "Valen", "Kaelith", "Darion", "Eredor", "Galanor", "Lothar", "Vandar"};
    string[] nombresElfos = {"Aelarion", "Thalindra", "Lirael", "Elendril", "Faelar", "Nymira", "Aerandir", "Thalion", "Galadreth", "Elaria"};
    string[] nombresEnanos = {"Durin", "Thrain", "Bromir", "Gromli", "Durgan", "Thorgrim", "Kargan", "Brokk", "Thalrik", "Grimbol"};
    string[] nombresOrcos = {"Gorbag", "Azog", "Bolg", "Lurtz", "Ugluk", "Shagrat", "Grishnakh", "Snaga", "Krashnak", "Murgash"};
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

    public void GenerarDatos(string claseElegida, int id, string[] nombresElegidos)
    {
        bool control;
        var rand = new Random();
        int indiceNombres;
        int indiceRaza = rand.Next(0, 4), indiceApodos = rand.Next(0, 11);

        Id = id;
        Clase = claseElegida;

    // nombre y apodo son aleatorios, como controlas que no se repitan ...?
        Raza = razasPosibles[indiceRaza];
        Apodo = apodosPosibles[indiceApodos];

        switch (Raza)
        {
            case "Humano":
                int anioHumano = rand.Next(710, 731);
                Nacimiento = $"{rand.Next(0,25)}/{rand.Next(0,12)}/{anioHumano}"; // 20 a 40 años
                Edad = 750 - anioHumano;
                do
                {
                    indiceNombres = rand.Next(0,10);
                    control = nombresElegidos.Contains(nombresHumanos[indiceNombres]);
                } while (control);
                Nombre = nombresHumanos[indiceNombres];
                break;
            case "Enano":
                int anioEnano = rand.Next(600, 671);
                Nacimiento = $"{rand.Next(0,25)}/{rand.Next(0,12)}/{anioEnano}";  // 70 a 150 años
                Edad = 750 - anioEnano;
                do
                {
                    indiceNombres = rand.Next(0,10);
                    control = nombresElegidos.Contains(nombresEnanos[indiceNombres]);
                } while (control);
                Nombre = nombresEnanos[indiceNombres];
                break;
            case "Elfo":
                int anioElfo = rand.Next(450, 651);
                Nacimiento = $"{rand.Next(0,25)}/{rand.Next(0,12)}/{anioElfo}";   // 100 a 300 años
                Edad = 750 - anioElfo;
                do
                {
                    indiceNombres = rand.Next(0,10);
                    control = nombresElegidos.Contains(nombresElfos[indiceNombres]);
                } while (control);
                Nombre = nombresElfos[indiceNombres];
                break;
            case "Orco":
                int anioOrco = rand.Next(710, 736);
                Nacimiento = $"{rand.Next(0,25)}/{rand.Next(0,12)}/{anioOrco}";   // 15 a 40 años
                Edad = 750 - anioOrco;
                do
                {
                    indiceNombres = rand.Next(0,10);
                    control = nombresElegidos.Contains(nombresOrcos[indiceNombres]);
                } while (control);
                Nombre = nombresOrcos[indiceNombres];
                break;
        }
    }
}