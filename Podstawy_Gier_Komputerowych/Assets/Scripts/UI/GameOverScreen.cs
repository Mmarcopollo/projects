using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public Canvas[] canvas;
    public GameObject background;
    public GameObject gameObjects;
    public int countingSpeed = 5;
    private int _numberOfStagesCompleted = 0;
    private int _stagesScore = 0;
    private int _numberOfChallengesCompleted = 0;
    private int _challengesScore = 0;
    private int _totalScoreNumber;
    private AudioSource audio;
    //public GameObject mutations;

    void Start()
    {
        if(_numberOfStagesCompleted != 14)
        {
            if(FindObjectOfType<PlayerBacteria>() == null)
            {
                audio = GetComponent<AudioSource>();
                audio.Play(0);
            }
            
        }
    }

    IEnumerator TransferPoints(TextMeshProUGUI totalScore, TextMeshProUGUI scoreIncrement, int scoreIncrementNumber)
    {
        WaitForSeconds wait = new WaitForSeconds(0.001f);
        scoreIncrement.text = "+" + scoreIncrementNumber.ToString();
        totalScore.text = "Total Score: \n\n" + _totalScoreNumber.ToString();
        yield return new WaitForSeconds(0.5f);
        while (scoreIncrementNumber>= countingSpeed)
        {
            scoreIncrementNumber-= countingSpeed;
            _totalScoreNumber+= countingSpeed;
            scoreIncrement.text = "+" + scoreIncrementNumber.ToString();
            totalScore.text = "Total Score: \n\n" + _totalScoreNumber.ToString();
            yield return wait;
        }
        _totalScoreNumber += scoreIncrementNumber;
        scoreIncrementNumber = 0;
        scoreIncrement.text = "+" + scoreIncrementNumber.ToString();
        totalScore.text = "Total Score:  \n\n" + _totalScoreNumber.ToString();
        yield return wait;
    }
    
    private IEnumerator AddUpPoints()
    {
        TextMeshProUGUI totalScore = this.transform.Find("TotalScore").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI scoreIncrement = this.transform.Find("ScoreIncrement").GetComponent<TextMeshProUGUI>();
        // _totalScoreNumber = PlayerProgression.score;
        _totalScoreNumber = PlayerPrefs.GetInt("score");
        this.transform.Find("LevelsScore").GetComponent<TextMeshProUGUI>().text = _numberOfStagesCompleted.ToString();
        yield return StartCoroutine(TransferPoints(totalScore, scoreIncrement, _stagesScore));
        yield return new WaitForSeconds(0.5f);

        this.transform.Find("ChallengesScore").GetComponent<TextMeshProUGUI>().text = _numberOfChallengesCompleted.ToString();
        this.transform.Find("ChallengesText").GetComponent<TextMeshProUGUI>().text = "Challenges completed: ";
        yield return StartCoroutine(TransferPoints(totalScore, scoreIncrement, _challengesScore));

        transform.Find("TryAgainButton").gameObject.SetActive(true);
    }

    public void StartGameOverScript()
    {
        gameObjects.gameObject.SetActive(false);
        foreach (var i in canvas) i.gameObject.SetActive(false);
        this.gameObject.SetActive(true);

        foreach(Transform i in background.transform)
        {
            i.gameObject.SetActive(true);
        }

        StartCoroutine(AddUpPoints());
    }

    public void AddStagePrize(int prize)
    {
        _numberOfStagesCompleted++;
        _stagesScore += prize;
    }

    public void AddChallengePrize(int prize)
    {
        _numberOfChallengesCompleted++;
        _challengesScore += prize;
    }
    public void CreditsScene()
    {
        SceneManager.LoadScene("Credits");
    }
    public void TryAgain()
    {
        PlayerProgression.score += _stagesScore;
        PlayerProgression.score += _challengesScore;
        Debug.Log(_stagesScore + " " + _challengesScore);
        PlayerPrefs.SetInt("score", _totalScoreNumber);

        transform.Find("TryAgainButton").gameObject.SetActive(false);
        this.gameObject.SetActive(false);

        Application.LoadLevel(Application.loadedLevel);
    }
}
