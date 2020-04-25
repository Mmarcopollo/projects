using UnityEngine;

public class StyleMutation : Mutation
{
    public int bonusPoints;

    public StyleMutation(string name, string description, bool isUnlocked, int bonusPoints) : base(name, description, isUnlocked)
    {
        this.bonusPoints = bonusPoints;
    }

    public override void Mutate(PlayerBacteria player)
    {
        StageGenerator stageGenerator = GameObject.Find("StageGenerator").GetComponent<StageGenerator>();
        stageGenerator.StagePrizeBonus = bonusPoints;
    }

    public override void UpgradeMutation()
    {
        //Name += " +";
        bonusPoints *= 2;
    }
}
