using System;
using System.Drawing;
using GTA;
using GTA.UI;

namespace GrandLifeAdventures.UI
{
    public class EnhancedUIManager
    {
        private bool menuActive = false;
        private MenuScreen currentScreen = MenuScreen.Main;
        
        // UI Colors
        private static readonly Color PRIMARY_COLOR = Color.FromArgb(255, 30, 144, 255); // Dodger Blue
        private static readonly Color SECONDARY_COLOR = Color.FromArgb(255, 70, 130, 180); // Steel Blue
        private static readonly Color ACCENT_COLOR = Color.FromArgb(255, 0, 206, 209); // Dark Turquoise
        private static readonly Color TEXT_PRIMARY = Color.White;
        private static readonly Color TEXT_SECONDARY = Color.LightGray;
        private static readonly Color BG_DARK = Color.FromArgb(180, 10, 10, 10);
        private static readonly Color BG_SEMI = Color.FromArgb(100, 20, 20, 30);
        private static readonly Color HIGHLIGHT = Color.FromArgb(255, 0, 255, 0);
        private static readonly Color WARNING = Color.FromArgb(255, 255, 100, 0);
        private static readonly Color SUCCESS = Color.FromArgb(255, 50, 205, 50);

        private SaveManager saveManager;
        private int selectedMenuIndex = 0;
        private int selectedSubIndex = 0;

        public EnhancedUIManager(SaveManager saveManager)
        {
            this.saveManager = saveManager;
        }

        public void Update()
        {
            if (menuActive)
            {
                DrawMenu();
            }
        }

        public void ToggleMainMenu()
        {
            menuActive = !menuActive;
            selectedMenuIndex = 0;
            selectedSubIndex = 0;
            currentScreen = MenuScreen.Main;
        }

        private void DrawMenu()
        {
            switch (currentScreen)
            {
                case MenuScreen.Main:
                    DrawMainMenu();
                    break;
                case MenuScreen.Relationships:
                    DrawRelationshipsMenu();
                    break;
                case MenuScreen.Career:
                    DrawCareerMenu();
                    break;
                case MenuScreen.Family:
                    DrawFamilyMenu();
                    break;
                case MenuScreen.LifeStatus:
                    DrawLifeStatusMenu();
                    break;
                case MenuScreen.Save:
                    DrawSaveMenu();
                    break;
            }
        }

        private void DrawMainMenu()
        {
            const int menuX = 50;
            const int menuY = 100;
            const int menuWidth = 500;
            const int menuHeight = 450;

            // Draw main background
            DrawMenuBackground(menuX, menuY, menuWidth, menuHeight);

            // Draw title with gradient effect
            DrawTitle("GRAND LIFE ADVENTURES", menuX + 20, menuY + 20);

            // Draw menu options
            string[] menuOptions = {
                "~ RELATIONSHIPS",
                "~ CAREER & FINANCES",
                "~ FAMILY",
                "~ LIFE STATUS",
                "~ SAVE GAME",
                "~ LOAD GAME",
                "~ SETTINGS",
                "~ EXIT"
            };

            int optionY = menuY + 80;
            for (int i = 0; i < menuOptions.Length; i++)
            {
                bool isSelected = (selectedMenuIndex == i);
                DrawMenuOption(menuOptions[i], menuX + 30, optionY, isSelected, i);
                optionY += 45;
            }

            // Draw footer
            DrawFooter(menuX, menuY + menuHeight - 40, menuWidth);
        }

        private void DrawRelationshipsMenu()
        {
            const int menuX = 50;
            const int menuY = 100;
            const int menuWidth = 600;
            const int menuHeight = 500;

            DrawMenuBackground(menuX, menuY, menuWidth, menuHeight);
            DrawTitle("RELATIONSHIPS", menuX + 20, menuY + 20);

            string[] romanticOptions = {
                "Amanda - Confident & Independent",
                "Tracey - Energetic & Fun-Loving",
                "Kate - Artistic & Thoughtful",
                "Michelle - Ambitious & Driven"
            };

            int optionY = menuY + 80;
            for (int i = 0; i < romanticOptions.Length; i++)
            {
                bool isSelected = (selectedSubIndex == i);
                DrawMenuOption(romanticOptions[i], menuX + 30, optionY, isSelected, i);
                optionY += 90;
            }

            // Draw info panel
            DrawInfoPanel(menuX + menuWidth - 180, menuY + 80, 160, 350);
            new UIText("Affection: --/100", new PointF(menuX + menuWidth - 170, menuY + 100), 0.5f, TEXT_SECONDARY).Draw();
            new UIText("Status: Single", new PointF(menuX + menuWidth - 170, menuY + 130), 0.5f, TEXT_SECONDARY).Draw();

            DrawFooter(menuX, menuY + menuHeight - 40, menuWidth);
        }

