using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetConfirm : MonoBehaviour {

    public GameObject removeConfirmation;


    public void Cancel()
    {
        this.gameObject.SetActive(false);
    }

    public void Accept()
    {
        int height = PlayerPrefs.GetInt("height");
        int width = PlayerPrefs.GetInt("width");
        int fullscreen = 1;
        int hints = 1; 

        PlayerPrefs.DeleteAll();

        PlayerPrefs.SetInt("height", height);
        PlayerPrefs.SetInt("width", width);
        PlayerPrefs.SetInt("fullscreen", fullscreen);
        PlayerPrefs.SetInt("hints", hints);

        Cancel();
        removeConfirmation.SetActive(true);
    }
}
