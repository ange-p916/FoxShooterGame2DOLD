using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public class CheckpointManager : MonoBehaviour
{
    PlayerHealthController phc;
    private MakeEnemiesRespawn enemyRespawner;
    public static CheckpointManager Instance;
    public List<Checkpoint> checkpoints = new List<Checkpoint>();
    PlayablePlayer player;
    public int currentCheckPointIndex;
    public bool isDead = false;
    public bool startCinematicStuff = false;
    public bool twhasfinished = false;

    List<LifeCapsule> capsules = new List<LifeCapsule>();

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        int j = 0;
        
        GameScript.current = SaveLoad.savedGame;
        phc = FindObjectOfType<PlayerHealthController>();
        enemyRespawner = GetComponent<MakeEnemiesRespawn>();
        //checkpoints
        checkpoints = FindObjectsOfType<Checkpoint>().OrderBy(t => t.gameObject.name).ToList();
        //life capsules
        capsules = FindObjectsOfType<LifeCapsule>().OrderBy(l => l.gameObject.name).ToList();


        //if continuing game
        if (GameScript.gameContinue)
        {
            //loop over all capsules
            for (int i = 0; i < capsules.Count; i++)
            {
                //capsules[i].hasPickedUpYet = SaveLoad.savedGame.capsulesSaves[i];
                //if capsules exists and are saved, set them to false
                if(SaveLoad.savedGame.capsulesSaves.Count > 0)
                {
                    if (SaveLoad.savedGame.capsulesSaves[i])
                    {
                        //gameobjects in the game
                        capsules[i].gameObject.SetActive(false);
                    }
                }
                else
                {
                    break;
                }
                
            }

            //for (int i = 0; i < checkpoints.Count; i++)
            //{
            //    checkpoints[i].hasExited = SaveLoad.savedGame.cpsaves[i];
            //}
            phc.curHealth = SaveLoad.savedGame.currentHealth;
            phc.maxHealth = SaveLoad.savedGame.maxHealth;
        }

        //cam = FindObjectOfType<CameraScript>();
        player = FindObjectOfType<PlayablePlayer>();
        if(GameScript.current.gamesBeenStarted)
        {
            SaveLoad.savedGame.currentCheckpoint = 0;
        }
        j = SaveLoad.savedGame.currentCheckpoint;
        //print("cpman, new game?: " + GameScript.current.gamesBeenStarted);

        if (SaveLoad.savedGame.currentCheckpoint >= 1 && GameScript.gameContinue)
        {
            checkpoints[SaveLoad.savedGame.currentCheckpoint].SpawnPlayerAtCheckpoint(player);
            //print("spawned at " + GameScript.current.currentCheckpoint);
        }
        else if(checkpoints[j].checkpointID >= 1 && GameScript.gameContinue)
        {
            checkpoints[SaveLoad.savedGame.currentCheckpoint].SpawnPlayerAtCheckpoint(player);
            //print("spawned at " + GameScript.current.currentCheckpoint);
        }
        else
        {
            checkpoints[0].SpawnPlayerAtCheckpoint(player);
            //print("spawned default");
        }
        
        //if (SceneManager.GetActiveScene().buildIndex > 2)
        //{
        //    if (currentCheckPointIndex >= 0)
        //    {
        //        print("checkpoint " + PlayerPrefs.GetInt("Checkpoint") + " " + "level " + PlayerPrefs.GetInt("CurrentLevel"));
        //        SceneManager.LoadScene(SaveLoadManager.currentLevelID);
        //        checkpoints[PlayerPrefs.GetInt(AssociatedSaveDataScript.Instance.Checkpoint) - 1].SpawnPlayerAtCheckpoint(player);
        //    }
        //    else
        //    {
        //        checkpoints[PlayerPrefs.GetInt(AssociatedSaveDataScript.Instance.Checkpoint)].SpawnPlayerAtCheckpoint(player);
        //    }

        //}
    }

    public void KillPlayer()
    {
        StartCoroutine(KillPlayerCo());
    }

    private IEnumerator KillPlayerCo()
    {
        PlayerDisableUtility.Instance.PlayerAbility(false);
        //player.GetComponent<BoxCollider2D>().enabled = false;
        //player.GetComponent<Controller2D>().enabled = false;
        //player.enabled = false;
        //player.anim.SetInteger("AnimState", 0);

        yield return new WaitForSeconds(0.5f);
        isDead = false;
        //print("gs cp: " + GameScript.current.currentCheckpoint);
        //print("sl cp: " + SaveLoad.savedGame.currentCheckpoint);
        //print("cm cp: " + currentCheckPointIndex);
        if (GameScript.current.currentCheckpoint != -1)
        {
            checkpoints[SaveLoad.savedGame.currentCheckpoint].SpawnPlayerAtCheckpoint(player);
        }
        enemyRespawner.SpawnEnemies();
        //player.GetComponent<BoxCollider2D>().enabled = true;
        //player.GetComponent<Controller2D>().enabled = true;
        //player.enabled = true;
        PlayerDisableUtility.Instance.PlayerAbility(true);
        phc = FindObjectOfType<PlayerHealthController>();
        phc.curHealth = phc.maxHealth;
        //print("found player health");
        //for (int i = 0; i < player.GetComponent<PlayerHealthController>().health; i++)
        //{
        //    if (!player.GetComponent<PlayerHealthController>().healthImages[i].gameObject.activeInHierarchy)
        //    {
        //        player.GetComponent<PlayerHealthController>().healthImages[i].gameObject.SetActive(true);
        //    }
        //}
    }

}
