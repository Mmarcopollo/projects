using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial3 : TutorialMaster
{

    public float bacteriaNutritionModificator = 1;
    private GameObject enemy;
    public GameObject chocieMutation;

    protected override void Start()
    {
        base.Start();
        List<string> enemyPrefabs = new List<string>();
        enemyPrefabs.Add("ChargingEnemyPrefab");

        stage = new StagePrototype(15, 15, enemyPrefabs, null, null, 1, 0, 10, 40, 1, 70, 0,1,  false);
        Time.timeScale = 1;

        StartStage();
        StartCoroutine(SpawnFood());

        chocieMutation.GetComponent<MutationTutorial>().isLocked = true;

        Player.GetComponent<PlayerBacteria>().CurrentWeapons.Add(Instantiate(Resources.Load<Weapon>("Weapons/MinesWeapon")));
    }

    void Update()
    {
        if (enemy == null)
        {
            GameObject.Find("MutationsChoiceTut").GetComponent<MutationTutorial>().isLocked = false;
        }
        if (Player.GetComponent<PlayerBacteria>().CurrentHp <= 0)
        {
            SceneManager.LoadScene("Tutorial3");
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
        GenerateEnemies(stage);
    }

    public void LoadNextLevel()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        EndDescription.enabled = false;
        SceneManager.LoadScene("Tutorial4");
    }
    private void GenerateEnemies(StagePrototype currentStage)
    {
        Vector3 position;
        for (int i = 0; i < currentStage.NumberOfEnemies; i++)
        {
            if (unoccupiedPositions.Count == 0) return;
            position = unoccupiedPositions[unoccupiedPositions.Count - 1];
            unoccupiedPositions.RemoveAt(unoccupiedPositions.Count - 1);
            string enemyPrefab = currentStage.EnemyPrefabs[Random.Range(0, currentStage.EnemyPrefabs.Count)];
            GameObject go = (GameObject)Instantiate(Resources.Load(enemyPrefab), position, new Quaternion());
            go.transform.parent = this.transform;
            enemy = go;
            go.GetComponent<Enemy>().Player = Player;
            go.GetComponent<Enemy>().EnergyDrop *= bacteriaNutritionModificator;
        }
    }
}
