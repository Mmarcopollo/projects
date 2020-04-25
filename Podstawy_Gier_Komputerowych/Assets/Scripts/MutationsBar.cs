using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutationsBar : MonoBehaviour
{

    public GameObject img;
    public List<GameObject> imgList;
    public int index = -1;

    private void Start()
    {
        int width = Screen.width;
        int height = Screen.height;

        for (int i = 130; i < width - 130; i += 100)
        {
            GameObject newImg = Instantiate(img, new Vector3(i, 33, 0), Quaternion.identity, this.transform);
            newImg.SetActive(false);
            imgList.Add(newImg);
        }
    }
    void Update()
    {

    }

    public int FindIndex()
    {
        int i = 0;
        index = 0;
        foreach (GameObject img in imgList)
        {
            Debug.Log("ala ma kota");
            if (img.activeSelf == false)
            {
                index = i;
                break;
            }
            i++;
        }

        return index;
    }

    public int FindIndex(string name)
    {
        int i = 0;
        index = 0;

        foreach(GameObject img in imgList)
        {
            
        }


        return index;
    }


    public void SetImage(Sprite mainImg)
    {
        Debug.Log(imgList.Count);
        GameObject prefab = imgList[FindIndex()];
        prefab.SetActive(true);
        prefab.GetComponent<MutationBarImg>().SetMainImg(mainImg);
        prefab.GetComponent<MutationBarImg>().SetUpperImg(null);
    }

    public void SetMultipleImage(string name, Sprite img)
    {

    }
}
