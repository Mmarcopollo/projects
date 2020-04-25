using System.Collections.Generic;
using UnityEngine;

public class RadialMenuSpawner : MonoBehaviour
{

    [System.Serializable]
    public class Action
    {
        public GameObject IdleSprite;
        public GameObject Animation;
        public Color Color;
        public Sprite Sprite;
        public string Title;
        public Weapon Weapon;
        public int Number;
        public bool IsChoosen = false;
    }

    public RadialMenu MenuPrefab;
    public string Title;
    public GameObject Background;
    public List<Action> Options;
    public GameObject bossNamePanel;
    public GameObject levelNumberPanel;
    [System.NonSerialized] public bool IsOpen = false;
    [System.NonSerialized] public int choosenButton = 0;


    void Start()
    {
        if (Title == null || Title == "")
            Title = "Weapons";

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && IsOpen == false && GameObject.FindObjectOfType<PlayerBacteria>() != null)
        {
            if (bossNamePanel != null && bossNamePanel.activeSelf) bossNamePanel.SetActive(false);
            if (levelNumberPanel != null && levelNumberPanel.activeSelf) levelNumberPanel.SetActive(false);
            Time.timeScale = 0.3f;
            GetWeapons();
            Background.SetActive(true);
            SpawnMenu();
            IsOpen = true;
        }
    }

    public void SpawnMenu()
    {
        RadialMenu newMenu = Instantiate(MenuPrefab) as RadialMenu;
        newMenu.RadialMenuSpawner = this;
        newMenu.transform.SetParent(transform, false);
        newMenu.transform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0f);
        newMenu.Label.text = Title.ToUpper();
        newMenu.SpawnButtons();
    }

    void GetWeapons()
    {
        Options = new List<Action>();
        int number = 0;
        foreach (var weapon in FindObjectOfType<PlayerBacteria>().CurrentWeapons)
        {
            Debug.Log("RMS Choosen: " + choosenButton);
            if (number == choosenButton)
            {
                Options.Add(new Action()
                {
                    IdleSprite = weapon.IdleSprite,
                    Animation = weapon.Animation,
                    Color = weapon.Color,
                    Title = weapon.Name,
                    Weapon = weapon,
                    Number = number,
                    IsChoosen = true
                });
            }
            else
            {
                Options.Add(new Action()
                {
                    IdleSprite = weapon.IdleSprite,
                    Animation = weapon.Animation,
                    Color = weapon.Color,
                    Title = weapon.Name,
                    Weapon = weapon,
                    Number = number
                });
            }
            number++;
        }
    }
}
