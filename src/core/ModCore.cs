using System;
using System.IO;
using GTA;
using GTA.Native;

namespace GrandLifeAdventures.Core
{
    public class ModCore : Script
    {
        public static ModCore Instance { get; private set; }
        
        private RelationshipManager relationshipManager;
        private CareerSystem careerSystem;
        private FamilySystem familySystem;
        private UIManager uiManager;
        private ConfigManager configManager;

        public ModCore()
        {
            Instance = this;
            
            // Initialize managers
            configManager = new ConfigManager();
            relationshipManager = new RelationshipManager();
            careerSystem = new CareerSystem();
            familySystem = new FamilySystem();
            uiManager = new UIManager();

            // Register event handlers
            Tick += OnTick;
            KeyDown += OnKeyDown;
            Aborted += OnAborted;

            Log("Grand Life Adventures mod initialized successfully!");
        }

        private void OnTick(object sender, EventArgs e)
        {
            // Update all systems
            relationshipManager?.Update();
            careerSystem?.Update();
            familySystem?.Update();
            uiManager?.Update();
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            // Menu activation keybind (default: F9)
            if (e.KeyCode == System.Windows.Forms.Keys.F9)
            {
                uiManager?.ToggleMainMenu();
            }
        }

        private void OnAborted()
        {
            Log("Grand Life Adventures mod unloaded.");
        }

        public static void Log(string message)
        {
            string logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "grand_life_adventures.log");
            File.AppendAllText(logPath, $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}\n");
        }
    }
}
