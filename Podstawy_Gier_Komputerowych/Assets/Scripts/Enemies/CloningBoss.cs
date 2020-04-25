using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloningBoss : Boss
{
    private bool _attackPhase = false;       //Is enemy attacking
    private bool _isItOriginal = true;       //Is object an original or a copy
    private float stageHeight;
    private float stageWidth;
    private List<GameObject> clones = new List<GameObject>();

    public GameObject bulletPrefab;
    public float bulletDamage;
    public float bulletRange;
    public float timeBetweenShots;
    public int numberOfLives;
    public int numberOfClones;

    protected override void Start()
    {
        base.Start();

        StartCoroutine(Shoot());
        GameObject stageGenerator = GameObject.Find("StageGenerator");
        stageHeight = stageGenerator.GetComponent<StageGenerator>().GetCurrentStageHeight();
        stageWidth = stageGenerator.GetComponent<StageGenerator>().GetCurrentStageWidth();
    }

    public void SetOriginality(bool isItOriginal)
    {
        this._isItOriginal = isItOriginal;
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

    private IEnumerator Shoot()
    {
        _attackPhase = true;
        while(_attackPhase)
        {
            SpawnBullet(new Vector2(1, 1));
            yield return new WaitForSeconds(timeBetweenShots);
            SpawnBullet(new Vector2(0, 1));
            yield return new WaitForSeconds(timeBetweenShots);
            SpawnBullet(new Vector2(-1, 1));
            yield return new WaitForSeconds(timeBetweenShots);
            SpawnBullet(new Vector2(-1, 0));
            yield return new WaitForSeconds(timeBetweenShots);
            SpawnBullet(new Vector2(-1, -1));
            yield return new WaitForSeconds(timeBetweenShots);
            SpawnBullet(new Vector2(0, -1));
            yield return new WaitForSeconds(timeBetweenShots);
            SpawnBullet(new Vector2(1, -1));
            yield return new WaitForSeconds(timeBetweenShots);
            SpawnBullet(new Vector2(1, 0));

        }
    }

    protected override void Move()
    {
        MoveRandomly();
    }

    
    private void CloneItself()
    {
        
        for (int i=0; i<numberOfClones; i++)
        {
            float x = Random.Range(-stageHeight / 2, stageHeight / 2);
            float y = Random.Range(-stageWidth / 2, stageWidth / 2);
            Vector3 position = new Vector3(x, y, this.transform.position.z);
            GameObject cloneGO = (GameObject)Instantiate(Resources.Load("CloningBossPrefab"), position, Quaternion.identity);
            clones.Add(cloneGO);
            cloneGO.transform.SetParent(GameObject.Find("StageGenerator").transform);
            CloningBoss clone = cloneGO.GetComponent<CloningBoss>();
            clone.MaxHp = 1;
            clone.SetOriginality(false);
            clone.Player = Player;
        }
    }

    protected override void ActOnDeath()
    {
        //base.ActOnDeath();
        if (_isItOriginal)
        {
            numberOfLives--;
            if(numberOfLives==0)
            {
                for (int i = 0; i < NumberOfDrops; i++) DropFood(EnergyDrop);
                UnlockMutation();
                Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
                foreach (GameObject clone in clones)
                {
                    if (clone != null) clone.GetComponent<CloningBoss>().ActOnDeath();
                }
                Destroy(this.gameObject);
            }
            else
            {
                CurrentHp = MaxHp;
                BloodFill.fillAmount = CurrentHp / MaxHp;
                float x = Random.Range(-stageHeight / 2, stageHeight / 2);
                float y = Random.Range(-stageWidth / 2, stageWidth / 2);
                this.transform.position = new Vector3(x, y, this.transform.position.z);
                CloneItself();
            }
        }
        else
        {
            Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    protected override void SetMutationChoices()
    {
        DoubleShootingMutation mutation = new DoubleShootingMutation("Double shot", "Enables shooting two bullets at the same time", true);
        foreach (Upgrade upgrade in PlayerProgression.upgradesAvailable.ToArray())
        {
            if (upgrade.isUnlocked && upgrade.name.Equals("Double bullet")) mutation.UpgradeMutation();
        }
        Choice1 = mutation;
        //editableSprites.Add("Tougher cell wall");

        BulletDefenseMutation mutation2 = new BulletDefenseMutation("Bullet Defense", "Decreases damage dealt by enemy bullets", true, 2);
        foreach (Upgrade upgrade in PlayerProgression.upgradesAvailable.ToArray())
        {
            if (upgrade.isUnlocked && upgrade.name.Equals("Anti Bullet Cell Wall")) mutation2.UpgradeMutation();
        }
        Choice2 = mutation2;
    }
}
