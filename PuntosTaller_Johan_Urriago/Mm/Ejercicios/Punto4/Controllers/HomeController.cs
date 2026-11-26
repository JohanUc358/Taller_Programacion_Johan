using Microsoft.AspNetCore.Mvc;
using _04_GestorGastos.Models;

namespace _04_GestorGastos.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        ViewBag.Mensual = RepositorioGastos.ResumenMensual().ToList();
        var mes = DateTime.Today.ToString("yyyy-MM");
        ViewBag.MesActual = mes;
        ViewBag.PorCategoria = RepositorioGastos.ResumenCategorias(mes).ToList();
        return View(RepositorioGastos.Todos().ToList());
    }

    [HttpPost]
    public IActionResult Agregar(Gasto gasto)
    {
        if (!string.IsNullOrWhiteSpace(gasto.Descripcion) && gasto.Monto > 0)
            RepositorioGastos.Agregar(gasto);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public IActionResult Eliminar(int id)
    {
        RepositorioGastos.Eliminar(id);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Error() => Content("Ocurrió un error.");
}
