using Microsoft.AspNetCore.Mvc;
using _11_CronometroOnline.Models;

namespace _11_CronometroOnline.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() => View(new CronometroModel());

    public IActionResult Error() => Content("Ocurrió un error.");
}
