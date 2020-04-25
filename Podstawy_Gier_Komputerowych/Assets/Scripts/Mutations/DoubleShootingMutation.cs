using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleShootingMutation : Mutation
{
    float bonusDamage = 0;

    public DoubleShootingMutation(string name, string description, bool isUnlocked) : base(name, description, isUnlocked)
    {
    }

    public override void Mutate(PlayerBacteria player)
    {
        foreach(Weapon weapon in player.CurrentWeapons)
        {
            if (weapon.Name.Equals("Bullets"))
            {
                ShootingWeapon shootingWeapon = (ShootingWeapon)weapon;
                shootingWeapon.doubleShooting = true;
                shootingWeapon.damage += bonusDamage;
            }
        }
    }

    public override void UpgradeMutation()
    {
        //Name += " +";
        bonusDamage += 1;
    }
}

