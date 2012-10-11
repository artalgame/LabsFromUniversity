using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimPressDomainModel.Entities;

namespace SimPressDomainModel.Interfaces
{
    public interface ILikeCRUD
    {
        IQueryable<Like> Likes { get; }
        void CreateLike(Like like);
        void UpdateLike(Like like);
        void DeleteLike(Like like);
    }
}
