using Microsoft.AspNetCore.Mvc;
using _07_CalendarioEventos.Models;

namespace _07_CalendarioEventos.Controllers;

public class HomeController : Controller
{
    public IActionResult Index(int? anio, int? mes)
    {
        var hoy = DateTime.Today;
        var a = anio ?? hoy.Year;
        var m = mes ?? hoy.Month;
        var primero = new DateTime(a, m, 1);
        ViewBag.Mes = primero;
        ViewBag.Anterior = primero.AddMonths(-1);
        ViewBag.Siguiente = primero.AddMonths(1);
        ViewBag.Proximos = Calendario.Todos().Where(e => e.Inicio >= DateTime.Now).ToList(); // para recordatorios en el navegador
        return View(Calendario.DelMes(a, m).ToList());
    }

    public IActionResult Editar(int id)
    {
        var e = Calendario.Buscar(id);
        return e == null ? RedirectToAction(nameof(Index)) : View(e);
    }

    [HttpPost]
    public IActionResult Guardar(Evento evento)
    {
        if (!string.IsNullOrWhiteSpace(evento.Titulo))
        {
            if (evento.Id == 0) Calendario.Agregar(evento); else Calendario.Editar(evento);
        }
        return RedirectToAction(nameof(Index), new { anio = evento.Inicio.Year, mes = evento.Inicio.Month });
    }

    [HttpPost]
    public IActionResult Eliminar(int id)
    {
        Calendario.Eliminar(id);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Error() => Content("Ocurrió un error.");
}
