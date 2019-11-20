using Moq;
using NUnit.Framework;
using System;
using System.Data;
using System.Data.Common;

namespace NUnitTests
{
    class FoodManagerTests
    {

        private FoodManager foodManager;
        private Mock<IDataReader> dataReaderMock;
        private Mock<DbConnection> dbConnection;


        [SetUp]
        public void Setup()
        {
            foodManager = new FoodManager("");
            dataReaderMock = new Mock<IDataReader>();
            dbConnection = new Mock<DbConnection>();

            dataReaderMock.SetupSequence(x => x.Read())
                .Returns(true)
                .Returns(false);

            dbConnection.Setup(c => c.Open());
        }

        [Test]
        public void GetFood_ReturnsNullFood()
        {
            var result = foodManager.GetFood("name", "John");

            Assert.IsNotNull(result);
            Assert.IsNull(result.donor);
            Assert.IsNull(result.FoodName);
            Assert.IsNull(result.FoodDesc);
            Assert.IsNull(result.PostingDate);
            Assert.IsNull(result.FId);
        }

        [Test]
        public void GetUserFoodList_ReturnsEmptyList()
        {
            var result = foodManager.GetUserFoodList();

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count, 0);
        }

        [Test]
        public void GetAdminFoodList_ReturnsEmptyList()
        {
            var result = foodManager.GetAdminFoodList();

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count, 0);
        }

        [Test]
        public void SearchFood_ReturnsEmptyList()
        {
            var result = foodManager.SearchFood(string.Empty);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count, 0);
        }

        [Test]
        public void UpdateFoodStatus_NoException()
        {
            Assert.DoesNotThrow(() => foodManager.UpdateFoodStatus("1", 3));
        }

        [Test]
        public void DeleteFood_NoException()
        {
            Assert.DoesNotThrow(() => foodManager.DeleteFood("1"));
        }

        [Test]
        public void AddFood_NoException()
        {
            Assert.DoesNotThrow(() => foodManager.AddFood(new Food("") {
                FId = "1",
                FoodName = "food",
                FoodDesc = "desc",
                donor = new User()
            }));
        }

        [Test]
        public void AddFood_NullReferenceException()
        {
            Assert.Throws<NullReferenceException>(() => foodManager.AddFood(new Food("")));
        }
    }
}
