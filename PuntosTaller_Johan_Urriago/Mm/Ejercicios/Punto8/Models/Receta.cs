namespace _08_PlataformaRecetas.Models;

public class Receta
{
    public int Id { get; set; }
    public string Nombre { get; set; } = "";
    public string Tipo { get; set; } = "Otros";
    public string Ingredientes { get; set; } = ""; // separados por coma
    public string Preparacion { get; set; } = "";
    public string Autor { get; set; } = "Anónimo";
    public bool Guardada { get; set; }

    public IEnumerable<string> ListaIngredientes() =>
        Ingredientes.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
}

// Modelo: recetas en memoria con búsqueda, filtros y favoritos.
public static class LibroRecetas
{
    public static readonly string[] Tipos = { "Desayuno", "Almuerzo", "Cena", "Postre", "Bebida", "Otros" };
    private static readonly List<Receta> _recetas = new()
    {
        new() { Id = 1, Nombre = "Arepas", Tipo = "Desayuno", Ingredientes = "harina de maíz, agua, sal, queso", Preparacion = "Mezclar, formar discos y asar.", Autor = "Chef" },
        new() { Id = 2, Nombre = "Ensalada Caprese", Tipo = "Almuerzo", Ingredientes = "tomate, queso mozzarella, albahaca, aceite", Preparacion = "Cortar, alternar y aderezar.", Autor = "Chef" },
        new() { Id = 3, Nombre = "Brownies", Tipo = "Postre", Ingredientes = "chocolate, huevos, harina, mantequilla, azúcar", Preparacion = "Mezclar y hornear 25 min a 180°C.", Autor = "Chef" },
    };
    private static int _id = 4;

    public static Receta? Obtener(int id) => _recetas.FirstOrDefault(r => r.Id == id);

    public static void Agregar(Receta r) { r.Id = _id++; _recetas.Add(r); }

    public static void AlternarGuardada(int id)
    {
        var r = Obtener(id);
        if (r != null) r.Guardada = !r.Guardada;
    }

    public static IEnumerable<Receta> Buscar(string? texto, string? tipo, string? ingrediente, bool soloGuardadas)
    {
        var q = _recetas.AsEnumerable();
        if (!string.IsNullOrWhiteSpace(texto)) q = q.Where(r => r.Nombre.Contains(texto, StringComparison.OrdinalIgnoreCase));
        if (!string.IsNullOrWhiteSpace(tipo)) q = q.Where(r => r.Tipo == tipo);
        if (!string.IsNullOrWhiteSpace(ingrediente)) q = q.Where(r => r.Ingredientes.Contains(ingrediente, StringComparison.OrdinalIgnoreCase));
        if (soloGuardadas) q = q.Where(r => r.Guardada);
        return q;
    }
}
