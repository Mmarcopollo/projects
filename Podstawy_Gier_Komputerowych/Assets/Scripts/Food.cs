using UnityEngine;

public class Food : MonoBehaviour
{
    public float NutritionalValued = 10;
    //private bool IsHurtingOnCollision;

    public GameObject Player;
    public Rigidbody2D Rb2D;
    private GameObject challengeManager;

    // Use this for initialization
    protected virtual void Start()
    {
        challengeManager = GameObject.Find("Challenges");
        GameObject[] objectsWithTagPlayer = GameObject.FindGameObjectsWithTag("Player");
        if (objectsWithTagPlayer.Length != 0)
            Player = objectsWithTagPlayer[0];
        Rb2D = GetComponent<Rigidbody2D>();
        //IsHurtingOnCollision = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerBacteria playerStats = Player.GetComponent<PlayerBacteria>();
        if (other.gameObject.CompareTag("Player"))
        {
            playerStats.ChangeEnergy(NutritionalValued);

         //   if(challengeManager.GetComponent<Canvas>().enabled == true)
            //    challengeManager.GetComponent<ChallengeManager>().Refresh(this.gameObject);


            if(challengeManager) challengeManager.GetComponent<ChallengeManager>().Refresh(this.gameObject);
            //Debu/iophg.Log(Player.GetComponent<PlayerBacteria>().CurrentEnergy);

            Destroy(this.gameObject);
        }
        else if (other.gameObject.CompareTag("SolidObject"))
        {
            Vector3 direction = this.transform.position - new Vector3(0,0,0);
            direction = -direction / direction.magnitude;
            this.transform.position += direction*2;
        }
    }
}
