using Microsoft.AspNetCore.Mvc;
using _08_PlataformaRecetas.Models;

namespace _08_PlataformaRecetas.Controllers;

public class HomeController : Controller
{
    public IActionResult Index(string? q, string? tipo, string? ingrediente, bool guardadas = false)
    {
        ViewBag.Q = q; ViewBag.Tipo = tipo; ViewBag.Ingrediente = ingrediente; ViewBag.Guardadas = guardadas;
        return View(LibroRecetas.Buscar(q, tipo, ingrediente, guardadas).ToList());
    }

    public IActionResult Detalle(int id)
    {
        var r = LibroRecetas.Obtener(id);
        return r == null ? RedirectToAction(nameof(Index)) : View(r);
    }

    [HttpPost]
    public IActionResult Crear(Receta receta)
    {
        if (!string.IsNullOrWhiteSpace(receta.Nombre)) LibroRecetas.Agregar(receta);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public IActionResult Guardar(int id)
    {
        LibroRecetas.AlternarGuardada(id);
        return RedirectToAction(nameof(Detalle), new { id });
    }

    public IActionResult Error() => Content("Ocurrió un error.");
}
