using MakeNotes.DAL.Infrastructure;
using Xunit;

namespace MakeNotes.UnitTests.DAL
{
    public class DefaultConnectionFactoryTests
    {
        [Fact]
        public void Create_Should_ReturnConnectionStringGiven()
        {
            var connectionString = "Data Source=testpath;Database=test";
            var factory = new DefaultConnectionFactory(connectionString);

            var result = factory.Create();

            Assert.NotNull(result);
            Assert.Equal(connectionString, result.ConnectionString);
        }
    }
}
