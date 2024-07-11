
using System.Runtime.CompilerServices;

namespace  EspacioPersonajes;

public class personaje
{
    private int contador = 0;
    private datos generales = new datos();
    private characteristicas estadisticas = new characteristicas();

    public datos datosGenerales { get => generales; set => generales = value; }
    public characteristicas Estadisticas { get => estadisticas; set => estadisticas = value; }

    public static List<personaje> FabricaDePersonajes(List<personaje> lista)
    {
    // por como armaste los metodos para generar los datos
    // si o si tenes que elegir las clases y mandarlas
        int id = 1;
        int contador = 0;
        string[] Clases = { "Guerrero", "Monje", "Arquero", "Luchador", "Ladrón",};

        for (int indice = 0; indice < 10; indice++)
        {
            personaje instancia = new personaje();

            instancia.datosGenerales.GenerarDatos(Clases[contador], id);
            instancia.Estadisticas.GenerarEstadisticas(Clases[contador]);
            contador += 1;
        // solo tenes 5 clases, lo tenes que reiniciar en algun momento
            if (contador == 5)
            {
                contador = 0;
            }
            lista.Add(instancia);
        }

        return lista;
    }

    public string DevolverDatos()
    {
        return  $"{datosGenerales.Nombre};{datosGenerales.Apodo};{datosGenerales.Clase};" +
                $"{datosGenerales.Raza};{datosGenerales.Nacimiento};{datosGenerales.Edad}";
    }

    public string DevolverEstadisticas()
    {
        return  $"{Estadisticas.Salud};{Estadisticas.Armadura};{Estadisticas.Fuerza};" +
                $"{Estadisticas.Destreza};{Estadisticas.Velocidad}";
    }
}
