using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextStage : MonoBehaviour {

    public StageGenerator stageGenerator;
    public GameObject shop;

    private int counter = 0;
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.F2))
        {
            stageGenerator.GenerateNextStage();
        }

        if(Input.GetKeyDown(KeyCode.F3) && counter % 2 == 0)
        {
            Time.timeScale = 0;
            shop.SetActive(true);
            counter++;
        }
        else if(Input.GetKeyDown(KeyCode.F3) && counter % 2 == 1)
        {
            Time.timeScale = 1;
            shop.SetActive(false);
            counter++;
        }

        if (Input.GetKeyDown(KeyCode.F4))
        {
            PlayerProgression.Initiate();
        }
    }
}
