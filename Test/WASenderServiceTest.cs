using FahdCloud.ThirdParty.WhatsappProviders.Enums;
using FahdCloud.ThirdParty.WhatsappProviders.Interfaces.Services;
using FahdCloud.ThirdParty.WhatsappProviders.Models.WASender;
using Microsoft.Extensions.DependencyInjection;

namespace Test.ServiceTests
{
    public static class WASenderServiceTest
    {
        private static readonly WASenderSetting Settings = new()
        {
            ApiKey = "387ff390164d09623bdde2c9f457ed481ee37f59f26cca762dfc42ca143147fc",
            InstanceId = "your_instance_id_here"
        };

        private const string TestMessage = "Hello from Whats Providers! from WASender";
        private const string MediaUrl = "https://fahd-cloud-medical-dev.b-cdn.net/UploadedFiles/Storage%20Files/eaf89276-1c52-8407-91ce-48ddb297aa45.png";
        private const string MediaCaption = "Check out this image from WASender!";
        private const string MediaFilename = "wasender-test.png";

        public static async Task RunTestAsync(ServiceProvider serviceProvider, string phoneNumber)
        {
            Console.WriteLine("=== Testing WASender Service ===");

            try
            {
                var waSenderService = serviceProvider.GetRequiredService<IWASenderService>();

                var res = await waSenderService.GetWhatsappSessionAsync(Settings);


                // Test connection
                var isConnected = await waSenderService.CheckConnectionAsync(Settings);
                Console.WriteLine($"WASender Connection status: {isConnected}");

                if (!isConnected)
                {
                    Console.WriteLine("❌ WASender Connection failed. Skipping message tests.");
                    Console.WriteLine("💡 Please check your API key and instance ID in WASenderServiceTest.cs");
                    return;
                }

                // Test text message
                await TestTextMessageAsync(waSenderService, phoneNumber);

                // Test media message
                await TestMediaMessageAsync(waSenderService, phoneNumber);

                Console.WriteLine("✅ WASender tests completed successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ WASender Error: {ex.Message}");
            }
            finally
            {
                Console.WriteLine();
            }
        }

        private static async Task TestTextMessageAsync(IWASenderService service, string phoneNumber)
        {
            try
            {
                await service.SendMessageAsync(Settings, phoneNumber, TestMessage);
                Console.WriteLine("✅ WASender text message sent successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Failed to send text message: {ex.Message}");
            }
        }

        private static async Task TestMediaMessageAsync(IWASenderService service, string phoneNumber)
        {
            try
            {
                await service.SendMediaMessageAsync(
                    Settings,
                    phoneNumber,
                    MediaUrl,
                    EnumWASenderMediaType.image,
                    MediaCaption,
                    MediaFilename
                );
                Console.WriteLine("✅ WASender media message sent successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Failed to send media message: {ex.Message}");
            }
        }
    }
}