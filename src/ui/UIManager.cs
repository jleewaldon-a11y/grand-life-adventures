using System;
using GTA;
using GTA.UI;

namespace GrandLifeAdventures.UI
{
    public class UIManager
    {
        private bool menuActive = false;
        private MainMenu mainMenu;
        private RelationshipMenu relationshipMenu;
        private CareerMenu careerMenu;
        private FamilyMenu familyMenu;

        public UIManager()
        {
            mainMenu = new MainMenu();
            relationshipMenu = new RelationshipMenu();
            careerMenu = new CareerMenu();
            familyMenu = new FamilyMenu();
        }

        public void Update()
        {
            if (menuActive)
            {
                DrawMainMenu();
            }
        }

        public void ToggleMainMenu()
        {
            menuActive = !menuActive;
        }

        private void DrawMainMenu()
        {
            // Draw semi-transparent background
            new Rectangle(0, 0, 400, 300, System.Drawing.Color.FromArgb(100, 0, 0, 0)).Draw();

            // Draw title
            new UIText("GRAND LIFE ADVENTURES", new System.Drawing.PointF(50, 20), 1.5f, System.Drawing.Color.White).Draw();

            // Draw menu options
            new UIText("[1] Relationships", new System.Drawing.PointF(50, 60), 0.7f, System.Drawing.Color.Yellow).Draw();
            new UIText("[2] Career", new System.Drawing.PointF(50, 100), 0.7f, System.Drawing.Color.Yellow).Draw();
            new UIText("[3] Family", new System.Drawing.PointF(50, 140), 0.7f, System.Drawing.Color.Yellow).Draw();
            new UIText("[4] Life Status", new System.Drawing.PointF(50, 180), 0.7f, System.Drawing.Color.Yellow).Draw();
            new UIText("[ESC] Close Menu", new System.Drawing.PointF(50, 220), 0.7f, System.Drawing.Color.White).Draw();
        }
    }

    public class MainMenu
    {
        public string Title { get; set; } = "Grand Life Adventures";
    }

    public class RelationshipMenu
    {
        // Relationship menu implementation
    }

    public class CareerMenu
    {
        // Career menu implementation
    }

    public class FamilyMenu
    {
        // Family menu implementation
    }
}
