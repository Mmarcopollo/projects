using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial4 : TutorialMaster
{
    public float bacteriaNutritionModificator = 1;
    private GameObject enemy;
    public GameObject chociceMutation;

    protected override void Start()
    {
        base.Start();
        List<string> enemyPrefabs = new List<string>();
        enemyPrefabs.Add("BasicEnemyPrefab");

        stage = new StagePrototype(6, 80, enemyPrefabs, null, null, 1, 0, 2, 40, 1, 70, 0, 1, false);
        Time.timeScale = 1;

        StartStage();

        chociceMutation.GetComponent<MutationTutorial>().isLocked = true;

        Player.GetComponent<PlayerBacteria>().CurrentWeapons.Add(Instantiate(Resources.Load<Weapon>("Weapons/MinesWeapon")));
    }

    void Update()
    {
        if (enemy == null)
        {
            GameObject.Find("MutationTutorial").GetComponent<MutationTutorial>().isLocked = false;
        }
        if (Player.GetComponent<PlayerBacteria>().CurrentHp <= 0)
        {
            SceneManager.LoadScene("Tutorial4");
        }
    }


    public void StartStage()
    {
        FillUnoccupiedPositions();

        gameObjects.gameObject.SetActive(true);
        Player.transform.position = new Vector2(-35, 0);

        GenerateWalls(stage);

        //FOOD
        Vector3 position = new Vector3(35, 0, 1);
        GameObject go = (GameObject)Instantiate(Resources.Load("FoodPrefab"), position, new Quaternion());
        go.GetComponent<Food>().NutritionalValued = 100;
        go.transform.parent = this.transform;
        go.GetComponent<Food>().Player = Player;
        //LITLE SHIT 
        Vector3 positionOfShit = new Vector3(-30, 0, 1);

        GameObject shit = (GameObject)Instantiate(Resources.Load("SpeedPrefab"), positionOfShit, new Quaternion());
        shit.GetComponent<Speed>().speadBonus = 100;
        shit.transform.parent = this.transform;
        shit.GetComponent<Collectable>().Player = Player;

        //Stones
        Vector3 positionOfStone1 = new Vector3(-25, 3, 1);
        Vector3 positionOfStone2 = new Vector3(-15, -3, 1);
        Vector3 positionOfStone3 = new Vector3(0, 0, 1);
        Vector3 positionOfStone4 = new Vector3(10, 1, 1);
        Vector3 positionOfStone5 = new Vector3(-27, -2, 1);
        Vector3 positionOfStone6 = new Vector3(-25, 3, 1);

        GameObject stone1 = (GameObject)Instantiate(Resources.Load("StonePrefab"), positionOfStone1, new Quaternion());
        GameObject stone2 = (GameObject)Instantiate(Resources.Load("BigStonePrefab"), positionOfStone2, new Quaternion());
        GameObject stone3 = (GameObject)Instantiate(Resources.Load("StonePrefab"), positionOfStone3, new Quaternion());
        GameObject stone4 = (GameObject)Instantiate(Resources.Load("BigStonePrefab"), positionOfStone4, new Quaternion());
        GameObject stone5 = (GameObject)Instantiate(Resources.Load("StonePrefab"), positionOfStone5, new Quaternion());
        GameObject stone6 = (GameObject)Instantiate(Resources.Load("BigStonePrefab"), positionOfStone6, new Quaternion());
        stone1.transform.parent = this.transform;
        stone2.transform.parent = this.transform;
        stone3.transform.parent = this.transform;
        stone4.transform.parent = this.transform;
        stone5.transform.parent = this.transform;
        stone6.transform.parent = this.transform;

    }

    public void LoadNextLevel()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        EndDescription.enabled = false;
        SceneManager.LoadScene("SampleScene");
    }

}
