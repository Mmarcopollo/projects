using System.Collections;
using UnityEngine;

public class ChargingEnemy : Enemy
{
    public float AlertRange;            //Distance from which Player is being chased 
    public float PlayerRecoilWhenCharging;          //Recoil of Player
    public float ChargingSpeed;         //Speed while charging
    public float ChargingTime;          //Time spent charging
    public float AimingTime;            //Time spent aiming

    public Sprite WithoutAttack;
    public Sprite WithAttack;

    private float originalPlayerRecoil;

    private bool _isCharging = false;    //Is enemy charging

    private void MoveRandomly()
    {
        float moveHorizontal = Random.Range(-1.0f, 1.0f);
        float moveVertical = Random.Range(-1.0f, 1.0f);

        //Use the two store floats to create a new Vector2 variable movement.
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        Rb2D.AddForce(movement * Speed);
    }

    private IEnumerator Charge()
    {
        _isCharging = true;

        yield return new WaitForSeconds(AimingTime);
        this.GetComponent<SpriteRenderer>().sprite = WithAttack;
        PlayerRecoil = PlayerRecoilWhenCharging;

        IsHurtingOnCollision = true;
        Vector2 movement = CalculateAfterThePlayerMovement();
        Rb2D.AddForce(movement * ChargingSpeed);
        yield return new WaitForSeconds(ChargingTime);

        IsHurtingOnCollision = false;
        _isCharging = false;
        PlayerRecoil = originalPlayerRecoil;
        this.GetComponent<SpriteRenderer>().sprite = WithoutAttack;
        //this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        yield return null;
    }

    protected override void Start()
    {
        base.Start();
        IsHurtingOnCollision = false;
        originalPlayerRecoil = PlayerRecoil;
    }

    protected override void Move()
    {
        if (!_isCharging)
        {
            float distanceFromPlayer = Vector2.Distance(Rb2D.position, Player.transform.position);
            if (distanceFromPlayer > AlertRange)
                MoveRandomly();
            else if (Player.GetComponent<PlayerBacteria>().isVisible)
                StartCoroutine(Charge());
        }
    }
}
