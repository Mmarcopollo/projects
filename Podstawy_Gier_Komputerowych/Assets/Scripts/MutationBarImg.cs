using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MutationBarImg : MonoBehaviour
{

    public Image mainImg;
    public Image upperImg;

    public void SetMainImg(Sprite sprite)
    {
        mainImg.sprite = sprite;
    }

    public void SetUpperImg(Sprite sprite)
    {
        if (sprite == null)
        {
            upperImg.color = new Color(0, 0, 0, 0);
        }
        else
        {
            upperImg.sprite = sprite;
        }

    }
}
