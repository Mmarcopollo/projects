using UnityEngine;

public class SizeMutation : Mutation
{
    public float AmountToScale = 0;
    public float Speed = 0;

    public float upgradeBonus = 1;

    public SizeMutation(string name, string description, bool isUnlocked) : base(name, description, isUnlocked)
    {

    }

    public override void Mutate(PlayerBacteria player)
    {
        Transform temp = player.GetComponent<Transform>();
        temp.localScale *= AmountToScale;
        player.Speed += Speed;
    }

    public override void UpgradeMutation()
    {
        //Name += " +";
        Speed *= upgradeBonus;
        AmountToScale *= upgradeBonus;
    }
}
