using System.Collections;
using UnityEngine;

public class SeparatingBoss : Boss
{
    public float ChargingSpeed;         //Speed while charging
    public float ChargingTime;          //Time spent charging
    public float AimingTime;            //Time spent aiming
    public float PlayerRecoilWhenCharging;
    public float afterSpawnDamageResistanceDuration;
    public int numberOfSeparations;
    public float bulletDamage;
    public float timeBetweenShots;
    public float bulletRange;
    public GameObject bulletPrefab;
    public float waitingPhaseDuration;
    public float numberOfChargesDuringChargingPhase;
    public int numberOfShotsDuringShootingPhase;
    public static int childrenLeftToKill;

    public Sprite WithoutAttack;
    public Sprite WithAttack;

    private bool _attackPhase = false;       //Is enemy charging
    private bool _isItOriginal = true;       //Is object an original or spawned after death copy
    private float originalPlayerRecoil;



    protected override void Start()
    {
        base.Start();
        originalPlayerRecoil = PlayerRecoil;
        if (_isItOriginal)
        {
            int result = 0;
            for(int i=0, j=1; i<numberOfSeparations; i++, j*=2)
            {
                result += j;
            }
            childrenLeftToKill = result;
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
    
    private void SpawnBullet(Vector2 direction)
    {
        Bullet bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity).GetComponent<Bullet>();
        bullet.damage = bulletDamage;
        bullet.targetDirection = direction;
        bullet.range = bulletRange;
    }

    private IEnumerator Wait()
    {
        _attackPhase = true;
        yield return new WaitForSeconds(waitingPhaseDuration);
        _attackPhase = false;
    }

    
    public void StartResistingDamage()
    {
        StartCoroutine(ResistDamage());
    }

    private IEnumerator ResistDamage()
    {
        _attackPhase = true;
        isDamageResistant = true;
        yield return new WaitForSeconds(afterSpawnDamageResistanceDuration);
        isDamageResistant = false;
        _attackPhase = false;
    }
    

    private IEnumerator Charge()
    {
        _attackPhase = true;

        for (int i=0; i<numberOfChargesDuringChargingPhase; i++)
        {
            yield return new WaitForSeconds(AimingTime);
            this.GetComponent<SpriteRenderer>().sprite = WithAttack;
            IsHurtingOnCollision = true;
            PlayerRecoil = PlayerRecoilWhenCharging;
            Vector2 movement = CalculateAfterThePlayerMovement();
            Rb2D.AddForce(movement * ChargingSpeed);
            yield return new WaitForSeconds(ChargingTime);
            this.GetComponent<SpriteRenderer>().sprite = WithoutAttack;
            PlayerRecoil = originalPlayerRecoil;
            //this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);

            IsHurtingOnCollision = false;
        }

        _attackPhase = false;
    }

    private IEnumerator Shoot()
    {
        _attackPhase = true;
        for (int i=0; i<numberOfShotsDuringShootingPhase; i++)
        {
            SpawnBullet(new Vector2(1, 1));
            SpawnBullet(new Vector2(-1, 1));
            SpawnBullet(new Vector2(1, -1));
            SpawnBullet(new Vector2(-1, -1));
            yield return new WaitForSeconds(timeBetweenShots / 2);
            SpawnBullet(new Vector2(0, 1));
            SpawnBullet(new Vector2(1, 0));
            SpawnBullet(new Vector2(0, -1));
            SpawnBullet(new Vector2(-1, 0));
            yield return new WaitForSeconds(timeBetweenShots / 2);

        }
        _attackPhase = false;
    }
    
    protected override void Move()
    {
        MoveRandomly();
        if (!_attackPhase)
        {
            int phaseChoice = Random.Range(0, 3);
            if (phaseChoice == 0) StartCoroutine(Shoot());
            else if (phaseChoice == 1) StartCoroutine(Wait());
            else if(Player.GetComponent<PlayerBacteria>().isVisible) StartCoroutine(Charge());
        }

    }

    private void SpawnChild()
    {
        GameObject childGO = (GameObject)Instantiate(Resources.Load("SeparatingBossPrefab"), transform.position, Quaternion.identity);
        childGO.transform.SetParent(GameObject.Find("StageGenerator").transform);
        childGO.transform.localScale = new Vector3(transform.localScale.x / 2, transform.localScale.y/2, 1);
        SeparatingBoss child = childGO.GetComponent<SeparatingBoss>();
        child.numberOfSeparations = numberOfSeparations - 1;
        child.Player = Player;
        child.MutationChoice = MutationChoice;
        child.MaxHp = MaxHp / 2;
        //child.Strength = Strength / 2;
        //child.bulletDamage = bulletDamage / 2;
        child._isItOriginal = false;
        child.StartResistingDamage();
    }

    protected override void ActOnDeath()
    {
        childrenLeftToKill--;
        if (numberOfSeparations > 1)
        {
            SpawnChild();
            SpawnChild();
        }
        else if (childrenLeftToKill == 0)
        {
            for (int i = 0; i < NumberOfDrops; i++) DropFood(EnergyDrop);
            UnlockMutation();
        }
        Destroy(this.gameObject);
        Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
    }

    protected override void SetMutationChoices()
    {

        AdditionalLifeMutation mutation1 = new AdditionalLifeMutation("Second Life", "You will get reborn next time you die", true, 0.5f);
        foreach (Upgrade upgrade in PlayerProgression.upgradesAvailable.ToArray())
        {
            if (upgrade.isUnlocked && upgrade.name.Equals("Ressurection")) mutation1.UpgradeMutation();
        }
        Choice1 = mutation1;

        UpgradedSpikesMutation mutation2 = new UpgradedSpikesMutation("Ergonomic Spikes", "Your spikes deal more damage and cost less energy", true);
        mutation2.spikesDamageChange = 2;
        mutation2.energyDecrementChange = -5;
        foreach (Upgrade upgrade in PlayerProgression.upgradesAvailable.ToArray())
        {
            if (upgrade.isUnlocked && upgrade.name.Equals("Close Combat")) mutation2.UpgradeMutation();
        }
        Choice2 = mutation2;
    }
}
