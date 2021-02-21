using UnityEngine;
using System.Collections;

public class AssociatedSaveDataScript : MonoBehaviour {

    public static AssociatedSaveDataScript Instance;

    public bool DontDestroyThisOnLoad = true;

    public static bool Slot1;
    public static bool Slot2;
    public static bool Slot3;

    public string Checkpoint = "Checkpoint1";
    public string Level = "CurrentLevel1";

    void Awake()
    {
        Instance = this;
        if(DontDestroyThisOnLoad)
        {
            DontDestroyOnLoad(this);
        }
    }



}
