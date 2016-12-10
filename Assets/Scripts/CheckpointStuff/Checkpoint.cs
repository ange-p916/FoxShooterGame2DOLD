using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Rewired;
using UnityEngine.UI;

public class Checkpoint : MonoBehaviour
{
    public Text saveImg;

    tk2dSpriteAnimator anim;
    private float radius = 2f;
    private Vector2 direction = new Vector2(1f, 1f);
    private float distance = 2f;
    public LayerMask WhatIsPlayer;
    PlayablePlayer player;
    Player input;
    public int checkpointID;

    public bool inside = false;

    PlayerHealthController phc;

    void Awake()
    {
        anim = GetComponent<tk2dSpriteAnimator>();
        player = FindObjectOfType<PlayablePlayer>();
        input = ReInput.players.GetPlayer(0);
        phc = FindObjectOfType<PlayerHealthController>();
    }

    void Update()
    {
        //RaycastHit2D hit = Physics2D.BoxCast(transform.position, transform.localScale, 0, Vector2.zero, transform.localScale.x, WhatIsPlayer);
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, radius, direction, distance, WhatIsPlayer);
        


        if (hit && checkpointID >= -1 && input.GetButtonDown("Interact"))
        {
            //hasExited = true;
            phc.curHealth = phc.maxHealth;

            StartCoroutine(DoCheckpointStuff(3f));

            SaveLoad.savedGame.currentCheckpoint = checkpointID;
            SaveLoad.savedGame.currentLevel = SceneManager.GetActiveScene().buildIndex;
            SaveLoad.savedGame.currentHealth = (int)phc.curHealth;
            SaveLoad.savedGame.maxHealth = (int)phc.maxHealth;
            SaveLoad.OverwriteSave();
            //print("sl sg cp: " + SaveLoad.savedGame.currentCheckpoint);
            anim.Play("CheckpointEnd");
        }
        else if(hit)
        {
            anim.Play("CheckpointMiddle");
            anim.AnimationCompleted = null;
        }
        else
        {
            anim.Play("CheckpointIdle");
        }
    }

    public void SpawnPlayerAtCheckpoint(PlayablePlayer player)
    {
        player = FindObjectOfType<PlayablePlayer>();
        player.RespawnAt(transform);
    }

    IEnumerator DoCheckpointStuff(float theTimer)
    {
        saveImg.enabled = true;
        yield return new WaitForSeconds(theTimer);
        saveImg.enabled = false;
    }

}
