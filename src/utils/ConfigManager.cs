using System;
using System.IO;
using System.Xml.Linq;

namespace GrandLifeAdventures.Utils
{
    public class ConfigManager
    {
        private string configPath;
        private XDocument configDocument;

        public ConfigManager()
        {
            configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.xml");
            LoadConfig();
        }

        private void LoadConfig()
        {
            if (File.Exists(configPath))
            {
                configDocument = XDocument.Load(configPath);
            }
            else
            {
                CreateDefaultConfig();
            }
        }

        private void CreateDefaultConfig()
        {
            configDocument = new XDocument(
                new XElement("GrandLifeAdventures",
                    new XElement("Settings",
                        new XElement("MenuKey", "F9"),
                        new XElement("RelationshipDecayRate", "0.05"),
                        new XElement("MaxChildren", "4"),
                        new XElement("Difficulty", "Normal")
                    )
                )
            );
            configDocument.Save(configPath);
        }

        public string GetSetting(string key)
        {
            var element = configDocument?.Root?.Element("Settings")?.Element(key);
            return element?.Value ?? "";
        }

        public void SetSetting(string key, string value)
        {
            var settingsElement = configDocument?.Root?.Element("Settings");
            if (settingsElement != null)
            {
                var element = settingsElement.Element(key);
                if (element != null)
                {
                    element.Value = value;
                }
                else
                {
                    settingsElement.Add(new XElement(key, value));
                }
                configDocument.Save(configPath);
            }
        }
    }
}
