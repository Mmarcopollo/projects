using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDefenseMutation : Mutation
{
    public float bulletDefense;

    public BulletDefenseMutation(string name, string description, bool isUnlocked, float bulletDefense) : base(name, description, isUnlocked)
    {
        this.bulletDefense = bulletDefense;
    }

    public override void Mutate(PlayerBacteria player)
    {
        player.defenseFromBullets = bulletDefense;
    }

    public override void UpgradeMutation()
    {
        //Name += " +";
        bulletDefense *= 2;
    }
}
