using NUnit.Framework;
using System;

namespace NUnitTests
{
    public class UserManagerTests
    {
        private UserManager userManager;
        private string connStr = string.Empty;

        [SetUp]
        public void Setup()
        {
            userManager = new UserManager(connStr);
        }

        [Test]
        public void GetUser_ReturnsEmptyUser()
        {
            var field = string.Empty;
            var value = string.Empty;

            var result = userManager.GetUser(field, value);

            Assert.IsNotNull(result);
            Assert.IsNull(result.email);
            Assert.IsNull(result.firstName);
            Assert.IsNull(result.lastName);
            Assert.IsNull(result.phone);
            Assert.AreEqual(result.privilege, 0);
        }

        [TestCase("")]
        [TestCase("username")]
        public void UsernameExists_ThrowsException(string username)
        {
            Assert.Throws<NullReferenceException>(() => userManager.UsernameExists(username));
        }

        [Test]
        public void UpdateUser_NullRequest_NoException()
        {
            Assert.DoesNotThrow(() => userManager.UpdateUser(new User()));
        }

        [Test]
        public void AddUser_NullRequest_NoException()
        {
            Assert.DoesNotThrow(() => userManager.AddUser(new User()));
        }

        [Test]
        public void AddUser_ThrowsException()
        {
            Assert.Throws<NullReferenceException>(() => userManager.AddUser(null));
        }

        [Test]
        public void ValidateUser_ThrowsException()
        {
            var username = string.Empty;
            var password = string.Empty;

            Assert.Throws<NullReferenceException>(() => userManager.ValidateUser(username, password));
        }
    }
}
