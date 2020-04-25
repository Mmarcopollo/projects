using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ChallengeState
{
    completed,
    uncompleted,
    failed
}

public abstract class Challenge
{
    public string challengeName;
    public string description;
    public Sprite sprite;
    //public Sprite spriteFill;
    protected int pointPrize;
    public ChallengeState state = ChallengeState.uncompleted;
 
    public Challenge(int pointPrize)
    {
        this.pointPrize = pointPrize;
    }

    public int getPointPrize()
    {
        return pointPrize;
    }

    public abstract void InitiateChallenge();
    public abstract void RefreshChallenge(GameObject obj);
}
