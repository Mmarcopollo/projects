using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public GameObject Player;

    protected void moveWhenStuck(Collider2D other)
    {
        if (other.gameObject.CompareTag("SolidObject"))
        {
            Vector3 direction = this.transform.position - new Vector3(0, 0, 0);
            direction = -direction / direction.magnitude;
            this.transform.position += direction * 2;
        }
    }
}
