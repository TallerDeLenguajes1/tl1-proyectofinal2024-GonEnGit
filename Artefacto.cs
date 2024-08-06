using System.Data.Common;

namespace EspacioJuego;

public class Artefacto
{

    private string nombre;
    private string[] descripcion;
    private string efecto;
    private int cantidad;

    public string Nombre { get => nombre; set => nombre = value; }
    public string[] Descripcion { get => descripcion; set => descripcion = value; }
    public string Efecto { get => efecto; set => efecto = value; }
    public int Cantidad { get => cantidad; set => cantidad = value; }
}