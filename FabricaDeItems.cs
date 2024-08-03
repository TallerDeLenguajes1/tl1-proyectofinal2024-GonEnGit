namespace EspacioJuego;

public class FabricaDeArtefactos
{
// la idea es que todos coinciden y el id va a ser el indice
    private string[] nombres = { "nom1","nom2","nom3","nom4","nom5","nom6","nom7","nom8","nom9","nom10","nom11","nom12",};
    private string[] descripciones = { "desc1","desc2","desc3","desc4","desc5","desc6","desc7","desc8","desc9","desc10","desc11","desc12",};
    private string[] efectos = {"efect1","efect2","efect3","efect4","efect5","efect6","efect7","efect8","efect9","efect10","efect11","efect12",};
    private int[] cantidades = {1,2,3,4,5,6,7,8,9,10,11,12};


    public List<Artefacto> CreadorDeArtefactos(List<Artefacto> lista)
    {
        Random rnd = new Random();
        int numElegido;
        string[] artElegidos = new string[12];

        for (int iter = 0; iter < 12; iter++)
        {
            do  // basicamente, lo mismo que hiciste para nombres y apodos
            {
                numElegido = rnd.Next(0, 12);
            } while (artElegidos.Contains(nombres[numElegido]));
            artElegidos[iter] = nombres[numElegido];
            Artefacto iteracion = new Artefacto
            {
                Id = iter + 1,
                Nombre = nombres[numElegido],
                Descripcion = descripciones[numElegido],
                Efecto = efectos[numElegido],
                Cantidad = cantidades[numElegido],
            };
            lista.Add(iteracion);
        }
        return lista;
    }
}