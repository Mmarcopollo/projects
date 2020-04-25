public class UpgradedShootingMutation : Mutation
{
    public float bonusRange = 0;
    public float bonusDamage = 0;
    public int energyCostChange = 0;
    public float timeBetweenShotsBonus = 1;

    public UpgradedShootingMutation(string name, string description, bool isUnlocked) : base(name, description, isUnlocked)
    {

    }

    public override void Mutate(PlayerBacteria player)
    {
        foreach (Weapon weapon in player.CurrentWeapons)
        {
            if (weapon.Name.Equals("Bullets"))
            {
                ShootingWeapon shootingWeapon = (ShootingWeapon)weapon;
                shootingWeapon.damage += bonusDamage;
                shootingWeapon.range += bonusRange;
                shootingWeapon.EnergyCost += energyCostChange;
                shootingWeapon.timeBetweenShots *= timeBetweenShotsBonus;
                if (shootingWeapon.EnergyCost < 0) shootingWeapon.EnergyCost = 0;
            }
        }
    }

    public override void UpgradeMutation()
    {
        //Name += " +";
        bonusRange *= 2;
        bonusDamage *= 2;
        energyCostChange *= 2;
    }
}
