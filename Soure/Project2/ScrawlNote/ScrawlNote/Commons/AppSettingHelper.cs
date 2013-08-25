using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrawlNote.Commons
{
    public static class AppSettingHelper
    {
        public static readonly string LanguageKey = "LanguageKey";
        private static readonly IsolatedStorageSettings settings = IsolatedStorageSettings.ApplicationSettings;
        public static void AddOrUpdateValue(string key, object value)
        {
            bool valueChanged = false;

            if (settings.Contains(key))
            {
                if (settings[key] != value)
                {
                    settings[key] = value;
                    valueChanged = true;
                }
            }
            else
            {
                settings.Add(key, value);
                valueChanged = true;
            }
            if (valueChanged)
            {
                Save();
            }
        }

        public static T GetValueOrDefault<T>(string key, T defaultValue)
        {
            T value;

            if (settings.Contains(key))
            {
                value = (T)settings[key];
            }
            else
            {
                value = defaultValue;
            }
            return value;
        }

        private static void Save()
        {
            settings.Save();
        }

    }
}
