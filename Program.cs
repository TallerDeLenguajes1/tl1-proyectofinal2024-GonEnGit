
using System.Reflection;
using EspacioPersonajes;

personaje[] arreglo = new personaje[10];

for (int i = 0; i < 10; i++)
{
    arreglo[i] = new personaje("SuNombre");
}

foreach (var item in arreglo)
{
    Console.WriteLine(item.Generales.Nombre);
    Console.WriteLine(item.Generales.Apodo);
}
