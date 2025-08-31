using FahdCloud.ThirdParty.WhatsappProviders.Enums;
using FahdCloud.ThirdParty.WhatsappProviders.Interfaces.Services;
using FahdCloud.ThirdParty.WhatsappProviders.Models.Ultramsg;
using Microsoft.Extensions.DependencyInjection;

namespace Test
{
    public static class UltramsgServiceTest
    {
        private static readonly UltramsgSetting Settings = new()
        {
            InstanceId = "instance107496",
            Token = "2lfisz1lq2zpxkzh"
        };

        private const string TestMessage = "Hello from Whats Providers! from Ultramsg";
        private const string MediaUrl = "https://fahd-cloud-medical-dev.b-cdn.net/UploadedFiles/Storage%20Files/eaf89276-1c52-8407-91ce-48ddb297aa45.png";
        private const string MediaCaption = "Check out this amazing image from Ultramsg!";
        private const string MediaFilename = "ultramsg-test.png";

        public static async Task RunTestAsync(ServiceProvider serviceProvider, string phoneNumber)
        {
            Console.WriteLine("=== Testing Ultramsg Service ===");

            try
            {
                var ultramsgService = serviceProvider.GetRequiredService<IUltramsgService>();

                // Test connection
                var isConnected = await ultramsgService.CheckConnectionAsync(Settings);
                Console.WriteLine($"Ultramsg Connection status: {isConnected}");

                if (!isConnected)
                {
                    Console.WriteLine("❌ Connection failed. Skipping message tests.");
                    return;
                }

                // Test text message
                await TestTextMessageAsync(ultramsgService, phoneNumber);

                // Test media message
                await TestMediaMessageAsync(ultramsgService, phoneNumber);

                Console.WriteLine("✅ Ultramsg tests completed successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Ultramsg Error: {ex.Message}");
            }
            finally
            {
                Console.WriteLine();
            }
        }

        private static async Task TestTextMessageAsync(IUltramsgService service, string phoneNumber)
        {
            try
            {
                await service.SendMessageAsync(Settings, phoneNumber, TestMessage);
                Console.WriteLine("✅ Ultramsg text message sent successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Failed to send text message: {ex.Message}");
            }
        }

        private static async Task TestMediaMessageAsync(IUltramsgService service, string phoneNumber)
        {
            try
            {
                await service.SendMediaMessageAsync(
                    Settings,
                    phoneNumber,
                    MediaUrl,
                    EnumMediaType.image,
                    MediaCaption,
                    MediaFilename
                );
                Console.WriteLine("✅ Ultramsg media message sent successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Failed to send media message: {ex.Message}");
            }
        }
    }
}