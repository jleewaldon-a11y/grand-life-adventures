using System;
using System.IO;
using GTA;
using GTA.Native;
using GrandLifeAdventures.Systems;
using GrandLifeAdventures.UI;
using GrandLifeAdventures.Utils;

namespace GrandLifeAdventures.Core
{
    public class ModCore : Script
    {
        public static ModCore Instance { get; private set; }
        
        private RelationshipManager relationshipManager;
        private CareerSystem careerSystem;
        private FamilySystem familySystem;
        private EnhancedUIManager uiManager;
        private SaveManager saveManager;
        private ConfigManager configManager;
        private AutoSaveSystem autoSaveSystem;

        private int updateCounter = 0;
        private const int AUTOSAVE_INTERVAL = 18000; // 5 minutes at 60fps

        public ModCore()
        {
            Instance = this;
            
            try
            {
                // Initialize managers in order of dependency
                configManager = new ConfigManager();
                saveManager = new SaveManager();
                relationshipManager = new RelationshipManager();
                careerSystem = new CareerSystem();
                familySystem = new FamilySystem();
                uiManager = new EnhancedUIManager(saveManager);
                autoSaveSystem = new AutoSaveSystem(saveManager);

                // Register event handlers
                Tick += OnTick;
                KeyDown += OnKeyDown;
                KeyUp += OnKeyUp;
                Aborted += OnAborted;

                Log("═══════════════════════════════════════");
                Log("Grand Life Adventures v1.0 Initialized");
                Log("═══════════════════════════════════════");
                Log("Press F9 to open the mod menu");
                Log("Game saves are stored in Documents\\GrandLifeAdventures_Saves\\");
                Log("═══════════════════════════════════════");
            }
            catch (Exception ex)
            {
                Log($"CRITICAL ERROR: {ex.Message}");
                Log($"Stack Trace: {ex.StackTrace}");
            }
        }

        private void OnTick(object sender, EventArgs e)
        {
            try
            {
                // Update all game systems
                relationshipManager?.Update();
                careerSystem?.Update();
                familySystem?.Update();
                uiManager?.Update();

                // Handle auto-save
                updateCounter++;
                if (updateCounter >= AUTOSAVE_INTERVAL)
                {
                    PerformAutoSave();
                    updateCounter = 0;
                }

                // Display status indicators in-game
                DisplayStatusBar();
            }
            catch (Exception ex)
            {
                Log($"ERROR in OnTick: {ex.Message}");
            }
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                // F9 - Toggle main menu
                if (e.KeyCode == System.Windows.Forms.Keys.F9)
                {
                    uiManager?.ToggleMainMenu();
                    e.Handled = true;
                }

                // F10 - Quick save
                if (e.KeyCode == System.Windows.Forms.Keys.F10)
                {
                    QuickSave();
                    e.Handled = true;
                }

                // F11 - Quick load
                if (e.KeyCode == System.Windows.Forms.Keys.F11)
                {
                    QuickLoad();
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                Log($"ERROR in OnKeyDown: {ex.Message}");
            }
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            // Handle additional key releases if needed
        }

        private void OnAborted()
        {
            Log("═══════════════════════════════════════");
            Log("Grand Life Adventures mod unloaded");
            Log("═══════════════════════════════════════");
            PerformAutoSave();
        }

        /// <summary>
        /// Saves the current game state to a specific slot
        /// </summary>
        public void SaveGameToSlot(string slotName)
        {
            try
            {
                GameState state = CaptureGameState();
                saveManager.SaveGame(slotName, state);
                DisplayNotification("Game saved to: " + slotName, NotificationType.Success);
                Log($"Game saved to slot: {slotName}");
            }
            catch (Exception ex)
            {
                Log($"ERROR saving game: {ex.Message}");
                DisplayNotification("Failed to save game!", NotificationType.Error);
            }
        }

