using System;
using System.Collections.Generic;

namespace GrandLifeAdventures.Systems
{
    public class FamilySystem
    {
        public Child[] Children { get; private set; }
        public int MaxChildren = 4;
        public int ChildCount { get; private set; }
        public float FamilyHappiness { get; set; }

        private const float HAPPINESS_DECAY = 0.1f;
        private const float HAPPINESS_MAX = 100f;

        public FamilySystem()
        {
            Children = new Child[MaxChildren];
            ChildCount = 0;
            FamilyHappiness = 50f;
        }

        public void Update()
        {
            // Decay family happiness if not maintained
            FamilyHappiness = Math.Max(0, FamilyHappiness - HAPPINESS_DECAY);

            // Update all children
            for (int i = 0; i < ChildCount; i++)
            {
                if (Children[i] != null)
                {
                    Children[i].Update();
                }
            }
        }

        public bool HaveChild(string childName, int age = 0)
        {
            if (ChildCount >= MaxChildren)
            {
                return false; // Maximum children reached
            }

            Children[ChildCount] = new Child
            {
                Name = childName,
                Age = age,
                BirthDate = DateTime.Now,
                Happiness = 50f,
                Discipline = 50f
            };

            ChildCount++;
            FamilyHappiness += 20f;
            return true;
        }

        public void SpendTimeWithChildren(float happinessIncrease)
        {
            FamilyHappiness = Math.Min(HAPPINESS_MAX, FamilyHappiness + happinessIncrease);
            
            for (int i = 0; i < ChildCount; i++)
            {
                if (Children[i] != null)
                {
                    Children[i].Happiness = Math.Min(100, Children[i].Happiness + (happinessIncrease * 0.75f));
                }
            }
        }

        public void DisciplineChild(int childIndex, float amount)
        {
            if (childIndex >= 0 && childIndex < ChildCount && Children[childIndex] != null)
            {
                Children[childIndex].Discipline = Math.Min(100, Children[childIndex].Discipline + amount);
            }
        }
    }

    public class Child
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime BirthDate { get; set; }
        public float Happiness { get; set; }
        public float Discipline { get; set; }
        public string[] Skills { get; set; }

        public void Update()
        {
            // Age the child
            Age = (int)(DateTime.Now - BirthDate).TotalDays / 365;

            // Decay happiness
            Happiness = Math.Max(0, Happiness - 0.5f);
        }
    }
}
