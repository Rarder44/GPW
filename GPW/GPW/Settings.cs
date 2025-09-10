using Microsoft.Win32;
using System;
using System.IO;
using System.Text.Json;
namespace GPW
{
    internal static class Settings
    {
        public static readonly string appName = "GPW";
        public static readonly string ProcessesFilePath;
        public static readonly string SettingsFilePath;

        static Settings()
        {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string appFolder = Path.Combine(appDataPath, appName);
            if (!Directory.Exists(appFolder))
            {
                Directory.CreateDirectory(appFolder);
            }
            ProcessesFilePath = Path.Combine(appFolder, "processes.txt");
            SettingsFilePath = Path.Combine(appFolder, "settings.txt");
        }


        public enum NotificationType
        {
            MessageBox,
            Toast
        }







        // Classe interna per rappresentare i settings
        private class SettingsData
        {
            public NotificationType Notification { get; set; } = NotificationType.MessageBox;
            // Aggiungi qui altre proprietà in futuro
        }

        // Proprietà Notification che usa il cache
        public static NotificationType Notification
        {
            get
            {
                if (_cache == null) readAll();
                return _cache.Notification;
            }
            set
            {
                if (_cache == null) readAll();
                _cache.Notification = value;
                saveAll();
            }
        }







        private static SettingsData _cache;

        // Legge tutti i settings dal file JSON
        public static void readAll()
        {
            if (!File.Exists(SettingsFilePath))
            {
                _cache = new SettingsData();
                return;
            }
            try
            {
                var json = File.ReadAllText(SettingsFilePath);
                _cache = JsonSerializer.Deserialize<SettingsData>(json);
            }
            catch
            {
                _cache = new SettingsData();
            }
        }

        // Salva tutti i settings nel file JSON
        public static void saveAll()
        {
            var json = JsonSerializer.Serialize(_cache ?? new SettingsData());
            File.WriteAllText(SettingsFilePath, json);
        }








      



    }
}
