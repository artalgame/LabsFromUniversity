using System.Linq;
using SimPressDomainModel.Entities;

namespace SimPressDomainModel.Interfaces
{
    public interface IUserCRUD
    {
        IQueryable<User> Users { get; }
        void CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
    }
}
