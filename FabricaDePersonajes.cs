namespace EspacioPersonajes;


using System.Data;
using System.Runtime.CompilerServices;


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

    public int[] GenerarOrdenAleatorio()
    {
        int[] ordenGenerado = new int[9];
        int numeroAleatorio;
        Random rnd = new Random();

        for (int indice = 0; indice < 9; indice++)     // necesitas el 0 pero inicializa con 0 por defecto
        {
            ordenGenerado[indice] = 721;                       // cambias los valores a otra cosa
        }
        for (int indice = 0; indice < 9; indice++)     // generas el nuevo ordenGenerado
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

        // guardas pj elegido en el lugar 0
        auxiliar = listaTemp[eleccion];
        auxiliar.DatosGenerales.Id = 1;     // cambias el id
        listaFinal.Add(auxiliar);           // guardas el personaje elegido
        listaTemp.RemoveAt(eleccion);       // lo sacas de la lista Temporal

        // mezclas la lista para que sea aleatoria siempre
        // count devuelve la cantidad de personajes
        // el indice va a ser .Count - 1
        for (int indice = 0; indice < listaTemp.Count; indice++)
        {
            auxiliar = listaTemp[orden[indice]];
            auxiliar.DatosGenerales.Id = indice + 2;
            listaFinal.Add(auxiliar);
        }

        return listaFinal;
    }
}
