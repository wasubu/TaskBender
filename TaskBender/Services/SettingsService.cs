using System;
using System.IO;
using System.Text.Json;

namespace TaskBender.Services
{
    public class AppSettings
    {
        public int SpotlightKey { get; set; } = 0x1D; // VK_NONCONVERT
        public int QuickLookKey { get; set; } = 0x1C; // VK_CONVERT
    }

    public class SettingsService
    {
        private readonly string _settingsPath;
        public AppSettings Settings { get; private set; }

        public event EventHandler? SettingsChanged;

        public SettingsService()
        {
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string folder = Path.Combine(appData, "TaskBender");
            Directory.CreateDirectory(folder);
            _settingsPath = Path.Combine(folder, "settings.json");
            Settings = LoadSettings();
        }

        private AppSettings LoadSettings()
        {
            if (File.Exists(_settingsPath))
            {
                try
                {
                    string json = File.ReadAllText(_settingsPath);
                    var settings = JsonSerializer.Deserialize<AppSettings>(json);
                    if (settings != null) return settings;
                }
                catch
                {
                    // Ignore errors and return default
                }
            }
            return new AppSettings();
        }

        public void SaveSettings()
        {
            string json = JsonSerializer.Serialize(Settings, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_settingsPath, json);
            SettingsChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
