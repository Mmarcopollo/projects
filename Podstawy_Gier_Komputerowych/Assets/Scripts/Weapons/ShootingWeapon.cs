using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingWeapon : Weapon
{
    public int EnergyCost = 10;
    public float damage = 5;
    public float range;
    public float speed;
    public float timeBetweenShots;
    public bool doubleShooting = false;
    public GameObject bulletPrefab;

    void Start()
    {
        bulletPrefab = Resources.Load<GameObject>("BulletPrefab");
    }

    public override void Attack(PlayerBacteria player)
    {
        player.IsAttacking = true;
    }

    private void SpawnBullet(PlayerBacteria player)
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - player.transform.position;
        direction.z = 0;
        direction = direction.normalized;

        if (doubleShooting)
        {
            Vector3 perpendicularDirection = Vector2.Perpendicular(direction);
            Vector3 position1 = player.transform.position - perpendicularDirection/3;
            Vector3 position2 = player.transform.position + perpendicularDirection/3;
            Bullet bullet1 = Instantiate(bulletPrefab, position1, Quaternion.identity).GetComponent<Bullet>();
            Bullet bullet2 = Instantiate(bulletPrefab, position2, Quaternion.identity).GetComponent<Bullet>();
            SetBulletParameters(bullet1, direction);
            SetBulletParameters(bullet2, direction);
        }
        else
        {
            Bullet bullet = Instantiate(bulletPrefab, player.transform.position, Quaternion.identity).GetComponent<Bullet>();
            SetBulletParameters(bullet, direction);
        }
    }

    private void SetBulletParameters(Bullet bullet, Vector3 direction)
    {
        bullet.target = "Enemy";
        bullet.damage = damage;
        bullet.targetDirection = direction;
        bullet.range = range;
        bullet.speed = speed;
    }

    public override IEnumerator AttackCheck(PlayerBacteria player, KeyCode keycode)
    {
        for (; ; )
        {
            if (!Input.GetKey(keycode) || player.CurrentEnergy <= EnergyCost)
            {
                player.IsAttacking = false;
                yield break;
            }
            else
            {
                SpawnBullet(player);
                player.ChangeEnergy(-EnergyCost);
                yield return new WaitForSeconds(timeBetweenShots);
            }
            yield return new WaitForSeconds(.1f);
        }
    }
}