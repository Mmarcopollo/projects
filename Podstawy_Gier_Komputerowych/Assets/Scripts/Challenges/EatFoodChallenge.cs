using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatFoodChallenge : Challenge
{
    private int foodEaten = 0;
    private int foodLimit;

    public EatFoodChallenge(int foodLimit, int pointPrize) : base(pointPrize)
    {
        this.foodLimit = foodLimit;
    }

    public override void InitiateChallenge()
    {
        foodEaten = 0;
        state = ChallengeState.uncompleted;
        challengeName = "Gluttony";
        description = "Eat " + foodLimit +" proteins";
    }

    public override void RefreshChallenge(GameObject obj)
    {
        if (obj!= null && obj.tag == "Food")
        {
            foodEaten++;
            if (foodEaten >= foodLimit) state = ChallengeState.completed;
        }
    }
}
