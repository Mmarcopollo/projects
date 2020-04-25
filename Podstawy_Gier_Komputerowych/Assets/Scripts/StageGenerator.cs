using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class StageGenerator : MonoBehaviour
{
    //public Sprite sprite;
    public GameObject Player;
    public float minimalObjectDistance = 3;
    public GameObject gameOverScreen;
    public GameObject challengeManager;
    private List<StagePrototype> stages = new List<StagePrototype>();
    private List<Vector3> unoccupiedPositions = new List<Vector3>();
    private int _stageIterator = -1;                      //-1 so that next stage will be stage number 0 (first stage)
    public float foodNutritionModificator = 1;
    public float bacteriaNutritionModificator = 1;
    public GameObject bossNamePanel;
    public GameObject levelNumberPanel;
    public float BossNameDuration;
    public float levelNumberDuration;
    public GameObject MutationChoice;
    public AudioSource myAudio;
    public Sprite World1Background;
    public Sprite World2Background;
    public int StagePrizeBonus = 0;
    //private string _musicNamesPath = "Assets/Resources/music/tracks.txt";
    private string[] TrackList; //Holds All tracks read from file
    private List<string> AvailableTracks; //Holds All tracks available to play

    private bool wasLastStageABossRoom = false;


    void Start()
    {
        myAudio = GetComponent<AudioSource>();
        /*chwilowo, potem sie wczyta z pliku*/
        List<string> enemyPrefabs = new List<string>();
        List<string> enemyPrefabs2 = new List<string>();
        List<string> enemyPrefabs3 = new List<string>();
        List<string> enemyPrefabs4 = new List<string>();
        List<string> enemyPrefabs5 = new List<string>();
        List<string> enemyPrefabs6 = new List<string>();
        List<string> enemyPrefabsBoss = new List<string>();

        enemyPrefabs.Add("BasicEnemyPrefab");
        enemyPrefabs2.AddRange(new string[] { "BasicEnemyPrefab", "ChargingEnemyPrefab" });
        enemyPrefabs3.AddRange(new string[] { "BasicEnemyPrefab", "ChargingEnemyPrefab", "ShootingEnemyPrefab" });
        enemyPrefabs4.AddRange(new string[] { "BasicEnemyPrefab", "ChargingEnemyPrefab", "ShootingEnemyPrefab", "StealingEnemyPrefab" });
        enemyPrefabs5.AddRange(new string[] { "BasicEnemyPrefab", "ChargingEnemyPrefab", "ShootingEnemyPrefab", "SpawningEnemyPrefab", "StealingEnemyPrefab" });
        enemyPrefabs6.AddRange(new string[] { "BasicEnemyPrefab", "ChargingEnemyPrefab", "ShootingEnemyPrefab", "SpawningEnemyPrefab", "StealingEnemyPrefab", "ExplodingEnemyPrefab" });
        enemyPrefabsBoss.AddRange(new string[] { "SeparatingBossPrefab", "CloningBossPrefab", "InvisibleBossPrefab" });

        List<string> collectablePrefabs1 = new List<string>();
        collectablePrefabs1.Add("HealPrefab");
        List<string> collectablePrefabs2 = new List<string>();
        collectablePrefabs2.AddRange(new string[] { "HealPrefab", "SpeedPrefab" });
        List<string> collectablePrefabs3 = new List<string>();
        collectablePrefabs3.AddRange(new string[] { "HealPrefab", "SpeedPrefab", "SlowPrefab" });
        List<string> collectablePrefabs4 = new List<string>();
        collectablePrefabs4.AddRange(new string[] { "HealPrefab", "SpeedPrefab", "SlowPrefab", "ReversePrefab" });
        List<Challenge> challenges = new List<Challenge>();
        challenges.Add(new KillAllOfAKindChallenge(150));
        challenges.Add(new EatFoodChallenge(50, 120));
        challenges.Add(new PacifismChallenge(200));
        challenges.Add(new TimeLimitChallenge(30, 120));

        stages.Add(new StagePrototype(20, 20, enemyPrefabs, collectablePrefabs1, challenges, 4, 0, 5, 80, 1, 100, 100, 1, false));
        stages.Add(new StagePrototype(20, 20, enemyPrefabs, collectablePrefabs1, challenges, 8, 5, 8, 60, 1, 150, 100, 1, false));
        stages.Add(new StagePrototype(25, 30, enemyPrefabs2, collectablePrefabs1, challenges, 8, 10, 13, 200, 1, 250, 100, 1, false));
        stages.Add(new StagePrototype(25, 30, enemyPrefabs2, collectablePrefabs2, challenges, 12, 10, 15, 200, 1, 300, 100, 1, false));
        stages.Add(new StagePrototype(30, 35, enemyPrefabs3, collectablePrefabs3, challenges, 16, 15, 15, 200, 1, 330, 100, 1, false));
        stages.Add(new StagePrototype(40, 40, enemyPrefabs4, collectablePrefabs4, challenges, 20, 15, 15, 200, 1, 350, 100, 1, false));

        stages.Add(new StagePrototype(20, 20, enemyPrefabsBoss, collectablePrefabs1, challenges, 1, 0, 2, 50, 1, 200, 300, 1, true));

        stages.Add(new StagePrototype(20, 20, enemyPrefabs5, collectablePrefabs4, challenges, 5, 5, 3, 80, 1, 200, 150, 2, false));
        stages.Add(new StagePrototype(40, 40, enemyPrefabs5, collectablePrefabs4, challenges, 20, 15, 6, 200, 1, 250, 100, 2, false));
        stages.Add(new StagePrototype(40, 40, enemyPrefabs5, collectablePrefabs4, challenges, 25, 15, 8, 200, 1, 375, 100, 2, false));
        stages.Add(new StagePrototype(60, 60, enemyPrefabs6, collectablePrefabs4, challenges, 30, 25, 10, 600, 1, 425, 100, 2, false));
        stages.Add(new StagePrototype(60, 60, enemyPrefabs6, collectablePrefabs4, challenges, 35, 25, 10, 600, 1, 590, 100, 2, false));
        stages.Add(new StagePrototype(80, 80, enemyPrefabs6, collectablePrefabs4, challenges, 40, 35, 15, 800, 1, 650, 100, 2, false));

        stages.Add(new StagePrototype(20, 20, enemyPrefabsBoss, collectablePrefabs1, challenges, 1, 0, 2, 50, 1, 200, 300, 2, true));
        /*reszta juz nie chwilowo*/
        Loadtracks();
    }


    Vector3 CalculateRandomPosition(StagePrototype stage)
    {
        float randomX = Random.Range(-stage.Width / 2, stage.Width / 2);
        float randomY = Random.Range(-stage.Height / 2, stage.Height / 2);
        return new Vector3(randomX, randomY, 0);
    }

    public float GetCurrentMutationEnergyCost()
    {
        return stages[_stageIterator].EnergyRequirement;
    }

    public float GetCurrentStageHeight()
    {
        return stages[_stageIterator].Height;
    }

    public float GetCurrentStageWidth()
    {
        return stages[_stageIterator].Width;
    }

    public bool isCurrentStageABossRoom()
    {
        return stages[_stageIterator].IsItBossRoom;
    }

    void GenerateWalls(StagePrototype stage)
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

    private void GenerateFood(StagePrototype currentStage)
    {
        Vector3 position;
        for (int i = 0; i < currentStage.FoodStartNumber; i++)
        {
            position = CalculateRandomPosition(currentStage);
            position.z = 1;
            GameObject go = (GameObject)Instantiate(Resources.Load("FoodPrefab"), position, new Quaternion());
            go.transform.parent = this.transform;
            go.GetComponent<Food>().Player = Player;
            go.GetComponent<Food>().NutritionalValued *= foodNutritionModificator;
        }
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
            Enemy newEnemy = go.GetComponent<Enemy>();
            if (currentStage.IsItBossRoom)
            {
                Boss boss = (Boss)newEnemy;
                boss.NumberOfDrops = 10;
                boss.EnergyDrop = currentStage.EnergyRequirement;
                MutationChoice.GetComponent<MutationChoice>().SetChoices(boss.Choice1, boss.Choice2);
                boss.SetMutationChoice(MutationChoice.GetComponent<MutationChoice>());
                StartCoroutine(ShowBossName(boss.Species));
            }
            go.transform.parent = this.transform;
            newEnemy.Player = Player;
            newEnemy.EnergyDrop *= bacteriaNutritionModificator;
        }
    }

    private void GenerateCollectables(StagePrototype currentStage)
    {
        Vector3 position;
        for (int i = 0; i < currentStage.NumberOfCollectables; i++)
        {
            if (unoccupiedPositions.Count == 0) return;
            position = unoccupiedPositions[unoccupiedPositions.Count - 1];
            unoccupiedPositions.RemoveAt(unoccupiedPositions.Count - 1);
            string collectablePrefab = currentStage.CollectablePrefabs[Random.Range(0, currentStage.CollectablePrefabs.Count)];
            GameObject go = (GameObject)Instantiate(Resources.Load(collectablePrefab), position, new Quaternion());
            go.transform.parent = this.transform;
            go.GetComponent<Collectable>().Player = Player;
        }
    }

    private void GenerateStones(StagePrototype currentStage)
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
            GameObject go = (GameObject)Instantiate(Resources.Load(prefabName), position, new Quaternion(Random.value * 360, Random.value * 360, 0, 1));
            go.transform.parent = this.transform;
        }
    }

    private IEnumerator GenerateChallenge(StagePrototype currentStage)
    {
        yield return 1;
        Challenge challenge = currentStage.PossibleChallenges[Random.Range(0, currentStage.PossibleChallenges.Count)];
        challengeManager.GetComponent<ChallengeManager>().ChangeChallenge(challenge);
        yield return null;
    }

    private IEnumerator ShowBossName(string name)
    {
        bossNamePanel.transform.Find("BossName").GetComponent<TextMeshProUGUI>().text = name;
        bossNamePanel.SetActive(true);
        yield return new WaitForSeconds(BossNameDuration);
        bossNamePanel.SetActive(false);
    }

    private IEnumerator ShowLevelNumber()
    {
        int level = _stageIterator+1;
        levelNumberPanel.transform.Find("LevelNumber").GetComponent<TextMeshProUGUI>().text = "Stage " + level;
        levelNumberPanel.SetActive(true);
        yield return new WaitForSeconds(levelNumberDuration);
        levelNumberPanel.SetActive(false);
    }

    private void ShuffleUnoccupiedPositions()
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

    private void FillUnoccupiedPositions()
    {
        unoccupiedPositions = new List<Vector3>();
        float horizontalLimit = stages[_stageIterator].Width / 2 - 1;   //-1 to not spawn on walls
        float verticalLimit = stages[_stageIterator].Height / 2 - 1;   //-1 to not spawn on walls
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

    private void InitiateBossRoom()
    {
        MutationChoice.GetComponent<MutationChoice>().mutationLocked = true;
        challengeManager.SetActive(false);
        Player.GetComponent<PlayerBacteria>().isEnergyFalling = false;
        wasLastStageABossRoom = true;
    }

    private void CleanAfterBossRoom()
    {
        challengeManager.SetActive(true);
        Player.GetComponent<PlayerBacteria>().isEnergyFalling = true;
        wasLastStageABossRoom = false;
    }

    #region Music Handler

    private void ChooseAndPlayAudioTrack(StagePrototype currentStage)
    {
        if (AvailableTracks.Count == 0)
        {
            AvailableTracks = ((string[])TrackList.Clone()).ToList();
        }
        int trackNumber = Random.Range(0, AvailableTracks.Count);
        myAudio.Stop();
        myAudio.clip = Resources.Load<AudioClip>(AvailableTracks[trackNumber]);
        AvailableTracks.RemoveAt(trackNumber);
        myAudio.loop = true;
        myAudio.Play();
    }

    private void Loadtracks()
    {
        string str = "Bathed_in_the_Light_Calming,Cylinder_Five,Cylinder_One,Infinite_Perspective,Infinite_Perspective_2,Level1,Level5," +
                     "Level5_2,Level6,Martian_Cowboy,Oxygen_Garden,Oxygen_Garden_2,Readers_Do_You_Read";
        /*using (StreamReader sr = new StreamReader(_musicNamesPath))
        {
            str = sr.ReadToEnd();
            TrackList = str.Split(',');
        }*/
        str.Replace(" ", string.Empty);
        TrackList = str.Split(',');

        for (int i = 0; i < TrackList.Length; i++)
        {
            string temp = TrackList[i];
            TrackList[i] = "music/" + temp;
        }

        AvailableTracks = ((string[])TrackList.Clone()).ToList();
    }

    #endregion



    public void GenerateNextStage()
    {
        if (_stageIterator != -1) gameOverScreen.GetComponent<GameOverScreen>().AddStagePrize(stages[_stageIterator].StagePointPrize + StagePrizeBonus);
        if (_stageIterator + 1 >= stages.Count)
        {
            //GameObject winningAlert = GameObject.FindGameObjectsWithTag("WinningAlert")[0];
            //winningAlert.GetComponent<Canvas>().enabled = true;
            gameOverScreen.GetComponent<GameOverScreen>().StartGameOverScript();
            return;
        }
        _stageIterator++;
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        FillUnoccupiedPositions();

        Player.transform.position = new Vector2(0, 0);
        StagePrototype currentStage = stages[_stageIterator];

        Player.GetComponent<PlayerBacteria>().MaxEnergy = currentStage.EnergyRequirement * 1.5f;

        GenerateStones(currentStage);
        GenerateFood(currentStage);
        GenerateWalls(currentStage);
        GenerateEnemies(currentStage);
        GenerateCollectables(currentStage);
        ChooseAndPlayAudioTrack(currentStage);

        if (currentStage.WorldNumber == 1)
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<D2FogsNoiseTexPE>().enabled = false;
            GameObject.FindGameObjectWithTag("Background").GetComponent<SpriteRenderer>().sprite = World1Background;
        }
        else if (currentStage.WorldNumber == 2)
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<D2FogsNoiseTexPE>().enabled = true;
            GameObject.FindGameObjectWithTag("Background").GetComponent<SpriteRenderer>().sprite = World2Background;
        }


        StartCoroutine(SpawnFood());

        if (currentStage.IsItBossRoom) InitiateBossRoom();
        else
        {
            MutationChoice.GetComponent<MutationChoice>().SetRandomChoices();
            if (wasLastStageABossRoom) CleanAfterBossRoom();
            StartCoroutine(GenerateChallenge(currentStage));
            StartCoroutine(ShowLevelNumber());
        }

    }

    IEnumerator SpawnFood()
    {
        int startingStage = _stageIterator;
        while (true)
        {
            if (startingStage != _stageIterator) yield break;
            Vector3 position = CalculateRandomPosition(stages[_stageIterator]);
            position.z = 1;
            GameObject go = (GameObject)Instantiate(Resources.Load("FoodPrefab"), position, new Quaternion());
            go.transform.parent = this.transform;
            go.GetComponent<Food>().Player = Player;
            go.GetComponent<Food>().NutritionalValued *= foodNutritionModificator;
            yield return new WaitForSeconds(stages[_stageIterator].FoodTimeInterval);
        }
    }

}

