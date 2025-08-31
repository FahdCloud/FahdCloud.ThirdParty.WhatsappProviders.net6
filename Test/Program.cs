using FahdCloud.ThirdParty.WhatsappProviders.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Test.ServiceTests;

namespace Test
{
    internal abstract class Program
    {
        private static async Task Main()
        {
            var services = new ServiceCollection();

            services.AddWhatsappProviders();

            var serviceProvider = services.BuildServiceProvider();

            // Test phone numbers
            var phoneNumber = "+201127256117" ;

            // Run individual service tests
            // await UltramsgServiceTest.RunTestAsync(serviceProvider, phoneNumber);
            // await WABotMasterServiceTest.RunTestAsync(serviceProvider, phoneNumber);
            // await WASenderServiceTest.RunTestAsync(serviceProvider, phoneNumber);
            // await ElwhatsServiceTest.RunTestAsync(serviceProvider, phoneNumber);
            // await Whats360CrmServiceUnitTest.RunTestAsync(serviceProvider, phoneNumber);
            await Whats360ServiceUnitTest.RunTestAsync(serviceProvider, phoneNumber);

            Console.WriteLine("All tests completed. Press any key to exit...");
            Console.ReadKey();
        }
    }
}