using System.Collections;
using UnityEngine;

public class MinesWeapon : Weapon
{
    public int EnergyCost = 40;
    public GameObject bulletPrefab;

    private bool _shouldISendBullet = false;
    void Start()
    {
        bulletPrefab = Resources.Load<GameObject>("Weapons/MinePrefab");
    }

    public override void Attack(PlayerBacteria player)
    {
        if (player.CurrentEnergy >= 5)
        {
            player.IsAttacking = true;
            player.CurrentEnergy -= EnergyCost;
            _shouldISendBullet = true;
            //player.GetComponent<SpriteRenderer>().sprite = CurrentWeaponSprite;
        }
    }

    public override IEnumerator AttackCheck(PlayerBacteria player, KeyCode keycode)
    {
        for (; ; )
        {
            if (!Input.GetKey(keycode))
            {
                player.GetComponent<SpriteRenderer>().sprite = player.WithoutAttack;
                if (_shouldISendBullet)
                {
                    Instantiate(Resources.Load<GameObject>("Weapons/MinePrefab"), new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + 0.01f), Quaternion.identity);
                    _shouldISendBullet = false;
                }
                player.IsAttacking = false;
                yield break;
            }
            yield return new WaitForSeconds(.1f);
        }
    }
}