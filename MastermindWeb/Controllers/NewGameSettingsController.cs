using MastermindCore;
using MastermindWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace MastermindWeb.Controllers;

public class NewGameSettingsController : Controller
{
    public IActionResult Index()
    {
        if (!HttpContext.Session.ExistsKey(FieldController.PlayerKey))
        {
            ViewBag.logged = false;
            return RedirectToAction("Index", "Home");
        }
        ViewBag.logged = true;

        var game = HttpContext.Session.GetObject<Game>(FieldController.GameKey);
        if (game?.State is GameState.Playing or GameState.Lose or GameState.Solved)
            return RedirectToAction("Index", "Field");

        var input = new GameInputModel();
        return View(input);
    }
    
    [HttpPost]
    public IActionResult Index(GameInputModel input)  
    {
        if (!ModelState.IsValid)
            return View();

        var game = new Game(input.CodeLength, input.TurnsCount, input.AllowDuplicates);
        game.StartNew();
        HttpContext.Session.SetObject(FieldController.GameKey, game);
        return RedirectToAction("Index", "Field");  //new game created
    }
}