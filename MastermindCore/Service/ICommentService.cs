using MastermindCore.Entity;

namespace MastermindCore.Service;

public interface ICommentService
{
    /// <summary>
    /// Prida komentar do databazy.
    /// </summary>
    /// <param name="comment">komentar</param>
    void Add(Comment comment);

    /// <summary>
    /// Vrati vsetky komentare z databazy.
    /// </summary>
    /// <returns></returns>
    IList<Comment> GetAll();

    /// <summary>
    /// Odstrani vsetky komentare z databazy.
    /// </summary>
    void Reset();
}