public class AddWeaponMutation : Mutation
{
    private Weapon WeaponToAdd;
    public AddWeaponMutation(string name, string description, bool isUnlocked, Weapon weaponToAdd) : base(name, description, isUnlocked)
    {
        WeaponToAdd = weaponToAdd;
    }

    public override void Mutate(PlayerBacteria player)
    {
        player.CurrentWeapons.Add(WeaponToAdd);
        this.IsUnlocked = false;
    }

    public override void UpgradeMutation()
    {

    }
}
