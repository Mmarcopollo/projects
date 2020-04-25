using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NutritionMutation : Mutation
{
    public float foodNutritionModificator = 1;
    public float bacteriaNutritionModificator = 1;

    public float upgradeBonus = 1;

    public NutritionMutation(string name, string description, bool isUnlocked) : base(name, description, isUnlocked)
    {

    }

    public override void Mutate(PlayerBacteria player)
    {
        StageGenerator stageGenerator = GameObject.Find("StageGenerator").GetComponent<StageGenerator>();
        stageGenerator.foodNutritionModificator *= foodNutritionModificator;
        stageGenerator.bacteriaNutritionModificator *= bacteriaNutritionModificator;
}

    public override void UpgradeMutation()
    {
        //Name += " +";
        foodNutritionModificator--;
        foodNutritionModificator *= upgradeBonus;
        foodNutritionModificator++;
        bacteriaNutritionModificator--;
        bacteriaNutritionModificator *= upgradeBonus;
        bacteriaNutritionModificator++;
    }
}
