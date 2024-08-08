namespace EspacioAPI;


using System.Net.Http;
using System.Text.Json;


public static class API
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
    public static async Task<List<Carta>> ObtenerCartasAsync(string idMazo)
    {
        string urlCartas = "https://deckofcardsapi.com/api/deck/" + idMazo + "/draw/?count=2"; // count = n° de cartas

        HttpClient cliente = new HttpClient();
        HttpResponseMessage respuesta = await cliente.GetAsync(urlCartas);
        respuesta.EnsureSuccessStatusCode();
        string datosObtenidos = await respuesta.Content.ReadAsStringAsync();
        ListaCartas cartasRecibidas = JsonSerializer.Deserialize<ListaCartas>(datosObtenidos);

        return cartasRecibidas.cards;
    }
}