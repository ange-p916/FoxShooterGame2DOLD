using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {


	public void LoadNewGameScene()
    {
        SceneManager.LoadScene("NewGameMenu");
    }

    public void LoadContinueGameScene()
    {
        SceneManager.LoadScene("NewGameMenu");
    }

    public void GoToControlsScene()
    {
        SceneManager.LoadScene("KeybindingScene");
    }

    public void GoToOptionsScene()
    {
        SceneManager.LoadScene("Options");
    }

}
