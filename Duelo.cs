namespace EspacioDuelos;

using EspacioPersonajes;

public class Duelo
{
    public int ContarEnemigosActivos(List<Personaje> lista)
    {
        int contador = 1;   // no en 0, tenes que tener en cuenta el jugador
        if (lista[0].Estadisticas.Salud == 0)
        {
            contador = -1;
        }
        else
        {
            foreach (Personaje instancia in lista)
            {
                if (instancia.Estadisticas.Salud == 0)
                {
                    contador += 1;
                }
            }
        }
        return contador;
    }

    public double CalcularIniciativa(Personaje pers)
    {
        return pers.Estadisticas.Velocidad * 0.5 + 2;   // esta ecuacion cambiala, es cualquier cosa
    }
}