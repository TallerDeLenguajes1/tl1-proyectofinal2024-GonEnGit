
namespace EspacioCartas;

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

// mirá vos... si se pueden eliminar campos de las clases
// la aPI parece funcionar igualmente
public class Mazo
{
    public string deck_id { get; set; }
    public int remaining { get; set; }
}

public class Card
{
    public string value { get; set; }
    public string suit { get; set; }
}

public class Lista
{
    public string deck_id { get; set; }
    public List<Card> cards { get; set; }
    public int remaining { get; set; }
}