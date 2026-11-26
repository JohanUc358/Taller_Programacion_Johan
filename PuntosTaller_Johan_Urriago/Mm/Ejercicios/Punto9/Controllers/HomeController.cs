using Microsoft.AspNetCore.Mvc;
using _09_JuegoMemoria.Models;

namespace _09_JuegoMemoria.Controllers;

public class HomeController : Controller
{
    public IActionResult Index(string nivel = "Fácil")
    {
        if (!Juego.Niveles.ContainsKey(nivel)) nivel = "Fácil";
        ViewBag.Nivel = nivel;
        ViewBag.Mejores = Juego.Mejores().ToList();
        return View(Juego.NuevoTablero(nivel));
    }

    // El navegador envía el resultado al terminar la partida.
    [HttpPost]
    public IActionResult Puntaje(string jugador, string nivel, int intentos, int segundos)
    {
        var p = Juego.Registrar(jugador, nivel, intentos, segundos);
        return Json(p);
    }

    public IActionResult Error() => Content("Ocurrió un error.");
}
