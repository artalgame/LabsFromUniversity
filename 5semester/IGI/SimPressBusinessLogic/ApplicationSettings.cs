using RepositoryInterfaces;
using SimPressDomainModel;
using SimPressDomainModel.EFDbContext;

namespace SimPressBusinessLogic
{
    /// <summary>
    /// This class realize general settings and basic mechanism of bussiness logic
    /// </summary>
    public static class ApplicationSettings
    {
        private const string ConnectionString = @"Data Source=ARTAL-ПК\SQLEXPRESS;Initial Catalog=SimPressDataBase;Integrated Security=True;Pooling=False";

        public static readonly string LoginRegex = @"^[A-Za-z0-9]{3,30}$";
        public static readonly string PasswordRegex = @"^((?=.*\d)(?=.*[A-Za-z])\w{6,30})$";
        public static readonly string EmailRegex = @"^(?=.*\d)(?=.*[a-zA-Z]).{6,24}$";
        public static readonly int    UserInfoMaxLength = 300;

        private static SimPressEFDBContext GetContext()
        {
            return new SimPressEFDBContext(ConnectionString);
        }

        public static IRepository GetRepository()
        {
            return new DBRepository(GetContext());
        }
    }
}
