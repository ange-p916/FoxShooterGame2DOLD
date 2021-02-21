using UnityEngine;
using System.Collections;
using Rewired;
using UnityEngine.SceneManagement;

public class PauseScreenManager : MonoBehaviour {

    public GameObject pauseCanvas;
    public bool isPaused = false;
    Player input;

    void Awake()
    {
        input = ReInput.players.GetPlayer(0);
    }

    public void Update()
    {
        if(input.GetButtonDown("PauseButton"))
        {
            isPaused = !isPaused;
        }

        if(isPaused)
        {
            PlayerDisableUtility.Instance.PlayerAbility(false);
            Time.timeScale = 0f;
            pauseCanvas.SetActive(true);
        }
        else
        {
            PlayerDisableUtility.Instance.PlayerAbility(true);
            Time.timeScale = 1f;
            pauseCanvas.SetActive(false);
        }
    }

    //void OnApplicationQuit()
    //{
    //    foreach (var c in CheckpointManager.Instance.checkpoints)
    //    {
    //        SaveLoad.savedGame.cpsaves.Add(c.hasExited);
    //    }
    //    SaveLoad.OverwriteSave();
    //}

    public void ContinueInGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void ReturnToTitleScreen()
    {
        //foreach (var c in CheckpointManager.Instance.checkpoints)
        //{
        //    SaveLoad.savedGame.cpsaves.Add(c.hasExited);
        //    print("c.hasExited: " + c.hasExited);
        //}

        //foreach (var s in SaveLoad.savedGame.cpsaves)
        //{
        //    print("savedgame bools: " + s);
        //}
        SaveLoad.OverwriteSave();
        SceneManager.LoadScene("MainMenu");
    }


}