        /// <summary>
        /// Loads a game state from a specific slot
        /// </summary>
        public void LoadGameFromSlot(string slotName)
        {
            try
            {
                GameState state = saveManager.LoadGame(slotName);
                if (state != null)
                {
                    ApplyGameState(state);
                    DisplayNotification("Game loaded: " + slotName, NotificationType.Success);
                    Log($"Game loaded from slot: {slotName}");
                }
                else
                {
                    DisplayNotification("Failed to load game!", NotificationType.Error);
                }
            }
            catch (Exception ex)
            {
                Log($"ERROR loading game: {ex.Message}");
                DisplayNotification("Failed to load game!", NotificationType.Error);
            }
        }

        /// <summary>
        /// Quick save to "QuickSave" slot
        /// </summary>
        private void QuickSave()
        {
            SaveGameToSlot("QuickSave");
        }

        /// <summary>
        /// Quick load from "QuickSave" slot
        /// </summary>
        private void QuickLoad()
        {
            LoadGameFromSlot("QuickSave");
        }

        /// <summary>
        /// Auto-save the game state
        /// </summary>
        private void PerformAutoSave()
        {
            try
            {
                GameState state = CaptureGameState();
                saveManager.SaveGame("AutoSave", state);
                Log("AutoSave completed");
            }
            catch (Exception ex)
            {
                Log($"ERROR in auto-save: {ex.Message}");
            }
        }

        /// <summary>
        /// Captures the current game state from all systems
        /// </summary>
        private GameState CaptureGameState()
        {
            var state = new GameState
            {
                SaveVersion = 1,
                PlayerName = Game.Player.Name,
                PlayTime = (float)Game.GameTime / 1000f,
                SaveDate = DateTime.Now,
                CurrentLocation = GetCurrentLocation(),
                Money = careerSystem?.Money ?? 0,
                Experience = careerSystem?.Experience ?? 0,
                CurrentCareer = careerSystem?.CurrentCareer?.Name ?? "None",
                FamilyHappiness = familySystem?.FamilyHappiness ?? 0,
                ChildCount = familySystem?.ChildCount ?? 0,
                Relationships = CaptureRelationships(),
                Children = CaptureChildren(),
                Achievements = new string[0] // To be implemented
            };

            return state;
        }

        /// <summary>
        /// Applies a game state to all systems
        /// </summary>
        private void ApplyGameState(GameState state)
        {
            if (state == null) return;

            try
            {
                // Apply career data
                if (careerSystem != null && !string.IsNullOrEmpty(state.CurrentCareer))
                {
                    careerSystem.SetCareer(state.CurrentCareer);
                    careerSystem.Money = state.Money;
                    careerSystem.AddExperience(state.Experience);
                }

                // Apply family data
                if (familySystem != null)
                {
                    familySystem.FamilyHappiness = state.FamilyHappiness;
                }

                // Apply relationships
                if (state.Relationships != null && relationshipManager != null)
                {
                    foreach (var relState in state.Relationships)
                    {
                        if (relationshipManager.Relationships.ContainsKey(relState.Name))
                        {
                            var rel = relationshipManager.Relationships[relState.Name];
                            rel.Affection = relState.Affection;
                            rel.Status = (RelationshipStatus)Enum.Parse(typeof(RelationshipStatus), relState.Status);
                            rel.StartDate = relState.StartDate ?? DateTime.Now;
                            rel.MarriageDate = relState.MarriageDate;
                        }
                    }
                }

                Log("Game state applied successfully");
            }
            catch (Exception ex)
            {
                Log($"ERROR applying game state: {ex.Message}");
            }
        }

