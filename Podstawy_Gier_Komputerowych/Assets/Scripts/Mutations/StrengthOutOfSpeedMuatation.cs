using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthOutOfSpeedMutation : Mutation
{
    public float bonusStrengthFactor;

    public float upgradeBonus = 1;

    public StrengthOutOfSpeedMutation(string name, string description, bool isUnlocked, float bonusStrengthFactor) : base(name, description, isUnlocked)
    {
        this.bonusStrengthFactor = bonusStrengthFactor;
    }

    public override void Mutate(PlayerBacteria player)
    {
        float bonusSpeed = player.Speed - player.baseSpeed;
        int bonusStrength = (int)(bonusSpeed * bonusStrengthFactor);
        Debug.Log(bonusStrength);
        player.Strength += bonusStrength;
    }

    public override void UpgradeMutation()
    {
        //Name += " +";
        bonusStrengthFactor *= 2;
    }
}
