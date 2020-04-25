using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ShopCard : Selectable
{
    public BaseEventData base_event;
    public GameObject reverse;
    public GameObject icon;
    public GameObject locked;
    public bool isHighlighted = false;

    public TextMeshProUGUI nameTxt;
    public TextMeshProUGUI descriptionTxt;
    public TextMeshProUGUI costTxt;

    public Upgrade upgrade;
    public UpgradeManager upgradeManager;

    //Muation
    //public bool unlocked = false;
    [TextArea]
    public string mutationName;
    [TextArea]
    public string descritption;

    protected override void Start()
    {
        int state = PlayerPrefs.GetInt(nameTxt.text);
        if (state == 1)
        {
            icon.SetActive(true);
            locked.SetActive(false);
        }
        else
        {
            icon.SetActive(false);
            locked.SetActive(true);
        }
    }

    void Update()
    {
        if (IsHighlighted(base_event))
        {
            icon.SetActive(false);
            locked.SetActive(false);
            reverse.SetActive(true);
            isHighlighted = true;
        }
        else
        {
            int state = PlayerPrefs.GetInt(nameTxt.text);
            if (state == 1)
            {
                icon.SetActive(true);
            }
            else
            {
                locked.SetActive(true);
            }

            reverse.SetActive(false);
            isHighlighted = false;
        }
    }

    public void BuyUpgrade()
    {
        int score = PlayerPrefs.GetInt("score");
        if (score < upgrade.cost || PlayerPrefs.GetInt(nameTxt.text) == 1) return;

        score -= upgrade.cost;
        upgrade.isUnlocked = true;
        PlayerProgression.upgradesAvailable.AddRange(upgrade.upgradesGettingUnlocked);
        foreach(Upgrade upgrade in upgrade.upgradesGettingUnlocked) upgradeManager.ShowUpgrade(upgrade);
        upgradeManager.Score.GetComponent<TextMeshProUGUI>().text = "Points: " + score;
        PlayerPrefs.SetInt("score", score);
        PlayerPrefs.SetInt(nameTxt.text, 1);

    }
}
