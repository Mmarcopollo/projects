public class StatMutation : Mutation
{
    public int EnergyDecrement = 0;
    public float Speed = 0;
    public int MaxHp = 0;
    public int Strength = 0;
    public int BonusCollTime = 0;
    public float upgradeBonus = 1;

    public StatMutation(string name, string description, bool isUnlocked) : base(name, description, isUnlocked)
    {

    }

    public override void Mutate(PlayerBacteria player)
    {
        player.EnergyDecrement += EnergyDecrement;
        player.Speed *= 1 + Speed;
        player.MaxHp += MaxHp;
        player.Strength += Strength;
        player.BonusCollectibleTime += BonusCollTime;
        player.ValidateStats();
    }

    public override void UpgradeMutation()
    {
        //Name += " +";
        EnergyDecrement = (int)(EnergyDecrement * upgradeBonus);
        Speed *= upgradeBonus;
        MaxHp = (int)(EnergyDecrement * upgradeBonus);
        Strength = (int)(Strength * upgradeBonus);
        BonusCollTime = (int)(BonusCollTime * upgradeBonus);
    }
}
