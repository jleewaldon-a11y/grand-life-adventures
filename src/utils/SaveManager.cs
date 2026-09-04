using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using GrandLifeAdventures.Systems;

namespace GrandLifeAdventures.Utils
{
    public class SaveManager
    {
        private string savePath;
        private const string SAVE_FOLDER = \"GrandLifeAdventures_Saves\";
        private const string SAVE_EXTENSION = \".gla\";

        public SaveManager()
        {
            savePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), SAVE_FOLDER);
            
            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }
        }

        public void SaveGame(string slotName, GameState gameState)
        {
            try
            {
                string filePath = Path.Combine(savePath, $\"{slotName}{SAVE_EXTENSION}\");
                
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                };

                string jsonData = JsonSerializer.Serialize(gameState, options);
                File.WriteAllText(filePath, jsonData);

                ModCore.Log($\"Game saved to slot: {slotName}\");
            }
            catch (Exception ex)
            {
                ModCore.Log($\"Error saving game: {ex.Message}\");
            }
        }

        public GameState LoadGame(string slotName)
        {
            try
            {
                string filePath = Path.Combine(savePath, $\"{slotName}{SAVE_EXTENSION}\");

                if (!File.Exists(filePath))
                {
                    ModCore.Log($\"Save file not found: {slotName}\");
                    return null;
                }

                string jsonData = File.ReadAllText(filePath);
                GameState gameState = JsonSerializer.Deserialize<GameState>(jsonData);

                ModCore.Log($\"Game loaded from slot: {slotName}\");
                return gameState;
            }
            catch (Exception ex)
            {
                ModCore.Log($\"Error loading game: {ex.Message}\");
                return null;
            }
        }

        public bool SaveFileExists(string slotName)
        {
            string filePath = Path.Combine(savePath, $\"{slotName}{SAVE_EXTENSION}\");
            return File.Exists(filePath);
        }

        public string[] GetAllSaveFiles()
        {
            try
            {
                var files = Directory.GetFiles(savePath, $\"*{SAVE_EXTENSION}\");
                string[] saveNames = new string[files.Length];

                for (int i = 0; i < files.Length; i++)
                {
                    saveNames[i] = Path.GetFileNameWithoutExtension(files[i]);
                }

                return saveNames;
            }
            catch
            {
                return new string[0];
            }
        }

        public void DeleteSave(string slotName)
        {
            try
            {
                string filePath = Path.Combine(savePath, $\"{slotName}{SAVE_EXTENSION}\");
                
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    ModCore.Log($\"Save deleted: {slotName}\");
                }
            }
            catch (Exception ex)
            {
                ModCore.Log($\"Error deleting save: {ex.Message}\");
            }
        }

        public FileInfo GetSaveFileInfo(string slotName)
        {
            string filePath = Path.Combine(savePath, $\"{slotName}{SAVE_EXTENSION}\");
            
            if (File.Exists(filePath))
            {
                return new FileInfo(filePath);
            }

            return null;
        }
    }

    public class GameState
    {
        [JsonPropertyName(\"saveVersion\")]
        public int SaveVersion { get; set; } = 1;

        [JsonPropertyName(\"playerName\")]
        public string PlayerName { get; set; }

        [JsonPropertyName(\"playTime\")]
        public float PlayTime { get; set; }

        [JsonPropertyName(\"saveDate\")]
        public DateTime SaveDate { get; set; }

        [JsonPropertyName(\"currentLocation\")]
        public string CurrentLocation { get; set; }

        [JsonPropertyName(\"money\")]
        public float Money { get; set; }

        [JsonPropertyName(\"experience\")]
        public int Experience { get; set; }

        [JsonPropertyName(\"currentCareer\")]
        public string CurrentCareer { get; set; }

        [JsonPropertyName(\"relationships\")]
        public RelationshipState[] Relationships { get; set; }

        [JsonPropertyName(\"currentRomance\")]
        public string CurrentRomanceName { get; set; }

        [JsonPropertyName(\"children\")]
        public ChildState[] Children { get; set; }

        [JsonPropertyName(\"childCount\")]
        public int ChildCount { get; set; }

        [JsonPropertyName(\"familyHappiness\")]
        public float FamilyHappiness { get; set; }

        [JsonPropertyName(\"properties\")]
        public PropertyState[] Properties { get; set; }

        [JsonPropertyName(\"achievements\")]
        public string[] Achievements { get; set; }
    }

    public class RelationshipState
    {
        [JsonPropertyName(\"name\")]
        public string Name { get; set; }

        [JsonPropertyName(\"affection\")]
        public float Affection { get; set; }

        [JsonPropertyName(\"status\")]
        public string Status { get; set; }

        [JsonPropertyName(\"startDate\")]
        public DateTime? StartDate { get; set; }

        [JsonPropertyName(\"marriageDate\")]
        public DateTime? MarriageDate { get; set; }
    }

    public class ChildState
    {
        [JsonPropertyName(\"name\")]
        public string Name { get; set; }

        [JsonPropertyName(\"age\")]
        public int Age { get; set; }

        [JsonPropertyName(\"birthDate\")]
        public DateTime BirthDate { get; set; }

        [JsonPropertyName(\"happiness\")]
        public float Happiness { get; set; }

        [JsonPropertyName(\"discipline\")]
        public float Discipline { get; set; }

        [JsonPropertyName(\"skills\")]
        public string[] Skills { get; set; }
    }

    public class PropertyState
    {
        [JsonPropertyName(\"name\")]
        public string Name { get; set; }

        [JsonPropertyName(\"address\")]
        public string Address { get; set; }

        [JsonPropertyName(\"purchasePrice\")]
        public float PurchasePrice { get; set; }

        [JsonPropertyName(\"currentValue\")]
        public float CurrentValue { get; set; }

        [JsonPropertyName(\"purchaseDate\")]
        public DateTime PurchaseDate { get; set; }
    }
}
