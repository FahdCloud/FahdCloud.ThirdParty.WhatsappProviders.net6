# FahdCloud.ThirdParty.WhatsappProviders

A .NET library that provides unified interfaces and implementations for integrating with various WhatsApp service providers. This library simplifies the process of sending WhatsApp messages through different third-party providers with a consistent API.

## Features

- 🔌 **Multiple Provider Support** - Support for Ultramsg, WASender, Whats360, Whats360 Pro and WABotMaster
- 💾 **Built-in Caching** - Memory caching support for improved performance
- 🏗️ **Dependency Injection Ready** - Seamless integration with Microsoft.Extensions.DependencyInjection
- ⚙️ **Configuration Support** - Easy configuration through Microsoft.Extensions.Options
- 📱 **Media Support** - Send text messages, images, documents, and other media types
- 🔄 **HTTP Client Integration** - Built-in HTTP client management with Microsoft.Extensions.Http

## Supported Providers

- **Ultramsg** - WhatsApp Business API provider
- **WASender** - WhatsApp messaging service
- **Whats360** - WhatsApp API platform
- **Whats360 Pro** - Professional WhatsApp Messaging API
- **WABotMaster** - WhatsApp automation service

## Installation

Install the package via NuGet Package Manager:

```bash
dotnet add package FahdCloud.ThirdParty.WhatsappProviders
```

Or via Package Manager Console:
```powershell
Install-Package FahdCloud.ThirdParty.WhatsappProviders
```

## Quick Start

### 1. Register Services
```csharp
using FahdCloud.ThirdParty.WhatsappProviders.Extensions;

// In your Program.cs or Startup.cs
builder.Services.AddWhatsappProviders();

// Configure your specific provider settings
builder.Services.Configure<UltramsgSettings>(builder.Configuration.GetSection("UltramsgSettings"));
builder.Services.Configure<WASenderSettings>(builder.Configuration.GetSection("WASenderSettings"));
builder.Services.Configure<Whats360Settings>(builder.Configuration.GetSection("Whats360Settings"));
builder.Services.Configure<Whats360ProSetting>(builder.Configuration.GetSection("Whats360ProSettings"));
builder.Services.Configure<WABotMasterSettings>(builder.Configuration.GetSection("WABotMasterSettings"));
```

### 2. Configuration
Add your WhatsApp provider settings to your `appsettings.json`:
```json
{
  "UltramsgSettings": {
    "Token": "your-ultramsg-token",
    "InstanceId": "your-instance-id",
    "BaseUrl": "https://api.ultramsg.com"
  },
  "WASenderSettings": {
    "ApiKey": "your-wasender-api-key",
    "BaseUrl": "https://api.wasender.com"
  },
  "Whats360Settings": {
    "ApiKey": "your-whats360-api-key",
    "BaseUrl": "https://api.whats360.com"
  },
  "Whats360ProSettings": {
    "Token": "your-whats360-pro-token",
    "InstanceId": "your-instance-id"
  },
  "WABotMasterSettings": {
    "ApiKey": "your-wabotmaster-api-key",
    "BaseUrl": "https://api.wabotmaster.com"
  }
}
```

## Usage Examples

### Ultramsg Provider
```csharp
public class UltramsgMessageService
{
    private readonly IUltramsgService _ultramsgService;

    public UltramsgMessageService(IUltramsgService ultramsgService)
    {
        _ultramsgService = ultramsgService;
    }

    // Send text message
    public async Task SendTextMessageAsync(string phoneNumber, string message)
    {
        var result = await _ultramsgService.SendTextMessageAsync(phoneNumber, message);
        if (result.IsSuccess)
        {
            Console.WriteLine($"Message sent successfully: {result.MessageId}");
        }
    }

    // Send image message
    public async Task SendImageAsync(string phoneNumber, string imageUrl, string caption = null)
    {
        var result = await _ultramsgService.SendImageAsync(phoneNumber, imageUrl, caption);
        if (result.IsSuccess)
        {
            Console.WriteLine($"Image sent successfully: {result.MessageId}");
        }
    }

    // Send document
    public async Task SendDocumentAsync(string phoneNumber, string documentUrl, string fileName)
    {
        var result = await _ultramsgService.SendDocumentAsync(phoneNumber, documentUrl, fileName);
        if (result.IsSuccess)
        {
            Console.WriteLine($"Document sent successfully: {result.MessageId}");
        }
    }
}
```

