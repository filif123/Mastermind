using MastermindCore.Service;
using MastermindCore.Service.SqlDatabase;
using MastermindWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace MastermindWeb.Controllers;

public class LoginController : Controller
{
    private readonly IPlayerService _playerService = new SqlPlayerService();

    public IActionResult Index()
    {
        if (HttpContext.Session.ExistsKey(FieldController.PlayerKey))
        {
            ViewBag.logged = true;
            return RedirectToAction("Index", "NewGameSettings");
        }

        return View();
    }

    [HttpPost]
    public IActionResult Index(PlayerLoginModel model)
    {
        if (!ModelState.IsValid)
            return View();

        var player = _playerService.Get(model.Name, model.Password);
        if (player is null)
        {
            ViewBag.error = "Account was not found.";
            return View();
        }

        HttpContext.Session.SetObject(FieldController.PlayerKey, player);
        ViewBag.logged = true;
        return RedirectToAction("Index","NewGameSettings");
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Remove(FieldController.PlayerKey);
        HttpContext.Session.Remove(FieldController.GameKey);
        return RedirectToAction("Index", "Home");
    }
}