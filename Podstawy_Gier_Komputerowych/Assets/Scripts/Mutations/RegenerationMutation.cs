public class RegenerationMutation : Mutation
{
    public float regeneration;
    public float regenerationPeriod;

    public RegenerationMutation(string name, string description, bool isUnlocked) : base(name, description, isUnlocked)
    {

    }

    public override void Mutate(PlayerBacteria player)
    {
        player.regeneration = regeneration;
        player.regenerationPeriod = regenerationPeriod;
    }

    public override void UpgradeMutation()
    {
        //Name += " +";
        regeneration *= 2;
    }
}

