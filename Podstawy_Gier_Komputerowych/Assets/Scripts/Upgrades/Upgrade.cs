using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Upgrade
{
    public int cost;
    public string name;
    public string description;
    public bool isUnlocked;
    public List<Upgrade> upgradesGettingUnlocked;

    protected Upgrade(string name, string description, int cost, List<Upgrade> upgradesGettingUnlocked, bool isUnlocked)
    {
        this.name = name;
        this.description = description;
        this.cost = cost;
        this.upgradesGettingUnlocked = upgradesGettingUnlocked;
        this.isUnlocked = isUnlocked;
    }

    public virtual void Initialize()
    {
        PlayerProgression.upgradesAvailable.AddRange(upgradesGettingUnlocked);
    }
}
