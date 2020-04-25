using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

    float timer = 0;

    private void Update()
    {
        if (timer < 5) 
            transform.Rotate(0, 0, Time.deltaTime * 360);
        else if(timer > 10)
            timer = 0;

        timer += Time.deltaTime;
    }

}
