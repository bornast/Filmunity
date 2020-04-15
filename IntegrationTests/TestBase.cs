using NUnit.Framework;
using System.Threading.Tasks;

namespace IntegrationTests
{
    using static Testing;

    public class TestBase
    {
        [SetUp]
        public async Task SetUp()
        {
            await ResetState();
        }

        [TearDown]
        public async Task TearDown()
        {
            await ResetState();
        }

    }
}
