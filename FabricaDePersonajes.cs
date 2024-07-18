using System.Runtime.CompilerServices;

namespace EspacioPersonajes;

public class FabricaDePersonajes
{
    // por como armaste los metodos para generar los datos
    // si o si tenes que elegir las clases y mandarlas
        private int id = 1;
        private string[] Clases = { "Guerrero", "Monje", "Arquero", "Luchador", "Ladrón"};
        private Dictionary<string, int> ContadorDeClases = new Dictionary<string,int>
        {
            {"Guerrero" , 0}, 
            {"Monje" , 0},
            {"Arquero" , 0},
            {"Luchador" , 0}, 
            {"Ladrón" , 0},
        };

    public List<Personaje> CreadorDePersonajes(List<Personaje> lista)
    {
        for (int vueltas = 0; vueltas < 2; vueltas++)
        {
            foreach (string clase in Clases)
            {
                Personaje instancia = new Personaje();
    
                instancia.DatosGenerales.GenerarDatos(clase, id);
                instancia.Estadisticas.GenerarEstadisticas(clase);
                id += 1;
                ContadorDeClases[clase] += 1;
                lista.Add(instancia);
            }
        }

        return lista;
    }

    public List<Personaje> MezclarLista(List<Personaje> listaFinal, int[] orden, List<Personaje> listaTemp)
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