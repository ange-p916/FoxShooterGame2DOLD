using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UnlockableManager : MonoBehaviour {

    public GameObject unlockableCanvas;

    public bool hasUnlocked = false;

    void Update()
    {
        if(hasUnlocked)
        {
            unlockableCanvas.SetActive(true);
            StartCoroutine(WaitAbitMang());
        }
        else
        {
            if(unlockableCanvas != null)
            {
                unlockableCanvas.SetActive(false);
            }
            
        }
    }

    IEnumerator WaitAbitMang()
    {
        yield return new WaitForSeconds(3f);
        unlockableCanvas.SetActive(false);
        hasUnlocked = false;
    }

}
