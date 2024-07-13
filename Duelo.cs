namespace EspacioDuelos;

using EspacioPersonajes;

public class duelo
{
    public int ContarEnemigosActivos(List<personaje> lista)
    {
        int contador = 0;
        foreach (personaje instancia in lista)
        {
            if (instancia.DatosGenerales.Activo == true)
            {
                contador += 1;
            }
        }
        return contador;
    }
}