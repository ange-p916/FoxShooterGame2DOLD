using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using Rewired;
public class LoadMenuScript : MonoBehaviour {
    public Image confirmDeletePanel;
    public Image confirmNewGamePanel;
    Player input;
    public GameObject chooseButton;

    void Awake()
    {
        input = ReInput.players.GetPlayer(0);
        
    }
    
    void Update()
    {
        if (input.GetButtonDown("UICancel"))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void ConfirmDelete()
    {

    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ReturnToScreen(GameObject selectableObject)
    {
        confirmNewGamePanel.gameObject.SetActive(false);
        EventSystem.current.SetSelectedGameObject(selectableObject);
    }

    public void DenyDelete(GameObject selectableObject)
    {
        confirmDeletePanel.gameObject.SetActive(false);
        EventSystem.current.SetSelectedGameObject(selectableObject);
        
    }

    public void ConfirmNewSaveGame()
    {
        GameScript.current = new GameScript();
        //print("creating a new level " + GameScript.current.currentLevel);
        //print("creating a new checkpoint " + GameScript.current.currentCheckpoint);
        SaveLoad.NewSave();
        SaveLoad.Load();
        SceneManager.LoadScene(GameScript.current.currentLevel);

        //print("Level: " + GameScript.current.currentLevel + " " +
        //    "Checkpoint: " + GameScript.current.currentCheckpoint + " " +
        //    "SaveName: " + GameScript.current.saveName + " " +
        //    "SaveID: " + GameScript.current.saveId);

        //print("(at newsavegame) Game has been started: " + GameScript.current.gamesBeenStarted);
    }

    public void PlayGame()
    {
        SaveLoad.NewSave();
        SceneManager.LoadScene("LevelIntro");
    }

    public void SaveButton1(GameObject selectableObject)
    {
        confirmNewGamePanel.gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(selectableObject);
        //GameScript.current = new GameScript();
        //SaveLoad.Save();
    }

    public void SaveButton2(GameObject selectableObject)
    {
        /* TODO if save file exists, prompt for continue
        * else prompt for start a new game
        */
        if(GameScript.current == null)
        {
            confirmNewGamePanel.gameObject.SetActive(true);
            EventSystem.current.SetSelectedGameObject(selectableObject);
        }
        else
        {
            confirmNewGamePanel.gameObject.SetActive(true);
            EventSystem.current.SetSelectedGameObject(selectableObject);
            SaveLoad.Load();
        }
        
    }

    public void SaveButton3(GameObject selectableObject)
    {
        confirmNewGamePanel.gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(selectableObject);

    }

    public void Continue()
    {
        SaveLoad.Load();
        GameScript.current = SaveLoad.savedGame;
        GameScript.gameContinue = true;
        GameScript.current.gamesBeenStarted = false;
        SceneManager.LoadScene(SaveLoad.savedGame.currentLevel);
        //print("(at continue) Game has been loaded: " + SaveLoad.savedGame.gamesBeenStarted);
    }

}
