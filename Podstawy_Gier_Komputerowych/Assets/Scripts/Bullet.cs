using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed;
    public float damage = 5;
    public float range;
    public string target;   //player or enemy;
    public Vector2 targetDirection;

    private Vector2 initialPoint;

    private void Start()
    {
        initialPoint = new Vector2(transform.position.x, transform.position.y);
    }

    void Update()
    {
        Vector3 increment = (targetDirection * speed * Time.deltaTime);
        transform.position += increment;
        if(Vector2.Distance(transform.position, initialPoint)>range)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(target.Equals("Player") && other.CompareTag("Player"))
        {
            PlayerBacteria playerStats = other.GetComponent<PlayerBacteria>();
            float damageToDeal = damage - playerStats.defenseFromBullets;
            if (damageToDeal < 0) damageToDeal = 0;
            playerStats.ChangeHealth(-damageToDeal);
            Destroy(gameObject);
        }
        else if(target.Equals("Enemy") && other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.DamageEnemy((int)damage);
            Destroy(gameObject);
        }
    }
}
