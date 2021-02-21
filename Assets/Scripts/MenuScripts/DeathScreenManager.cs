using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class DeathScreenManager : MonoBehaviour {

    public GameObject deathScreen;
    public GameObject selectedButton;

    PlayablePlayer player;

    void Start()
    {
        player = FindObjectOfType<PlayablePlayer>();
    }

    void Update()
    {
        if(SceneManager.GetActiveScene().name == "LevelIntro")
        {
            if(!CheckpointManager.Instance.startCinematicStuff && CheckpointManager.Instance.isDead)
            {
                PlayerDisableUtility.Instance.PlayerAbility(false);
                deathScreen.SetActive(true);
                EventSystem.current.SetSelectedGameObject(selectedButton);
            }
            else
            {
                deathScreen.SetActive(false);
            }
        }
        else if(SceneManager.GetActiveScene().buildIndex > 3)
        {
            if (CheckpointManager.Instance.isDead)
            {
                PlayerDisableUtility.Instance.PlayerAbility(false);
                deathScreen.SetActive(true);
                EventSystem.current.SetSelectedGameObject(selectedButton);
            }
            else
            {
                deathScreen.SetActive(false);
            }
        }
        else
        {
            deathScreen.SetActive(false);
        }
    }

    public void ConfirmRespawnButton()
    {
        CheckpointManager.Instance.KillPlayer();
    }

    public void DenyRespawnButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
