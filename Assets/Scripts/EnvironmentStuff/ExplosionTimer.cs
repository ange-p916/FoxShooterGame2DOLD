using UnityEngine;
using System.Collections;

public class ExplosionTimer : MonoBehaviour {

    public float timer = 0f;
    public float explodeDuration = 0.04f;

    void OnEnable()
    {
        timer = 0;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= explodeDuration)
        {
            gameObject.SetActive(false);

        }
    }
}
