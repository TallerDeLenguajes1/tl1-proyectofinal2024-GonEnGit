namespace  EspacioPersonajes;


public class Datos
{
// Galdreth -- 10 letras
    string[] nombresHumanos = {"Aelric", "Thalorin", "Branthor", "Valen", "Kaelith", "Darion", "Eredor", "Galanor", "Lothar", "Bob"};
    string[] nombresElfos = {"Aelarion", "Thalindra", "Lirael", "Elendril", "Faelar", "Nymira", "Aerandir", "Thalion", "Galadreth", "Elaria"};
    string[] nombresEnanos = {"Durin", "Thrain", "Bromir", "Gromli", "Durgan", "Thorgrim", "Kargan", "Brokk", "Thalrik", "Grimbol"};
    string[] nombresOrcos = {"Gorbag", "Big Stick", "Bolg", "Lurtz", "Swan", "Shagrat", "Grishnakh", "Snaga", "Krashnak", "Murgash"};
// Humano -- 6 letras
    string[] razasPosibles = {"Humano", "Enano", "Elfo", "Orco",};
// 3 de los apodos tienen 22 caracteres
    string[] apodosPosibles = { "El Defensor Inmortal","El Héroe Ancestral","La Espada Infernal","El Cazador Silencioso",
                                "Maidenless","Puño de Hierro","Unkindled","Martir de la Llama",
                                "Comandante de Hierro","El Monarca Olvidado","El Caballero Dragon","El Emperador Codicioso",
                                "El Héroe Antiguo","El Centinela Fantasma","El Rey Omnisciente"};
// Gladiadior y Berserker -- 10 letras

    private int id, edad;
    private string clase, nombre;
    private string raza, apodo, nacimiento;

    public int Id { get => id; set => id = value; }
    public int Edad { get => edad; set => edad = value; }
    public string Clase { get => clase; set => clase = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Raza { get => raza; set => raza = value; }
    public string Apodo { get => apodo; set => apodo = value; }
    public string Nacimiento { get => nacimiento; set => nacimiento = value; }

    public Datos(int idNuevo, string nombreNuevo, string apodoNuevo, string claseNuevo, string razaNuevo, int edadNuevo, string nacimientoNuevo)
    {
        Id = idNuevo;
        Nombre = nombreNuevo;
        Apodo = apodoNuevo;
        Clase = claseNuevo;
        Raza = razaNuevo;
        Edad = edadNuevo;
        Nacimiento = nacimientoNuevo;
    }
}