using FahdCloud.ThirdParty.WhatsappProviders.Enums;
using FahdCloud.ThirdParty.WhatsappProviders.Interfaces.Services;
using FahdCloud.ThirdParty.WhatsappProviders.Models.Whats360Pro;
using Microsoft.Extensions.DependencyInjection;

namespace Test.ServiceTests
{
    public static class Whats360ProServiceTest
    {
        private static readonly Whats360ProSetting Settings = new()
        {
            Token = "",
            InstanceId = ""
        };

        private const string TestJid = "201234567890@s.whatsapp.net";
        private const string TestMessage = "Hello from Whats Providers! from Whats360 Pro";
        private const string MediaUrl = "https://fahd-cloud-medical-dev.b-cdn.net/UploadedFiles/Storage%20Files/eaf89276-1c52-8407-91ce-48ddb297aa45.png";
        private const string MediaCaption = "Check out this amazing image from Whats360 Pro!";

        public static async Task RunTestAsync(ServiceProvider serviceProvider, string phoneNumber)
        {
            Console.WriteLine("=== Testing Whats360 Pro Service ===");

            try
            {
                var whats360ProService = serviceProvider.GetRequiredService<IWhats360ProService>();

                // Test connection
                var isConnected = await whats360ProService.CheckConnectionAsync(Settings);
                Console.WriteLine($"Whats360 Pro Connection status: {isConnected}");

                if (!isConnected)
                {
                    Console.WriteLine("❌ Connection failed. Skipping message tests.");
                    // In a real test we might want to continue if we are testing other methods,
                    // but following the pattern in the project.
                    return;
                }

                // Test text message
                await TestTextMessageAsync(whats360ProService, phoneNumber);

                // Test media message
                await TestMediaMessageAsync(whats360ProService, phoneNumber);

                // Test instance management
                await TestInstanceManagementAsync(whats360ProService);

                Console.WriteLine("✅ Whats360 Pro tests completed successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Whats360 Pro Error: {ex.Message}");
            }
            finally
            {
                Console.WriteLine();
            }
        }

        private static async Task TestTextMessageAsync(IWhats360ProService service, string phoneNumber)
        {
            try
            {
                // Normalize phone number to JID if needed, or use JID directly
                string jid = phoneNumber.EndsWith("@s.whatsapp.net") ? phoneNumber : $"{phoneNumber.Replace("+", "")}@s.whatsapp.net";
                
                var result = await service.SendMessageAsync(Settings, jid, TestMessage);
                if (result.isSuccess)
                {
                    Console.WriteLine("✅ Whats360 Pro text message sent successfully!");
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

        private static async Task TestMediaMessageAsync(IWhats360ProService service, string phoneNumber)
        {
            try
            {
                string jid = phoneNumber.EndsWith("@s.whatsapp.net") ? phoneNumber : $"{phoneNumber.Replace("+", "")}@s.whatsapp.net";

                var result = await service.SendMediaMessageAsync(
                    Settings,
                    jid,
                    MediaUrl,
                    EnumWhats360ProMediaType.image,
                    MediaCaption
                );

                if (result.isSuccess)
                {
                    Console.WriteLine("✅ Whats360 Pro media message sent successfully!");
                }
                else
                {
                    Console.WriteLine($"❌ Failed to send media message: {result.meessage}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Failed to send media message: {ex.Message}");
            }
        }

        private static async Task TestInstanceManagementAsync(IWhats360ProService service)
        {
            try
            {
                Console.WriteLine("Testing Instance Management...");
                var instances = await service.GetInstancesAsync(Settings);
                if (instances.success)
                {
                    Console.WriteLine("✅ Successfully retrieved instances.");
                }
                else
                {
                    Console.WriteLine($"❌ Failed to retrieve instances: {instances.message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Failed during instance management tests: {ex.Message}");
            }
        }
    }
}
