using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Reflection;
using System.Text;
using Tests;

namespace NUnitTests
{
    class FoodManagerTests
    {
        private Mock<Tests.IConfiguration> ConfigurationMock;
       //private Mock<IConfigurationManager> ConfigurationMock;
        private Mock<IDataReader> ReaderMock;

        [SetUp]
        public void Setup()
        {
            //ConfigurationMock = new Mock<Tests.IConfiguration>();
            //ReaderMock = new Mock<IDataReader>();


            //ConfigurationMock.SetupGet(c => c.savefood).Returns("");
            //var mockConfSection = new Mock<IConfigurationSection>();
            //mockConfSection.SetupGet(m => m[It.IsAny<string>()]).Returns("mock value");
            //var mockConfiguration = new Mock<Microsoft.Extensions.Configuration.IConfiguration>();
            //mockConfiguration.Setup(a => a.GetSection(It.Is<string>(s => s == "ConnectionStrings"))).Returns(mockConfSection.Object);
            var config = ConfigurationManager.OpenExeConfiguration(Assembly.GetCallingAssembly().Location);
            var connectionStrings = (ConnectionStringsSection)config.GetSection("connectionStrings");
            connectionStrings.ConnectionStrings["savefood"]
                .ConnectionString = @"Data Source=C:\Dev\commands.sqlite";
            config.Save();
            ConfigurationManager.RefreshSection("connectionStrings");
        }

        [Test]
        public void AddFood()
        {
            var food = new Food();

            var result = FoodManager.getFood("name", "John");

            Assert.IsNotNull(result);
            Assert.IsNull(result.donor);
            Assert.AreEqual(result.FoodName, "dfd");
        }
        
    }
}