### WASender Provider
```csharp
public class WASenderMessageService
{
    private readonly IWASenderService _waSenderService;

    public WASenderMessageService(IWASenderService waSenderService)
    {
        _waSenderService = waSenderService;
    }

    // Send text message
    public async Task SendTextMessageAsync(string phoneNumber, string message)
    {
        var result = await _waSenderService.SendTextMessageAsync(phoneNumber, message);
        if (result.IsSuccess)
        {
            Console.WriteLine($"Message sent via WASender: {result.MessageId}");
        }
    }

    // Send media message
    public async Task SendMediaMessageAsync(string phoneNumber, string mediaUrl, string mediaType, string caption = null)
    {
        var result = await _waSenderService.SendMediaMessageAsync(phoneNumber, mediaUrl, mediaType, caption);
        if (result.IsSuccess)
        {
            Console.WriteLine($"Media sent via WASender: {result.MessageId}");
        }
    }

    // Get message status
    public async Task<MessageStatus> GetMessageStatusAsync(string messageId)
    {
        return await _waSenderService.GetMessageStatusAsync(messageId);
    }
}
```

### Whats360 Provider
```csharp
public class Whats360MessageService
{
    private readonly IWhats360Service _whats360Service;

    public Whats360MessageService(IWhats360Service whats360Service)
    {
        _whats360Service = whats360Service;
    }

    // Send text message
    public async Task SendTextMessageAsync(string phoneNumber, string message)
    {
        var result = await _whats360Service.SendTextMessageAsync(Settings, phoneNumber, message);
        if (result.isSuccess)
        {
            Console.WriteLine($"Message sent via Whats360: {result.meessage}");
        }
    }

}

### Whats360 Pro Provider
```csharp
public class Whats360ProMessageService
{
    private readonly IWhats360ProService _whats360ProService;
    private readonly Whats360ProSetting _settings = new() { Token = "token", InstanceId = "id" };

    public Whats360ProMessageService(IWhats360ProService whats360ProService)
    {
        _whats360ProService = whats360ProService;
    }

    // Send text message
    public async Task SendTextMessageAsync(string jid, string message)
    {
        var result = await _whats360ProService.SendMessageAsync(_settings, jid, message);
        if (result.isSuccess)
        {
            Console.WriteLine($"Message sent via Whats360 Pro: {result.meessage}");
        }
    }

    // Send image
    public async Task SendImageAsync(string jid, string imageUrl, string caption)
    {
        var result = await _whats360ProService.SendMediaMessageAsync(_settings, jid, imageUrl, EnumWhats360ProMediaType.image, caption);
        if (result.isSuccess)
        {
            Console.WriteLine($"Image sent via Whats360 Pro");
        }
    }
}
```

### WABotMaster Provider
```csharp
public class WABotMasterMessageService
{
    private readonly IWABotMasterService _waBotMasterService;

    public WABotMasterMessageService(IWABotMasterService waBotMasterService)
    {
        _waBotMasterService = waBotMasterService;
    }

    // Send text message
    public async Task SendTextMessageAsync(string phoneNumber, string message)
    {
        var result = await _waBotMasterService.SendTextMessageAsync(phoneNumber, message);
        if (result.IsSuccess)
        {
            Console.WriteLine($"Message sent via WABotMaster: {result.MessageId}");
        }
    }
    
}
```

## Complete Integration Example
```csharp
// Program.cs
using FahdCloud.ThirdParty.WhatsappProviders.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add WhatsApp providers
builder.Services.AddWhatsappProviders();

// Configure settings
builder.Services.Configure<UltramsgSettings>(builder.Configuration.GetSection("UltramsgSettings"));
builder.Services.Configure<WASenderSettings>(builder.Configuration.GetSection("WASenderSettings"));
builder.Services.Configure<Whats360Settings>(builder.Configuration.GetSection("Whats360Settings"));
builder.Services.Configure<Whats360ProSetting>(builder.Configuration.GetSection("Whats360ProSettings"));
builder.Services.Configure<WABotMasterSettings>(builder.Configuration.GetSection("WABotMasterSettings"));

