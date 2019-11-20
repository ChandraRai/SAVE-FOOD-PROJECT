using NUnit.Framework;
using System;

namespace NUnitTests
{
    public class OrderManagerTests
    {
        private OrderManager orderManager;
        private string connStr = string.Empty;

        [SetUp]
        public void Setup()
        {
            orderManager = new OrderManager(connStr);
        }

        [Test]
        public void GetOrder_ReturnsNullFood()
        {
            var result = orderManager.GetOrder("name", "field");

            Assert.IsNotNull(result);
            Assert.IsNull(result.OId);
            Assert.IsNull(result.request);
            Assert.IsNull(result.foodOrder);
            Assert.IsNull(result.consumer);
        }

        [Test]
        public void AddOrder_NullRequest_NoException()
        {
            Assert.DoesNotThrow(() => orderManager.AddOrder(new Order() {
                foodOrder = new Food(connStr),
                consumer = new User()
            }));
        }

        [Test]
        public void AddOrder_NotNullRequest_NoException()
        {
            Assert.DoesNotThrow(() => orderManager.AddOrder(new Order()
            {
                foodOrder = new Food(connStr),
                consumer = new User(),
                request = new UserRequest(connStr)
            }));
        }

        [Test]
        public void AddOrder_ThrowsException()
        {
            Assert.Throws<NullReferenceException>(() => orderManager.AddOrder(new Order()));
        }

        [Test]
        public void GetUserOrders_ReturnsEmptyList()
        {
            var result = orderManager.GetUserOrders(connStr);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count, 0);
        }

        [Test]
        public void CancelOrder_NoException()
        {
            Assert.DoesNotThrow(() => orderManager.CancelOrder(new Order()
            {
                foodOrder = new Food(connStr),
                consumer = new User(),
                request = new UserRequest(connStr)
            }));
        }

        [Test]
        public void CancelOrder_ThrowsException()
        {
            Assert.Throws<NullReferenceException>(() => orderManager.CancelOrder(new Order()));
        }
    }
}
