namespace EspacioAPI;

using System.Net.Http;
using System.Text.Json;
using EspacioCartas;

// lamentablemente, la API funciona llamando un mazo de cartas primero y despues cartas de ese mazo
// osea que necesitas 2 clases, una para el mazo y una para las cartas

public class API
{
    public static readonly HttpClient cliente = new HttpClient();      // nuevo cliente http

// llamas un mazo al principio del juego, lo guardas? tarda bastante en conectar
    public static async Task<Mazo> ObtenerIdMazoAsync()
    {
        string urlMazo = $"http://deckofcardsapi.com/api/deck/new/shuffle/?deck_count=1"; // count = n° de mazos

        HttpResponseMessage respuesta = await cliente.GetAsync(urlMazo);   // respuesta de la API
        respuesta.EnsureSuccessStatusCode();
        string datosObtenidos = await respuesta.Content.ReadAsStringAsync();
        Mazo nuevoMazo = JsonSerializer.Deserialize<Mazo>(datosObtenidos);

        return nuevoMazo;
    }

// pedis cartas cuando haga falta.... quiza sea mejor pedir todo junto, no necesitas tantas cartas
    public static async Task<List<Card>> ObtenerCartasAsync(string idMazo)
    {
        string urlCartas = "https://deckofcardsapi.com/api/deck/" + idMazo + "/draw/?count=2"; // count = n° de cartas

        HttpClient cliente = new HttpClient();
        HttpResponseMessage respuesta = await cliente.GetAsync(urlCartas);
        respuesta.EnsureSuccessStatusCode();
        string datosObtenidos = await respuesta.Content.ReadAsStringAsync();
        Root cartas = JsonSerializer.Deserialize<Root>(datosObtenidos);

        return cartas.cards;
    }
}

/*
static async Task<Chistes> ObtenerChistesAsync()
{
    NOTA: vos lo copiaste basicamente
    pero no tenes que usar try catch por ahora

    var url = $"https://official-joke-api.appspot.com/random_joke";
    try
    {
        HttpClient client = new HttpClient();
        HttpResponseMessage response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();
        Chistes chiste = JsonSerializer.Deserialize<Chistes>(responseBody);
        return chiste;
    }
    catch (HttpRequestException e)
    {
        Console.WriteLine("Problemas de acceso a la API");
        Console.WriteLine("Message :{0} ", e.Message);
        return null;
    }
}
*/