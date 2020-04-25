using UnityEngine;

public class ConfusionMutation : Mutation
{
    public ConfusionMutation(string name, string description, bool isUnlocked) : base(name, description, isUnlocked)
    {

    }

    public override void Mutate(PlayerBacteria player)
    {
        Object.FindObjectOfType<Camera>().projectionMatrix *= Matrix4x4.Scale(new Vector3(-1, 1, 1));
    }

    public override void UpgradeMutation()
    {

    }
}