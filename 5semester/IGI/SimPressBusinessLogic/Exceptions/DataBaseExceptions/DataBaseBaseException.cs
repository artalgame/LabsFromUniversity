using System;

namespace SimPressBusinessLogic.Exceptions.DataBaseExceptions
{
    public class DataBaseBaseException:Exception
    {
        public DataBaseBaseException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
