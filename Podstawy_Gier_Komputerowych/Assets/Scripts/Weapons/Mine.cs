using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public int damage = 12;
    public int timer = 4;
    public GameObject explosion = null;
    private AudioSource audio;
    void Start()
    {
        StartCoroutine(Disappearing());
        audio = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.DamageEnemy(damage);
            audio.Play(0);
            Destroy(gameObject);
            Instantiate(explosion, other.gameObject.transform.position, Quaternion.identity);
        }
    }

    private IEnumerator Disappearing()
    {
        yield return new WaitForSeconds(timer);
        audio.Play(0);
        GetComponent<CircleCollider2D>().radius = 10;
        Destroy(gameObject);
        Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
    }
}
