using System;
using System.Collections.Generic;

namespace GrandLifeAdventures.Systems
{
    public class CareerSystem
    {
        public Career CurrentCareer { get; set; }
        public Dictionary<string, Career> AvailableCareers { get; private set; }
        public float Money { get; set; }
        public int Experience { get; set; }

        private int daysSinceLastPaycheck = 0;
        private const int PAYCHECK_INTERVAL = 7; // Days

        public CareerSystem()
        {
            AvailableCareers = new Dictionary<string, Career>();
            Money = 5000f;
            Experience = 0;
            InitializeCareers();
        }

        private void InitializeCareers()
        {
            AvailableCareers.Add("Security Guard", new Career 
            { 
                Name = "Security Guard", 
                BaseSalary = 800f, 
                Prestige = 20,
                Description = "Protect property and assets. Entry-level security work."
            });

            AvailableCareers.Add("Taxi Driver", new Career 
            { 
                Name = "Taxi Driver", 
                BaseSalary = 600f, 
                Prestige = 15,
                Description = "Drive passengers around Los Santos for fare."
            });

            AvailableCareers.Add("Mechanic", new Career 
            { 
                Name = "Mechanic", 
                BaseSalary = 1200f, 
                Prestige = 40,
                Description = "Fix and upgrade vehicles. Requires some skill."
            });

            AvailableCareers.Add("Real Estate Agent", new Career 
            { 
                Name = "Real Estate Agent", 
                BaseSalary = 2000f, 
                Prestige = 60,
                Description = "Sell properties and manage investments."
            });

            AvailableCareers.Add("Business Owner", new Career 
            { 
                Name = "Business Owner", 
                BaseSalary = 5000f, 
                Prestige = 100,
                Description = "Own and operate your own business. High risk, high reward."
            });
        }

        public void Update()
        {
            daysSinceLastPaycheck++;

            if (daysSinceLastPaycheck >= PAYCHECK_INTERVAL && CurrentCareer != null)
            {
                ProcessPaycheck();
                daysSinceLastPaycheck = 0;
            }
        }

        private void ProcessPaycheck()
        {
            float salary = CurrentCareer.BaseSalary * (1 + (Experience / 100f));
            Money += salary;
        }

        public void SetCareer(string careerName)
        {
            if (AvailableCareers.ContainsKey(careerName))
            {
                CurrentCareer = AvailableCareers[careerName];
                Experience = 0; // Reset experience when changing careers
            }
        }

        public void AddExperience(int amount)
        {
            Experience = Math.Min(100, Experience + amount);
        }
    }

    public class Career
    {
        public string Name { get; set; }
        public float BaseSalary { get; set; }
        public int Prestige { get; set; }
        public string Description { get; set; }
    }
}
