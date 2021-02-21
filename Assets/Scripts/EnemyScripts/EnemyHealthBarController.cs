using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyHealthBarController : MonoBehaviour {

    public Slider healthBar;

    public EnemyBehaviourTemplate thisBoss;

    public bool isBossInitiated = false;
    public bool enableHealthBar = true;
    void Start()
    {
        if(enableHealthBar)
        {
            healthBar.maxValue = thisBoss.health;
        }
        
    }

    void Update()
    {
        if (enableHealthBar)
        {
            if (isBossInitiated)
            {
                healthBar.gameObject.SetActive(true);
                healthBar.value = thisBoss.health;
            }
        }
        

    }
}
