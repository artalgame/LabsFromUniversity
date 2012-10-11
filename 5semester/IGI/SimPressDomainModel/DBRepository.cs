using System;
using System.Data;
using System.IO;
using System.Linq;
using Entities;
using RepositoryInterfaces;
using SimPressDomainModel.EFDbContext;

namespace SimPressDomainModel
{
    /// <summary>
    /// This class wraps dbContext that returns data from database
    /// </summary>
    public class DBRepository : IRepository
    {
        private readonly SimPressEFDBContext _dbContext;

        public DBRepository(SimPressEFDBContext dbContext)
        {
            if (dbContext != null)
            {
                _dbContext = dbContext;
            }
            else
            {
                throw new NullReferenceException("dbContext is null");
            }
        }

        public IQueryable<User> Users
        {
            get
            {
                return _dbContext.Users;
            }
        }

        public IQueryable<Presentation> Presentations
        {
            get
            {
                return _dbContext.Presentations;
            }
        }

        public IQueryable<Comment> Comments
        {
            get
            {
                return _dbContext.Comments;
            }
        }

        public IQueryable<Like> Likes
        {
            get
            {
                return _dbContext.Likes;
            }
        }

        public void CreateUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user", "user is null");
            }
            _dbContext.Users.Add(user);
        }

        private void CopyUsersProperties(User fromUser, User toUser)
        {
            if ((fromUser == null))
            {
                throw new ArgumentNullException("fromUser", "fromUser is null");
            }
            if (toUser == null)
            {
                throw new ArgumentNullException("toUser", "toUser is null");
            }
            toUser.CreateDate = fromUser.CreateDate;
            toUser.Email = fromUser.Email;
            toUser.Info = fromUser.Info;
            toUser.IsApproved = fromUser.IsApproved;
            toUser.PasswordHash = fromUser.PasswordHash;
            toUser.PasswordSalt = fromUser.PasswordSalt;
            toUser.Login = fromUser.Login;
            toUser.Role = fromUser.Role;
        }

        public void UpdateUser(User user)
        {
            var existUser = _dbContext.Users.FirstOrDefault(x => x.UserId == user.UserId);
            if (user == null)
            {
                throw new ArgumentNullException("user", "user is null");
            }
            if (existUser == null)
            {
                throw new InvalidDataException("User doesn't exist in current database");
            }
            if (existUser != user)
            {
                CopyUsersProperties(user, existUser);
            }
            _dbContext.Entry(existUser).State = EntityState.Modified;
        }

        public void DeleteUser(User user)
        {
            if(user == null)
            {
                throw new ArgumentNullException("user");
            }
            var existUser = _dbContext.Users.FirstOrDefault(x => x.UserId == user.UserId);
            if (existUser != null)
            {
                _dbContext.Users.Remove(existUser);
            }
            else
            {
                throw new ArgumentNullException("user", "user is Null");
            }
        }

        public void CreateComment(Comment comment)
        {
            if(comment == null)
            {
                throw  new ArgumentNullException("comment","comment is null");
            }
            _dbContext.Comments.Add(comment);
        }

        private void CopyCommentsProperties(Comment fromComment, Comment toComment)
        {
            if(fromComment == null)
            {
                throw new ArgumentNullException("fromComment");
            }
            if(toComment == null)
            {
                throw new ArgumentNullException("toComment");
            }
            toComment.CreateDate = fromComment.CreateDate;
            toComment.PresentationId = fromComment.PresentationId;
            toComment.Text = fromComment.Text;
            toComment.User.UserId = fromComment.User.UserId;
        }

        public void UpdateComment(Comment comment)
        {
            if (comment == null)
            {
                throw new ArgumentNullException("comment","comment is null");
            }
            
            Comment existComment = _dbContext.Comments.FirstOrDefault(x => x.CommentId == comment.CommentId);
            if(existComment == null)
            {
                throw new InvalidDataException("This comment doesn't exist in current data base");
            }
            if (comment != existComment)
            {
                CopyCommentsProperties(comment, existComment);
            }
            _dbContext.Entry(existComment).State = EntityState.Modified;
        }

        public void DeleteComment(Comment comment)
        {
            if(comment == null)
            {
                throw new ArgumentNullException("comment");
            }
            var existComment = _dbContext.Comments.FirstOrDefault(x => x.CommentId == comment.CommentId);
            if(existComment != null)
            {
                _dbContext.Comments.Remove(existComment);
            }
            else
            {
                throw  new InvalidDataException("This comment doesn't exist in current data base");
            }
      }

        public void CreatePresentation(Presentation presentation)
        {
           if(presentation == null)
           {
               throw new ArgumentNullException("presentation");
           }
            _dbContext.Presentations.Add(presentation);
        }

        private void CopyPresentationProperties(Presentation fromPresentation, Presentation toPresentation)
        {
            if(fromPresentation == null)
            {
                throw new ArgumentNullException("fromPresentation");
            }
            if(toPresentation == null)
            {
                throw new ArgumentNullException("toPresentation");
            }
            toPresentation.CreateDate = fromPresentation.CreateDate;
            toPresentation.Description = fromPresentation.Description;
            toPresentation.Name = fromPresentation.Name;
            toPresentation.SlidesJSON = fromPresentation.SlidesJSON;
            toPresentation.Tags = fromPresentation.Tags;
        }

        public void UpdatePresentation(Presentation presentation)
        {
            if(presentation == null)
            {
                throw new ArgumentNullException("presentation");
            }
            var existPresentation =
                _dbContext.Presentations.FirstOrDefault(x => x.PresentationId == presentation.PresentationId);
            if(existPresentation == null)
            {
                throw new InvalidDataException("This presentation doesn't exist in the current data base");
            }
            if (presentation != existPresentation)
            {
                CopyPresentationProperties(presentation, existPresentation);
            }
            _dbContext.Entry(existPresentation).State = EntityState.Modified;
        }

        public void DeletePresentation(Presentation presentation)
        {
            if(presentation == null)
            {
                throw new ArgumentNullException("presentation");
            }
            var existPresentation =
                _dbContext.Presentations.FirstOrDefault(x => x.PresentationId == presentation.PresentationId);
            if(existPresentation == null)
            {
                throw new InvalidDataException("this presentation doesn't exist in current data base");
            }
            _dbContext.Presentations.Remove(existPresentation);
        }

        public void CreateLike(Like like)
        {
            if(like == null)
            {
                throw new ArgumentNullException("like");
            }
            _dbContext.Likes.Add(like);
        }

        private void CopyLikeProperties(Like fromLike, Like toLike)
        {
            if(fromLike == null)
            {
                throw new ArgumentNullException("fromLike");
            }
            if (toLike == null)
            {
                throw new ArgumentNullException("toLike");
            }
            toLike.CreateDate = fromLike.CreateDate;
            toLike.PresentationId = fromLike.PresentationId;
            toLike.User.UserId = fromLike.User.UserId;
        }

        public void UpdateLike(Like like)
        {
           if(like == null)
           {
               throw new ArgumentNullException("like");
           }

            var existLike = _dbContext.Likes.FirstOrDefault(x => x.LikeId == like.LikeId);
            if(existLike == null)
            {
                throw new InvalidDataException("this like doesn't exist in current data base");
            }
            CopyLikeProperties(like,existLike);
        }

        public void DeleteLike(Like like)
        {
            if(like == null)
            {
                throw new ArgumentNullException("like");
            }
            var existLike = _dbContext.Likes.FirstOrDefault(x => x.LikeId == like.LikeId);
            if(existLike == null)
            {
                throw new InvalidDataException("this like doesn't exist in the current data base");
            }
            _dbContext.Likes.Remove(existLike);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~DBRepository()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool isDisposed)
        {
            _dbContext.Dispose();
        }
    }
}
