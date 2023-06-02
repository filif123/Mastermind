using Microsoft.AspNetCore.Mvc;

namespace MastermindWeb.Controllers;

public class AboutController : Controller
{
    public IActionResult Index()
    {
        ViewBag.logged = HttpContext.Session.ExistsKey(FieldController.PlayerKey);
        return View();
    }
}