using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialMenu : MonoBehaviour
{
    public Text Label;
    public RadialButton ButtonPrefab;
    [System.NonSerialized] public RadialButton SelectedButton;
    [System.NonSerialized] public RadialMenuSpawner RadialMenuSpawner;
    [System.NonSerialized] public int ChoosenButton;
    private List<RadialButton> _buttonList = new List<RadialButton>();

    // Use this for initialization
    public void SpawnButtons()
    {
        StartCoroutine(AnimateButtons());
    }

    System.Collections.IEnumerator AnimateButtons()
    {
        for (int i = 0; i < RadialMenuSpawner.Options.Count; i++)
        {
            RadialButton newButton = Instantiate(ButtonPrefab) as RadialButton;
            newButton.transform.SetParent(transform, false);
            float theta = (2 * Mathf.PI / RadialMenuSpawner.Options.Count) * i;
            float xPos = Mathf.Sin(theta);
            float yPos = Mathf.Cos(theta);
            newButton.transform.localPosition = new Vector3(xPos, yPos, 0f) * 200f;
            //
            newButton.IdleSprite = RadialMenuSpawner.Options[i].IdleSprite;
            newButton.Animation = RadialMenuSpawner.Options[i].Animation;
            newButton.Animation.transform.position = newButton.transform.position;
            newButton.Title = RadialMenuSpawner.Options[i].Title.ToUpper();
            newButton.Weapon = RadialMenuSpawner.Options[i].Weapon;
            newButton.ButtonNumber = RadialMenuSpawner.Options[i].Number;
            newButton.isChoosen = RadialMenuSpawner.Options[i].IsChoosen;
            newButton.DefaultColor = RadialMenuSpawner.Options[i].Color;
            newButton.MyMenu = this;
            newButton.Anim();
            _buttonList.Add(newButton);
            yield return new WaitForSeconds(0.06f);
        }
    }

    public void SetChoosenButton(int number)
    {
        RadialMenuSpawner.choosenButton = number;
        if (number != -1)
            foreach (var button in _buttonList)
            {
                if (button.ButtonNumber != number)
                {
                    button.Circle.color = button.DefaultColor;
                    button.isChoosen = false;
                }
            }
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Tab) && RadialMenuSpawner.IsOpen == true)
        {
            RadialMenuSpawner.Background.SetActive(false);
            RadialMenuSpawner.IsOpen = false;
            Time.timeScale = 1.0f;
            Destroy(gameObject);
        }

        if (FindObjectOfType<GameOverScreen>() != null)
        {
            if (GameObject.FindObjectOfType<GameOverScreen>().isActiveAndEnabled)
            {
                RadialMenuSpawner.Background.SetActive(false);
                RadialMenuSpawner.IsOpen = false;
                Destroy(gameObject);
            }
        }

        if (Time.timeScale == 0)
        {
            RadialMenuSpawner.Background.SetActive(false);
            RadialMenuSpawner.IsOpen = false;
            Destroy(gameObject);
        }
    }
}