        private void DrawCareerMenu()
        {
            const int menuX = 50;
            const int menuY = 100;
            const int menuWidth = 600;
            const int menuHeight = 500;

            DrawMenuBackground(menuX, menuY, menuWidth, menuHeight);
            DrawTitle("CAREER & FINANCES", menuX + 20, menuY + 20);

            string[] careers = {
                "Security Guard - $800/week",
                "Taxi Driver - $600/week",
                "Mechanic - $1200/week",
                "Real Estate Agent - $2000/week",
                "Business Owner - $5000/week"
            };

            int optionY = menuY + 80;
            for (int i = 0; i < careers.Length; i++)
            {
                bool isSelected = (selectedSubIndex == i);
                DrawMenuOption(careers[i], menuX + 30, optionY, isSelected, i);
                optionY += 75;
            }

            // Financial info panel
            DrawFinancialPanel(menuX + menuWidth - 200, menuY + 80);

            DrawFooter(menuX, menuY + menuHeight - 40, menuWidth);
        }

        private void DrawFamilyMenu()
        {
            const int menuX = 50;
            const int menuY = 100;
            const int menuWidth = 600;
            const int menuHeight = 500;

            DrawMenuBackground(menuX, menuY, menuWidth, menuHeight);
            DrawTitle("FAMILY & CHILDREN", menuX + 20, menuY + 20);

            string[] familyOptions = {
                "Have a Child",
                "Spend Time with Family",
                "Discipline Children",
                "View Children Info",
                "Family Happiness: --/100"
            };

            int optionY = menuY + 80;
            for (int i = 0; i < familyOptions.Length; i++)
            {
                bool isSelected = (selectedSubIndex == i);
                Color textColor = (i == 4) ? SUCCESS : TEXT_PRIMARY;
                DrawMenuOption(familyOptions[i], menuX + 30, optionY, isSelected, i);
                optionY += 75;
            }

            DrawFooter(menuX, menuY + menuHeight - 40, menuWidth);
        }

        private void DrawLifeStatusMenu()
        {
            const int menuX = 50;
            const int menuY = 100;
            const int menuWidth = 600;
            const int menuHeight = 500;

            DrawMenuBackground(menuX, menuY, menuWidth, menuHeight);
            DrawTitle("LIFE STATUS OVERVIEW", menuX + 20, menuY + 20);

            // Draw status panels
            int panelY = menuY + 80;
            DrawStatusPanel("PLAYER STATS", menuX + 30, panelY);
            DrawStatusPanel("RELATIONSHIPS", menuX + 330, panelY);

            panelY += 150;
            DrawStatusPanel("FINANCIAL", menuX + 30, panelY);
            DrawStatusPanel("FAMILY", menuX + 330, panelY);

            DrawFooter(menuX, menuY + menuHeight - 40, menuWidth);
        }

        private void DrawSaveMenu()
        {
            const int menuX = 50;
            const int menuY = 100;
            const int menuWidth = 600;
            const int menuHeight = 500;

            DrawMenuBackground(menuX, menuY, menuWidth, menuHeight);
            DrawTitle("SAVE GAME", menuX + 20, menuY + 20);

            new UIText("Save Slot Name:", new PointF(menuX + 30, menuY + 80), 0.7f, TEXT_PRIMARY).Draw();
            DrawInputBox(menuX + 30, menuY + 110, 300, "Slot 1");

            new UIText("Confirm Save?", new PointF(menuX + 30, menuY + 160), 0.6f, TEXT_SECONDARY).Draw();
            DrawMenuOption("YES", menuX + 30, menuY + 190, selectedSubIndex == 0, 0);
            DrawMenuOption("NO", menuX + 220, menuY + 190, selectedSubIndex == 1, 1);

            DrawFooter(menuX, menuY + menuHeight - 40, menuWidth);
        }

        // Helper drawing methods
        private void DrawMenuBackground(int x, int y, int width, int height)
        {
            // Main background
            new Rectangle(x, y, width, height, BG_DARK).Draw();

            // Border
            new Rectangle(x, y, width, height, PRIMARY_COLOR, false, 3).Draw();

            // Top accent bar
            new Rectangle(x, y, width, 5, ACCENT_COLOR).Draw();

            // Bottom accent bar
            new Rectangle(x, y + height - 5, width, 5, ACCENT_COLOR).Draw();
        }

