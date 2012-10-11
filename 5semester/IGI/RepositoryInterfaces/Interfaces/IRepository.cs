using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimPressDomainModel.Entities;

namespace SimPressDomainModel.Interfaces
{
    /// <summary>
    /// This interface describes behaviour of classes, which provide interaction with some source of data(databases, xml files or other)
    /// </summary>
    public interface IRepository : IDisposable, IUserCRUD, IPresentationCRUD, ICommentCRUD,ILikeCRUD
    {
       void SaveChanges();
    }
}
