namespace _07_CalendarioEventos.Models;

public class Evento
{
    public int Id { get; set; }
    public string Titulo { get; set; } = "";
    public DateTime Inicio { get; set; } = DateTime.Now;
    public int RecordatorioMin { get; set; } = 15; // minutos antes; 0 = sin recordatorio
}

// Modelo: eventos del calendario en memoria.
public static class Calendario
{
    private static readonly List<Evento> _eventos = new();
    private static int _id = 1;

    public static IEnumerable<Evento> Todos() => _eventos.OrderBy(e => e.Inicio);

    public static Evento? Buscar(int id) => _eventos.FirstOrDefault(e => e.Id == id);

    public static IEnumerable<Evento> DelMes(int anio, int mes) =>
        Todos().Where(e => e.Inicio.Year == anio && e.Inicio.Month == mes);

    public static void Agregar(Evento e) { e.Id = _id++; _eventos.Add(e); }

    public static void Editar(Evento e)
    {
        var o = Buscar(e.Id);
        if (o == null) return;
        o.Titulo = e.Titulo; o.Inicio = e.Inicio; o.RecordatorioMin = e.RecordatorioMin;
    }

    public static void Eliminar(int id) => _eventos.RemoveAll(e => e.Id == id);
}
