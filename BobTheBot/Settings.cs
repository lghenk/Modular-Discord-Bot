using System;
using System.IO;
using BobTheBot.models;
using Newtonsoft.Json;

namespace BobTheBot {
    public class Settings {

        public bool IsValid { get; private set; } = false;
        public SettingsModel Options { get; private set; }

        private string _settinsPath;

        public Settings() {
            _settinsPath = AppDomain.CurrentDomain.BaseDirectory + "/settings.json";

            if (File.Exists(_settinsPath)) {
                // Load in settings (if valid)
                SettingsModel settingsModel = JsonConvert.DeserializeObject<SettingsModel>(File.ReadAllText(_settinsPath));
                if (settingsModel != null) {
                    Console.WriteLine($"settings :: Initialized and loaded");
                    Options = settingsModel;
                    IsValid = true;
                }
            } else {
                Console.WriteLine($"settings :: First time Initialized... Created settings file.. Please update the token and any other settings");

                SettingsModel newModel = new SettingsModel();
                File.WriteAllText(_settinsPath, JsonConvert.SerializeObject(newModel));
            }
        }
    }
}
