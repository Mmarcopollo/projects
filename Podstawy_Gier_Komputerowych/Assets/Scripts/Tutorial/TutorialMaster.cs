using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TutorialMaster : MonoBehaviour {

    public GameObject Player;
    public float minimalObjectDistance = 3;
    public  StagePrototype stage;
    public List<Vector3> unoccupiedPositions = new List<Vector3>();
    public Canvas EndDescription;
    public GameObject gameObjects;

    public static int iterator = 0;

    protected virtual void Start()
    {
        Player.GetComponent<PlayerBacteria>().isItTutorial = true;
    }

    public float GetCurrentMutationEnergyCost()
    {
        return stage.EnergyRequirement;
    }


    public void EndLevel()
    {
        EndDescription.enabled = true;
        iterator++;
    }
    public Vector3 CalculateRandomPosition(StagePrototype stage)
    {
        float randomX = Random.Range(-stage.Width / 2, stage.Width / 2);
        float randomY = Random.Range(-stage.Height / 2, stage.Height / 2);
        return new Vector3(randomX, randomY, 0);
    }

    public void GenerateWalls(StagePrototype stage)
    {
        float halfOfStageWidth = stage.Width / 2;
        float halfOfStageHeight = stage.Height / 2;

        // +1 so that food doesnt spawn on walls
        Vector3 up = new Vector3(0, halfOfStageHeight + 1);
        Vector3 right = new Vector3(halfOfStageWidth + 1, 0);
        Vector3 down = new Vector3(0, -(halfOfStageHeight + 1));
        Vector3 left = new Vector3(-(halfOfStageWidth + 1), 0);
        GameObject upWall = (GameObject)Instantiate(Resources.Load("WallPrefab"), up, new Quaternion());
        upWall.transform.parent = this.transform;
        GameObject rightWall = (GameObject)Instantiate(Resources.Load("WallPrefab"), right, new Quaternion());
        rightWall.transform.parent = this.transform;
        GameObject downWall = (GameObject)Instantiate(Resources.Load("WallPrefab"), down, new Quaternion());
        downWall.transform.parent = this.transform;
        GameObject leftWall = (GameObject)Instantiate(Resources.Load("WallPrefab"), left, new Quaternion());
        leftWall.transform.parent = this.transform;

        upWall.transform.localScale = new Vector3(stage.Width + 1, 1, 1);
        rightWall.transform.localScale = new Vector3(1, stage.Height + 3, 1);
        downWall.transform.localScale = new Vector3(stage.Width + 1, 1, 1);
        leftWall.transform.localScale = new Vector3(1, stage.Height + 3, 1);

    }

    public void GenerateFood(StagePrototype currentStage)
    {
        Vector3 position;
        for (int i = 0; i < currentStage.FoodStartNumber; i++)
        {
            position = CalculateRandomPosition(currentStage);
            position.z = 1;
            GameObject go = (GameObject)Instantiate(Resources.Load("FoodPrefab"), position, new Quaternion());
            go.transform.parent = this.transform;
            go.GetComponent<Food>().Player = Player;
        }
    }

    public void GenerateStones(StagePrototype currentStage)
    {
        Vector3 position;
        for (int i = 0; i < currentStage.NumberOfStones; i++)
        {
            if (unoccupiedPositions.Count == 0) return;
            position = unoccupiedPositions[unoccupiedPositions.Count - 1];
            unoccupiedPositions.RemoveAt(unoccupiedPositions.Count - 1);
            int stoneSize = Random.Range(0, 2);
            string prefabName;
            if (stoneSize == 0) prefabName = "StonePrefab";
            else prefabName = "BigStonePrefab";
            GameObject go = (GameObject)Instantiate(Resources.Load(prefabName), position, new Quaternion());
            go.transform.parent = this.transform;
        }
    }
    public void ShuffleUnoccupiedPositions()
    {
        int count = unoccupiedPositions.Count;
        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(i, count - 1);
            Vector3 temp = unoccupiedPositions[i];
            unoccupiedPositions[i] = unoccupiedPositions[randomIndex];
            unoccupiedPositions[randomIndex] = temp;
        }
    }

    public void FillUnoccupiedPositions()
    {
        unoccupiedPositions = new List<Vector3>();
        float horizontalLimit = stage.Width / 2 - 1;   //-1 to not spawn on walls
        float verticalLimit = stage.Height / 2 - 1;   //-1 to not spawn on walls
        for (float xPos = -horizontalLimit; xPos < horizontalLimit; xPos += minimalObjectDistance)
        {
            for (float yPos = -verticalLimit; yPos < verticalLimit; yPos += minimalObjectDistance)
            {
                Vector3 position = new Vector3(xPos, yPos, 0);
                if (Mathf.Abs(xPos) > minimalObjectDistance || Mathf.Abs(yPos) > minimalObjectDistance) unoccupiedPositions.Add(position);
            }
        }
        ShuffleUnoccupiedPositions();
    }



    public IEnumerator SpawnFood()
    {
        while (true)
        {
            Vector3 position = CalculateRandomPosition(stage);
            GameObject go = (GameObject)Instantiate(Resources.Load("FoodPrefab"), position, new Quaternion());
            go.transform.parent = this.transform;
            go.GetComponent<Food>().Player = Player;
            yield return new WaitForSeconds(stage.FoodTimeInterval);
        }
    }
}