// Add your application services
builder.Services.AddScoped<INotificationService, NotificationService>();

var app = builder.Build();

// NotificationService.cs
public class NotificationService : INotificationService
{
    private readonly IUltramsgService _ultramsgService;
    private readonly IWASenderService _waSenderService;
    private readonly IWhats360Service _whats360Service;
    private readonly IWhats360ProService _whats360ProService;
    private readonly IWABotMasterService _waBotMasterService;

    public NotificationService(
        IUltramsgService ultramsgService,
        IWASenderService waSenderService,
        IWhats360Service whats360Service,
        IWhats360ProService whats360ProService,
        IWABotMasterService waBotMasterService)
    {
        _ultramsgService = ultramsgService;
        _waSenderService = waSenderService;
        _whats360Service = whats360Service;
        _whats360ProService = whats360ProService;
        _waBotMasterService = waBotMasterService;
    }

    public async Task SendNotificationAsync(string phoneNumber, string message, WhatsAppProvider provider)
    {
        try
        {
            var result = provider switch
            {
                WhatsAppProvider.Ultramsg => await _ultramsgService.SendTextMessageAsync(phoneNumber, message),
                WhatsAppProvider.WASender => await _waSenderService.SendMessageAsync(new WASenderSetting(), phoneNumber, message),
                WhatsAppProvider.Whats360 => await _whats360Service.SendMessageAsync(new Whats360Setting(), phoneNumber, message),
                WhatsAppProvider.Whats360Pro => await _whats360ProService.SendMessageAsync(new Whats360ProSetting(), phoneNumber, message),
                WhatsAppProvider.WABotMaster => await _waBotMasterService.SendTextMessageAsync(phoneNumber, message),
                _ => throw new ArgumentException($"Unsupported provider: {provider}")
            };

            if (result.IsSuccess)
            {
                Console.WriteLine($"Notification sent successfully via {provider}: {result.MessageId}");
            }
            else
            {
                Console.WriteLine($"Failed to send notification via {provider}: {result.ErrorMessage}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending notification via {provider}: {ex.Message}");
        }
    }
}

public enum WhatsAppProvider
{
    Ultramsg,
    WASender,
    Whats360,
    WABotMaster
}
```

## Error Handling
All services return a result object that includes success status and error information:
```csharp
var result = await _ultramsgService.SendTextMessageAsync(phoneNumber, message);

if (result.IsSuccess)
{
    Console.WriteLine($"Message sent successfully: {result.MessageId}");
}
else
{
    Console.WriteLine($"Failed to send message: {result.ErrorMessage}");
    Console.WriteLine($"Error Code: {result.ErrorCode}");
}
```

## Caching
The library includes built-in caching for improved performance. You can configure cache settings:
```csharp
// The library automatically handles caching for:
// - User data validation
// - Message status tracking
// - Provider response caching
```

## Dependencies
This library is built on top of the following Microsoft packages:
- **Microsoft.Extensions.Caching.Memory** (9.0.6) - For caching functionality
- **Microsoft.Extensions.DependencyInjection.Abstractions** (9.0.6) - For dependency injection
- **Microsoft.Extensions.Http** (9.0.6) - For HTTP client management
- **Microsoft.Extensions.Options.ConfigurationExtensions** (9.0.6) - For configuration binding
- **Newtonsoft.Json** (13.0.3) - For JSON serialization
- **Scrutor** (6.0.0) - For assembly scanning and service registration

## Requirements
- .NET 9.0 or later
- C# 12 with nullable reference types enabled

## Contributing
Contributions are welcome! Please feel free to submit a Pull Request.

## License
This project is licensed under the terms specified in the project repository.

## Support
For support and questions, please refer to the project's issue tracker or documentation.
using FahdCloud.ThirdParty.WhatsappProviders.Interfaces.Services;
using FahdCloud.ThirdParty.WhatsappProviders.Models.Elwhats;
using Microsoft.Extensions.DependencyInjection;

namespace Test.ServiceTests
{
    public static class ElwhatsServiceTest
    {
        private static readonly ElwhatsSetting Settings = new()
        {
            ClientId = "your-client-id-here" // Replace with actual client ID for testing
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

                if (result.isSuccess)
                {
                    Console.WriteLine("✅ Elwhats media message sent successfully!");
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
    }
}