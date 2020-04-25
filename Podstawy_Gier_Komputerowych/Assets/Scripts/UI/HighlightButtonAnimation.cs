using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HighlightButtonAnimation : Selectable
{

    public BaseEventData base_event;
    public GameObject anim = null;
    public bool isHighlighted = false;

    void Update ()
    {
		if(IsHighlighted(base_event))
        {
            anim.SetActive(true);
            isHighlighted = true;
        }
        else
        {
            anim.SetActive(false);
            isHighlighted = false;
        }
	}
}
