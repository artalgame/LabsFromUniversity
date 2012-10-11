using System.Linq;
using Entities;

namespace RepositoryInterfaces
{
    public interface IUserCRUD
    {
        IQueryable<User> Users { get; }
        void CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
    }
}
