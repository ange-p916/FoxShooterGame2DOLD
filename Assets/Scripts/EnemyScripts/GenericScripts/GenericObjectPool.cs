using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GenericObjectPool : MonoBehaviour
{
    GameObject theObj;
    public GameObject newObj;
    public int amount;
    public List<GameObject> objs = new List<GameObject>();

    void Awake()
    {
        Pool();
    }

    public void Pool()
    {
        for (int i = 0; i < amount; i++)
        {
            theObj = Instantiate(newObj);
            theObj.SetActive(false);
            objs.Add(newObj);
        }
    }

}
