using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibilityMutation : Mutation
{
    public float idleDuration;

    public float upgradeBonus = 1;

    public InvisibilityMutation(string name, string description, bool isUnlocked, float idleDuration) : base(name, description, isUnlocked)
    {
        this.idleDuration = idleDuration;
    }

    public override void Mutate(PlayerBacteria player)
    {
        player.canBeInvisible = true;
        player.idleDurationForInvisibility = idleDuration;
    }

    public override void UpgradeMutation()
    {
        //Name += " +";
        idleDuration /= 2;
    }
}
