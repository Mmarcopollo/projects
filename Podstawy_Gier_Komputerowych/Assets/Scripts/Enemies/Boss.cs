using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Boss : Enemy
{
    public Mutation Choice1;
    public Mutation Choice2;
    protected MutationChoice MutationChoice;

    public Boss()
    {
        SetMutationChoices();
    }

    protected abstract void SetMutationChoices();


    protected void UnlockMutation()
    {
        MutationChoice.GetComponent<MutationChoice>().mutationLocked = false;
    }

    public void SetMutationChoice(MutationChoice mutationChoice)
    {
        this.MutationChoice = mutationChoice;
    }
}
