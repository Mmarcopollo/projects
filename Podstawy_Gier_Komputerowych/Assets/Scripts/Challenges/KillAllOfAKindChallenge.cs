using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillAllOfAKindChallenge : Challenge
{
    private string species;
    private int numberOfEnemiesLeft = 0;

    public KillAllOfAKindChallenge(int pointPrize) : base(pointPrize)
    {

    }

    public override void InitiateChallenge()
    {
        state = ChallengeState.uncompleted;
        numberOfEnemiesLeft = 0;
        challengeName = "Genocide";
        description = "Kill all enemies of species: ";
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        List<Enemy> allSpecies = new List<Enemy>();
        foreach(GameObject enemyGameObject in enemies)
        {
            Enemy enemy = enemyGameObject.GetComponent<Enemy>();
            bool isThereEnemyOfThatSpecies = false;
            foreach(Enemy enemySpeciesExample in allSpecies)
            {
                if(enemySpeciesExample.Species == enemy.Species) isThereEnemyOfThatSpecies = true;
            }
            if (!isThereEnemyOfThatSpecies) allSpecies.Add(enemy);
        }
        Enemy targetSpecies = allSpecies[Random.Range(0, allSpecies.Count)];
        species = targetSpecies.Species;
        sprite = targetSpecies.GetComponent<SpriteRenderer>().sprite;
        //spriteFill = targetSpecies.BloodFill.sprite;
        //Vector2 fillSize = targetSpecies.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta;
        //fillScale = targetSpecies.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().localScale;
       // Debug.Log("renderer size x " + targetSpecies.gameObject.GetComponent<SpriteRenderer>().bounds.size.x * targetSpecies.transform.localScale.x);
        //Debug.Log("renderer size y " + targetSpecies.gameObject.GetComponent<SpriteRenderer>().bounds.size.y * targetSpecies.transform.localScale.y);

        //Debug.Log("fill size x " + fillSize.x);
        //Debug.Log("fill size y " + fillSize.y);
        //spriteWidthRatio = (fillSize.x *5 /** fillScale.x * 5*/) / targetSpecies.gameObject.GetComponent<SpriteRenderer>().size.x;
        //spriteHeightRatio = (fillSize.y *5 /** fillScale.y * 5*/) / targetSpecies.gameObject.GetComponent<SpriteRenderer>().size.y;
        description += species;
        foreach (GameObject enemyGameObject in enemies)
        {
            Enemy enemy = enemyGameObject.GetComponent<Enemy>();
            if (enemy.Species == species) numberOfEnemiesLeft++;
        }
    }

    public override void RefreshChallenge(GameObject obj)
    {
        Enemy enemy = null;
        if (obj)  enemy = obj.GetComponent<Enemy>();
        if(enemy)
        {
            if (enemy.Species == species)
            {
                numberOfEnemiesLeft--;
                if (numberOfEnemiesLeft == 0) state = ChallengeState.completed;
            }
        }
    }
}
