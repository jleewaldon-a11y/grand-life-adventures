using System;
using System.Collections.Generic;
using GTA;

namespace GrandLifeAdventures.Systems
{
    public class RelationshipManager
    {
        public Dictionary<string, Relationship> Relationships { get; private set; }
        public Relationship CurrentRomance { get; set; }

        private const float RELATIONSHIP_DECAY_RATE = 0.05f;
        private const float MAX_RELATIONSHIP = 100f;

        public RelationshipManager()
        {
            Relationships = new Dictionary<string, Relationship>();
            InitializeRelationships();
        }

        private void InitializeRelationships()
        {
            // Initialize available romance options
            Relationships.Add("Amanda", new Relationship { Name = "Amanda", Affection = 0, Status = RelationshipStatus.Single });
            Relationships.Add("Tracey", new Relationship { Name = "Tracey", Affection = 0, Status = RelationshipStatus.Single });
            Relationships.Add("Kate", new Relationship { Name = "Kate", Affection = 0, Status = RelationshipStatus.Single });
            Relationships.Add("Michelle", new Relationship { Name = "Michelle", Affection = 0, Status = RelationshipStatus.Single });
        }

        public void Update()
        {
            // Decay relationships over time if not maintained
            foreach (var relationship in Relationships.Values)
            {
                if (relationship.Status == RelationshipStatus.Dating || relationship.Status == RelationshipStatus.Married)
                {
                    relationship.Affection = Math.Max(0, relationship.Affection - RELATIONSHIP_DECAY_RATE);
                }
            }
        }

        public void IncreasAffection(string characterName, float amount)
        {
            if (Relationships.ContainsKey(characterName))
            {
                Relationships[characterName].Affection = Math.Min(MAX_RELATIONSHIP, Relationships[characterName].Affection + amount);
            }
        }

        public void StartRelationship(string characterName)
        {
            if (Relationships.ContainsKey(characterName))
            {
                if (CurrentRomance != null && CurrentRomance.Status != RelationshipStatus.Single)
                {
                    CurrentRomance.Status = RelationshipStatus.Single;
                }
                CurrentRomance = Relationships[characterName];
                CurrentRomance.Status = RelationshipStatus.Dating;
                CurrentRomance.StartDate = DateTime.Now;
            }
        }

        public void Marry(string characterName)
        {
            if (Relationships.ContainsKey(characterName))
            {
                Relationships[characterName].Status = RelationshipStatus.Married;
                Relationships[characterName].MarriageDate = DateTime.Now;
            }
        }

        public void BreakUp(string characterName)
        {
            if (Relationships.ContainsKey(characterName))
            {
                Relationships[characterName].Status = RelationshipStatus.Single;
                Relationships[characterName].Affection = 0;
            }
        }
    }

    public class Relationship
    {
        public string Name { get; set; }
        public float Affection { get; set; }
        public RelationshipStatus Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime MarriageDate { get; set; }
    }

    public enum RelationshipStatus
    {
        Single,
        Dating,
        Engaged,
        Married,
        Divorced
    }
}
