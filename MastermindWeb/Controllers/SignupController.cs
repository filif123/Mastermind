using MastermindCore.Entity;
using MastermindCore.Service;
using MastermindCore.Service.SqlDatabase;
using MastermindWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace MastermindWeb.Controllers;

public class SignupController : Controller
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
    public IActionResult Index(PlayerRegisterModel model)
    {
        if (!ModelState.IsValid)
            return View();
            
        var exists = _playerService.Exists(model.Name);
        if (exists)
        {
            ViewBag.error = "Account with this name already exists.";
            return View();
        }

        var player = new Player(model.Name, model.Password, DateTime.Now);
        HttpContext.Session.SetObject(FieldController.PlayerKey, player);
        _playerService.Add(player);
        ViewBag.logged = true;
        return RedirectToAction("Index", "NewGameSettings");
    }
}