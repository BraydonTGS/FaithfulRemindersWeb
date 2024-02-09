using FaithfulRemindersWeb.Business.Passwords;
using FaithfulRemindersWeb.Business.Passwords.Dto;
using FaithfulRemindersWeb.Business.Tests.Base;
using FaithfulRemindersWeb.Business.ToDoItems;
using Microsoft.Extensions.DependencyInjection;
using static FaithfulRemindersWeb.Global.Constants.Enums;

namespace FaithfulRemindersWeb.Business.Tests
{
    [TestClass]
    public class PasswordHasherTests : TestBase
    {
        private readonly IServiceCollection _services;
        private readonly IServiceProvider _serviceProvider;
        private readonly IPasswordHasher<PasswordDto> _passwordHasher;
        public PasswordHasherTests()
        {
            _services = ConfigureServices(seedDatabase: true);
            _serviceProvider = _services.BuildServiceProvider();
            _passwordHasher = _serviceProvider.GetRequiredService<IPasswordHasher<PasswordDto>>();
        }

        [TestMethod]
        public void ConstructorNotNull_Success()
        {
            Assert.IsNotNull(_passwordHasher);
        }

        [TestMethod]
        [DataRow("PASSWORD")]
        [DataRow("passWord")]
        [DataRow("PaSsWord")]
        [DataRow("12345678z")]
        [DataRow("ABCDEFG")]
        [DataRow("CatsAndDogs")]
        [DataRow("TestingIsFun")]
        [DataRow("StarWars")]
        [DataRow("YoloBaggins")]
        [DataRow("WhatsUp")]
        public void HashPassword_Success(string input)
        {
            var (salt, hash) = _passwordHasher.HashPassword(input);

            Assert.IsNotNull(salt);
            Assert.IsNotNull(hash);
            Assert.AreEqual(64, salt.Length);
            Assert.AreEqual(16, hash.Length);
        }

        [TestMethod]
        [DataRow("PASSWORD")]
        [DataRow("passWord")]
        [DataRow("PaSsWord")]
        [DataRow("12345678z")]
        [DataRow("ABCDEFG")]
        [DataRow("CatsAndDogs")]
        [DataRow("TestingIsFun")]
        [DataRow("StarWars")]
        [DataRow("YoloBaggins")]
        [DataRow("WhatsUp")]
        public void VerifyGivenPassword_Success(string input)
        {
            var passwordDto = DtoGenerationHelper.GeneratePasswordDto();

            var (hash, salt) = _passwordHasher.HashPassword(input);

            passwordDto.Salt = salt;
            passwordDto.Hash = hash;

            var isValid = _passwordHasher.VerifyHashedPassword(passwordDto, input);

            Assert.AreEqual(PasswordVerificationResults.Success, isValid);
        }

        [TestMethod]
        [DataRow("PASSWORD")]
        [DataRow("passWord")]
        [DataRow("PaSsWord")]
        [DataRow("12345678z")]
        [DataRow("ABCDEFG")]
        [DataRow("CatsAndDogs")]
        [DataRow("TestingIsFun")]
        [DataRow("StarWars")]
        [DataRow("YoloBaggins")]
        [DataRow("WhatsUp")]
        public void VerifyGivenPassword_IsNotCorrect_Success(string input)
        {
            var invalidPassword = "Invalid";

            var passwordDto = DtoGenerationHelper.GeneratePasswordDto();

            var (hash, salt) = _passwordHasher.HashPassword(input);

            passwordDto.Salt = salt;
            passwordDto.Hash = hash;

            var isValid = _passwordHasher.VerifyHashedPassword(passwordDto, invalidPassword);

            Assert.AreEqual(PasswordVerificationResults.Failed, isValid);
        }
    }
}
