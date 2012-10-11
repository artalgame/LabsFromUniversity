using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimPressDomainModel.Entities;

namespace SimPressDomainModel.Interfaces
{
    public interface ICommentCRUD
    {
        IQueryable<Comment> Comments { get; }
        void CreateComment(Comment comment);
        void UpdateComment(Comment comment);
        void DeleteComment(Comment comment);

    }
}
