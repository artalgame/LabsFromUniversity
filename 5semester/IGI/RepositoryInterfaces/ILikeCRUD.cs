using System.Linq;
using Entities;

namespace RepositoryInterfaces
{
    public interface ILikeCRUD
    {
        IQueryable<Like> Likes { get; }
        void CreateLike(Like like);
        void UpdateLike(Like like);
        void DeleteLike(Like like);
    }
}
