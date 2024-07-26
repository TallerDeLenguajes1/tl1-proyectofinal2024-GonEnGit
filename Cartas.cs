
namespace EspacioCartas;

// si se pueden remover campos de estas clases,
// incluso podes remover toda una sub clase

// si podes cambiar los nombres de las clases en si segun parece
public class Mazo
{
    public string idMAzo { get; set; }
    public int restantes { get; set; }
}

public class Carta
{
    public string valor { get; set; }
    public string palo { get; set; }
}

public class Lista
{
    public string idMAzo { get; set; }
    public List<Carta> listaCartas { get; set; }
    public int restantes { get; set; }
}
