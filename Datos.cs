namespace  EspacioPersonajes;


public class Datos
{
    private int id, edad;
    private string clase, nombre;
    private string raza, apodo, nacimiento;

    public int Id { get => id; set => id = value; }
    public int Edad { get => edad; set => edad = value; }
    public string Clase { get => clase; set => clase = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Raza { get => raza; set => raza = value; }
    public string Apodo { get => apodo; set => apodo = value; }
    public string Nacimiento { get => nacimiento; set => nacimiento = value; }

    public Datos(){}

    public Datos(int idNuevo, string nombreNuevo, string apodoNuevo, string claseNuevo, string razaNuevo, int edadNuevo, string nacimientoNuevo)
    {
        Id = idNuevo;
        Nombre = nombreNuevo;
        Apodo = apodoNuevo;
        Clase = claseNuevo;
        Raza = razaNuevo;
        Edad = edadNuevo;
        Nacimiento = nacimientoNuevo;
    }
}