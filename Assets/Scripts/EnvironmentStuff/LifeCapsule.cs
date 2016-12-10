using UnityEngine;
using System.Collections;

public class LifeCapsule : MonoBehaviour {

    public LayerMask WhatIsPlayer;

    private PlayerHealthController phc;
    public bool hasPickedUpYet = false;
    TypeWriter twman;

    void Start()
    {
        phc = FindObjectOfType<PlayerHealthController>();
        twman = GetComponent<TypeWriter>();
    }

	void Update()
    {
        var boxcastHit = Physics2D.BoxCast(transform.position, transform.localScale, 0, Vector2.zero, transform.localScale.x, WhatIsPlayer);
        if (boxcastHit && !hasPickedUpYet)
        {
            hasPickedUpYet = true;
            phc.maxHealth += 3;
            phc.curHealth = phc.maxHealth;
            this.enabled = false;
            twman.startDialogue = true;
            StartCoroutine(WaitAbitDude());
            SaveLoad.savedGame.capsulesSaves.Add(hasPickedUpYet);
            SaveLoad.savedGame.currentHealth = (int)phc.curHealth;
            SaveLoad.savedGame.maxHealth = (int)phc.maxHealth;
            SaveLoad.OverwriteSave();

        }
    }

    IEnumerator WaitAbitDude()
    {
        yield return new WaitForSeconds(5f);
        this.gameObject.SetActive(false);
    }
}
