using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoad {

    public static List<GameScript> savedGames = new List<GameScript>();
    public static GameScript savedGame;

    public static void OverwriteSave()
    {
        savedGame = GameScript.current;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Path.Combine(Application.persistentDataPath, "SaveGame0.gd"));
        bf.Serialize(file, savedGame);
        file.Close();
    }

    public static void NewSave()
    {
        savedGame = new GameScript();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Path.Combine(Application.persistentDataPath, "SaveGame0.gd"));
        bf.Serialize(file, savedGame);
        file.Close();
    }

    public static void Load()
    {
        if (File.Exists(Path.Combine(Application.persistentDataPath, "SaveGame0.gd")))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Path.Combine(Application.persistentDataPath, "SaveGame0.gd"), FileMode.Open);
            savedGame = (GameScript)bf.Deserialize(file);
            file.Close();
        }
    }

}
