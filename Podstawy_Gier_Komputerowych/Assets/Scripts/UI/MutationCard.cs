using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class MutationCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    public Image img;
    public TextMeshProUGUI description;
    public Image background;

    public void OnPointerEnter(PointerEventData eventData)
    {
        img.enabled = false;
        description.enabled = true;
        background.color = new Color(1, 1, 1, 0.5647059f);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        img.enabled = false;
        description.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        img.enabled = true;
        description.enabled = false;
        background.color = new Color(0.75f, 0.75f, 0.75f, 0.5647059f);
    }
}
