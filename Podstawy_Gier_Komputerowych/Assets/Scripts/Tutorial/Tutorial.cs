using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : TutorialMaster
{

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        stage  = new StagePrototype(10, 10, null, null, null, 0, 0, 2, 40, 1, 70, 0, 1,false);
        Time.timeScale = 1;

        StartStage();
        StartCoroutine(SpawnFood());
    }

    void Update()
    {
        if (Player.GetComponent<PlayerBacteria>().CurrentHp <= 0)
        {
            SceneManager.LoadScene("Tutorial");
        }
    }
    public void StartStage()
    {
        FillUnoccupiedPositions();

        gameObjects.gameObject.SetActive(true);
        Player.transform.position = new Vector2(0, 0);

        GenerateStones(stage);
        GenerateFood(stage);
        GenerateWalls(stage);
 
    }


    public void LoadNextLevel()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        EndDescription.enabled = false;
        SceneManager.LoadScene("Tutorial2");
    }

}