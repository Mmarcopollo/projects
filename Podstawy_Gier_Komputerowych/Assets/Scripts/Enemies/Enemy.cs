using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour
{
    //Stats
    public float Speed;                     //floating point variable to store enemy movement Speed.
    public float MaxHp;                     //enemy hit points cap
    protected float CurrentHp;              //current number of hit points
    public int Strength;                    //determines the number of damage dealt

    protected bool IsHurtingOnCollision;    //determines whether enemy is hurting Player on collision

    private float _baseRadius;              //base sprite radius
    public float MinimumRadius;             //minimum scale object can scaled to

    public float EnergyDrop;                //energy dropped after death
    public int NumberOfDrops;               //number of energy drops
    public float DropDispersion;            //dispersion of food after death
    public float HitRecoil;                 //Recoil from hiting Player       
    public float PlayerRecoil;

    public string Species;                  //unique enemy type name

    public GameObject Player;
    protected Rigidbody2D Rb2D;             //Store a reference to the Rigidbody2D component required to use 2D Physics.
    public Image BloodFill;
    private GameObject stageGenerator;      //Stores stageGenerator reference
    protected GameObject challengeManager;     //Stores reference to challengeManager
    public GameObject explosion;
    protected bool isDamageResistant = false;

    protected virtual void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        Rb2D = GetComponent<Rigidbody2D>();
        CurrentHp = MaxHp;
        _baseRadius = this.transform.localScale.x;
        GameObject[] objectsFound = GameObject.FindGameObjectsWithTag("StageGenerator");
        if (objectsFound.Length != 0) stageGenerator = objectsFound[0];
        challengeManager = GameObject.Find("Challenges");
    }

    protected Vector2 calculateRandomMovement()
    {
        float moveHorizontal = Random.Range(-1.0f, 1.0f);
        float moveVertical = Random.Range(-1.0f, 1.0f);

        //Use the two store floats to create a new Vector2 variable movement.
        return new Vector2(moveHorizontal, moveVertical);
    }

    protected Vector2 CalculateAfterThePlayerMovement()
    {
        Vector2 movement = this.transform.position - Player.transform.position;
        return -movement / movement.magnitude;
    }

    protected void DropFood(float nutrition)
    {
        Vector2 movement = this.transform.position - Player.transform.position;
        Vector2 direction = movement / movement.magnitude;
        float xAddition = (direction.x + Random.Range(-0.5f, 0.5f)) * DropDispersion;
        float yAddition = (direction.y + Random.Range(-0.5f, 0.5f)) * DropDispersion;
        Vector3 position = this.transform.position + new Vector3(xAddition, yAddition);
        GameObject newFood = (GameObject)Instantiate(Resources.Load("FoodPrefabEnemy"), position, new Quaternion());
        if(stageGenerator != null)newFood.transform.parent = stageGenerator.transform;
        newFood.GetComponent<Food>().Player = Player;

        newFood.GetComponent<Food>().NutritionalValued = nutrition;
    }

    protected virtual void ActOnDeath()
    {
        for (int i = 0; i < NumberOfDrops; i++) DropFood(EnergyDrop);
        if(challengeManager != null)challengeManager.GetComponent<ChallengeManager>().Refresh(this.gameObject);
        Destroy(this.gameObject);
        Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
    }

    protected virtual void FixedUpdate()
    {
        if (CurrentHp <= 0)
        {
            ActOnDeath();
        }
        Move();
    }

    private void UpdateRadius()
    {
        float healthRatio = CurrentHp / MaxHp;
        float newRadius = healthRatio * _baseRadius;
        if (newRadius < MinimumRadius)
            return;
        this.transform.localScale = new Vector3(newRadius, newRadius, 1);

        Debug.Log(newRadius);
        Debug.Log(this.transform.localScale.x);
    }

    protected void ChangeHealthBar()
    {
        BloodFill.fillAmount = CurrentHp / MaxHp;
    }

    public void DamageEnemy(int damage)
    {
        CurrentHp -= damage;
        ChangeHealthBar();
    }

    protected abstract void Move();

    protected virtual void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject == Player)
        {
            Vector2 movement = CalculateAfterThePlayerMovement();
            if(Rb2D != null) Rb2D.AddForce(-movement * Speed * HitRecoil);
            PlayerBacteria playerStats = Player.GetComponent<PlayerBacteria>();
            if (IsHurtingOnCollision)
            {
                playerStats.ChangeHealth(-Strength);
            }
            if (playerStats.IsHurtingOnCollision)
            {
                movement = CalculateAfterThePlayerMovement();
                Player.GetComponent<Rigidbody2D>().AddForce(movement * Player.GetComponent<PlayerBacteria>().Speed * PlayerRecoil);
                if (!isDamageResistant) CurrentHp -= playerStats.Strength;
                //UpdateRadius();
                ChangeHealthBar();
                //DropFood(10);
            }
        }
    }
}
