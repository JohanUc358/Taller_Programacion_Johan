using Microsoft.AspNetCore.Mvc;
using _01_ListaTareas.Models;

namespace _01_ListaTareas.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() => View(ListaTareas.Todas());

    [HttpPost]
    public IActionResult Agregar(string titulo)
    {
        ListaTareas.Agregar(titulo);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public IActionResult Completar(int id)
    {
        ListaTareas.Alternar(id);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public IActionResult Eliminar(int id)
    {
        ListaTareas.Eliminar(id);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Error() => Content("Ocurrió un error.");
}
