using System.Collections.Generic;

public class StagePrototype
{
    public float Height;
    public float Width;
    public List<string> EnemyPrefabs;
    public List<string> CollectablePrefabs;
    public List<Challenge> PossibleChallenges;
    public int NumberOfEnemies;
    public int NumberOfCollectables;
    public int NumberOfStones;
    public int FoodStartNumber;
    public float FoodTimeInterval;
    public float EnergyRequirement;
    public int StagePointPrize;
    public int WorldNumber;
    public bool IsItBossRoom = false;
    

    public StagePrototype(float height, float width, List<string> enemyPrefabs, List<string> collectablePrefabs,
        List<Challenge> possibleChallenges, int numberOfEnemies, int numberOfCollectables, int numberOfStones, int foodStartNumber,
        float foodTimeInterval, float energyRequirement, int stagePointPrize, int worldNumber, bool isItBossRoom)
    {
        Height = height;
        Width = width;
        EnemyPrefabs = enemyPrefabs;
        CollectablePrefabs = collectablePrefabs;
        PossibleChallenges = possibleChallenges;
        NumberOfEnemies = numberOfEnemies;
        NumberOfCollectables = numberOfCollectables;
        NumberOfStones = numberOfStones;
        FoodStartNumber = foodStartNumber;
        FoodTimeInterval = foodTimeInterval;
        EnergyRequirement = energyRequirement;
        StagePointPrize = stagePointPrize;
        WorldNumber = worldNumber;
        IsItBossRoom = isItBossRoom;
    }
}
