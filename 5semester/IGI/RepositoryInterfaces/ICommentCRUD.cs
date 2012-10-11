using System.Linq;
using Entities;

namespace RepositoryInterfaces
{
    public interface ICommentCRUD
    {
        IQueryable<Comment> Comments { get; }
        void CreateComment(Comment comment);
        void UpdateComment(Comment comment);
        void DeleteComment(Comment comment);

    }
}
