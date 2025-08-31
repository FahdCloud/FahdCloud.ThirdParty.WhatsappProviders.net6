# FahdCloud.ThirdParty.WhatsappProviders

A .NET library that provides unified interfaces and implementations for integrating with various WhatsApp service providers. This library simplifies the process of sending WhatsApp messages through different third-party providers with a consistent API.

## Features

- 🔌 **Multiple Provider Support** - Support for Ultramsg, WASender, Whats360, and WABotMaster
- 💾 **Built-in Caching** - Memory caching support for improved performance
- 🏗️ **Dependency Injection Ready** - Seamless integration with Microsoft.Extensions.DependencyInjection
- ⚙️ **Configuration Support** - Easy configuration through Microsoft.Extensions.Options
- 📱 **Media Support** - Send text messages, images, documents, and other media types
- 🔄 **HTTP Client Integration** - Built-in HTTP client management with Microsoft.Extensions.Http

## Supported Providers

- **Ultramsg** - WhatsApp Business API provider
- **WASender** - WhatsApp messaging service
- **Whats360** - WhatsApp API platform
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
        var result = await _whats360Service.SendTextMessageAsync(phoneNumber, message);
        if (result.IsSuccess)
        {
            Console.WriteLine($"Message sent via Whats360: {result.MessageId}");
        }
    }

    // Send template message
    public async Task SendTemplateMessageAsync(string phoneNumber, string templateName, object[] parameters)
    {
        var result = await _whats360Service.SendTemplateMessageAsync(phoneNumber, templateName, parameters);
        if (result.IsSuccess)
        {
            Console.WriteLine($"Template message sent: {result.MessageId}");
        }
    }

    // Send contact message
    public async Task SendContactAsync(string phoneNumber, string contactName, string contactPhone)
    {
        var result = await _whats360Service.SendContactAsync(phoneNumber, contactName, contactPhone);
        if (result.IsSuccess)
        {
            Console.WriteLine($"Contact sent: {result.MessageId}");
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

    // Send location message
    public async Task SendLocationAsync(string phoneNumber, double latitude, double longitude, string address = null)
    {
        var result = await _waBotMasterService.SendLocationAsync(phoneNumber, latitude, longitude, address);
        if (result.IsSuccess)
        {
            Console.WriteLine($"Location sent: {result.MessageId}");
        }
    }

    // Send button message
    public async Task SendButtonMessageAsync(string phoneNumber, string message, List<string> buttons)
    {
        var result = await _waBotMasterService.SendButtonMessageAsync(phoneNumber, message, buttons);
        if (result.IsSuccess)
        {
            Console.WriteLine($"Button message sent: {result.MessageId}");
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
    private readonly IWABotMasterService _waBotMasterService;

    public NotificationService(
        IUltramsgService ultramsgService,
        IWASenderService waSenderService,
        IWhats360Service whats360Service,
        IWABotMasterService waBotMasterService)
    {
        _ultramsgService = ultramsgService;
        _waSenderService = waSenderService;
        _whats360Service = whats360Service;
        _waBotMasterService = waBotMasterService;
    }

    public async Task SendNotificationAsync(string phoneNumber, string message, WhatsAppProvider provider)
    {
        try
        {
            var result = provider switch
            {
                WhatsAppProvider.Ultramsg => await _ultramsgService.SendTextMessageAsync(phoneNumber, message),
                WhatsAppProvider.WASender => await _waSenderService.SendTextMessageAsync(phoneNumber, message),
                WhatsAppProvider.Whats360 => await _whats360Service.SendTextMessageAsync(phoneNumber, message),
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