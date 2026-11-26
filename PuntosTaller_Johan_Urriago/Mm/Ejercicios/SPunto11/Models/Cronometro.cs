namespace _11_CronometroOnline.Models;

// Modelo: datos de una vuelta y utilidades de formato (el conteo en vivo lo hace JavaScript).
public record Vuelta(int Numero, long Parcial, long Total)
{
    public static string Formato(long ms)
    {
        var t = TimeSpan.FromMilliseconds(ms);
        return $"{(int)t.TotalMinutes:00}:{t.Seconds:00}.{t.Milliseconds / 10:00}";
    }
}

public class CronometroModel
{
    public string Titulo { get; set; } = "Cronómetro Online";
    public string TiempoInicial { get; } = Vuelta.Formato(0);
}
