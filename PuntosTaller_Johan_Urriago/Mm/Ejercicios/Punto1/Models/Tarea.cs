namespace _01_ListaTareas.Models;

public class Tarea
{
    public int Id { get; set; }
    public string Titulo { get; set; } = "";
    public bool Completada { get; set; }
}

// Modelo: guarda y administra las tareas en memoria.
public static class ListaTareas
{
    private static readonly List<Tarea> _tareas = new();
    private static int _siguienteId = 1;

    public static IReadOnlyList<Tarea> Todas() => _tareas;

    public static void Agregar(string titulo)
    {
        if (string.IsNullOrWhiteSpace(titulo)) return;
        _tareas.Add(new Tarea { Id = _siguienteId++, Titulo = titulo.Trim() });
    }

    public static void Eliminar(int id) => _tareas.RemoveAll(t => t.Id == id);

    public static void Alternar(int id)
    {
        var t = _tareas.FirstOrDefault(x => x.Id == id);
        if (t != null) t.Completada = !t.Completada;
    }
}
