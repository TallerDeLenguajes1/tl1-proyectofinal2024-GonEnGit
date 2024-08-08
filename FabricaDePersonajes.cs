using System.Runtime.CompilerServices;

namespace EspacioPersonajes;



public class FabricaDePersonajes
{
    private int id = 1;
    private string[] razasPosibles = {"Humano", "Enano", "Elfo", "Orco",};
    private string[] clases = { "Guerrero", "Monje", "Paladín", "Berserker", "Gladiador"};

    private string[] nombresHumanos = {"Aelric", "Thalorin", "Valdemar", "Valen", "Kaelith", "Darion", "Eredor", "Galanor", "Lothar", "Bob"};
    private string[] nombresElfos = {"Aelarion", "Thalindra", "Lirael", "Elendril", "Faelar", "Nymira", "Aerandir", "Thalion", "Galadreth", "Elaria"};
    private string[] nombresEnanos = {"Durin", "Thrain", "Bromir", "Gromli", "Durgan", "Thorgrim", "Kargan", "Brokk", "Thalrik", "Grimbol"};
    private string[] nombresOrcos = {"Gorbag", "Big Stick", "Bolg", "Lurtz", "Swan", "Shagrat", "Grishnakh", "Snaga", "Krashnak", "Murgash"};

    private string[] apodosPosibles = { "El Defensor Inmortal","El Héroe Ancestral","La Espada Infernal","El Cazador Silencioso",
                                "Maidenless","Puño de Hierro","Unkindled","Martir de la Llama",
                                "Comandante de Hierro","El Monarca Olvidado","El Caballero Dragon","El Emperador Codicioso",
                                "El Héroe Antiguo","El Centinela Fantasma","El Rey Omnisciente"};

    private Dictionary<string, int> contadorDeClases = new Dictionary<string,int>
    {
        {"Guerrero" , 0}, 
        {"Monje" , 0},
        {"Paladín" , 0},
        {"Berserker" , 0}, 
        {"Gladiador" , 0},
    };

    private string[] nombresElegidos = new string[10];  // no hay mas de 10 nombres en total
    private string[] apodosElegidos = new string[10];   // ni apodos

    public List<Personaje> CreadorDePersonajes()
    {
        List<Personaje> lista = new List<Personaje>();
        for (int vueltas = 0; vueltas < 2; vueltas++)
        {
            foreach (string clase in clases)
            {
                Datos datos = GenerarDatos(clase, id);
                Estadisticas estadisticas = new Estadisticas(clase);
                Personaje instancia = new Personaje(datos, estadisticas);

                contadorDeClases[clase] += 1;
                nombresElegidos[id - 1] = instancia.DatosGenerales.Nombre;
                apodosElegidos[id - 1] = instancia.DatosGenerales.Apodo;
                id += 1;
                lista.Add(instancia);
            }
        }
        return lista;
    }

    public Datos GenerarDatos(string claseElegida, int id)
    {
        bool control;
        var rnd = new Random();
        int dia, mes, edad = 0, indiceNombres, indiceApodos, indiceRaza;
        string nombre = "", apodo, raza, nacimiento = "", diaString, mesString;

        indiceRaza = rnd.Next(0,4);
        raza = razasPosibles[indiceRaza];

        do
        {
            indiceApodos = rnd.Next(0,15);
            control = apodosElegidos.Contains(apodosPosibles[indiceApodos]);
        } while (control);
        apodo = apodosPosibles[indiceApodos];

        dia = rnd.Next(1,27);
        if (dia <= 9)
        {
            diaString = "0" + $"{dia}";
        }
        else
        {
            diaString = $"{dia}";
        }
        mes = rnd.Next(1,12);
        if (mes <= 9)
        {
            mesString = "0" + $"{mes}";
        }
        else
        {
            mesString = $"{mes}";
        }
        switch (raza)
        {
            case "Humano":
                int anioHumano = rnd.Next(710,731);
                nacimiento = $"{diaString}/{mesString}/{anioHumano}"; // 20 a 40 años
                edad = 750 - anioHumano;
                do
                {
                    indiceNombres = rnd.Next(1,10);
                    control = nombresElegidos.Contains(nombresHumanos[indiceNombres]);
                } while (control);
                nombre = nombresHumanos[indiceNombres];
                break;
            case "Enano":
                int anioEnano = rnd.Next(600,671);
                nacimiento = $"{diaString}/{mesString}/{anioEnano}";  // 70 a 150 años
                edad = 750 - anioEnano;
                do
                {
                    indiceNombres = rnd.Next(1,10);
                    control = nombresElegidos.Contains(nombresEnanos[indiceNombres]);
                } while (control);
                nombre = nombresEnanos[indiceNombres];
                break;
            case "Elfo":
                int anioElfo = rnd.Next(450,651);
                nacimiento = $"{diaString}/{mesString}/{anioElfo}";   // 100 a 300 años
                edad = 750 - anioElfo;
                do
                {
                    indiceNombres = rnd.Next(1,10);
                    control = nombresElegidos.Contains(nombresElfos[indiceNombres]);
                } while (control);
                nombre = nombresElfos[indiceNombres];
                break;
            case "Orco":
                int anioOrco = rnd.Next(710,736);
                nacimiento = $"{diaString}/{mesString}/{anioOrco}";   // 15 a 40 años
                edad = 750 - anioOrco;
                do
                {
                    indiceNombres = rnd.Next(1,10);
                    control = nombresElegidos.Contains(nombresOrcos[indiceNombres]);
                } while (control);
                nombre = nombresOrcos[indiceNombres];
                break;
        }

        Datos nuevosDatos = new Datos(id, nombre, apodo, claseElegida, raza, edad, nacimiento);
        return nuevosDatos;
    }

    public int[] GenerarOrdenAleatorio()
    {
        int[] ordenGenerado = new int[9];
        int numeroAleatorio;
        Random rnd = new Random();

        for (int indice = 0; indice < 9; indice++)
        {
            ordenGenerado[indice] = 21; // necesitas el 0, cambias todos los valores
        }
        for (int indice = 0; indice < 9; indice++)
        {
            do
            {
                numeroAleatorio = rnd.Next(0, 9);
            } while (ordenGenerado.Contains(numeroAleatorio));
            ordenGenerado[indice] = numeroAleatorio;
        }
        return ordenGenerado;
    }

    public List<Personaje> MezclarLista(int eleccion, List<Personaje> listaTemp)
    {
        int[] orden = GenerarOrdenAleatorio();
        Personaje auxiliar;
        List<Personaje> listaFinal = new List<Personaje>();

        auxiliar = listaTemp[eleccion];     // guardas pj elegido en el lugar 0
        auxiliar.DatosGenerales.Id = 1;
        listaFinal.Add(auxiliar);
        listaTemp.RemoveAt(eleccion);

        for (int indice = 0; indice < listaTemp.Count; indice++)
        {
            auxiliar = listaTemp[orden[indice]];
            auxiliar.DatosGenerales.Id = indice + 2;
            listaFinal.Add(auxiliar);
        }

        return listaFinal;
    }
}
