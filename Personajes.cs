
using System.Runtime.CompilerServices;

namespace  EspacioPersonajes;

public class Personaje
{
    private bool vencido = false;
    private Datos generales = new Datos();
    private Caracteristicas estadisticas = new Caracteristicas();

    public bool Vencido { get => vencido; set => vencido = value; }
    public Datos DatosGenerales { get => generales; set => generales = value; }
    public Caracteristicas Estadisticas { get => estadisticas; set => estadisticas = value; }

    public static List<Personaje> FabricaDePersonajes(List<Personaje> lista)
    {
    // por como armaste los metodos para generar los datos
    // si o si tenes que elegir las clases y mandarlas
        int id = 1;
        int contador = 0;
        string[] Clases = { "Guerrero", "Monje", "Arquero", "Luchador", "Ladrón",};

        for (int indice = 0; indice < 10; indice++)
        {
            // elegir una clase con random, iniciar ocntador en 0, recorrer la lista
            // por cada coincidencia entre la clase randomizada y las que ya estan en la lista
            // sumar 
            Personaje instancia = new Personaje();

            instancia.DatosGenerales.GenerarDatos(Clases[contador], id);
            instancia.Estadisticas.GenerarEstadisticas(Clases[contador]);
            id += 1;
            contador += 1;
        // solo tenes 5 clases, lo tenes que reiniciar en algun momento
            if (contador == 5)
            {
                contador = 0;
            }
            lista.Add(instancia);
        }

        return lista;
    }

    public string DevolverDatos()
    {
        return  $"{DatosGenerales.Id};{DatosGenerales.Nombre};{DatosGenerales.Apodo};{DatosGenerales.Clase};" +
                $"{DatosGenerales.Raza};{DatosGenerales.Nacimiento};{DatosGenerales.Edad}";
    }

    public string DevolverEstadisticas()
    {
        return  $"{Estadisticas.Salud};{Estadisticas.Armadura};{Estadisticas.Fuerza};" +
                $"{Estadisticas.Destreza};{Estadisticas.Velocidad}";
    }

    public static List<Personaje> MezclarLista(List<Personaje> listaFinal, int[] orden, List<Personaje> listaTemp)
    {
        // count devuelve cuanto la cantidad de personajes
        // el indice va a ser .Count - 1
        for (int indice = 0; indice < listaTemp.Count; indice++)
        {
            Personaje auxiliar = listaTemp[orden[indice]];
            auxiliar.DatosGenerales.Id = indice + 2;
            listaFinal.Add(auxiliar);
        }

        return listaFinal;
    }
}
