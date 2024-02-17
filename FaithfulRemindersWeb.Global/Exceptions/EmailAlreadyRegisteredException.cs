namespace FaithfulRemindersWeb.Global.Exceptions
{
    public class EmailAlreadyRegisteredException : Exception
    {
        public EmailAlreadyRegisteredException(string message) : base(message) { }

        public EmailAlreadyRegisteredException(string message, Exception innerException) : base(message, innerException) { }
    }
}
