using MastermindCore;
using MastermindCore.Entity;
using MastermindCore.Service;
using MastermindCore.Service.SqlDatabase;
using MastermindWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace MastermindWeb.Controllers;

public class FieldController : Controller
{
    private readonly IScoreService _scoreService = new SqlScoreService();
    private readonly ICommentService _commentService = new SqlCommentService();
    private readonly IRatingService _ratingService = new SqlRatingService();

    public const string GameKey = "game";
    public const string PlayerKey = "player";

    public IActionResult Index([FromQuery] int perPage = -1, [FromQuery] int pindex = 0, [FromQuery] bool scroll = false)
    {
        if (!HttpContext.Session.ExistsKey(PlayerKey))
        {
            ViewBag.logged = false;
            HttpContext.Session.Remove(GameKey);
            return RedirectToAction("Index", "Home");
        }
        ViewBag.logged = true;

        if (perPage <= 0) 
            perPage = 10;
        else
            scroll = true;

        if (pindex <= 0) 
            pindex = 1;

        var game = HttpContext.Session.GetObject<Game>(GameKey);
        if (game?.State is GameState.Playing or GameState.Lose or GameState.Solved)
            return View(PrepareModel(perPage, pindex, scroll)); //game continues

        return RedirectToAction("Index","NewGameSettings");
    } 

    public IActionResult Reset() 
    {
        if (!HttpContext.Session.ExistsKey(PlayerKey))
        {
            ViewBag.logged = false;
            HttpContext.Session.Remove(GameKey);
            return RedirectToAction("Index", "Home");
        }
        ViewBag.logged = true;

        var game = HttpContext.Session.GetObject<Game>(GameKey);
        if (game is not null)
        {
            game.Reset();
            HttpContext.Session.SetObject(GameKey, game);
        }

        return RedirectToAction("Index");  
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Move([FromForm] string inputText)
    {
        var player = HttpContext.Session.GetObject<Player>(PlayerKey);
        if (player is null)
        {
            ViewBag.logged = false;
            HttpContext.Session.Remove(GameKey);
            return RedirectToAction("Index", "Home");
        }
        ViewBag.logged = true;

        var game = HttpContext.Session.GetObject<Game>(GameKey)!;

        if (string.IsNullOrEmpty(inputText) || inputText.Length != game.CodeLength)
            return View("Index", PrepareModel()); //invalid move

        
        var codes = new CodePeg[game.CodeLength];
        for (var i = 0; i < codes.Length; i++)
        {
            var peg = CodePeg.Parse(inputText[i]);
            if (peg == null)
                return View("Index", PrepareModel()); //invalid move

            codes[i] = peg;
        }
        
        game.DoMove(codes);

        HttpContext.Session.SetObject(GameKey, game);

        if (game.State is GameState.Solved or GameState.Lose)
            _scoreService.Add(new Score
            {
                PlayerName = player.Name, 
                Points = game.GetScore(), 
                PlayedAt = DateTime.Now,
                Duration = game.EndTime - game.StartTime
            });

        return RedirectToAction(nameof(Index)); //moved
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult SendComment([FromForm] string commentText)
    {
        var player = HttpContext.Session.GetObject<Player>(PlayerKey);
        if (player is null)
        {
            ViewBag.logged = false;
            HttpContext.Session.Remove(GameKey);
            return RedirectToAction("Index", "Home");
        }
        ViewBag.logged = true;

        _commentService.Add(new Comment
        {
            PlayerName = player.Name, 
            CommentedAt = DateTime.Now, 
            Text = commentText
        });

        return RedirectToAction(nameof(Index), new {scroll = true}); //comment written
    }

    [Route("Rating/{id:int}")]
    public IActionResult Rating(int id)
    {
        var player = HttpContext.Session.GetObject<Player>(PlayerKey);
        if (player is null)
        {
            ViewBag.logged = false;
            HttpContext.Session.Remove(GameKey);
            return RedirectToAction("Index", "Home");
        }
        ViewBag.logged = true;

        _ratingService.Add(new Rating
        {
            PlayerName = player.Name, 
            RatedAt = DateTime.Now, 
            Stars = id
        });
        return RedirectToAction(nameof(Index)); //rating set
    }

    private IList<Comment> GetRequiredComments(int perPage, ref int index, out int maxPages)
    {
        var comments = _commentService.GetAll();
        maxPages = (int) Math.Ceiling(comments.Count/(double)perPage);
        if (index > maxPages) index = 1;
        return comments.Reverse().Skip(perPage * (index - 1)).Take(perPage).ToList();
    }

    private GameModel PrepareModel(int perPage = 10, int index = 1, bool scroll = false)
    {
        var comments = GetRequiredComments(perPage, ref index, out var maxPages);

        var game = HttpContext.Session.GetObject<Game>(GameKey)!;
        var player = HttpContext.Session.GetObject<Player>(PlayerKey)!;
        var codes = new CodePeg[game.CodeLength];
        for (var i = 0; i < codes.Length; i++) 
            codes[i] = CodePeg.Red;

        var commentsModel = new CommentsModel
        {
            CommentsList = comments,
            CommentsPerPage = perPage,
            PageIndex = index,
            MaxCommentsPages = maxPages,
            ScrollToComments = scroll
        };

        return new GameModel
        {
            Game = game,
            Scores = _scoreService.GetTopThree(),
            Ratings = _ratingService.GetAll(),
            AverageRating = (float) _ratingService.GetAvg(),
            Comments = commentsModel,
            Player = player,
            Codes = codes,
            OwnRating = _ratingService.GetByPlayer(player)?.Stars ?? 0
        };
    }

    public IActionResult YourAttempts()
    {
        var player = HttpContext.Session.GetObject<Player>(PlayerKey);
        if (player is null)
        {
            ViewBag.logged = false;
            HttpContext.Session.Remove(GameKey);
            return RedirectToAction("Index", "Home");
        }
        ViewBag.logged = true;

        var attempts = _scoreService.GetAll().Where(s=>s.PlayerName == player.Name).OrderByDescending(s => s.PlayedAt).ToList();
        return View(new YourAttemptsModel{ Scores = attempts});
    }
}