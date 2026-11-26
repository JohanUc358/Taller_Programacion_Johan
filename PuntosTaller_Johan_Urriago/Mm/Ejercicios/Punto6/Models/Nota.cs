namespace _06_GestorNotas.Models;

public class Nota
{
    public int Id { get; set; }
    public string Titulo { get; set; } = "";
    public string Contenido { get; set; } = "";
    public string Categoria { get; set; } = "General";
    public DateTime Creada { get; set; } = DateTime.Now;
}

// Modelo: guarda notas en memoria con búsqueda y filtro por categoría.
public static class RepositorioNotas
{
    private static readonly List<Nota> _notas = new();
    private static int _id = 1;

    public static void Guardar(Nota n)
    {
        n.Id = _id++;
        n.Creada = DateTime.Now;
        if (string.IsNullOrWhiteSpace(n.Categoria)) n.Categoria = "General";
        _notas.Add(n);
    }

    public static void Eliminar(int id) => _notas.RemoveAll(n => n.Id == id);

    public static IEnumerable<string> Categorias() =>
        _notas.Select(n => n.Categoria).Distinct().OrderBy(c => c);

    public static IEnumerable<Nota> Buscar(string? texto, string? categoria)
    {
        var q = _notas.AsEnumerable();
        if (!string.IsNullOrWhiteSpace(categoria)) q = q.Where(n => n.Categoria == categoria);
        if (!string.IsNullOrWhiteSpace(texto))
            q = q.Where(n => n.Titulo.Contains(texto, StringComparison.OrdinalIgnoreCase)
                          || n.Contenido.Contains(texto, StringComparison.OrdinalIgnoreCase));
        return q.OrderByDescending(n => n.Creada);
    }
}
