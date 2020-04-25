using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectWaving : MonoBehaviour
{

    public float maxY = 5.0f;
    public float y = 5.0f;
    float index;

    float startX;
    float startY;
    public bool play = false;

    public void Start()
    {
        startX = gameObject.GetComponent<RectTransform>().localPosition.x;
        startY = gameObject.GetComponent<RectTransform>().localPosition.y;
    }

    public void Update()
    {
        if (play == true)
        {
            index += Time.unscaledDeltaTime;
            float posY = maxY * Mathf.Sin(index * y);
            transform.localPosition = new Vector3(startX, posY, 0);
        }
        else
        {
            transform.localPosition = new Vector3(startX, startY, 0);
        }
    }
}
