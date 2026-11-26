using Microsoft.AspNetCore.Mvc;
using _06_GestorNotas.Models;

namespace _06_GestorNotas.Controllers;

public class HomeController : Controller
{
    public IActionResult Index(string? q, string? categoria)
    {
        ViewBag.Q = q;
        ViewBag.Categoria = categoria;
        ViewBag.Categorias = RepositorioNotas.Categorias().ToList();
        return View(RepositorioNotas.Buscar(q, categoria).ToList());
    }

    [HttpPost]
    public IActionResult Guardar(Nota nota)
    {
        if (!string.IsNullOrWhiteSpace(nota.Titulo)) RepositorioNotas.Guardar(nota);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public IActionResult Eliminar(int id)
    {
        RepositorioNotas.Eliminar(id);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Error() => Content("Ocurrió un error.");
}
