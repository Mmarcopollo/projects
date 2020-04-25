using System.Collections;
using UnityEngine;

public class InvisibleBoss : Boss
{
    public float ChargingSpeed;         //Speed while charging
    public float ChargingTime;          //Time spent charging
    public float BeforeAtackWaitingTime;            //Time spent aiming
    public float AfterAtackWaitingTime;
    public float PlayerRecoilWhenCharging;
    public float TeleportingDistancFromPlayer;
    public float VisibilityTime;
    public float VisibilityChangingTime;

    public Sprite WithoutAttack;
    public Sprite WithAttack;

    private bool _attackPhase = false;       //Is enemy charging
    //private bool _isItOriginal = true;       //Is object an original or spawned after death copy
    private float originalPlayerRecoil;
    private float stageHeight;
    private float stageWidth;
    private SpriteRenderer spriteRenderer;
    private bool isChangingTransparency = false;



    protected override void Start()
    {
        base.Start();
        originalPlayerRecoil = PlayerRecoil;

        spriteRenderer = GetComponent<SpriteRenderer>();

        GameObject stageGenerator = GameObject.Find("StageGenerator");
        stageHeight = stageGenerator.GetComponent<StageGenerator>().GetCurrentStageHeight();
        stageWidth = stageGenerator.GetComponent<StageGenerator>().GetCurrentStageWidth();
        ChangeTransparency(0);
    }

    private void MoveRandomly()
    {
        float moveHorizontal = Random.Range(-1.0f, 1.0f);
        float moveVertical = Random.Range(-1.0f, 1.0f);

        //Use the two store floats to create a new Vector2 variable movement.
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        Rb2D.AddForce(movement * Speed);
    }

    private IEnumerator MakeVisible()
    {
        StartCoroutine(ChangeTransparencyGradually(1));
        yield return new WaitForSeconds(VisibilityTime);
        StartCoroutine(ChangeTransparencyGradually(0));
    }

    private void TeleportRandomly()
    {
        Vector3 randomPosition;
        do
        {
            float x = Random.Range(-stageHeight / 2, stageHeight / 2);
            float y = Random.Range(-stageWidth / 2, stageWidth / 2);
            randomPosition = new Vector3(x, y, this.transform.position.z);

        } while (Vector3.Distance(randomPosition, this.transform.position) < TeleportingDistancFromPlayer);
        Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
        this.transform.position = randomPosition;
    }

    private void ChangeTransparency(float transparency)
    {
        Color color = spriteRenderer.color;
        color.a = transparency;
        spriteRenderer.color = color;
        BloodFill.color = color;
    }

    private IEnumerator ChangeTransparencyGradually(float transparency)
    {
        if(!isChangingTransparency)
        {
            isChangingTransparency = true;
            int numberOfVisibilityChanges = 10;
            float currentTransparency = spriteRenderer.color.a;
            float transparencyIncrement = (transparency - currentTransparency) / numberOfVisibilityChanges;
            for (int i = 0; i < numberOfVisibilityChanges; i++)
            {
                yield return new WaitForSeconds(VisibilityChangingTime / numberOfVisibilityChanges);
                ChangeTransparency(currentTransparency + transparencyIncrement);
                currentTransparency = spriteRenderer.color.a;

            }
            isChangingTransparency = false;
        }
    }

    private IEnumerator Charge()
    {
        _attackPhase = true;
        SpriteRenderer spriteRenderer = this.GetComponent<SpriteRenderer>();
        yield return new WaitForSeconds(BeforeAtackWaitingTime);
        float currentTransparency = spriteRenderer.color.a;
        spriteRenderer.sprite = WithAttack;
        ChangeTransparency(currentTransparency);
        IsHurtingOnCollision = true;
        PlayerRecoil = PlayerRecoilWhenCharging;
        Vector2 movement = CalculateAfterThePlayerMovement();
        Rb2D.AddForce(movement * ChargingSpeed);
        yield return new WaitForSeconds(ChargingTime);
        currentTransparency = spriteRenderer.color.a;
        this.GetComponent<SpriteRenderer>().sprite = WithoutAttack;
        ChangeTransparency(currentTransparency);
        PlayerRecoil = originalPlayerRecoil;

        IsHurtingOnCollision = false;
        this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        yield return new WaitForSeconds(AfterAtackWaitingTime);
        TeleportRandomly();

        _attackPhase = false;
    }

    protected override void Move()
    {
        MoveRandomly();
        if (!_attackPhase && Player.GetComponent<PlayerBacteria>().isVisible)
        {
            StartCoroutine(Charge());
        }

    }

    protected override void OnCollisionEnter2D(Collision2D col)
    {
        base.OnCollisionEnter2D(col);
        if(col.gameObject == Player) StartCoroutine(MakeVisible());
    }
    
    protected override void ActOnDeath()
    {
        UnlockMutation();
        Destroy(this.gameObject);
        for (int i = 0; i < NumberOfDrops; i++) DropFood(EnergyDrop);
        Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
    }

    protected override void SetMutationChoices()
    {

        InvisibilityMutation mutation1 = new InvisibilityMutation("Invisibility", "When standing still you will be invisible to enemies", true, 2f);
        foreach (Upgrade upgrade in PlayerProgression.upgradesAvailable.ToArray())
        {
            if (upgrade.isUnlocked && upgrade.name.Equals("Camouflage")) mutation1.UpgradeMutation();
        }
        Choice1 = mutation1;

        StrengthOutOfSpeedMutation mutation2 = new StrengthOutOfSpeedMutation("Dangerous Speed", "increases your strength based on a bonus speed you have", true, 0.02f);
        foreach (Upgrade upgrade in PlayerProgression.upgradesAvailable.ToArray())
        {
            if (upgrade.isUnlocked && upgrade.name.Equals("Demon of speed")) mutation2.UpgradeMutation();
        }
        Choice2 = mutation2;
    }
}
