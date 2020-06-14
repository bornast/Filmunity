using NUnit.Framework;
using System.Threading.Tasks;

namespace IntegrationTests
{
    using static Testing;

    public class TestBase
    {        
        [TearDown]
        public async Task TearDown()
        {
            await ResetState();
        }

    }
}
