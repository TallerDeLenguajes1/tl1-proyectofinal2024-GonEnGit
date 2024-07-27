namespace EspacioAPI;

// lamentablemente, la API funciona llamando un mazo de cartas primero y despues cartas de ese mazo
// osea que necesitas 2 clases, una para el mazo y una para las cartas

// esto es confuso... 

// si se pueden remover campos de estas clases,
// incluso podes remover toda una sub clase

// pero no podes cambiar los nombres de los campos
// es Deserialize no sabe donde poner los datos

public class Mazo
{
    public string deck_id { get; set; }
    public int remaining { get; set; }
}

public class Carta
{
    public string value { get; set; }
    public string suit { get; set; }
}

public class ListaCartas
{
    public string deck_id { get; set; }
    public List<Carta> cards { get; set; }
    public int remaining { get; set; }
}
