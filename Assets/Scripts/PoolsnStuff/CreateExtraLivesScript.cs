using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreateExtraLivesScript : MonoBehaviour
{

    GameObject newObj;
    public List<GameObject> healthFabs = new List<GameObject>();

    public static CreateExtraLivesScript Instance;
    public int extraHealthCountInstantiate; //amount
    public Transform healthPosition;
    public GameObject healthPrefab;

    void Awake()
    {
        Instance = this;
        for (int i = 0; i < extraHealthCountInstantiate; i++)
        {
            newObj = Instantiate(healthPrefab);
            newObj.SetActive(false);
            healthFabs.Add(newObj);
        }
    }

    public void CreateLives()
    {
        for (int i = 0; i < extraHealthCountInstantiate; i++)
        {
            if (!healthFabs[i].activeInHierarchy)
            {
                healthFabs[i].SetActive(true);
                healthFabs[i].transform.position = healthPosition.position;
                break;
            }
        }
    }

}
