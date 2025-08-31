using FahdCloud.ThirdParty.WhatsappProviders.Enums;
using FahdCloud.ThirdParty.WhatsappProviders.Interfaces.Services;
using FahdCloud.ThirdParty.WhatsappProviders.Models.WABotMaster;
using Microsoft.Extensions.DependencyInjection;

namespace Test
{
    public static class WABotMasterServiceTest
    {
        private static readonly WABotMasterSetting Settings = new()
        {
            AccessToken = "6783d632ccc4b",
            InstanceId = "685BD5BE2F270"
        };

        private const string TestMessage = "Hello from Whats Providers! from WABotMaster";
        private const string MediaUrl = "https://fahd-cloud-medical-dev.b-cdn.net/UploadedFiles/Storage%20Files/eaf89276-1c52-8407-91ce-48ddb297aa45.png";
        private const string MediaCaption = "Check out this image from WABotMaster!";
        private const string MediaFilename = "wabotmaster-test.png";

        public static async Task RunTestAsync(ServiceProvider serviceProvider, string phoneNumber)
        {
            Console.WriteLine("=== Testing WABotMaster Service ===");

            try
            {
                var waBotMasterService = serviceProvider.GetRequiredService<IWABotMasterService>();

                // Test connection
                var isConnected = await waBotMasterService.CheckConnectionAsync(Settings);
                Console.WriteLine($"WABotMaster Connection status: {isConnected}");

                if (!isConnected)
                {
                    Console.WriteLine("❌ Connection failed. Skipping message tests.");
                    return;
                }

                // Test text message
                await TestTextMessageAsync(waBotMasterService, phoneNumber);

                // Test media message
                await TestMediaMessageAsync(waBotMasterService, phoneNumber);

                Console.WriteLine("✅ WABotMaster tests completed successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ WABotMaster Error: {ex.Message}");
            }
            finally
            {
                Console.WriteLine();
            }
        }

        private static async Task TestTextMessageAsync(IWABotMasterService service, string phoneNumber)
        {
            try
            {
                await service.SendMessageAsync(Settings, phoneNumber, TestMessage);
                Console.WriteLine("✅ WABotMaster text message sent successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Failed to send text message: {ex.Message}");
            }
        }

        private static async Task TestMediaMessageAsync(IWABotMasterService service, string phoneNumber)
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
                Console.WriteLine("✅ WABotMaster media message sent successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Failed to send media message: {ex.Message}");
            }
        }
    }
}