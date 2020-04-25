using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradedSpikesMutation : Mutation
{
    public int energyDecrementChange;
    public int spikesDamageChange;

    public UpgradedSpikesMutation(string name, string description, bool isUnlocked) : base(name, description, isUnlocked)
    {

    }

    public override void Mutate(PlayerBacteria player)
    {
        foreach(Weapon weapon in player.CurrentWeapons)
        {
            if(weapon.Name.Equals("Spikes"))
            {
                SpikesWeapon spikes = (SpikesWeapon)weapon;
                spikes.additionalDamage += spikesDamageChange;
                spikes.additionalEnergyDecrement += spikesDamageChange;
                if (spikes.additionalEnergyDecrement < 0) spikes.additionalEnergyDecrement = 0;
            }
        }
    }

    public override void UpgradeMutation()
    {
        //Name += " +";
        energyDecrementChange *= 2;
        spikesDamageChange *= 2;
    }
}
