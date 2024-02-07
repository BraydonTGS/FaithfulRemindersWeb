using FaithfulRemindersWeb.Business.Passwords.Dto;
using System.Security.Cryptography;
using System.Text;

namespace FaithfulRemindersWeb.Business.Passwords
{
    internal class PasswordGenerator : IPasswordGenerator
    {
        #region GeneratePassword
        /// <summary>
        /// Take the Password Entered by the User and Generate a <see cref="PasswordDto"/>
        /// 
        /// Generate a Salt, Then using the Password & Salt, Generate the Hash
        /// 
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public PasswordDto GeneratePassword(string password)
        {
            try
            {
                PasswordDto passwordDto = new PasswordDto();
                passwordDto.Salt = GenerateSalt();
                passwordDto.Hash = HashPassword(password, passwordDto.Salt);
                return passwordDto;
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


        #region HashPassword
        /// <summary>
        /// Computes the SHA512 hash of a password combined with a salt using HMACSHA512.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <param name="salt">The salt to combine with the password before hashing.</param>
        /// <returns>A byte array containing the computed hash.</returns>
        private byte[] HashPassword(string password, byte[] salt)
        {
            try
            {
                using var hmac = new HMACSHA512(salt);

                return hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
