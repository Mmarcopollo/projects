using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExplodingEnemy : Enemy
{

    public float AlertRange;        //Distance from which Player is being chased 
    public int timeInAttackStage;
    public float ExplosionDamage;
    public Image HealthBar;
    private bool isInAttackStage = false;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        base.Start();
    }

    protected override void Move()
    {
        float distanceFromPlayer = Vector2.Distance(Rb2D.position, Player.transform.position);
        if (distanceFromPlayer < AlertRange && !isInAttackStage && Player.GetComponent<PlayerBacteria>().isVisible)
        {
            isInAttackStage = true;
        }
        else if (!isInAttackStage)
        {
            Vector2 movement = calculateRandomMovement();
            Rb2D.AddForce(movement * Speed);
        }

        if (isInAttackStage)
        {
            HealthBar.color = new Color(255, 0 , 0);
            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        yield return new WaitForSeconds(timeInAttackStage);
        float distanceFromPlayer = Vector2.Distance(Rb2D.position, Player.transform.position);
        if (distanceFromPlayer < AlertRange && Player.GetComponent<PlayerBacteria>().isVisible)
        {
            PlayerBacteria player = FindObjectOfType<PlayerBacteria>();
            player.ChangeHealth(-ExplosionDamage);
        }
        DamageEnemy((int)CurrentHp);
    }
}
