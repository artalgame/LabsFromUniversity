using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using SimPressDomainModel.Interfaces;
using SimPressDomainModel.EFDbContext;
using SimPressDomainModel.Entities;

namespace SimPressDomainModel.RepositoryClasses
{
    /// <summary>
    /// This class wraps dbContext that returns data from database
    /// </summary>
    public class DBRepository : IRepository
    {
        public SimPressEFDBContext dbContext;

        public DBRepository(SimPressEFDBContext dbContext)
        {
            if (dbContext != null)
            {
                this.dbContext = dbContext;
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
                return dbContext.Users;
            }
        }

        public IQueryable<Entities.Presentation> Presentations
        {
            get
            {
                return dbContext.Presentations;
            }
        }

        public IQueryable<Entities.Comment> Comments
        {
            get
            {
                return dbContext.Comments;
            }
        }

        public IQueryable<Entities.Like> Likes
        {
            get
            {
                return dbContext.Likes;
            }
        }

        public void CreateUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user", "user is null");
            }
            dbContext.Users.Add(user);
        }

        private void CopyUsersProperties(User fromUser, User toUser)
        {
            if ((fromUser == null))
            {
                throw new ArgumentNullException("fromUser", "fromUser is null");
            }
            else
                if (toUser == null)
                {
                    throw new ArgumentNullException("toUser", "toUser is null");
                }
                else
                {
                    toUser.CreateDate = fromUser.CreateDate;
                    toUser.Email = fromUser.Email;
                    toUser.Info = fromUser.Info;
                    toUser.IsApproved = fromUser.IsApproved;
                    toUser.PasswordHash = fromUser.PasswordHash;
                    toUser.PasswordSalt = fromUser.PasswordSalt;
                    toUser.Login = fromUser.Login;
                    toUser.Role = fromUser.Role;
                }
        }

        public void UpdateUser(User user)
        {
            var existUser = dbContext.Users.FirstOrDefault<User>(x => x.UserId == user.UserId);
            if (user == null)
            {
                throw new ArgumentNullException("user", "user is null");
            }
            if (existUser == null)
            {
                throw new InvalidDataException("User doesn't exist in current database");
            }
            else
            {
                if (existUser != user)
                {
                    CopyUsersProperties(user, existUser);
                }
                dbContext.Entry(existUser).State = System.Data.EntityState.Modified;
            }
        }

        public void DeleteUser(User user)
        {
            if(user == null)
            {
                throw new ArgumentNullException("user");
            }
            var existUser = dbContext.Users.FirstOrDefault(x => x.UserId == user.UserId);
            if (existUser != null)
            {
                dbContext.Users.Remove(existUser);
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
            dbContext.Comments.Add(comment);
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
            else
            {
                toComment.CreateDate = fromComment.CreateDate;
                toComment.PresentationId = fromComment.PresentationId;
                toComment.Text = fromComment.Text;
                toComment.UserId = fromComment.UserId;
            }
        }

        public void UpdateComment(Comment comment)
        {
            if (comment == null)
            {
                throw new ArgumentNullException("comment","comment is null");
            }
            
            Comment existComment = dbContext.Comments.FirstOrDefault(x => x.CommentId == comment.CommentId);
            if(existComment == null)
            {
                throw new InvalidDataException("This comment doesn't exist in current data base");
            }
            else
            {
                if (comment != existComment)
                {
                    CopyCommentsProperties(comment, existComment);
                }
                dbContext.Entry(existComment).State = System.Data.EntityState.Modified;
            }

        }

        public void DeleteComment(Comment comment)
        {
            if(comment == null)
            {
                throw new ArgumentNullException("comment");
            }
            var existComment = dbContext.Comments.FirstOrDefault(x => x.CommentId == comment.CommentId);
            if(existComment != null)
            {
                dbContext.Comments.Remove(existComment);
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
            dbContext.Presentations.Add(presentation);
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
                dbContext.Presentations.FirstOrDefault(x => x.PresentationId == presentation.PresentationId);
            if(existPresentation == null)
            {
                throw new InvalidDataException("This presentation doesn't exist in the current data base");
            }
            if (presentation != existPresentation)
            {
                CopyPresentationProperties(presentation, existPresentation);
            }
            dbContext.Entry(existPresentation).State = EntityState.Modified;
        }

        public void DeletePresentation(Presentation presentation)
        {
            if(presentation == null)
            {
                throw new ArgumentNullException("presentation");
            }
            var existPresentation =
                dbContext.Presentations.FirstOrDefault(x => x.PresentationId == presentation.PresentationId);
            if(existPresentation == null)
            {
                throw new InvalidDataException("this presentation doesn't exist in current data base");
            }
            dbContext.Presentations.Remove(existPresentation);
        }

        public void CreateLike(Like like)
        {
            if(like == null)
            {
                throw new ArgumentNullException("like");
            }
            dbContext.Likes.Add(like);
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
            toLike.UserId = fromLike.UserId;
        }

        public void UpdateLike(Like like)
        {
           if(like == null)
           {
               throw new ArgumentNullException("like");
           }

            var existLike = dbContext.Likes.FirstOrDefault(x => x.LikeId == like.LikeId);
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
            var existLike = dbContext.Likes.FirstOrDefault(x => x.LikeId == like.LikeId);
            if(existLike == null)
            {
                throw new InvalidDataException("this like doesn't exist in the current data base");
            }
            dbContext.Likes.Remove(existLike);
        }

        public void SaveChanges()
        {
            dbContext.SaveChanges();
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
            dbContext.Dispose();
        }
    }
}
