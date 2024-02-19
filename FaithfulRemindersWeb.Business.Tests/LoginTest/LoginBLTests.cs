﻿using FaithfulRemindersWeb.Business.Login;
using FaithfulRemindersWeb.Business.Registration;
using FaithfulRemindersWeb.Business.Tests.Base;
using FaithfulRemindersWeb.Business.Users;
using FaithfulRemindersWeb.Global.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using static FaithfulRemindersWeb.Global.Constants.Enums;

namespace FaithfulRemindersWeb.Business.Tests
{
    [TestClass]
    public class LoginBLTests : TestBase
    {
        private readonly IServiceCollection _services;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILoginBL _loginBL;
        private readonly IRegistrationBL _registrationBL;
        private readonly DatabaseSeeder _databaseSeeder;

        public LoginBLTests()
        {
            _services = ConfigureServices(seedDatabase: true);
            _serviceProvider = _services.BuildServiceProvider();
            _loginBL = _serviceProvider.GetRequiredService<ILoginBL>();
            _registrationBL = _serviceProvider.GetRequiredService<IRegistrationBL>();
            _databaseSeeder = _serviceProvider.GetRequiredService<DatabaseSeeder>();
        }

        [TestInitialize]
        public async Task TestInitialize()
        {
            await _databaseSeeder.Seed();
        }

        [TestMethod]
        public async Task LoginCurrentUserAsync_Success()
        {
            var newUser = DtoGenerationHelper.GenerateUserDto();

            newUser = await _registrationBL.RegisterNewUserAsync(newUser);

            Assert.IsNotNull(newUser);

            newUser.TempPassword = "MonkeyDBanana";

            newUser = await _loginBL.LoginUserAsync(newUser);

            Assert.IsNotNull(newUser);
            Assert.AreEqual(string.Empty, newUser.TempPassword);
        }

        [TestMethod]
        public async Task LoginCurrentUserAsync_InvalidPassword_Throws_InvalidPasswordException_Success()
        {
            InvalidPasswordException? passwordException = null;
            try
            {
                var newUser = DtoGenerationHelper.GenerateUserDto();

                newUser = await _registrationBL.RegisterNewUserAsync(newUser);

                Assert.IsNotNull(newUser);

                newUser.TempPassword = "BananaDMonkey";

                newUser = await _loginBL.LoginUserAsync(newUser);
            }
            catch (InvalidPasswordException ex)
            {
                passwordException = ex;
            }
            Assert.IsNotNull(passwordException);
            Assert.IsNotNull(passwordException.Message);

            Assert.IsNotNull("Password Verification Failure for the Specified User.", passwordException.Message);
        }

        [TestCleanup]
        public async Task TestCleanup()
        {
            await _databaseSeeder.Clear();
        }
    }
}
