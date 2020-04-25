using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePause : MonoBehaviour {


    public GameObject gameObj;
	void Update ()
    {
        if(gameObj.gameObject.activeSelf == true)
        {
            foreach (Transform child in gameObject.transform)
            {
                child.gameObject.SetActive(false);
            }
        }
    }
}
