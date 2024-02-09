using FaithfulRemindersWeb.Business.Passwords.Dto;
using System.Security.Cryptography;
using System.Text;
using static FaithfulRemindersWeb.Global.Constants.Enums;

namespace FaithfulRemindersWeb.Business.Passwords
{
    /// <summary>
    /// Hash Generator
    /// 
    /// Hashing: The process of converting an input (like a password) into a fixed size string of bytes. Typically a sequence of characters in a hash code. 
    /// 
    /// Salting: Involves adding random data (a "salt") to the input before hashing to ensure the input does not always result in the same hash.
    /// </summary>
    public class PasswordHasher : IPasswordHasher<PasswordDto>
    {

        #region HashPassword
        /// <summary>
        /// Hash the given input
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public (byte[] hash, byte[] salt) HashPassword(string input)
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

        #region VerifyHashedPassword
        /// <summary>
        /// 
        /// </summary>
        /// <param name="password"></param>
        /// <param name="providedPassword"></param>
        /// <returns></returns>
        public PasswordVerificationResults VerifyHashedPassword(PasswordDto password, string providedPassword)
        {
            try
            {
                var providedPasswordHash = GenerateHash(providedPassword, password.Salt);

                var success = providedPasswordHash.SequenceEqual(password.Hash);

                if (!success) { return PasswordVerificationResults.Failed; }

                return PasswordVerificationResults.Success;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}
