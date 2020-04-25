using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : Enemy
{

    public float shootDistance;    //Distance from which Player is being chased 
    public float bulletDamage;
    public float bulletRange;
    
    public GameObject bulletPrefab;
    public Transform player;

    private float timeBtwwShoots;
    public float startTimeBtwShoots=1.5f;

    private void MoveRandomly()
    {
        float moveHorizontal = Random.Range(-1.0f, 1.0f);
        float moveVertical = Random.Range(-1.0f, 1.0f);

        //Use the two store floats to create a new Vector2 variable movement.
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        Rb2D.AddForce(movement * Speed);
    }

    protected override void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; 
        base.Start();
        IsHurtingOnCollision = false;
        timeBtwwShoots = startTimeBtwShoots;
    }

    protected override void Move()
    {

         float distanceFromPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceFromPlayer > shootDistance)
        {
            MoveRandomly();
        }
        else if (Player.GetComponent<PlayerBacteria>().isVisible)
        {
            if(timeBtwwShoots <= 0)
            {
                Bullet bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity).GetComponent<Bullet>();
                bullet.damage = bulletDamage;
                bullet.targetDirection = (player.transform.position - transform.position).normalized;
                bullet.range = bulletRange;
                MoveRandomly();
                timeBtwwShoots = startTimeBtwShoots;
            }
            else
            {

                timeBtwwShoots -= Time.deltaTime;
            }
        }
      

    }
    
}
