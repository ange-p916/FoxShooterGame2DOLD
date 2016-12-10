using UnityEngine;
using System.Collections;

public class GameStateManager : MonoBehaviour {

    public PlayablePlayer mPlayer;
    public MetroidCamera mCamera;
    public static GameStateManager Instance;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        if(Instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

    }


    void OnLevelWasLoaded(int level)
    {
        if(level == 1)
        {
            mPlayer = FindObjectOfType<PlayablePlayer>();
            mCamera = FindObjectOfType<MetroidCamera>();

            mPlayer.enabled = false;
            mCamera.enabled = false;
            
        }

        if (level >= 3)
        {
            mPlayer = FindObjectOfType<PlayablePlayer>();
            mCamera = FindObjectOfType<MetroidCamera>();

            mPlayer.enabled = true;
            mCamera.enabled = true;
        }
    }
}
