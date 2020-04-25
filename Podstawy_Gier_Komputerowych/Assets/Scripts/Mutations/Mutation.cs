public abstract class Mutation
{
    public string Name;
    public string Description;
    public bool IsUnlocked;

    protected Mutation(string name, string description, bool isUnlocked)
    {
        Name = name;
        Description = description;
        IsUnlocked = isUnlocked;
    }

    public abstract void Mutate(PlayerBacteria player);

    public abstract void UpgradeMutation();
}
