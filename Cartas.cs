
namespace EspacioCartas;

// lamentablemente, no podes cambiar los nombres de los campos
// si podes cambiar los nombres de las clases en si segun parece
public class Mazo
{
    public bool success { get; set; }
    public string deck_id { get; set; }
    public int remaining { get; set; }
    public bool shuffled { get; set; }
}

public class Card
{
    public string code { get; set; }
    public string image { get; set; }
    public Images images { get; set; }
    public string value { get; set; }
    public string suit { get; set; }
}

public class Images
{
    public string svg { get; set; }
    public string png { get; set; }
}

public class Root
{
    public bool success { get; set; }
    public string deck_id { get; set; }
    public List<Card> cards { get; set; }
    public int remaining { get; set; }
}
