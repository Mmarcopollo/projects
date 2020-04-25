using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MutationTutorial : MonoBehaviour
{
    public GameObject Player;
    private PlayerBacteria _playerStats;
    public GameObject _buttonChoice1;
    public GameObject _buttonChoice2;
    public float MutationEnergyCost = 70;
    public GameObject MutationAlert;
    //public StageGenerator StageGenerator;
    public TutorialMaster Tutorial;
    //public Text MutationAlert;
    public List<MyDictionary> icons;
    public List<MyDictionary> playerAfterMutaitionWO; //without attack
    public List<MyDictionary> playerAfterMutationA; //attack

   // private GameObject challengeManager;
    public GameObject energyBar;
    public GameObject player;
    public GameObject hint;
    private GameObject hintInstantiated;

    public Image img1;
    public Image img2;
    public TextMeshProUGUI description1;
    public TextMeshProUGUI description2;

    public bool isLocked = false;

    //tylko na ewaluacje
    //public int NumberOfMutations = 0;

    private Mutation _choice1;
    private Mutation _choice2;
    private bool canMutate = false;
    public bool mutationLocked = false;
    public List<Mutation> Mutations = new List<Mutation>();
    private AudioSource audio;
    private List<string> editableSprites;
    private List<string> chosen;


    [System.Serializable]
    public class MyDictionary
    {
        public string key;
        public Sprite value;
    }

    // Use this for initialization
    void Awake()
    {
        editableSprites = new List<string>();
        chosen = new List<string>();
        //challengeManager = GameObject.Find("Challenges");
        Player = GameObject.FindGameObjectsWithTag("Player")[0];
        _playerStats = Player.GetComponent<PlayerBacteria>();
        Debug.Log(energyBar);
        energyBar.GetComponent<Image>().color = new Color(0.8980392f, 0.8980392f, 0.282353f);

        //to tak tylko tymczasowo, docelowo mutacje beda wczytywane z pliku
        StatMutation mutation = new StatMutation("Streamlined shape", "increases Speed", true);
        mutation.Speed = 0.2f;
        Mutations.Add(mutation);

        /*
        mutation = new StatMutation("Getting bigger", "decreases Speed but increases maximum HP", true);
        mutation.Speed = -2;
        mutation.MaxHp = 20;
        Mutations.Add(mutation);
        
        mutation = new StatMutation("Thinner cell wall", "decreases maximum HP", true);
        mutation.MaxHp = -20;
        Mutations.Add(mutation);
        */

        mutation = new StatMutation("Tougher cell wall", "Increases strength level", true);
        mutation.Strength = 1;
        mutation.upgradeBonus = 1.5f;
        Mutations.Add(mutation);
        editableSprites.Add("Tougher cell wall");

        mutation = new StatMutation("Thicker capsule", "Increases maximum health level", true);
        mutation.MaxHp = 20;
        mutation.upgradeBonus = 1.5f;
        Mutations.Add(mutation);
        editableSprites.Add("Thicker capsule");

        mutation = new StatMutation("Energy management", "Energy will decrease at lower rate", true);
        mutation.EnergyDecrement = -1;
        mutation.upgradeBonus = 1.5f;
        Mutations.Add(mutation);

        mutation = new StatMutation("Aggressive behaviour", "Strength will be increased, but energy consumption will rise as well", true);
        mutation.EnergyDecrement = 1;
        mutation.Strength = 1;
        mutation.upgradeBonus = 1.5f;
        Mutations.Add(mutation);
        editableSprites.Add("Aggressive behaviour");

        mutation = new StatMutation("Superior vascular system", "Extends positive and decreases negative effects of collected items", true);
        mutation.BonusCollTime = 1;
        mutation.upgradeBonus = 1.5f;
        Mutations.Add(mutation);

        SizeMutation mutation2 = new SizeMutation("Gigantism", "Bacteria size will be increased, alongside decreased speed", true);
        mutation2.AmountToScale = 1.2f;
        mutation2.Speed = -0.15f;
        mutation2.upgradeBonus = 1.5f;
        Mutations.Add(mutation2);

        mutation2 = new SizeMutation("Dwarfism", "Bacteria size will be decreased, alongside increased speed", true);
        mutation2.AmountToScale = 0.8f;
        mutation2.Speed = 0.2f;
        mutation2.upgradeBonus = 1.5f;
        Mutations.Add(mutation2);

        AddWeaponMutation mutation3;
        mutation3 = new AddWeaponMutation("Deadly mines", "Bacteria will be able to leave mines behind it", true,
            Instantiate(Resources.Load<Weapon>("Weapons/MinesWeapon")));
        Mutations.Add(mutation3);

        audio = GetComponent<AudioSource>();

        SetRandomChoices();
    }

    public void SetRandomChoices()
    {
        _choice1 = Mutations[Random.Range(0, Mutations.Count)];
        _choice2 = Mutations[Random.Range(0, Mutations.Count)];
        while (_choice1 == _choice2)
        {
            _choice2 = Mutations[Random.Range(0, Mutations.Count)];
        }

        Debug.Log(_choice1);
    }

    public void SetChoices(Mutation mutation1, Mutation mutation2)
    {
        _choice1 = mutation1;
        _choice2 = mutation2;
    }

    public void ShowChoice()
    {
        //challengeManager.GetComponent<ChallengeManager>().Refresh(this.gameObject);
        Time.timeScale = 0;
        _buttonChoice1.GetComponent<Button>().onClick.AddListener(delegate { Mutate(_choice1); });
        _buttonChoice2.GetComponent<Button>().onClick.AddListener(delegate { Mutate(_choice2); });

        this.GetComponent<Canvas>().enabled = true;
        _buttonChoice1.GetComponent<Button>().enabled = true;
        _buttonChoice2.GetComponent<Button>().enabled = true;

        img1.sprite = icons[FindIndex(_choice1.Name, icons)].value;
        img2.sprite = icons[FindIndex(_choice2.Name, icons)].value;
        
        description1.text = _choice1.Description;
        description2.text = _choice2.Description;

    }

    // Update is called once per frame
    void Update()
    {
        if (isLocked)
        {
            return;
        }
        if (_playerStats.CurrentEnergy >= Tutorial.GetCurrentMutationEnergyCost())
        {
            MutationAlert.gameObject.SetActive(true);
            energyBar.GetComponent<Image>().color = new Color(0.3921569f, 0.9254902f, 0.2313726f);
            if (!audio.isPlaying)
            {
                audio.Play(0);
            }

        }
        else if (_playerStats.CurrentEnergy < Tutorial.GetCurrentMutationEnergyCost())
        {
            MutationAlert.gameObject.SetActive(false);
            energyBar.GetComponent<Image>().color = new Color(0.8980392f, 0.8980392f, 0.282353f);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_playerStats.CurrentEnergy > Tutorial.GetCurrentMutationEnergyCost())
            {
                _playerStats.CurrentEnergy -= Tutorial.GetCurrentMutationEnergyCost();
                ShowChoice();
            }
        }
    }

    public void Mutate(Mutation choice)
    {
        choice.Mutate(_playerStats);

        _buttonChoice1.GetComponent<Button>().onClick.RemoveAllListeners();
        _buttonChoice2.GetComponent<Button>().onClick.RemoveAllListeners();

        _buttonChoice1.GetComponent<Button>().enabled = false;
        _buttonChoice2.GetComponent<Button>().enabled = false;

        this.GetComponent<Canvas>().enabled = false;
        _playerStats.ChangeHealth(_playerStats.MaxHp - _playerStats.CurrentHp);

        Tutorial.EndLevel();
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
        return -1;
    }
}
