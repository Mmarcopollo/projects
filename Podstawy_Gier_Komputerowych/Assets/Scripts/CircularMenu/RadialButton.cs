using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RadialButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public Image Circle;
    public GameObject IdleSprite;
    public GameObject Animation;
    public string Title;
    public Weapon Weapon;
    public RadialMenu MyMenu;
    public float Speed = 8f;
    public bool isChoosen = false;
    public int ButtonNumber;
    public Color DefaultColor;
    public Color SelectedColor;
    public Color HighlightedColor;

    private GameObject animationInstantion;
    private GameObject idleSpriteInstantion;


    public void Start()
    {
        animationInstantion = Instantiate(Animation, this.transform.position, Quaternion.identity, this.transform);
        animationInstantion.SetActive(false);

        idleSpriteInstantion = Instantiate(IdleSprite, this.transform.position, Quaternion.identity, this.transform);
        idleSpriteInstantion.GetComponent<ObjectWaving>().play = false;

        if (!isChoosen)
            Circle.color = DefaultColor;
        else
            Circle.color = SelectedColor;
    }

    public void Anim()
    {
        StartCoroutine(AnimateButtonIn());
    }

    IEnumerator AnimateButtonIn()
    {
        transform.localScale = Vector3.zero;
        float timer = 0f;
        while (timer < (1 / Speed))
        {
            transform.localScale = Vector3.one * timer * Speed;
            //timer += Time.deltaTime * 3;
            timer += Time.unscaledDeltaTime;
          
            yield return null;
        }

        transform.localScale = Vector3.one;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        MyMenu.SelectedButton = this;
        if (!isChoosen)
        {
            Circle.color = SelectedColor;
        }
        MyMenu.Label.text = MyMenu.SelectedButton.Title;
        animationInstantion.SetActive(true);
        idleSpriteInstantion.GetComponent<ObjectWaving>().play = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isChoosen)
        {
            Circle.color = HighlightedColor;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!isChoosen)
        {
            Circle.color = SelectedColor;
            FindObjectOfType<PlayerBacteria>().FirstChoosenWeapon = Weapon;
            MyMenu.SetChoosenButton(ButtonNumber);
            isChoosen = true;
        }

        /*else if (isChoosen)
        {
            Circle.color = DefaultColor;
            FindObjectOfType<PlayerBacteria>().FirstChoosenWeapon = null;
            MyMenu.SetChoosenButton(-1);
            isChoosen = false;
        }*/
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isChoosen)
        {
            Circle.color = DefaultColor;
        }
        MyMenu.SelectedButton = null;
        MyMenu.Label.text = "WEAPONS";
        animationInstantion.SetActive(false);
        idleSpriteInstantion.GetComponent<ObjectWaving>().play = false;
    }
}
