using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speed : Collectable
{
    public Rigidbody2D Rb2D;
    public PlayerBacteria playerStats;
    public float speadBonus = 30;

    // Use this for initialization
    protected virtual void Start()
    {
        GameObject[] objectsWithTagPlayer = GameObject.FindGameObjectsWithTag("Player");
        if (objectsWithTagPlayer.Length != 0)
            Player = objectsWithTagPlayer[0];
        Rb2D = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        base.moveWhenStuck(other);
        playerStats = Player.GetComponent<PlayerBacteria>();

        if (other.gameObject.CompareTag("Player"))
        {
            if (playerStats.additionalSpeed)
            {
                Destroy(this.gameObject);
            }
            else
            {
                playerStats.additionalSpeed = true;
                playerStats.Speed += speadBonus;
                StartCoroutine(speedTime());
                GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }
    IEnumerator speedTime()
    {
        yield return new WaitForSeconds(10 + playerStats.BonusCollectibleTime * 4);
        revertSpeed();
        Destroy(this.gameObject);
    }

    void revertSpeed()
    {
        playerStats.Speed -= 30f;
        playerStats.additionalSpeed = false;
    }
}
