using MastermindCore.Entity;
using MastermindCore.Service;
using MastermindCore.Service.SqlDatabase;
using Microsoft.AspNetCore.Mvc;

namespace MastermindWeb.APIControllers;

[Route("api/Comment")]
[ApiController]
[Produces("application/json")] 
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService = new SqlCommentService();
 
    // GET: api/Comment
    [HttpGet]
    public IEnumerable<Comment> Get() => _commentService.GetAll();

    // POST: api/Comment
    [HttpPost]
    public void Post([FromBody] Comment comment)
    {
        if (comment.CommentedAt == DateTime.MinValue) 
            comment.CommentedAt = DateTime.Now;
        _commentService.Add(comment);
    }

    // DELETE: api/Comment
    [HttpDelete]
    public void Delete() => _commentService.Reset();
}