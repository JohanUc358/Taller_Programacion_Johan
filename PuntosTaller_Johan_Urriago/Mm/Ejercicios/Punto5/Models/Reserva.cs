namespace _05_SistemaReservas.Models;

public class Reserva
{
    public int Id { get; set; }
    public string Cliente { get; set; } = "";
    public string Servicio { get; set; } = "";
    public DateTime Fecha { get; set; }
    public string Hora { get; set; } = "";
}

// Modelo: servicios, horarios y reservas confirmadas (en memoria).
public static class SistemaReservas
{
    public static readonly string[] Servicios = { "Corte de cabello", "Manicure", "Masaje", "Consulta" };
    public static readonly string[] Horas = { "09:00", "10:00", "11:00", "12:00", "14:00", "15:00", "16:00", "17:00" };
    private static readonly List<Reserva> _reservas = new();
    private static int _id = 1;

    public static IReadOnlyList<Reserva> Todas() => _reservas.OrderBy(r => r.Fecha).ThenBy(r => r.Hora).ToList();

    public static bool EstaOcupado(string servicio, DateTime fecha, string hora) =>
        _reservas.Any(r => r.Servicio == servicio && r.Fecha.Date == fecha.Date && r.Hora == hora);

    public static IEnumerable<string> HorasDisponibles(string servicio, DateTime fecha) =>
        Horas.Where(h => !EstaOcupado(servicio, fecha, h));

    public static bool Confirmar(Reserva r)
    {
        if (string.IsNullOrWhiteSpace(r.Cliente) || r.Fecha.Date < DateTime.Today) return false;
        if (EstaOcupado(r.Servicio, r.Fecha, r.Hora)) return false;
        r.Id = _id++;
        _reservas.Add(r);
        return true;
    }

    public static void Cancelar(int id) => _reservas.RemoveAll(r => r.Id == id);
}