        private void DrawTitle(string title, int x, int y)
        {
            // Shadow effect
            new UIText(title, new PointF(x + 2, y + 2), 1.2f, Color.Black).Draw();

            // Main title
            new UIText(title, new PointF(x, y), 1.2f, ACCENT_COLOR).Draw();
        }

        private void DrawMenuOption(string text, int x, int y, bool isSelected, int index)
        {
            if (isSelected)
            {
                // Highlight background
                new Rectangle(x - 10, y - 5, 400, 35, Color.FromArgb(100, ACCENT_COLOR.R, ACCENT_COLOR.G, ACCENT_COLOR.B)).Draw();

                // Selection indicator
                new UIText("→ ", new PointF(x - 20, y), 0.7f, HIGHLIGHT).Draw();
                new UIText(text, new PointF(x, y), 0.7f, HIGHLIGHT).Draw();
            }
            else
            {
                new UIText(text, new PointF(x, y), 0.7f, TEXT_PRIMARY).Draw();
            }
        }

        private void DrawFooter(int x, int y, int width)
        {
            new Rectangle(x, y, width, 40, Color.FromArgb(150, SECONDARY_COLOR.R, SECONDARY_COLOR.G, SECONDARY_COLOR.B)).Draw();
            new UIText("Use ↑ ↓ to navigate | ENTER to select | ESC to close", new PointF(x + 20, y + 8), 0.5f, TEXT_SECONDARY).Draw();
        }

        private void DrawInfoPanel(int x, int y, int width, int height)
        {
            new Rectangle(x, y, width, height, BG_SEMI).Draw();
            new Rectangle(x, y, width, height, SECONDARY_COLOR, false, 2).Draw();
        }

        private void DrawStatusPanel(string title, int x, int y)
        {
            int width = 270;
            int height = 120;

            new Rectangle(x, y, width, height, BG_SEMI).Draw();
            new Rectangle(x, y, width, height, SECONDARY_COLOR, false, 2).Draw();

            new UIText(title, new PointF(x + 10, y + 5), 0.6f, ACCENT_COLOR).Draw();
            new UIText("━━━━━━━━━━━━━━━", new PointF(x + 10, y + 20), 0.4f, SECONDARY_COLOR).Draw();

            // Placeholder stats
            new UIText("Status: Active", new PointF(x + 10, y + 35), 0.5f, TEXT_SECONDARY).Draw();
            new UIText("Progress: 75%", new PointF(x + 10, y + 55), 0.5f, TEXT_SECONDARY).Draw();
            new UIText("Updated: Now", new PointF(x + 10, y + 75), 0.5f, TEXT_SECONDARY).Draw();
        }

        private void DrawFinancialPanel(int x, int y)
        {
            int width = 180;
            int height = 200;

            new Rectangle(x, y, width, height, BG_SEMI).Draw();
            new Rectangle(x, y, width, height, PRIMARY_COLOR, false, 2).Draw();

            new UIText("FINANCES", new PointF(x + 10, y + 5), 0.6f, ACCENT_COLOR).Draw();
            new UIText("━━━━━━━━", new PointF(x + 10, y + 20), 0.4f, PRIMARY_COLOR).Draw();

            new UIText("Balance:", new PointF(x + 10, y + 35), 0.5f, TEXT_SECONDARY).Draw();
            new UIText("$0", new PointF(x + 10, y + 50), 0.6f, SUCCESS).Draw();

            new UIText("Income:", new PointF(x + 10, y + 70), 0.5f, TEXT_SECONDARY).Draw();
            new UIText("$0/week", new PointF(x + 10, y + 85), 0.6f, SUCCESS).Draw();

            new UIText("Career:", new PointF(x + 10, y + 105), 0.5f, TEXT_SECONDARY).Draw();
            new UIText("None", new PointF(x + 10, y + 120), 0.5f, WARNING).Draw();
        }

        private void DrawInputBox(int x, int y, int width, string placeholder)
        {
            new Rectangle(x, y, width, 30, BG_SEMI).Draw();
            new Rectangle(x, y, width, 30, SECONDARY_COLOR, false, 2).Draw();
            new UIText(placeholder, new PointF(x + 10, y + 5), 0.5f, TEXT_SECONDARY).Draw();
        }

        private enum MenuScreen
        {
            Main,
            Relationships,
            Career,
            Family,
            LifeStatus,
            Save
        }
    }
}
