namespace EspacioJuego;

public class FabricaDeArtefactos
{
// la idea es que todos coinciden y el id va a ser el indice
    private string[] nombres = {    "Estandarte de la inquisición","Espada en espiral","Espada De Fuego","nom4",
                                    "nom5","nom6","nom7","nom8","nom9","nom10","nom11","nom12",};
    private string[] descripciones = {  "Dev: Una bandera española;Narrador: No esperaba encontrar eso aquí",
                                        "Narrador: Te recuerda a una fogata;que te mantuvo seguro;Esa calma aumenta tu salud",
                                        "Narrador: Una mejor arma;Dev: Pero el fuego es irrelevante;en este juego!",
                                        "desc4;part2",
                                        "desc5;part2",
                                        "desc6;part2",
                                        "desc7;part2",
                                        "desc8;part2",
                                        "desc9;part2",
                                        "desc10;part2",
                                        "desc11;part2",
                                        "desc12;part2",};
    private string[] efectos = {"Destreza","Salud","Fuerza","efect4","efect5","efect6",
                                "efect7","efect8","efect9","efect10","efect11","efect12",};
    private int[] cantidades = {2, 10, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0};


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
                Nombre = nombres[numElegido],
                Descripcion = descripciones[numElegido].Split(";"),
                Efecto = efectos[numElegido],
                Cantidad = cantidades[numElegido],
            };
            lista.Add(iteracion);
        }
        return lista;
    }

    public void PasarAInventario(int entradaDeUsuario,List<Artefacto> cofre, List<Artefacto> inventario)
    {
        inventario.Add(cofre[entradaDeUsuario]);
        cofre.RemoveAt(entradaDeUsuario);
        MezclarCofre(cofre);
    }

    public void MezclarCofre(List<Artefacto> cofre)
    {
        Random rnd = new Random();
        int indiceAleatorio;
        List<Artefacto> cofreTemp = new List<Artefacto>();

        while (cofre.Count() > 0)
        {
            indiceAleatorio = rnd.Next(0, cofre.Count());
            cofreTemp.Add(cofre[indiceAleatorio]);
            cofre.RemoveAt(indiceAleatorio);
        }

        foreach (Artefacto Art in cofreTemp)
        {
            cofre.Add(Art);
        }
        cofreTemp.Clear();
    }
}