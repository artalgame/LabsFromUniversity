using System;

namespace RepositoryInterfaces
{
    /// <summary>
    /// This interface describes behaviour of classes, which provide interaction with some source of data(databases, xml files or other)
    /// </summary>
    public interface IRepository : IDisposable, IUserCRUD, IPresentationCRUD, ICommentCRUD,ILikeCRUD
    {
       void SaveChanges();
    }
}
