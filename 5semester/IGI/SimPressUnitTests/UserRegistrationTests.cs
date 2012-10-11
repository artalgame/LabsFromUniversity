using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimPressBusinessLogic.Actions;
using SimPressBusinessLogic.Entities.UserEntities;
using SimPressBusinessLogic.Exceptions.LoginExceptions;
using SimPressBusinessLogic.Exceptions.PasswordExceptions;

namespace SimPressUnitTests
{
    [TestClass]
    public class UserRegistrationTests
    {
        [TestMethod]
        public void CanRegistrateUserWithErrorLogin()
        {
            var registration = new UserActions();

            //login is too short
            try
            {
                var user = new PreRegistrateUser
                    {
                        Login = "bb",
                        Password = "lsadfj2",
                        ConfirmPassword = "lsadfj2",
                        Email = "alkor@yandex.ru",
                        Info = "dsfsdfsdf"
                    };
                registration.TryRegistrateUser(user);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.GetType(), typeof(LoginIncorrectedException));
            }
            finally
            {
                //login to long
                try
                {
                    var user = new PreRegistrateUser
                        {
                            Login = "skfjklsjdflkjskldfjlkasjfkljasdklfjsadklfjklsdjfklsdjfklasdjfkljasdkl",
                            Password = "lsadfj2",
                            ConfirmPassword = "lsadfj2",
                            Email = "alkor@yandex.ru",
                            Info = "dsfsdfsdf"
                        };
                    registration.TryRegistrateUser(user);
                }
                catch (Exception ex)
                {
                    Assert.AreEqual(ex.GetType(), typeof(LoginIncorrectedException));
                }
                finally
                {
                    //login includes wrong symbols
                    try
                    {
                        var user = new PreRegistrateUser
                            {
                                Login = "sdfsdf dsfsdffs",
                                Password = "lsadfj2",
                                ConfirmPassword = "lsadfj2",
                                Email = "alkor@yandex.ru",
                                Info = "dsfsdfsdf"
                            };
                        registration.TryRegistrateUser(user);
                    }

                    catch (Exception ex)
                    {
                        Assert.AreEqual(ex.GetType(), typeof(LoginIncorrectedException));
                    }
                }
            }
        }

        public void CanRegistrateUserWithWrongPassword()
        {
            var registration = new UserActions();
            //password is too short
            try
            {
                var user = new PreRegistrateUser
                    {
                        Login = "fafdsaf",
                        Password = "lsa",
                        ConfirmPassword = "lsadfj2",
                        Email = "alkor@yandex.ru",
                        Info = "dsfsdfsdf"
                    };
                registration.TryRegistrateUser(user);
            }
            catch (Exception ex)
            {
                Assert.AreEqual(ex.GetType(), typeof(PasswordIncorrectedException));
            }
            finally
            {

                //password too long
                try
                {
                    var user = new PreRegistrateUser
                        {
                            Login = "fkjsaklf",
                            Password = "skfjklsjdflkjskldfjlkasjfkljasdklfjsadklfjklsdjfklsdjfklasdjfkljasdkl",
                            ConfirmPassword = "lsadfj2",
                            Email = "alkor@yandex.ru",
                            Info = "dsfsdfsdf"
                        };
                    registration.TryRegistrateUser(user);
                }
                catch (Exception ex)
                {
                    Assert.AreEqual(ex.GetType(), typeof(PasswordIncorrectedException));
                }
                finally
                {
                    //password doesn't include number
                    try
                    {
                        var user = new PreRegistrateUser
                            {
                                Login = "asdbdfsf",
                                Password = "lsadfj2",
                                ConfirmPassword = "lsadfj2",
                                Email = "alkor@yandex.ru",
                                Info = "dsfsdfsdf"
                            };
                        registration.TryRegistrateUser(user);
                    }
                    catch (Exception ex)
                    {
                        Assert.AreEqual(ex.GetType(), typeof(PasswordIncorrectedException));
                    }
                    //password includes some wrong numbers
                    finally
                    {
                        try
                        {
                            var user = new PreRegistrateUser
                                {
                                    Login = "asdbdfsf",
                                    Password = "lsadfj2 df",
                                    ConfirmPassword = "lsadfj2",
                                    Email = "alkor@yandex.ru",
                                    Info = "dsfsdfsdf"
                                };
                            registration.TryRegistrateUser(user);
                        }
                        catch (Exception ex)
                        {
                            Assert.AreEqual(ex.GetType(), typeof(PasswordIncorrectedException));
                        }
                    }
                }
            }
        }
    }
}
