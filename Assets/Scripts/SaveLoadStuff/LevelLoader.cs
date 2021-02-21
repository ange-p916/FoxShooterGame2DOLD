using UnityEngine;
using System.Collections;
using Rewired;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {

    Player input;
    public string levelToLoad = "Level2_1";
    public LayerMask WhatIsPlayer;

    void Start()
    {
        input = ReInput.players.GetPlayer(0);
    }

    void Update()
    {
        var hit = Physics2D.CircleCast(transform.position, 2f, Vector2.zero, 2f, WhatIsPlayer);
        if(hit && input.GetButtonDown("Interact"))
        {
            SceneManager.LoadScene(levelToLoad);
        }
    }
}
