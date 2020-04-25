using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlHint : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(Disappearing());
	}

    private IEnumerator Disappearing()
    {
        yield return new WaitForSeconds(4);
        Destroy(gameObject);
    }
}
