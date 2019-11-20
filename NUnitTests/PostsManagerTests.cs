using NUnit.Framework;
using System;

namespace NUnitTests
{
    public class PostsManagerTests
    {
        private PostsManager postsManager;
        private string connStr = string.Empty;

        [SetUp]
        public void Setup()
        {
            postsManager = new PostsManager(connStr);
        }

        [Test]
        public void GetPostsList_ReturnsEmptyList()
        {
            var result = postsManager.GetPostsList(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count, 0);
        }

        [Test]
        public void AddOrder_NullRequest_NoException()
        {
            Assert.DoesNotThrow(() => postsManager.AddPost(new Post(connStr)
            {
                user = new User()
            }));
        }

        [Test]
        public void AddOrder_ThrowsException()
        {
            Assert.Throws<NullReferenceException>(() => postsManager.AddPost(new Post(connStr)));
        }
    }
}
