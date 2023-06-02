using MastermindCore.Entity;
using MastermindCore.Service;
using MastermindCore.Service.SqlDatabase;
using Microsoft.AspNetCore.Mvc;

namespace MastermindWeb.APIControllers;

[Route("api/Rating")]
[ApiController]
[Produces("application/json")]
public class RatingController : ControllerBase
{
    private readonly IRatingService _ratingService = new SqlRatingService();
 
    // GET: api/Rating
    [HttpGet]
    public IEnumerable<Rating> Get() => _ratingService.GetAll();

    // POST: api/Rating
    [HttpPost]
    public void Post([FromBody] Rating rating)
    {
        if (rating.RatedAt == DateTime.MinValue) 
            rating.RatedAt = DateTime.Now;
        _ratingService.Add(rating);
    }

    // DELETE: api/Rating
    [HttpDelete]
    public void Delete() => _ratingService.Reset();
}