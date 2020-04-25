using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningEnemy : Enemy
{


    private float timeBtwSpawn;
    public float startTimeBtwSpawn = 1.5f;
    public float spawnDistance;

    //private Transform player;
    public GameObject basicEnemyPrefab;

    // Use this for initialization
    protected override void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player").transform;
        base.Start();
        IsHurtingOnCollision = false;
        timeBtwSpawn = startTimeBtwSpawn;
    }


    protected override void Move()
    {
        float distanceFromPlayer = Vector2.Distance(transform.position, Player.transform.position);

        if (distanceFromPlayer > spawnDistance)
        {
            MoveRandomly();
        }
        else if (Player.GetComponent<PlayerBacteria>().isVisible)
        {
            if (timeBtwSpawn <= 0)
            {
                GameObject spawnedEnemy = (GameObject)Instantiate(basicEnemyPrefab, transform.position, Quaternion.identity);
                spawnedEnemy.GetComponent<Enemy>().Player = Player;
                spawnedEnemy.transform.parent = this.transform.parent;
                MoveRandomly();
                timeBtwSpawn = startTimeBtwSpawn;
            }
            else
            {
                timeBtwSpawn -= Time.deltaTime;
            }
        }

    }

    private void MoveRandomly()
    {
        float moveHorizontal = Random.Range(-1.0f, 1.0f);
        float moveVertical = Random.Range(-1.0f, 1.0f);

        //Use the two store floats to create a new Vector2 variable movement.
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        Rb2D.AddForce(movement * Speed);
    }


}
