using MastermindCore.Entity;
using MastermindCore.Service;
using MastermindCore.Service.SqlDatabase;
using Microsoft.AspNetCore.Mvc;

namespace MastermindWeb.APIControllers;

[Route("api/Player")]
[ApiController]
public class PlayerController : ControllerBase
{
    private readonly IPlayerService _playerService = new SqlPlayerService();
 
    // GET: api/Comment
    [HttpGet]
    public IEnumerable<Player> Get() => _playerService.GetAll();

    // POST: api/Comment
    [HttpPost]
    public void Post([FromBody] Player player)
    {
        if (player.RegisteredAt == DateTime.MinValue) 
            player.RegisteredAt = DateTime.Now;
        _playerService.Add(player);
    }

    // DELETE: api/Comment
    [HttpDelete]
    public void Delete() => _playerService.Reset();
}