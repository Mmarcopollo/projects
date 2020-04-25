using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject game;
    public GameObject mainMenu;
    public GameObject bubbles;
    public Canvas energyBar;
    public Canvas challenges;
    public Canvas ControlDescription;
    public GameObject settingCanvas;

    void Update ()
    {
		if(Input.GetKeyDown(KeyCode.Escape))
        {
            Resume();
        }
	}

    public void Resume()
    {
        Time.timeScale = 1;
        game.SetActive(true);
        gameObject.SetActive(false);
    }

    public void MainMenu()
    {
        /*Time.timeScale = 1;
        game.SetActive(false);
        mainMenu.SetActive(true);
        gameObject.SetActive(false);
        energyBar.enabled = false;
        challenges.enabled = false;
        Application.LoadLevel(Application.loadedLevel);

        foreach (Transform child in bubbles.transform)
        {
            child.gameObject.SetActive(true);
        } */
        SceneManager.LoadScene("SampleScene");
    }

    public void Settings()
    {
        settingCanvas.GetComponent<Settings>().isMainMenu = false;
        ControlDescription.gameObject.SetActive(false);
        settingCanvas.SetActive(true);
    }
}
