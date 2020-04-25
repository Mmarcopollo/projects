using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ExitShop : MonoBehaviour
{

	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.F3))
        {
            Time.timeScale = 1;
            //this.gameObject.setActive(false);
        }
	}
}
