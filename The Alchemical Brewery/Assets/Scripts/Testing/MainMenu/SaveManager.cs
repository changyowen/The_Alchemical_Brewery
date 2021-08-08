using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class SaveManager
{
    private static readonly string SAVE_FOLDER = Application.dataPath + "/Saves/";

    public static void Init()
    {
        //if saves folder is not exsist
        if (!Directory.Exists(SAVE_FOLDER))
        {
            //create saves folder
            Directory.CreateDirectory(SAVE_FOLDER);
        }
    }

    public static void Save()
    {
        //if saves folder is not exsist
        if (!Directory.Exists(SAVE_FOLDER))
        {
            //create saves folder
            Directory.CreateDirectory(SAVE_FOLDER);
        }

        SaveData saveData = new SaveData();
        string json = JsonUtility.ToJson(saveData);

        File.WriteAllText(SAVE_FOLDER + "/testing.txt", json);
    }

    public static void Load()
    {
        if (Directory.Exists(SAVE_FOLDER))
        {
            if (File.Exists(SAVE_FOLDER + "/testing.txt"))
            {
                string saveString = File.ReadAllText(SAVE_FOLDER + "/testing.txt");
                SaveData saveData = JsonUtility.FromJson<SaveData>(saveString);

                PlayerProfile.LoadData(saveData);
            }
        }
    }
}
