using Microsoft.AspNetCore.Mvc;
using _05_SistemaReservas.Models;

namespace _05_SistemaReservas.Controllers;

public class HomeController : Controller
{
    // Muestra el formulario con la disponibilidad del servicio/fecha elegidos.
    public IActionResult Index(string? servicio, DateTime? fecha)
    {
        servicio ??= SistemaReservas.Servicios[0];
        var f = (fecha ?? DateTime.Today).Date;
        ViewBag.Servicio = servicio;
        ViewBag.Fecha = f;
        ViewBag.Disponibles = SistemaReservas.HorasDisponibles(servicio, f).ToList();
        ViewBag.Mensaje = TempData["Mensaje"];
        return View(SistemaReservas.Todas());
    }

    [HttpPost]
    public IActionResult Reservar(Reserva reserva)
    {
        TempData["Mensaje"] = SistemaReservas.Confirmar(reserva)
            ? "¡Reserva confirmada!"
            : "No se pudo reservar: el horario está ocupado o los datos son inválidos.";
        return RedirectToAction(nameof(Index), new { servicio = reserva.Servicio, fecha = reserva.Fecha.ToString("yyyy-MM-dd") });
    }

    [HttpPost]
    public IActionResult Cancelar(int id)
    {
        SistemaReservas.Cancelar(id);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Error() => Content("Ocurrió un error.");
}
