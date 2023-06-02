using Microsoft.AspNetCore.Mvc;
using MastermindCore;

namespace MastermindWeb.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        if (HttpContext.Session.ExistsKey(FieldController.PlayerKey))
            return RedirectToAction("Index", "NewGameSettings");

        var game = HttpContext.Session.GetObject<Game>(FieldController.GameKey);
        if (game != null)
            return RedirectToAction("Index", "Field");

        return View();
    }
}