using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class UpgradeManager : MonoBehaviour
{
    public GameObject Score;
    public GameObject Content;
    public List<MyDictionary> images;

    [System.Serializable]
    public class MyDictionary
    {
        public string key;
        public Sprite value;
    }

    public void ShowUpgrade(Upgrade upgrade)
    {
        GameObject shopCardGameObject = (GameObject)Instantiate(Resources.Load("ShopCard"), Vector3.zero, new Quaternion());
        shopCardGameObject.transform.SetParent(Content.transform);
        ShopCard shopCard = shopCardGameObject.GetComponent<ShopCard>();
        shopCard.upgradeManager = this.GetComponent<UpgradeManager>();
        shopCard.descriptionTxt.text = upgrade.description;
        shopCard.nameTxt.text = upgrade.name;
        shopCard.upgrade = upgrade;
        shopCard.costTxt.text = "Cost: " + upgrade.cost.ToString();
        Debug.Log(FindIndex(upgrade.name, images));
        shopCard.icon.GetComponent<Image>().sprite = images[FindIndex(upgrade.name, images)].value;
        shopCard.locked.transform.Find("icon (1)").GetComponent<Image>().sprite = images[FindIndex(upgrade.name, images)].value;
    }

    private void ShowAvailableUpgrades()
    {
        foreach (Upgrade upgrade in PlayerProgression.upgradesAvailable) ShowUpgrade(upgrade);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    void Start ()
    {
        // Score.GetComponent<TextMeshProUGUI>().text += PlayerProgression.score.ToString();
        Score.GetComponent<TextMeshProUGUI>().text += PlayerPrefs.GetInt("score");
        ShowAvailableUpgrades();
    }

    private int FindIndex(string value, List<MyDictionary> list)
    {
        int index = 0;
        foreach (var i in list)
        {
            if (i.key.Equals(value))
            {
                return index;
            }

            index++;
        }
        return 0; //default
    }
}
