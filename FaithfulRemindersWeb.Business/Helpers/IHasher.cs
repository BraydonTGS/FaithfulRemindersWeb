namespace FaithfulRemindersWeb.Business.Helpers
{
    /// <summary>
    /// Interface used to define methods needed for Hashing and Salting
    /// </summary>
    internal interface IHasher
    {
        public (byte[] hash, byte[] salt) GenerateHash(string password);
    }
}
