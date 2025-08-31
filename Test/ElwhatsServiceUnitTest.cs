using FahdCloud.ThirdParty.WhatsappProviders.Interfaces.Services;
using FahdCloud.ThirdParty.WhatsappProviders.Models.Elwhats;
using Microsoft.Extensions.DependencyInjection;

namespace Test.ServiceTests
{
    public static class ElwhatsServiceTest
    {
        private static readonly ElwhatsSetting Settings = new()
        {
            ClientId = "6xJrt3xntXLLZYsa" // Replace with actual client ID for testing
        };

        private const string TestMessage = "Hello from Whats Providers! from Elwhats";
        private const string MediaUrl = "https://fahd-cloud-medical-dev.b-cdn.net/UploadedFiles/Storage%20Files/eaf89276-1c52-8407-91ce-48ddb297aa45.png";
        private const string MediaCaption = "Check out this amazing image from Elwhats!";

        public static async Task RunTestAsync(ServiceProvider serviceProvider, string phoneNumber)
        {
            Console.WriteLine("=== Testing Elwhats Service ===");

            try
            {
                var elwhatsService = serviceProvider.GetRequiredService<IElwhatsService>();

                // Test connection
                var isConnected = await elwhatsService.CheckConnectionAsync(Settings);
                Console.WriteLine($"Elwhats Connection status: {isConnected}");

                if (!isConnected)
                {
                    Console.WriteLine("❌ Connection failed. Skipping message tests.");
                    return;
                }

                // Test text message
                await TestTextMessageAsync(elwhatsService, phoneNumber);

                // Test media message
                await TestMediaMessageAsync(elwhatsService, phoneNumber);

                Console.WriteLine("✅ Elwhats tests completed successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Elwhats Error: {ex.Message}");
            }
            finally
            {
                Console.WriteLine();
            }
        }

        private static async Task TestTextMessageAsync(IElwhatsService service, string phoneNumber)
        {
            try
            {
                var result = await service.SendMessageAsync(Settings, phoneNumber, TestMessage);
                if (result.isSuccess)
                {
                    Console.WriteLine("✅ Elwhats text message sent successfully!");
                }
                else
                {
                    Console.WriteLine($"❌ Failed to send text message: {result.meessage}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Failed to send text message: {ex.Message}");
            }
        }

        private static async Task TestMediaMessageAsync(IElwhatsService service, string phoneNumber)
        {
            try
            {
                var result = await service.SendMediaMessageAsync(
                    Settings,
                    phoneNumber,
                    MediaCaption,
                    MediaUrl
                );

                Console.WriteLine(result.isSuccess
                    ? "✅ Elwhats media message sent successfully!"
                    : $"❌ Failed to send media message: {result.meessage}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Failed to send media message: {ex.Message}");
            }
        }
    }
}