using FahdCloud.ThirdParty.WhatsappProviders.Enums;
using FahdCloud.ThirdParty.WhatsappProviders.Interfaces.Services;
using FahdCloud.ThirdParty.WhatsappProviders.Models.Whats360Crm;
using Microsoft.Extensions.DependencyInjection;

namespace Test
{
    public static class Whats360CrmServiceUnitTest
    {
        private static readonly Whats360CrmSetting CrmSettings = new()
        {
            ApiKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1aWQiOiJRSVhPbmpWdmZ4UFVHRjNVc1NrdHpQYXpIM1l2ZjF3USIsInJvbGUiOiJ1c2VyIiwiaWF0IjoxNzUwNzgwNDU1fQ.WxXIrMfLDAyMCwXpp2T1FShxCSGydnmZ2Er4xI-N0OE",
            ClientId = "eyJ1aWQiOiJRSVhPbmpWdmZ4UFVHRjNVc1NrdHpQYXpIM1l2ZjF3USIsImNsaWVudF9pZCI6ImRzYWRzYSJ9",
            PhoneNationalNumber = "201096950327"
        };

        private const string TestMessage = "Hello from Whats Providers! from Whats360";
        private const string MediaUrl = "https://fahd-cloud-medical-dev.b-cdn.net/UploadedFiles/Storage%20Files/eaf89276-1c52-8407-91ce-48ddb297aa45.png";
        private const string MediaCaption = "Check out this image from Whats360!";
        private const string MediaFilename = "whats360-test.png";

        public static async Task RunTestAsync(ServiceProvider serviceProvider, string phoneNumber)
        {
            Console.WriteLine("=== Testing Whats360 Service ===");

            try
            {
                var whats360Service = serviceProvider.GetRequiredService<IWhats360CrmService>();

                // Test connection
                var isConnected = await whats360Service.CheckConnectionAsync(CrmSettings);
                Console.WriteLine($"Whats360 Connection status: {isConnected}");

                if (!isConnected)
                {
                    Console.WriteLine("❌ Connection failed. Skipping message tests.");
                    return;
                }

                // Test text message
                await TestTextMessageAsync(whats360Service, phoneNumber);

                // Test media message
                await TestMediaMessageAsync(whats360Service, phoneNumber);

                Console.WriteLine("✅ Whats360 tests completed successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Whats360 Error: {ex.Message}");
            }
            finally
            {
                Console.WriteLine();
            }
        }

        private static async Task TestTextMessageAsync(IWhats360CrmService crmService, string phoneNumber)
        {
            try
            {
                await crmService.SendMessageAsync(CrmSettings, phoneNumber, TestMessage);
                Console.WriteLine("✅ Whats360 text message sent successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Failed to send text message: {ex.Message}");
            }
        }

        private static async Task TestMediaMessageAsync(IWhats360CrmService crmService, string phoneNumber)
        {
            try
            {
                await crmService.SendMediaMessageAsync(
                    CrmSettings,
                    phoneNumber,
                    MediaUrl,
                    EnumWhats36oMediaType.image,
                    MediaCaption,
                    MediaFilename
                );
                Console.WriteLine("✅ Whats360 media message sent successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Failed to send media message: {ex.Message}");
            }
        }
    }
}