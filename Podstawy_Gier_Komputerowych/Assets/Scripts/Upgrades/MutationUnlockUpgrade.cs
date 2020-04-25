using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutationUnlockUpgrade : Upgrade
{
    public List<Mutation> mutations = new List<Mutation>();

    public MutationUnlockUpgrade(string name, string description, int cost, List<Upgrade> upgradesGettingUnlocked, bool isUnlocked)
        : base(name, description, cost, upgradesGettingUnlocked, isUnlocked)
    {

    }

    public override void Initialize()
    {
        base.Initialize();
        MutationChoice mutationChoice = GameObject.Find("MutationsChoice").GetComponent<MutationChoice>();

        mutationChoice.Mutations.AddRange(mutations);
    }
}
