using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmationAlert : MonoBehaviour {
    
    public void OK()
    {
        this.gameObject.SetActive(false);
    }
}
