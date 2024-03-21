using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Tests
{
    public class RepositoryTests
    {
        [Fact]
        public void SaveChanges_ThrowsRepositoryException_WhenConcurrencyConflictOccurs()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TestContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;

            using (var context = new TestContext(options))
            {
                var repo = new TestRepository(context);

                // Act
                var ex = Assert.Throws<RepositoryException>(() => repo.SaveChanges());

                // Assert
                Assert.Equal("Concurrency conflict occurred.", ex.Message);
            }
        }
    }
}
