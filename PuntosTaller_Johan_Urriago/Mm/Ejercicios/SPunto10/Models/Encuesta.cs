namespace _10_PlataformaEncuestas.Models;

public class Encuesta
{
    public int Id { get; set; }
    public string Pregunta { get; set; } = "";
    public List<string> Opciones { get; set; } = new();
    public List<int> Votos { get; set; } = new();

    public int Total => Votos.Sum();
    public double Porcentaje(int i) => Total == 0 ? 0 : Math.Round(Votos[i] * 100.0 / Total, 1);
}

// Modelo: encuestas y votos en memoria.
public static class RepositorioEncuestas
{
    private static readonly List<Encuesta> _encuestas = new();
    private static int _id = 1;

    public static IReadOnlyList<Encuesta> Todas() => _encuestas;
    public static Encuesta? Obtener(int id) => _encuestas.FirstOrDefault(e => e.Id == id);

    public static bool Crear(string pregunta, string opcionesTexto)
    {
        var opciones = (opcionesTexto ?? "")
            .Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Distinct().ToList();
        if (string.IsNullOrWhiteSpace(pregunta) || opciones.Count < 2) return false;
        _encuestas.Add(new Encuesta
        {
            Id = _id++, Pregunta = pregunta.Trim(), Opciones = opciones,
            Votos = opciones.Select(_ => 0).ToList()
        });
        return true;
    }

    public static void Votar(int id, int opcion)
    {
        var e = Obtener(id);
        if (e != null && opcion >= 0 && opcion < e.Votos.Count) e.Votos[opcion]++;
    }

    public static void Eliminar(int id) => _encuestas.RemoveAll(e => e.Id == id);
}
