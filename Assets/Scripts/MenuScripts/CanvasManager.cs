using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour {

    public Canvas healthCanvas;
    public Canvas deathScreen;
    public Canvas pauseScreen;

    void Awake()
    {
        if(SceneManager.GetActiveScene().buildIndex >= 3)
        {
            DontDestroyOnLoad(gameObject);
            //canvas
            DontDestroyOnLoad(healthCanvas);
            DontDestroyOnLoad(deathScreen);
            DontDestroyOnLoad(pauseScreen);
        }
    }

    void OnLevelWasLoaded(int level)
    {
        if(level == 1)
        {
            this.gameObject.SetActive(false);
            deathScreen.gameObject.SetActive(false);
            healthCanvas.gameObject.SetActive(false);
            pauseScreen.gameObject.SetActive(false);
        }
        if(level >= 4)
        {
            this.gameObject.SetActive(true);
            deathScreen.gameObject.SetActive(true);
            healthCanvas.gameObject.SetActive(true);
            pauseScreen.gameObject.SetActive(true);
        }
        
    }

}
