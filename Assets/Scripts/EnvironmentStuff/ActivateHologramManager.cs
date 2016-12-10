using UnityEngine;
using System.Collections;

public class ActivateHologramManager : MonoBehaviour {

    public SpriteRenderer hologram;
    public float secsToActivate = 3f;
    public float secsToDeactivate = 3f;

    void Start()
    {
        ActivateHologramNow();
    }

    public void ActivateHologramNow()
    {
        StartCoroutine(ActivateHologram());
    }

    IEnumerator ActivateHologram()
    {
        hologram.enabled = false;
        yield return new WaitForSeconds(secsToActivate);
        hologram.enabled = true;
        yield return new WaitForSeconds(secsToDeactivate);
        hologram.enabled = false;
    }
}
