namespace FahdCloud.ThirdParty.WhatsappProviders.Models.Ultramsg
{
    /// <summary>
    /// Represents user data information for a WhatsApp account, including profile details and device information.
    /// </summary>
    public class UltramsgSessionDetails
    {
        /// <summary>
        /// Gets or sets the unique identifier for the WhatsApp user.
        /// </summary>
        public string id { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the name of the WhatsApp user.
        /// </summary>
        public string name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the URL of the user's profile picture.
        /// </summary>
        public string profile_picture { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets whether the account uses multi-device mode.
        /// </summary>
        public bool is_md { get; set; }

        /// <summary>
        /// Gets or gets whether the account is a business account.
        /// </summary>
        public bool is_business { get; set; }

        /// <summary>
        /// Gets or gets whether the account is an enterprise account.
        /// </summary>
        public bool is_is_enterprise { get; set; }

        /// <summary>
        /// Gets or gets the battery level of the device, if available.
        /// </summary>
        public string battery { get; set; }


        /// <summary>
        /// Gets or sets the battery charging status of the device, if available.
        /// </summary>
        public string battery_charging { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the WhatsApp version running on the device, if available.
        /// </summary>
        public string wa_version { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the device information associated with the WhatsApp account.
        /// </summary>
        public DeviceInfo device { get; set; } = new DeviceInfo();
    }

    /// <summary>
    /// Represents nested device information for a WhatsApp account.
    /// </summary>
    public class DeviceInfo
    {
        /// <summary>
        /// Gets or sets the operating system version of the device, if available.
        /// </summary>
        public string os_version { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the platform of the device (e.g., smba).
        /// </summary>
        public string platform { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the manufacturer of the device, if available.
        /// </summary>
        public string manufacturer { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the model of the device, if available.
        /// </summary>
        public string model { get; set; } = string.Empty;
    }
}