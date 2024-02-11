namespace FaithfulRemindersWeb.Global.Exceptions
{
    /// <summary>
    /// Exception that is thrown if the User already has a Password that Exists in the Database
    /// </summary>
    public class PasswordAlreadyExistsException : Exception
    {
        public PasswordAlreadyExistsException(string message) : base(message) { }

        public PasswordAlreadyExistsException(string message, Exception innerException) : base(message, innerException) { }
    }
}
