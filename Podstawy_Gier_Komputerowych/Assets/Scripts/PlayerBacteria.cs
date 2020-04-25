using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBacteria : MonoBehaviour
{

    #region PlayerStats
    public Slider EnergyFill;       //Energy bar object
    public Image BloodFill;         //Player body fill object
    public GameObject GameOver;     //Game Over Text object
    public Sprite WithoutAttack;    //Sprite for attack
    public Sprite WithAttack;       //Sprite for player
    public Image PlayerHealth;      //Sprite for player body health bar
    private SpriteRenderer spriteRenderer;
    public bool isItTutorial = false;

    public List<Weapon> CurrentWeapons = new List<Weapon>(); //Bacteria's Current Weapon
    public Weapon FirstChoosenWeapon;

    public float CurrentEnergy;     //Current energy level
    public float MaxEnergy = 100;   //Maximum energy level
    public int EnergyDecrement = 5; //Energy decrease rate

    public float CurrentHp;         //Current health level
    public float MaxHp = 100;       //Maximum health level
    public int HealthDecrement = 10;//Health decrease rate
    public float MinimumMaxHp;

    public float Speed;             //Floating point variable to store the Player's movement Speed.
    public float MinimumSpeed;      //Minimum speed number

    public int Strength;
    public int MinimumStrength;

    public bool IsHurtingOnCollision = true;
    public int BonusCollectibleTime = 0;
    public bool isEnergyFalling = true;

    //private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rb2D;       //Store a reference to the Rigidbody2D component required to use 2D Physics.

    public bool IsAttacking = false;
    private bool _isLeft = false;

    public bool reversed = false;
    public bool additionalSpeed = false;
    public bool additionalSlow = false;
    public bool canBeInvisible = false;
    public float idleDurationForInvisibility = 4f;
    public bool isVisible = true;
    public float baseSpeed;
    public bool hasSecondLife = false;
    public float ressurectionHealthFraction;
    public float defenseFromBullets = 0;
    public float regeneration = 0.001f;
    public float regenerationPeriod = 0.1f;
    public float timePassedFromLastRegeneration;

    #endregion
    private AudioSource audio;
    float timePassedFromLastInput = 0;


    // Use this for initialization
    void Start()
    {
        audio = GetComponent<AudioSource>();
        _rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        baseSpeed = Speed;
        //_spriteRenderer = GetComponent<SpriteRenderer>();

        CurrentWeapons.Add(Instantiate(Resources.Load<Weapon>("Weapons/SpikesWeapon")));
        CurrentWeapons.Add(Instantiate(Resources.Load<Weapon>("Weapons/ShootingWeapon")));
        FirstChoosenWeapon = CurrentWeapons[0];
        Cursor.SetCursor(FirstChoosenWeapon.cursor, Vector2.zero, CursorMode.Auto);
        //CurrentWeapons.Add(Instantiate(Resources.Load<Weapon>("Weapons/MinesWeapon")));
        //CurrentWeapons.Add(Instantiate(Resources.Load<Weapon>("Weapons/MinigunWeapon")));

        CurrentEnergy = 10;
        CurrentHp = MaxHp;
    }

    private IEnumerator GetReborn()
    {
        float numberOfSteps = 100;
        float hpIncrement = MaxHp * ressurectionHealthFraction / numberOfSteps;
        for(int i=0; i< numberOfSteps; i++)
        {
            ChangeHealth(CurrentHp + hpIncrement);
            yield return new WaitForSeconds(0.05f);
        }
        ChangeHealth(MaxHp);
        hasSecondLife = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnergyFalling)
        {
            CurrentEnergy -= EnergyDecrement * Time.deltaTime;
            EnergyFill.value = CurrentEnergy / MaxEnergy;
            if (CurrentEnergy <= 0)
            {
                CurrentHp -= HealthDecrement * Time.deltaTime;
                BloodFill.fillAmount = CurrentHp / MaxHp;
            }
        }
        if (CurrentHp <= 0)
        {
            if (!hasSecondLife)
            {
                if(isItTutorial) Application.LoadLevel(Application.loadedLevel);
                else
                {
                    GameOver.GetComponent<GameOverScreen>().StartGameOverScript();
                    this.gameObject.SetActive(false);
                }
            }
            else StartCoroutine(GetReborn());
            //Application.LoadLevel(Application.loadedLevel);
        }

        ValidateStats();
    }

    public void ChangeEnergy(float amount)
    {
        CurrentEnergy += amount;
        CurrentEnergy = Mathf.Clamp(CurrentEnergy, 0, MaxEnergy);
        EnergyFill.value = CurrentEnergy / MaxEnergy;
    }

    public void ChangeHealth(float amount)
    {
        if (amount < 0)
        {
            audio.Play(0);
        }
        CurrentHp += amount;
        CurrentHp = Mathf.Clamp(CurrentHp, 0, MaxHp);
        BloodFill.fillAmount = CurrentHp / MaxHp;
    }

    public void ValidateStats()
    {
        if (EnergyDecrement < 0) EnergyDecrement = 0;
        if (Speed < MinimumSpeed) Speed = MinimumSpeed;
        if (MaxHp < MinimumMaxHp) MaxHp = MinimumMaxHp;
        if (Strength < MinimumStrength) Strength = MinimumStrength;
    }

    private void MakeInvisible()
    {
        isVisible = false;
        Color color = spriteRenderer.color;
        color.a = 0.5f;
        spriteRenderer.color = color;
        BloodFill.color = color;
    }

    private void MakeVisible()
    {
        isVisible = true;
        Color color = spriteRenderer.color;
        color.a = 1f;
        spriteRenderer.color = color;
        BloodFill.color = color;
    }

    private void Regenerate()
    {
        //Debug.Log(MaxHp + " " + regeneration + " " + MaxHp * regeneration);
        ChangeHealth(MaxHp * regeneration);
        ValidateStats();
        timePassedFromLastRegeneration = 0;
    }

    void FixedUpdate()
    {
        Cursor.SetCursor(FirstChoosenWeapon.cursor, Vector2.zero, CursorMode.Auto);
        timePassedFromLastInput += Time.deltaTime;
        timePassedFromLastRegeneration += Time.deltaTime;
        if(timePassedFromLastInput > idleDurationForInvisibility && canBeInvisible) MakeInvisible();
        if (timePassedFromLastRegeneration > regenerationPeriod) Regenerate();
        

        //Store the current horizontal input in the float moveHorizontal.
        float moveHorizontal = Input.GetAxis("Horizontal");

        //Store the current vertical input in the float moveVertical.
        float moveVertical = Input.GetAxis("Vertical");

        if (moveHorizontal != 0 || moveVertical != 0)
        {
            timePassedFromLastInput = 0;
            MakeVisible();
        }

            Vector2 movement;
        //Use the two store floats to create a new Vector2 variable movement.
        if (!reversed)
        {
            movement = new Vector2(moveHorizontal, moveVertical);
        }
        else
        {
            movement = new Vector2(moveVertical, moveHorizontal);
        }
        _rb2D.velocity += (movement * Speed / 50);
        if (Input.GetKey(KeyCode.Mouse0) && !IsAttacking && FirstChoosenWeapon != null)
        {
            timePassedFromLastInput = 0;
            MakeVisible();
            if (FindObjectOfType<RadialMenu>() == null)
            {
                FirstChoosenWeapon.CurrentWeaponSprite = WithAttack;
                FirstChoosenWeapon.Attack(this);
                StartCoroutine(FirstChoosenWeapon.AttackCheck(this, KeyCode.Mouse0));
            }
        }
        /*
        if (Input.GetKey(KeyCode.Mouse0) && !IsAttacking && CurrentWeapons.Count > 0)
        {
            CurrentWeapons[0].Attack(this);
            StartCoroutine(CurrentWeapons[0].AttackCheck(this, KeyCode.Mouse0));
        }
        if (Input.GetKey(KeyCode.Mouse1) && !IsAttacking && CurrentWeapons.Count > 1)
        {
            CurrentWeapons[1].Attack(this);
            StartCoroutine(CurrentWeapons[1].AttackCheck(this, KeyCode.Mouse1));
        }*/
        if (Input.GetAxis("Horizontal") > 0 && !_isLeft)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            _isLeft = true;
        }
        if (Input.GetAxis("Horizontal") < 0 && _isLeft)
        {
            timePassedFromLastInput = 0;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            _isLeft = false;
        }
    }

    public void Refresh()
    {
        GetComponent<SpriteRenderer>().sprite = WithoutAttack;
    }

}
