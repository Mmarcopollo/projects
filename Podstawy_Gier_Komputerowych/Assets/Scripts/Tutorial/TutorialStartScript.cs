using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialStartScript : MonoBehaviour {

    public Canvas ControlDescription;
    public Canvas[] canvas;
    public GameObject gameObjects;
    public GameObject mutations;
    private int iterator = 0;

    /// </summary>

    // Use this for initialization
    void Start()
    {
            foreach (var i in canvas)
            {
                i.enabled = false;
            }

        gameObjects.gameObject.SetActive(false);
        mutations.gameObject.SetActive(false);
        mutations.GetComponent<Canvas>().enabled = true;
    }


    public void StartTutorial()
    {
        Time.timeScale = 1;
        ControlDescription.gameObject.SetActive(false);

        foreach (var i in canvas)
        {
            i.enabled = true;
        }
        gameObjects.gameObject.SetActive(true);
        mutations.gameObject.SetActive(true);
        mutations.GetComponent<Canvas>().enabled = false;

    }

    public void Next()
    {
        if(iterator==0)
        {
            ControlDescription.gameObject.SetActive(false);
        }
        else if(iterator > 0)
        {
            canvas[iterator - 1].enabled = false;
        }
        canvas[iterator].enabled = true;
        iterator++;
    }
}
