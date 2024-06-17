
namespace  EspacioPersonajes;

public class personaje
{
    private datos generales = new datos();
    private characteristicas estadisticas = new characteristicas();

    public datos datosGenerales { get => generales; set => generales = value; }
    public characteristicas Estadisticas { get => estadisticas; set => estadisticas = value; }

    public personaje(string claseElegida)
    {
    // a estos 2 los podes usar para cambiar stats segun la clase
        int indiceApodos = new Random().Next(1,3);

        datosGenerales.GenerarDatos(claseElegida);
        //Estadisticas.GenerarEstadisticas(claseElegida);
    }

    public string devolverDatos()
    {
        return $"Nombre: {datosGenerales.Nombre} | Apodo: {datosGenerales.Apodo}" +
                $"| Clase: {datosGenerales.Clase} | Raza: {datosGenerales.Raza} " +
                $"| Nacimiento: {datosGenerales.Apodo} | Edad: {datosGenerales.Edad}";
    }
}
