using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class KeyDoneScript : MonoBehaviour {

	public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
