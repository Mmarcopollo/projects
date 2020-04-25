using System.Collections;
using UnityEngine;

public class SpikesWeapon : Weapon
{
    public int additionalDamage = 5;
    public int additionalEnergyDecrement = 15;

    void Start()
    {
    }

    public override void Attack(PlayerBacteria player)
    {
        player.IsAttacking = true;
        player.Strength += additionalDamage;
        player.EnergyDecrement += additionalEnergyDecrement;
        player.GetComponent<SpriteRenderer>().sprite = CurrentWeaponSprite;
    }

    public override IEnumerator AttackCheck(PlayerBacteria player, KeyCode keycode)
    {
        for (; ; )
        {
            if (!Input.GetKey(keycode) || player.CurrentEnergy <= 0)
            {
                player.GetComponent<SpriteRenderer>().sprite = player.WithoutAttack;
                player.Strength -= additionalDamage;
                player.EnergyDecrement -= additionalEnergyDecrement;
                player.IsAttacking = false;
                yield break;
            }
            yield return new WaitForSeconds(.1f);
        }
    }
}
