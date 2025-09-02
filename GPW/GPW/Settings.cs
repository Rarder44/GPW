using System;
using Microsoft.Win32;

namespace GPW
{
    internal static class Settings
    {
        private const string RegistryPath = @"Software\GPW";

        public enum NotificationType
        {
            MessageBox,
            Toast
        }

        // Proprietà generica per NotificationType
        public static NotificationType Notification
        {
            get
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(RegistryPath))
                {
                    /*
                     *   string propertyName = MethodBase.GetCurrentMethod().Name.Replace("get_", "");
                    string value = key?.GetValue(propertyName) as string;
                    */
                    string value = key?.GetValue(nameof(Notification)) as string;
                    if (Enum.TryParse(value, out NotificationType result))
                        return result;
                }
                return NotificationType.MessageBox; // Default
            }
            set
            {
                using (RegistryKey key = Registry.CurrentUser.CreateSubKey(RegistryPath))
                {
                    key.SetValue(nameof(Notification), value.ToString(), RegistryValueKind.String);
                }
            }
        }

        // Esempio di proprietà generica per future impostazioni
        // public static string AltraImpostazione
        // {
        //     get { ... }
        //     set { ... }
        // }
    }
}
