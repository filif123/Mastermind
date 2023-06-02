using MastermindCore.Entity;
using MastermindCore.Service;
using MastermindCore.Service.SqlDatabase;
using Microsoft.AspNetCore.Mvc;

namespace MastermindWeb.APIControllers;

[Route("api/Score")]
[ApiController]
[Produces("application/json")]
public class ScoreController : ControllerBase
{
    private readonly IScoreService _scoreService = new SqlScoreService();
 
    // GET: api/Score
    [HttpGet]
    public IEnumerable<Score> Get() => _scoreService.GetTopThree();

    // POST: api/Score
    [HttpPost]
    public void Post([FromBody] Score score)
    {
        if (score.PlayedAt == DateTime.MinValue) 
            score.PlayedAt = DateTime.Now;
        _scoreService.Add(score);
    }

    // DELETE: api/Score
    [HttpDelete]
    public void Delete() => _scoreService.Reset();
}