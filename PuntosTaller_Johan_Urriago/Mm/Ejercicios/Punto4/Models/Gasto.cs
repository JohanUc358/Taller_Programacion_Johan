namespace _04_GestorGastos.Models;

public class Gasto
{
    public int Id { get; set; }
    public string Descripcion { get; set; } = "";
    public decimal Monto { get; set; }
    public string Categoria { get; set; } = "Otros";
    public DateTime Fecha { get; set; } = DateTime.Today;
}

// Modelo: almacena los gastos en memoria y calcula el resumen mensual.
public static class RepositorioGastos
{
    public static readonly string[] Categorias = { "Comida", "Transporte", "Hogar", "Ocio", "Salud", "Otros" };
    private static readonly List<Gasto> _gastos = new();
    private static int _id = 1;

    public static IEnumerable<Gasto> Todos() => _gastos.OrderByDescending(g => g.Fecha);

    public static void Agregar(Gasto g)
    {
        g.Id = _id++;
        _gastos.Add(g);
    }

    public static void Eliminar(int id) => _gastos.RemoveAll(g => g.Id == id);

    public static IEnumerable<(string Mes, decimal Total)> ResumenMensual() =>
        _gastos.GroupBy(g => g.Fecha.ToString("yyyy-MM"))
               .OrderByDescending(x => x.Key)
               .Select(x => (x.Key, x.Sum(g => g.Monto)));

    public static IEnumerable<(string Categoria, decimal Total)> ResumenCategorias(string mes) =>
        _gastos.Where(g => g.Fecha.ToString("yyyy-MM") == mes)
               .GroupBy(g => g.Categoria)
               .Select(x => (x.Key, x.Sum(g => g.Monto)));
}
