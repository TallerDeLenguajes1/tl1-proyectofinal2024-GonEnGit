namespace  EspacioPersonajes;


public class Datos
{
// Galdreth -- 10 letras
    string[] nombresHumanos = {"Aelric", "Thalorin", "Branthor", "Valen", "Kaelith", "Darion", "Eredor", "Galanor", "Lothar", "Bob"};
    string[] nombresElfos = {"Aelarion", "Thalindra", "Lirael", "Elendril", "Faelar", "Nymira", "Aerandir", "Thalion", "Galadreth", "Elaria"};
    string[] nombresEnanos = {"Durin", "Thrain", "Bromir", "Gromli", "Durgan", "Thorgrim", "Kargan", "Brokk", "Thalrik", "Grimbol"};
    string[] nombresOrcos = {"Gorbag", "Big Stick", "Bolg", "Lurtz", "Ugluk", "Shagrat", "Grishnakh", "Snaga", "Krashnak", "Murgash"};
// Humano -- 6 letras
    string[] razasPosibles = {"Humano", "Enano", "Elfo", "Orco",};
// 3 de los apodos tienen 22 caracteres
    string[] apodosPosibles = { "El Defensor Inmortal","El Héroe Ancestral","La Espada Infernal","El Cazador Silencioso",
                                "El Espíritu Indomable","Puño de Hierro","Tempestad de Acero","Martir de la Llama",
                                "Comandante de Hierro","El Monarca Olvidado","El Caballero Dragon","El Emperador Codicioso",
                                "El Héroe Antiguo","El Centinela Fantasma","El Rey Omnisciente"};
// Gladiadior y Berserker -- 10 letras
// {"Guerrero", "Monje", "Paladín", "Berserker", "Gladiador"}

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

    public void GenerarDatos(string claseElegida, int id, string[] nombresElegidos, string[] apodosElegidos)
    {
        bool control;
        var rand = new Random();
        int  dia, mes, indiceNombres, indiceApodos;
        int indiceRaza = rand.Next(0,4);
        string diaString, mesString;

        Id = id;
        Clase = claseElegida;
        Raza = razasPosibles[indiceRaza];
        do
        {
            indiceApodos = rand.Next(0,15);
            control = apodosElegidos.Contains(apodosPosibles[indiceApodos]);
        } while (control);
        Apodo = apodosPosibles[indiceApodos];

        dia = rand.Next(1,27);
        if (dia <= 9)
        {
            diaString = "0" + $"{dia}";
        }
        else
        {
            diaString = $"{dia}";
        }
        mes = rand.Next(1,12);
        if (mes <= 9)
        {
            mesString = "0" + $"{mes}";
        }
        else
        {
            mesString = $"{mes}";
        }
        switch (Raza)
        {
            case "Humano":
                int anioHumano = rand.Next(710,731);
                Nacimiento = $"{diaString}/{mesString}/{anioHumano}"; // 20 a 40 años
                Edad = 750 - anioHumano;
                do
                {
                    indiceNombres = rand.Next(1,10);
                    control = nombresElegidos.Contains(nombresHumanos[indiceNombres]);
                } while (control);
                Nombre = nombresHumanos[indiceNombres];
                break;
            case "Enano":
                int anioEnano = rand.Next(600,671);
                Nacimiento = $"{diaString}/{mesString}/{anioEnano}";  // 70 a 150 años
                Edad = 750 - anioEnano;
                do
                {
                    indiceNombres = rand.Next(1,10);
                    control = nombresElegidos.Contains(nombresEnanos[indiceNombres]);
                } while (control);
                Nombre = nombresEnanos[indiceNombres];
                break;
            case "Elfo":
                int anioElfo = rand.Next(450,651);
                Nacimiento = $"{diaString}/{mesString}/{anioElfo}";   // 100 a 300 años
                Edad = 750 - anioElfo;
                do
                {
                    indiceNombres = rand.Next(1,10);
                    control = nombresElegidos.Contains(nombresElfos[indiceNombres]);
                } while (control);
                Nombre = nombresElfos[indiceNombres];
                break;
            case "Orco":
                int anioOrco = rand.Next(710,736);
                Nacimiento = $"{diaString}/{mesString}/{anioOrco}";   // 15 a 40 años
                Edad = 750 - anioOrco;
                do
                {
                    indiceNombres = rand.Next(1,10);
                    control = nombresElegidos.Contains(nombresOrcos[indiceNombres]);
                } while (control);
                Nombre = nombresOrcos[indiceNombres];
                break;
        }
    }
}