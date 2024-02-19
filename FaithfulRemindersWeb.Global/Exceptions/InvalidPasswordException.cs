namespace FaithfulRemindersWeb.Global.Exceptions
{
    /// <summary>
    /// Exception that is thrown when the Password Verification Fails
    /// </summary>
    public class InvalidPasswordException : Exception
    {
        public InvalidPasswordException(string message) : base(message) { }
        public InvalidPasswordException(string message, Exception innerException) : base(message, innerException) { }
    }
}
