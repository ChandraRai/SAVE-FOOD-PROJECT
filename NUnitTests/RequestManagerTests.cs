using NUnit.Framework;
using System;

namespace NUnitTests
{
    public class RequestManagerTests
    {
        private RequestManager requestManager;
        private string connStr = string.Empty;

        [SetUp]
        public void Setup()
        {
            requestManager = new RequestManager(connStr);
        }

        [Test]
        public void GetRequest_ReturnsEmptyRequest()
        {
            var field = string.Empty;
            var value = string.Empty;

            var result = requestManager.GetRequest(field, value);

            Assert.IsNotNull(result);
            Assert.IsNull(result.Date);
            Assert.IsNull(result.ItemDetails);
            Assert.AreEqual(result.Status, 0);
            Assert.IsNull(result.user);
        }

        [Test]
        public void AddRequest_NullRequest_NoException()
        {
            Assert.DoesNotThrow(() => requestManager.AddRequest(new UserRequest(connStr)
            {
                user = new User()
            }));
        }

        [Test]
        public void AddRequest_ThrowsException()
        {
            Assert.Throws<NullReferenceException>(() => requestManager.AddRequest(new UserRequest(connStr)));
        }

        [Test]
        public void GetRequests_ReturnsEmptyList()
        {
            var id = string.Empty;
            var requestType = true;

            var result = requestManager.GetRequests(id, requestType);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count, 0);
        }

        [Test]
        public void UpdateRequestStatus_NoException()
        {
            Assert.DoesNotThrow(() => requestManager.UpdateRequestStatus(new UserRequest(connStr)
            {
                user = new User()
            }));
        }

        [Test]
        public void UpdateRequestStatus_NullRequest_ThrowsException()
        {
            Assert.Throws<NullReferenceException>(() => requestManager.AddRequest(null));
        }


        [TestCase("")]
        [TestCase("id")]
        public void CancelRequest_NoException(string id)
        {
            Assert.DoesNotThrow(() => requestManager.CancelRequest(id));
        }
    }
}
