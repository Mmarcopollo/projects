using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject pauseMenu;
    public GameObject alert;
    public Dropdown resolution;
    public Toggle check;
    public Toggle hint;
    public Dropdown qualityDropdown;
    public AudioMixer audioMixer;
    public Slider volumeSlider;

    public bool isMainMenu;
    public bool hintsEnabled = true;

    #region prefs

    private const string HEIGHT_PREF = "height";
    private const string WIDTH_PREF = "width";
    private const string FULLSCREEN_PREF = "fullscreen";
    private const string QUALITY_PREF = "quality";
    private const string VOLUME_PREF = "volume";
    private const string HINTS_PRED = "hints";

    #endregion


    void Start()
    {
        Resolution[] resolutions = Screen.resolutions;
        resolution.ClearOptions();
        List<string> options = new List<string>();

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            if (!options.Contains(option))
            {
                options.Add(option);
            }
        }

        resolution.AddOptions(options);
        resolution.RefreshShownValue();

        int currentRes = 0;
        int height = GetPref("height");
        int width = GetPref("width");

        if (height == 0)
        {
            height = Screen.currentResolution.height;
            SetPref("height", height);
        }
        if (width == 0)
        {
            width = Screen.currentResolution.width;
            SetPref("width", width);
        }

        for (int i = 0; i < resolution.options.Capacity; i++)
        {
            if (resolution.options[i].text.Equals(height + "x" + width))
            {
                currentRes = i;
            }
        }

        resolution.value = currentRes;
        resolution.RefreshShownValue();

        SetFullscreen(GetBoolValue("fullscreen"));
        SetQuality(GetPref("quality"));
        SetVolume(GetFloatPref("volume"));
        volumeSlider.value = GetFloatPref("volume");

        SetHints(GetBoolValue("hints"));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isMainMenu)
        {
            gameObject.SetActive(false);
            mainMenu.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !isMainMenu)
        {
            gameObject.SetActive(false);
            pauseMenu.SetActive(true);
        }
    }

    public void SetResolution(int index)
    {
        string option = resolution.options[index].text;
        string[] splited = option.Split('x');

        Screen.SetResolution(int.Parse(splited[0]), int.Parse(splited[1]), Screen.fullScreen);
        SetPref("height", int.Parse(splited[0]));
        SetPref("width", int.Parse(splited[1]));
    }

    public void SetQuality(int index)
    {
        Debug.Log(index);
        QualitySettings.SetQualityLevel(index);
        SetPref("quality", index);
        qualityDropdown.value = index;
        qualityDropdown.RefreshShownValue();
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
        SetFloatPref("volume", volume);
    }

    public void SetFullscreen(bool isFull)
    {
        Screen.fullScreen = isFull;
        check.isOn = isFull;
        SetBoolPref("fullscreen", isFull);
    }

    public void SetHints(bool enabled)
    {
        hintsEnabled = enabled;
        hint.isOn = enabled;
        SetBoolPref("hints", enabled);
    }

    public void ResetGameButton()
    {
        alert.SetActive(true);
    }

    #region prefs

    private void SetPref(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
    }

    private int GetPref(string key)
    {
        return PlayerPrefs.GetInt(key);
    }

    private void SetBoolPref(string key, bool value)
    {
        int booleanValue = value == true ? 1 : 0;
        PlayerPrefs.SetInt(key, booleanValue);
    }

    private bool GetBoolValue(string key)
    {
        return PlayerPrefs.GetInt(key) == 1 ? true : false;
    }

    private void SetStringPref(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
    }

    private string GetStringPref(string key)
    {
        return PlayerPrefs.GetString(key);
    }

    private void SetFloatPref(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
    }

    private float GetFloatPref(string key)
    {
        return PlayerPrefs.GetFloat(key);
    }

    #endregion
}