        /// <summary>
        /// Captures current relationship data
        /// </summary>
        private RelationshipState[] CaptureRelationships()
        {
            if (relationshipManager?.Relationships == null)
                return new RelationshipState[0];

            var relationships = new RelationshipState[relationshipManager.Relationships.Count];
            int index = 0;

            foreach (var rel in relationshipManager.Relationships.Values)
            {
                relationships[index] = new RelationshipState
                {
                    Name = rel.Name,
                    Affection = rel.Affection,
                    Status = rel.Status.ToString(),
                    StartDate = rel.StartDate,
                    MarriageDate = rel.MarriageDate
                };
                index++;
            }

            return relationships;
        }

        /// <summary>
        /// Captures current children data
        /// </summary>
        private ChildState[] CaptureChildren()
        {
            if (familySystem?.Children == null || familySystem.ChildCount == 0)
                return new ChildState[0];

            var children = new ChildState[familySystem.ChildCount];

            for (int i = 0; i < familySystem.ChildCount; i++)
            {
                var child = familySystem.Children[i];
                if (child != null)
                {
                    children[i] = new ChildState
                    {
                        Name = child.Name,
                        Age = child.Age,
                        BirthDate = child.BirthDate,
                        Happiness = child.Happiness,
                        Discipline = child.Discipline,
                        Skills = child.Skills ?? new string[0]
                    };
                }
            }

            return children;
        }

        /// <summary>
        /// Displays a status bar showing current life stats
        /// </summary>
        private void DisplayStatusBar()
        {
            // Display brief status at top of screen
            string statusText = $"Money: ${careerSystem?.Money:F0} | Relationships: {relationshipManager?.CurrentRomance?.Name ?? "Single"} | Family: {familySystem?.ChildCount} Kids";
            
            // This would be displayed using GTA.UI if desired
            // For now, just keeping it in logs
        }

        /// <summary>
        /// Gets the player's current location
        /// </summary>
        private string GetCurrentLocation()
        {
            // Placeholder - could be expanded to get actual GTA location
            return "Los Santos";
        }

        /// <summary>
        /// Displays a notification to the player
        /// </summary>
        private void DisplayNotification(string message, NotificationType type)
        {
            try
            {
                string color = type switch
                {
                    NotificationType.Success => "~g~",  // Green
                    NotificationType.Error => "~r~",    // Red
                    NotificationType.Warning => "~y~",  // Yellow
                    _ => "~w~"  // White
                };

                Function.Call(Hash.BEGIN_TEXT_COMMAND_DISPLAY_HELP, "STRING");
                Function.Call(Hash.ADD_TEXT_COMPONENT_STRING, $"{color}{message}");
                Function.Call(Hash.END_TEXT_COMMAND_DISPLAY_HELP, 0, true, true, -1);
            }
            catch (Exception ex)
            {
                Log($"ERROR displaying notification: {ex.Message}");
            }
        }

        /// <summary>
        /// Logs a message to the mod log file
        /// </summary>
        public static void Log(string message)
        {
            try
            {
                string logPath = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    "GrandLifeAdventures_Saves",
                    "grand_life_adventures.log"
                );

                string logMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}";
                File.AppendAllText(logPath, logMessage + Environment.NewLine);
            }
            catch
            {
                // Silently fail if logging fails
            }
        }

        private enum NotificationType
        {
            Success,
            Error,
            Warning,
            Info
        }
    }

    /// <summary>
    /// Handles automatic game saves at intervals
    /// </summary>
    public class AutoSaveSystem
    {
        private SaveManager saveManager;
        private DateTime lastAutoSaveTime;
        private const int AUTOSAVE_INTERVAL_SECONDS = 300; // 5 minutes

        public AutoSaveSystem(SaveManager saveManager)
        {
            this.saveManager = saveManager;
            lastAutoSaveTime = DateTime.Now;
        }

        public void CheckAndAutoSave()
        {
            if ((DateTime.Now - lastAutoSaveTime).TotalSeconds >= AUTOSAVE_INTERVAL_SECONDS)
            {
                lastAutoSaveTime = DateTime.Now;
                // Auto-save logic handled in ModCore
            }
        }
    }
}
