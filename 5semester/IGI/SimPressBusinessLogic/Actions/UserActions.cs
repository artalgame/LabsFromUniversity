using System;
using System.Linq;
using Entities;
using RepositoryInterfaces;
using SimPressBusinessLogic.Entities.UserEntities;
using System.Text.RegularExpressions;
using SimPressBusinessLogic.Enums;
using SimPressBusinessLogic.Exceptions.DataBaseExceptions;
using SimPressBusinessLogic.Exceptions.EmailExceptions;
using SimPressBusinessLogic.Exceptions.LoginExceptions;
using SimPressBusinessLogic.Exceptions.PasswordExceptions;
using SimPressBusinessLogic.Exceptions.UserExceptions;

namespace SimPressBusinessLogic.Actions
{
    public class UserActions
    {
        public bool IsUserExist(User user)
        {
            return GetUserById(user.UserId) == null;
        }

        public User GetUserById(Guid userId)
        {
            using(var repo = ApplicationSettings.GetRepository())
            {
                return repo.Users.FirstOrDefault(x => x.UserId == userId); 
            }
        }

        public User GetUserByEmail(string email)
        {
            using(var repository = ApplicationSettings.GetRepository())
            {
                return repository.Users.FirstOrDefault(x => x.Email == email);
            }
        }

        public User GetUserByLogin(string login)
        {
            using (var repository = ApplicationSettings.GetRepository())
            {
                return repository.Users.FirstOrDefault(x => x.Login == login);
            }
        }

        private bool CheckIsLoginFree(string login)
        {
            using (IRepository repository = ApplicationSettings.GetRepository())
            {
                var user = repository.Users.FirstOrDefault(x => x.Login == login);
                return user == null;
            }
        }

        private bool CheckIsLoginIsCorrected(string login)
        {
            return Regex.IsMatch(login, ApplicationSettings.LoginRegex);
        }

        public TypeOfAuthentificatedLogin LoginType { get; set; }

        public TypeOfAuthentificatedLogin RecognizeLoginType(string login)
        {
            if (Regex.IsMatch(login, ApplicationSettings.LoginRegex))
            {
                return TypeOfAuthentificatedLogin.Login;
            }
            if (Regex.IsMatch(login, ApplicationSettings.EmailRegex))
            {
                return TypeOfAuthentificatedLogin.Email;
            }
            return TypeOfAuthentificatedLogin.Indefinite;
        }

        public User TryToAuthentificateUser(string login, string password)
        {
            LoginType = RecognizeLoginType(login);
            switch (LoginType)
            {
                case TypeOfAuthentificatedLogin.Login:
                    return GetUserByLogin(login, password);
                case TypeOfAuthentificatedLogin.Email:
                    return GetUserByEmail(login, password);
                default:
                    
                    return null;
            }
        }

        private User GetUserByEmail(string email, string password)
        {
            return GetUser(new UserActions().GetUserByEmail(email), password);
        }

        private User GetUserByLogin(string login, string password)
        {
            return GetUser(new UserActions().GetUserByLogin(login), password);
        }

        private User GetUser(User findedUser, string password)
        {
            if (findedUser == null)
            {
                throw new UserNotExistException();
            }
            if (!PasswordActions.CheckPassword(password, findedUser))
            {
                throw new WrongPasswordForUserException();
            }
            return findedUser;
        }

        public bool IsLoginFree(string login)
        {
            if (CheckIsLoginFree(login))
            {
                return true;
            }
            throw new LoginOccupiedException();
        }

        public bool IsLoginCorrected(string login)
        {
            if (CheckIsLoginIsCorrected(login))
            {
                return true;
            }
            throw new LoginIncorrectedException();
        }

        public bool IsLoginRight(string login)
        {
            return CheckIsLoginFree(login) && CheckIsLoginIsCorrected(login);
        }

        public bool IsPasswordCorrected(string password)
        {
            if (Regex.IsMatch(password, ApplicationSettings.PasswordRegex))
            {
                return true;
            }
            throw new PasswordIncorrectedException();
        }

        public bool IsPasswordRight(string password)
        {
            return (IsPasswordCorrected(password));
        }

        public bool IsConfirmPasswordMatched(string password, string confirmPassword)
        {
            if (PasswordActions.PasswordsAreEqual(password, confirmPassword))
            {
                return true;
            }

            throw new ConfirmPasswordNotMatchException();
        }

        public bool IsEmailFree(string email)
        {
            using (IRepository repository = ApplicationSettings.GetRepository())
            {
                string existEmail = repository.Users.Select(x => x.Email).FirstOrDefault(x => x == email);
                if (existEmail == null)
                {
                    return true;
                }
                throw new EmailOccupiedException();
            }
        }

        public bool IsEmailCorrected(string email)
        {
            return Regex.IsMatch(email, ApplicationSettings.EmailRegex);
        }

        public bool IsEmailRight(string email)
        {
            return IsEmailCorrected(email) && (IsEmailFree(email));
        }

        private string CorrectInfo(string info)
        {
            if (info == null)
            {
                return "";
            }
            return info.Length > ApplicationSettings.UserInfoMaxLength ? new string(info.Take(ApplicationSettings.UserInfoMaxLength).ToArray()) : info;
        }

        public void TryRegistrateUser(PreRegistrateUser user)
        {
                if (!IsLoginRight(user.Login))
                {
                    return;
                }
                if (!IsPasswordRight(user.Password))
                {
                    return;
                }
                if (!IsConfirmPasswordMatched(user.Password, user.ConfirmPassword))
                {
                    
                    return;
                }
                if (!IsEmailRight(user.Email))
                {
                    
                    return;
                }
                try
                {
                    RegistrateUser(user);
                }
            catch(Exception ex)
            {
                throw new DataBaseBaseException(ex.Message,ex);
            }

        }

        private void RegistrateUser(PreRegistrateUser user)
        {
            string salt = PasswordActions.GenerateSalt();
            //create user
            var regUser = new User
            {
                CreateDate = DateTime.Now,
                Email = user.Email,
                Info = CorrectInfo(user.Info),
                IsApproved = true,
                Login = user.Login,
                PasswordHash = PasswordActions.GetHashForPassword(user.Password, salt),
                PasswordSalt = salt,
                Role = Roles.SimpleUser,
                UserId = Guid.NewGuid()
            };
            using (var repo = ApplicationSettings.GetRepository())
            {
                repo.CreateUser(regUser);
                repo.SaveChanges();
            }
        }
    }

 }
