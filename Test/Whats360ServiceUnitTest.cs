using FahdCloud.ThirdParty.WhatsappProviders.Enums;
using FahdCloud.ThirdParty.WhatsappProviders.Interfaces.Services;
using FahdCloud.ThirdParty.WhatsappProviders.Models.Whats360;
using FahdCloud.ThirdParty.WhatsappProviders.Models.Whats360Crm;
using Microsoft.Extensions.DependencyInjection;

namespace Test
{
    public static class Whats360ServiceUnitTest
    {
        private static readonly Whats360Setting CrmSettings = new()
        {
            ApiKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1aWQiOiJJSVhhd2Z3YmlYaTJ6ckVWMnRiRmN3VDh6N1RveHNGciIsInJvbGUiOiJ1c2VyIiwiaWF0IjoxNzUwNzgxNjQ0fQ.TgE1whYeHlgLpirIAKocJiqWY5HSDKDWx62HG1RaEzU",
            ClientId = "eyJ1aWQiOiJJSVhhd2Z3YmlYaTJ6ckVWMnRiRmN3VDh6N1RveHNGciIsImNsaWVudF9pZCI6ImRldiJ9",
            PhoneNationalNumber = "201096950327"
        };

        private const string TestMessage = "Hello from Whats Providers! from Whats360";

        public static async Task RunTestAsync(ServiceProvider serviceProvider, string phoneNumber)
        {
            Console.WriteLine("=== Testing Whats360 Service ===");

            try
            {
                var whats360Service = serviceProvider.GetRequiredService<IWhats360Service>();

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

        private static async Task TestTextMessageAsync(IWhats360Service crmService, string phoneNumber)
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
    }
}