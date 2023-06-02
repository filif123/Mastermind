using MastermindCore.Entity;

namespace MastermindWeb.Models;

public class CommentsModel
{
    public IList<Comment> CommentsList { get; init; } = new List<Comment>();

    public int PageIndex { get; init; }

    public int CommentsPerPage { get; init; }

    public int MaxCommentsPages { get; init; }

    public bool ScrollToComments { get; init; }
}