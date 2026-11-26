using Microsoft.AspNetCore.Mvc;
using _10_PlataformaEncuestas.Models;

namespace _10_PlataformaEncuestas.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        ViewBag.Error = TempData["Error"];
        return View(RepositorioEncuestas.Todas());
    }

    [HttpPost]
    public IActionResult Crear(string pregunta, string opciones)
    {
        if (!RepositorioEncuestas.Crear(pregunta, opciones))
            TempData["Error"] = "Escribe una pregunta y al menos 2 opciones (una por línea).";
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Responder(int id)
    {
        var e = RepositorioEncuestas.Obtener(id);
        return e == null ? RedirectToAction(nameof(Index)) : View(e);
    }

    [HttpPost]
    public IActionResult Responder(int id, int opcion)
    {
        RepositorioEncuestas.Votar(id, opcion);
        return RedirectToAction(nameof(Resultados), new { id });
    }

    public IActionResult Resultados(int id)
    {
        var e = RepositorioEncuestas.Obtener(id);
        return e == null ? RedirectToAction(nameof(Index)) : View(e);
    }

    [HttpPost]
    public IActionResult Eliminar(int id)
    {
        RepositorioEncuestas.Eliminar(id);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Error() => Content("Ocurrió un error.");
}
