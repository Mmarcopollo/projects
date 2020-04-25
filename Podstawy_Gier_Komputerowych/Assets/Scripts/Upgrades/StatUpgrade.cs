using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatUpgrade : Upgrade
{
    public int EnergyDecrement = 0;
    public float Speed = 0;
    public int MaxHp = 0;
    public int Strength = 0;
    public int BonusCollTime = 0;

    public StatUpgrade(string name, string description, int cost, List<Upgrade> upgradesGettingUnlocked, bool isUnlocked) 
        : base(name, description, cost, upgradesGettingUnlocked, isUnlocked)
    {
        
    }

    public override void Initialize()
    {
        base.Initialize();
        PlayerBacteria player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBacteria>();

        player.EnergyDecrement += EnergyDecrement;
        player.Speed *= 1 + Speed;
        player.MaxHp += MaxHp;
        player.Strength += Strength;
        player.BonusCollectibleTime += BonusCollTime;
        player.ValidateStats();

    }
}
