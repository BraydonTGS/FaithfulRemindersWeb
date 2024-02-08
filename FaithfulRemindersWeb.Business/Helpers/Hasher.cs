using System.Security.Cryptography;
using System.Text;

namespace FaithfulRemindersWeb.Business.Helpers
{
    /// <summary>
    /// Hash Generator
    /// </summary>
    internal class Hasher : IHasher
    {

        #region GenerateHash
        /// <summary>
        /// Hash the given input
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public (byte[] hash, byte[] salt) GenerateHash(string input)
        {
            try
            {
                var salt = GenerateSalt();
                var hash = GenerateHash(input, salt);

                return (hash, salt);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion 

        #region GenerateSalt
        /// <summary>
        /// Generates a random salt of 16 bytes using a secure random number generator.
        /// </summary>
        /// <returns>A byte array containing the generated salt.</returns>
        private byte[] GenerateSalt()
        {
            try
            {
                byte[] salt = new byte[16];

                using var rng = RandomNumberGenerator.Create();

                rng.GetBytes(salt);

                return salt;
            }
            catch (Exception)
            {
                throw;
            }

        }
        #endregion


        #region GenerateHash
        /// <summary>
        /// Computes the SHA512 hash of an input combined with a salt using HMACSHA512.
        /// </summary>
        /// <param name="input">The input to hash.</param>
        /// <param name="salt">The salt to combine with the input before hashing.</param>
        /// <returns>A byte array containing the computed hash.</returns>
        private byte[] GenerateHash(string input, byte[] salt)
        {
            try
            {
                using var hmac = new HMACSHA512(salt);

                return hmac.ComputeHash(Encoding.UTF8.GetBytes(input));
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
