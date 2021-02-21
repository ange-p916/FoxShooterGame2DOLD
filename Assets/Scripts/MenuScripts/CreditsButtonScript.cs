using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CreditsButtonScript : MonoBehaviour {

	public void TitleScreen()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
