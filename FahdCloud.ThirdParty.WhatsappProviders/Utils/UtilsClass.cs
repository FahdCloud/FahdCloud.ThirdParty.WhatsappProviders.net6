namespace FahdCloud.ThirdParty.WhatsappProviders.Utils
{
    public static class UtilsClass
    {
        public static string NormalizePhoneNumber(string phoneNumber)
        {
            return phoneNumber.TrimStart('+');
        }
    }
}