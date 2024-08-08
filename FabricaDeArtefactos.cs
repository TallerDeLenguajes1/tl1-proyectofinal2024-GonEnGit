namespace EspacioJuego;

public class FabricaDeArtefactos
{
    private string[] nombres = {"Estandarte de la inquisición","Espada en espiral","Espada De Fuego",
                                "Bolsa de monedas de oro","Sabaton","Pocion de piel de plomo",
                                "Mensaje en el suelo","Una torta","Arco sin flechas",
                                "Pocion temporal","Piedra del despertar","Escudo de Gran Inicaitiva",};
    private string[] descripciones = {  "Dev: Una bandera española;Narrador: No esperaba encontrar eso aquí",
                                        "Narrador: Te recuerda a una fogata;que te mantuvo a salvo;Dev: Esa calma aumenta tu salud",
                                        "Narrador: Una mejor arma;Dev: Pero el fuego es irrelevante;en este juego!",
                                        "Narrador: Un monton de dinero;para usar en la tienda;Dev: Si solo hubiera programado una...",
                                        "Narrador: Una mejor armadura;Dev: Y muy buenas canciones",
                                        "Dev: Aun puedes atacar pero no moverte;Narrador: Igualmente siempre es bueno;poder detener una espada con la cara",
                                        "Narrador: \"Tesoros mas adelate\";Dev: no le creas, es un precipicio!",
                                        "Dev: No me voy a negar a una de estas;Narrador: Y esta es de verdad!",
                                        "Narrador: Para que se supone que;sirve esto?;Dev: ... pegales con fuerza?",
                                        "Narrador: deten el tiempo y a entrenar;Dev: no habia presupuesto para; todo un edificio...",
                                        "Dev: Ya está usada...;Narrador: Una pena, intenta no morir...",
                                        "Narrador: Lamentablemente no va a;aumentar la tuya;Dev: pero sigue sirviendo de escudo!",};
    private string[] efectos = {"Destreza","Salud","Fuerza",
                                "Oro","Armadura","Armadura",
                                "Destreza","Salud","Fuerza",
                                "Fuerza","Resurrecciones","Armadura",};
    private int[] cantidades = {2, 10, 1, 
                                10000, 2, 3,
                                3, 15, 2,
                                3, 0, 2};


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