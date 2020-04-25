using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealingEnemy : Enemy
{
    private Vector2 lastMovement = new Vector2(0, 0);
    private int iterations = 0;

    protected override void Start()
    {
        base.Start();
    }

    Transform GetClosestEnemy(GameObject[] food)
    {
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = this.transform.position;

        if (food.Length > 0)
        {
            Transform bestTarget = food[0].transform;
            foreach (GameObject potentialTarget in food)
            {
                Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
                float dSqrToTarget = directionToTarget.sqrMagnitude;
                if (dSqrToTarget < closestDistanceSqr)
                {
                    closestDistanceSqr = dSqrToTarget;
                    bestTarget = potentialTarget.transform;
                }
            }
            return bestTarget;
        }
        else return transform;
    }

    protected Vector2 CalculateAfterTheFoodMovement(Transform food)
    {
        Vector2 movement = this.transform.position - food.position;
        return -movement / movement.magnitude;
    }

    protected override void Move()
    {
        Transform food = GetClosestEnemy(GameObject.FindGameObjectsWithTag("Food"));
        Vector2 movement = CalculateAfterTheFoodMovement(food);
        if (iterations > 0)
        {
            iterations--;
            movement = calculateRandomMovement();
        }
        else if (lastMovement.x == transform.position.x && lastMovement.y == transform.position.y)
        {
            iterations += 250;
        }
        lastMovement = this.transform.position;
        Rb2D.AddForce(movement * Speed);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Food"))
        {
            Destroy(col.gameObject);
        }
    }
}

