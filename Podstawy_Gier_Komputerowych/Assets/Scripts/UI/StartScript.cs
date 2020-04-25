using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{
    public bool IsStarted = false;
    public Canvas ControlDescription;
    public GameObject settingCanvas;
    public Canvas[] canvas;
    public GameObject gameObjects;
    public GameObject mutations;
    public AudioMixer audioMixer;
    public GameObject hintControls;
    public GameObject settings;
    public AudioSource myAudio;

    // Use this for initialization
    void Start()
    {
            Time.timeScale = 1;

        foreach (var i in canvas)
        {
            i.enabled = false;
        }

        gameObjects.gameObject.SetActive(false);
        mutations.gameObject.SetActive(false);
        settingCanvas.gameObject.SetActive(false);
        mutations.GetComponent<Canvas>().enabled = true;
        audioMixer.SetFloat("volume", PlayerPrefs.GetFloat("volume"));
    }

    public void InitiateUpgrades()
    {
        foreach (Upgrade upgrade in PlayerProgression.upgradesAvailable.ToArray())
        {
            if (upgrade.isUnlocked) upgrade.Initialize();
        }
    }

    public void StartGame()
    {
        myAudio = GetComponent<AudioSource>();
        myAudio.Stop();
        Time.timeScale = 1;
        ControlDescription.gameObject.SetActive(false);

        foreach (var i in canvas)
        {
            i.enabled = true;
        }
        gameObjects.gameObject.SetActive(true);
        mutations.gameObject.SetActive(true);
        mutations.GetComponent<Canvas>().enabled = false;
        InitiateUpgrades();
        GameObject.Find("MutationsChoice").GetComponent<MutationChoice>().InitiateMutations();
        GameObject.Find("StageGenerator").GetComponent<StageGenerator>().GenerateNextStage();

        if (settings.GetComponent<Settings>().hintsEnabled == true)
        {
            Instantiate(hintControls);
        }
    }

    public void BuyUpgrades()
    {
        SceneManager.LoadScene("Upgrades");
    }
    public void StartTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void Settings()
    {
        settingCanvas.GetComponent<Settings>().isMainMenu = true;
        ControlDescription.gameObject.SetActive(false);
        settingCanvas.SetActive(true);
    }
}
