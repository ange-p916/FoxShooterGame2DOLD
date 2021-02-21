using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SaveLoadManager : MonoBehaviour {

    public bool DontDestroyThisOnLoad = true;

    public static SaveLoadManager Instance;

    public static bool save1 = false;
    public static bool save2 = false;
    public static bool save3 = false;
    public static bool testSave = false;

    public Text save1level;
    public Text save1checkpoint;
    public static bool hasLoadedNewLevel = false;
    public static int currentCheckpointID;
    public static int currentLevelID;

    void Awake()
    {
        Instance = this;
        if(DontDestroyThisOnLoad)
        {
            DontDestroyOnLoad(this);
        }
        Load();
        if(PlayerPrefs.GetInt("Checkpoint") == 0)
        {
            hasLoadedNewLevel = true;
        }

        //if(SceneManager.GetActiveScene().name == "NewGameMenu")
        //{
        //    transform.GetChild(0).GetChild(1).GetComponent<Text>().text = save1level.text;
        //    transform.GetChild(0).GetChild(2).GetComponent<Text>().text = save1checkpoint.text;
        //}

        //currentLevelID = SceneManager.GetActiveScene().buildIndex;
        //print("SaveLoadManager: " + currentLevelID + " " + PlayerPrefs.GetInt("CurrentLevel"));
    }

    //void Update()
    //{
    //    if (save1checkpoint != null || save1level != null)
    //    {
    //        save1checkpoint.text = PlayerPrefs.GetInt("Checkpoint").ToString();
    //        save1level.text = PlayerPrefs.GetInt("CurrentLevel").ToString();
    //    }
    //}

    
    public static void Save()
    {
        
    }

    public static void Load()
    {       
         
    }
}
