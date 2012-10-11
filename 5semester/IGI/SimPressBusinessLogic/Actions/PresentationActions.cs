using System;
using System.Collections.Generic;
using System.Linq;
using Entities;
using RepositoryInterfaces;
using SimPressBusinessLogic.Entities.PresentationEntities;
using SimPressBusinessLogic.Exceptions.DataBaseExceptions;
using SimPressBusinessLogic.Exceptions.PresentationExceptions;
using SimPressBusinessLogic.Exceptions.UserExceptions;

namespace SimPressBusinessLogic.Actions
{
    public class PresentationActions
    {
        public bool IsPresentationExist(Guid presentationId)
        {
            return FindPresentationById(presentationId) != null;
        }

        public Presentation FindPresentationById(Guid presentationId)
        {
            using (var repo = ApplicationSettings.GetRepository())
            {
                return repo.Presentations.FirstOrDefault(x => x.PresentationId == presentationId);
            }
        }
        public List<Presentation> GetSomeLatestPresentation(int count)
        {
            using (var repo = ApplicationSettings.GetRepository())
            {
                return repo.Presentations.OrderBy(x => x.CreateDate).Take(count).ToList();
            }
        }
        public Presentation FormNewPresentation(PreCreatedPresentation presentationData, Guid userId)
        {
            if (presentationData == null)
            {
                return null;
            }
            return new Presentation
            {
                CreateDate = DateTime.Now,
                Description = presentationData.Description,
                Name = presentationData.Name,
                PresentationId = Guid.NewGuid(),
                Tags = presentationData.Tags,
                User = new UserActions().GetUserById(userId)
            };
        }
  
        public void SaveNewPresentation(PreCreatedPresentation presentation, User user)
        {
            if (user == null)
            {
                throw new UserHaveNotRulesException();
            }
            if (new UserActions().IsUserExist(user))
            {
                throw new UserNotExistException();
            }
            if (presentation == null)
            {
                throw new PresentationNullDataException();
            }
            if (string.IsNullOrEmpty(presentation.Name))
            {
                throw new PresentationWrongNameException();
            }
            try
            {
                var createdPresentation = FormNewPresentation(presentation, user.UserId);
                TrySavePresentation(createdPresentation);
            }
            catch (Exception ex)
            {
                throw new DataBaseBaseException(ex.Message,ex);
            }
        }

        private void TrySavePresentation(Presentation createdPresentation)
        {
            using (IRepository repo = ApplicationSettings.GetRepository())
            {
                repo.CreatePresentation(createdPresentation);
                repo.SaveChanges();
            }
        }
    }
}
