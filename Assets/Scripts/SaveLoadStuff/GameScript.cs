using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class GameScript {

    public static GameScript current;
    public int currentCheckpoint;
    public int currentLevel = 3;
    public int saveId = 0;
    public string saveName = "SaveGame0";
    public bool gamesBeenStarted;
    public static bool gameContinue = false;
    public List<bool> capsulesSaves = new List<bool>();
    public int currentHealth = 3;
    public int maxHealth = 3;

    public GameScript()
    {
        current = this;
        gamesBeenStarted = true;
        gameContinue = false;

        currentHealth = 3;
        maxHealth = 3;

        if (saveId <= 3)
        {
            saveId = 0;
            saveName = "SaveGame" + saveId;
        }
        
        currentCheckpoint = -1;
        currentLevel = 3;
    }
}
