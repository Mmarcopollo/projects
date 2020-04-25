using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProgression
{
    public static int score = 1000;
    public int keyScore;

    public static List<Upgrade> upgradesAvailable;

    static PlayerProgression()
    {
        Initiate();
    }

    public static void Initiate()
    {
        //score = 1000;

        upgradesAvailable = new List<Upgrade>();
        List<Upgrade> nextLevelUpgrades = new List<Upgrade>();
        StatUpgrade statUpgrade = new StatUpgrade("More strength", "Increases strength by 2", 300, new List<Upgrade>(), false);
        statUpgrade.Strength = 2;
        nextLevelUpgrades.Add(statUpgrade);
        statUpgrade = new StatUpgrade("Strength", "Increases strength by 1", 100, nextLevelUpgrades, false);
        statUpgrade.Strength = 1;
        upgradesAvailable.Add(statUpgrade);

        MutationUpgrade mutationUpgrade;
        nextLevelUpgrades = new List<Upgrade>();
        mutationUpgrade = new MutationUpgrade("Demon of speed", "increases amout of Strength given by Dangerous Speed", 400, new List<Upgrade>(), false);
        mutationUpgrade.mutationsToUpgrade.Add("Dangerous Speed");
        nextLevelUpgrades.Add(mutationUpgrade);
        
        statUpgrade = new StatUpgrade("More speed", "Increases speed by 50%", 300, nextLevelUpgrades, false);
        statUpgrade.Speed = 0.5f;
        nextLevelUpgrades = new List<Upgrade>();
        nextLevelUpgrades.Add(statUpgrade);

        statUpgrade = new StatUpgrade("Speed", "Increases speed by 20%", 100, nextLevelUpgrades, false);
        statUpgrade.Speed = 0.2f;
        upgradesAvailable.Add(statUpgrade);
        
        nextLevelUpgrades = new List<Upgrade>();
        statUpgrade = new StatUpgrade("More health", "Increases health by 40", 300, new List<Upgrade>(), false);
        statUpgrade.MaxHp = 20;
        nextLevelUpgrades.Add(statUpgrade);
        statUpgrade = new StatUpgrade("Health", "Increases health by 20", 100, nextLevelUpgrades, false);
        statUpgrade.MaxHp = 10;
        upgradesAvailable.Add(statUpgrade);

        nextLevelUpgrades = new List<Upgrade>();
        mutationUpgrade = new MutationUpgrade("Upgraded digesting", "increases positive and decreses negative bonuses from digesting mutations", 500, new List<Upgrade>(), false);
        mutationUpgrade.mutationsToUpgrade.AddRange(new string[] { "Protein digesting", "Bacteria digesting" });
        nextLevelUpgrades.Add(mutationUpgrade);

        MutationUnlockUpgrade unlockUpgrade;
        unlockUpgrade = new MutationUnlockUpgrade("Specialized digesting", "unlocks protein and bacteria digesting", 300, nextLevelUpgrades, false);
        NutritionMutation mutation;
        mutation = new NutritionMutation("Protein digesting", "increases energy given by food but decreases energy given by enemies", true);
        mutation.foodNutritionModificator = 1.2f;
        mutation.bacteriaNutritionModificator = 0.8f;
        unlockUpgrade.mutations.Add(mutation);
        mutation = new NutritionMutation("Bacteria digesting", "increases energy given by enemies but decreases energy given by food", true);
        mutation.foodNutritionModificator = 0.8f;
        mutation.bacteriaNutritionModificator = 1.2f;
        unlockUpgrade.mutations.Add(mutation);
        upgradesAvailable.Add(unlockUpgrade);

        mutationUpgrade = new MutationUpgrade("Peaceful attitude", "increases bonuses given by Streamlined shape and Energy management", 500, new List<Upgrade>(), false);
        mutationUpgrade.mutationsToUpgrade.AddRange(new string[] { "Streamlined shape", "Energy management" });
        upgradesAvailable.Add(mutationUpgrade);

        mutationUpgrade = new MutationUpgrade("Battle time", "increases bonuses given by Tougher cell wall and increases Strength", 500, new List<Upgrade>(), false);
        mutationUpgrade.mutationsToUpgrade.AddRange(new string[] { "increases Strength", "Tougher cell wall" });
        upgradesAvailable.Add(mutationUpgrade);

        mutationUpgrade = new MutationUpgrade("Aggression", "increases positive and negative bonuses given by Aggressive behaviour", 400, new List<Upgrade>(), false);
        mutationUpgrade.mutationsToUpgrade.Add("Aggressive behaviour");
        upgradesAvailable.Add(mutationUpgrade);

        mutationUpgrade = new MutationUpgrade("Flexible size", "increases effect of Gigantism and Dwarfism", 400, new List<Upgrade>(), false);
        mutationUpgrade.mutationsToUpgrade.AddRange(new string[] { "Gigantism", "Dwarfism" });
        upgradesAvailable.Add(mutationUpgrade);

        mutationUpgrade = new MutationUpgrade("Camouflage", "decreases duration needed to become invisible while having Invisibility", 400, new List<Upgrade>(), false);
        mutationUpgrade.mutationsToUpgrade.Add("Invisibility");
        upgradesAvailable.Add(mutationUpgrade);

        mutationUpgrade = new MutationUpgrade("Resurrection", "Increases amount of health regained after getting reborn", 400, new List<Upgrade>(), false);
        mutationUpgrade.mutationsToUpgrade.Add("Second Life");
        upgradesAvailable.Add(mutationUpgrade);

        mutationUpgrade = new MutationUpgrade("Close Combat", "Increases bonuses gained from Ergonomic Spikes Mutation", 400, new List<Upgrade>(), false);
        mutationUpgrade.mutationsToUpgrade.Add("Ergonomic Spikes");
        upgradesAvailable.Add(mutationUpgrade);

        mutationUpgrade = new MutationUpgrade("Double bullet", "Increases damage dealt by double shooting", 400, new List<Upgrade>(), false);
        mutationUpgrade.mutationsToUpgrade.Add("Double Shot");
        upgradesAvailable.Add(mutationUpgrade);

        mutationUpgrade = new MutationUpgrade("Anti Bullet Cell Wall", "Increases bonus given by Bullet Defense", 400, new List<Upgrade>(), false);
        mutationUpgrade.mutationsToUpgrade.Add("Bullet Defense");
        upgradesAvailable.Add(mutationUpgrade);



    }
    
}
