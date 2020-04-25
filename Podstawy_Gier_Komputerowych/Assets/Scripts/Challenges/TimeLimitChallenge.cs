using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLimitChallenge : Challenge
{
    private int timeLimit;
    private int relativeStage = 0;
    //private ChallengeManager challengeManager = GameObject.Find("Challenges").GetComponent<ChallengeManager>();
    bool hasTimePassed = false;

    public TimeLimitChallenge(int timeLimit, int pointPrize) : base(pointPrize)
    {
        this.timeLimit = timeLimit;
    }

    private IEnumerator WaitUntilTimeLimitPasses()
    {
        int currentStage = relativeStage;
        yield return new WaitForSeconds(timeLimit);
        if (currentStage != relativeStage) yield break;
        hasTimePassed = true;
        GameObject challengesGO = GameObject.Find("Challenges");
        if(challengesGO != null) challengesGO.GetComponent<ChallengeManager>().Refresh(null);
        yield return null;
    }

    public override void InitiateChallenge()
    {
        state = ChallengeState.uncompleted;
        hasTimePassed = false;
        challengeName = "Need for speed";
        description = "Finish stage in "+timeLimit+" seconds";
        SceneController.instance.StartCoroutine(WaitUntilTimeLimitPasses());
    }

    public override void RefreshChallenge(GameObject obj)
    {
        if (hasTimePassed) state = ChallengeState.failed;
        if (!obj) return;
        MutationChoice mutationChoice = obj.GetComponent<MutationChoice>();
        if (mutationChoice)
        {
            if (state != ChallengeState.failed)
            {
                relativeStage++;
                state = ChallengeState.completed;
            }
        }
    }
}
