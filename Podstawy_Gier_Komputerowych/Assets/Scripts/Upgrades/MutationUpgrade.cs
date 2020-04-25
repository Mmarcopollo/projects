using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutationUpgrade : Upgrade
{
    public List<string> mutationsToUpgrade = new List<string>();

    public MutationUpgrade(string name, string description, int cost, List<Upgrade> upgradesGettingUnlocked, bool isUnlocked)
        : base(name, description, cost, upgradesGettingUnlocked, isUnlocked)
    {

    }

    public override void Initialize()
    {
        base.Initialize();
        MutationChoice mutationChoice = GameObject.Find("MutationsChoice").GetComponent<MutationChoice>();

        foreach(Mutation mutation in mutationChoice.Mutations)
        {
            if (mutationsToUpgrade.Contains(mutation.Name))
            {
                mutation.UpgradeMutation();
            }
        }
    }
}
