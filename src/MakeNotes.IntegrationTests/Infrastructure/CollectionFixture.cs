using Xunit;

namespace MakeNotes.IntegrationTests.Infrastructure
{
    /// <summary>
    /// Needed to share context among tests
    /// https://xunit.github.io/docs/shared-context.html
    /// </summary>
    [CollectionDefinition(FixtureNames.SharedContextFixture)]
    public class CollectionFixture : ICollectionFixture<SharedContextTestFixture>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
