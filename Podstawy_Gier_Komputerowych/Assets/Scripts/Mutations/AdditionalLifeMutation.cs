using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalLifeMutation : Mutation
{
    public float ressurectionHealthFraction;

    public AdditionalLifeMutation(string name, string description, bool isUnlocked, float ressurectionHealthFraction) : base(name, description, isUnlocked)
    {
        this.ressurectionHealthFraction = ressurectionHealthFraction;
    }

    public override void Mutate(PlayerBacteria player)
    {
        player.hasSecondLife = true;
        player.ressurectionHealthFraction = ressurectionHealthFraction;
    }

    public override void UpgradeMutation()
    {
        //Name += " +";
        ressurectionHealthFraction*=2;
        if (ressurectionHealthFraction > 1) ressurectionHealthFraction = 1;
    }
}
