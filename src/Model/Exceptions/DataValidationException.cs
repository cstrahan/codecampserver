using System;

namespace CodeCampServer.Model.Exceptions
{
    public class DataValidationException : Exception
    {
        public DataValidationException(string message) : base(message) { }
    }
}
