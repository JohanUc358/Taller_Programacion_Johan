namespace _09_JuegoMemoria.Models;

public record Puntaje(string Jugador, string Nivel, int Intentos, int Segundos, int Puntos);

// Modelo: genera el tablero según la dificultad y guarda los mejores puntajes.
public static class Juego
{
    private static readonly string[] Imagenes =
        { "🐶", "🐱", "🦊", "🐼", "🐸", "🦁", "🐵", "🐙", "🦄", "🐢", "🐧", "🦋" };

    public static readonly Dictionary<string, int> Niveles = new()
    {
        ["Fácil"] = 6,    // 12 cartas
        ["Medio"] = 8,    // 16 cartas
        ["Difícil"] = 12  // 24 cartas
    };

    private static readonly List<Puntaje> _puntajes = new();

    public static List<string> NuevoTablero(string nivel)
    {
        var pares = Niveles.TryGetValue(nivel, out var n) ? n : 6;
        return Imagenes.Take(pares).SelectMany(i => new[] { i, i }).OrderBy(_ => Random.Shared.Next()).ToList();
    }

    // Menos intentos y menos tiempo => más puntos.
    public static int CalcularPuntos(string nivel, int intentos, int segundos)
    {
        var pares = Niveles.GetValueOrDefault(nivel, 6);
        return Math.Max(10, pares * 200 - intentos * 15 - segundos * 3);
    }

    public static Puntaje Registrar(string jugador, string nivel, int intentos, int segundos)
    {
        var p = new Puntaje(string.IsNullOrWhiteSpace(jugador) ? "Anónimo" : jugador.Trim(), nivel, intentos, segundos,
                            CalcularPuntos(nivel, intentos, segundos));
        _puntajes.Add(p);
        return p;
    }

    public static IEnumerable<Puntaje> Mejores() => _puntajes.OrderByDescending(p => p.Puntos).Take(10);
}
