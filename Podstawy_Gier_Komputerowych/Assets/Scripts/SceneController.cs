using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class exists so that you can call coroutine from not MonoBehaviour class using it

public class SceneController : MonoBehaviour
{
    public static SceneController instance = new GameObject("SceneController").AddComponent<SceneController>();

    private void Start()
    {
        GameObject.DontDestroyOnLoad(instance);
    }
}
