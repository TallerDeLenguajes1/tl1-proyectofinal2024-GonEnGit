
using EspacioPersonajes;

namespace EspacioPartida;

public class Partida
{
    public void GuardarPartida(List<Personaje> lista)
    {

    }

    public void CargarPartida(List<Personaje> lista)
    {

    }

    public bool EjecutarOpcion(List<Personaje> lista, int opcion)
    {
        if (opcion == 1)
        {
            GuardarPartida(lista);
            return false;
        }
        else
        {
            if (opcion == 2)
            {
                GuardarPartida(lista);
            }
            return true;
        }
    }

    // un borrar partida? tambien podes, varias partidas?
}