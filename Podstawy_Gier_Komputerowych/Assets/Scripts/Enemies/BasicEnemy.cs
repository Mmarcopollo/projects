using UnityEngine;

public class BasicEnemy : Enemy
{
    public float AlertRange;        //Distance from which Player is being chased 

    protected override void Start()
    {
        //Player = GameObject.FindGameObjectWithTag("Player");
        base.Start();
        IsHurtingOnCollision = true;
    }

    protected override void Move()
    {
        Vector2 movement;
        float distanceFromPlayer = Vector2.Distance(Rb2D.position, Player.transform.position);
        if (Player.GetComponent<PlayerBacteria>().isVisible && distanceFromPlayer < AlertRange)
            movement = CalculateAfterThePlayerMovement();
        else movement = calculateRandomMovement();

        Rb2D.AddForce(movement * Speed);
    }
    /*
    protected override void OnCollisionEnter2D(Collision2D col)
    {
        base.OnCollisionEnter2D(col);
        if (col.gameObject == Player)
        {
            Vector2 movement = CalculateAfterThePlayerMovement();
            Rb2D.AddForce(-movement * Speed * HitRecoil);
        }
    }*/
}
