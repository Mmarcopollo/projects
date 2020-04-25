using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacifismChallenge : Challenge
{
    public PacifismChallenge(int pointSize) : base(pointSize)
    {

    }

    public override void InitiateChallenge()
    {
        state = ChallengeState.uncompleted;
        challengeName = "Pacifism";
        description = "Finish stage without killing any other bacteria";
    }

    public override void RefreshChallenge(GameObject obj)
    {
        if (obj == null) return;
        Enemy enemy = obj.GetComponent<Enemy>();
        if (enemy) state = ChallengeState.failed;
        else
        {
            MutationChoice mutationChoice = obj.GetComponent<MutationChoice>();
            if(mutationChoice)
            {
                if (state!=ChallengeState.failed) state = ChallengeState.completed;
            }
        }
    }
}
