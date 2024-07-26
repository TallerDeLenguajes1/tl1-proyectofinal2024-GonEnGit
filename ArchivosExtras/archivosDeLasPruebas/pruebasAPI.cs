
namespace EspacioAPI;

using System.Net.Http;
using System.Net.WebSockets;
using System.ComponentModel;
using System.Text.Json;
using EspacioCartas;

// lamentablemente, la API funciona llamando un mazo de cartas primero y despues cartas de ese mazo
// osea que necesitas 2 clases, una para el mazo y una para las cartas

public class API
{
    public static readonly HttpClient cliente = new HttpClient();      // nuevo cliente http
    public static async Task<Mazo> ObtenerIdMazoAsync()
    {
        string urlMazo = $"http://deckofcardsapi.com/api/deck/new/shuffle/?deck_count=1"; // count = n° de mazos

        
        HttpResponseMessage respuesta = await cliente.GetAsync(urlMazo);   // respuesta de la API
        respuesta.EnsureSuccessStatusCode();
        string datosObtenidos = await respuesta.Content.ReadAsStringAsync();
        Mazo nuevoMazo = JsonSerializer.Deserialize<Mazo>(datosObtenidos);

        return nuevoMazo;
    }


    public static async Task<List<Card>> ObtenerCartasAsync(string idMazo)
    {
        // quiza sea mejor pedir 1 mazo al principio del juego 
        // y usar uno solo, pidinedo cartas
        string urlCartas = "https://deckofcardsapi.com/api/deck/" + idMazo + "/draw/?count=2"; // count = n° de cartas
        HttpClient cliente = new HttpClient();
        HttpResponseMessage respuesta = await cliente.GetAsync(urlCartas);
        respuesta.EnsureSuccessStatusCode();
        string datosObtenidos = await respuesta.Content.ReadAsStringAsync();
        Lista cartas = JsonSerializer.Deserialize<Lista>(datosObtenidos);

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