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

        //assign save date
        string lastSaveTime = System.DateTime.UtcNow.ToLocalTime().ToString("dd-MM-yyyy   HH:mm");
        PlayerProfile.lastSave = lastSaveTime;

        SaveData newSaveData = new SaveData();
        newSaveData.DirectSave();
        //SaveData saveData = new SaveData();
        string json = JsonUtility.ToJson(newSaveData, true);

        File.WriteAllText(SAVE_FOLDER + "/" + PlayerProfile.profileName + ".txt", json);
    }

    public static void Load()
    {
        if (Directory.Exists(SAVE_FOLDER))
        {
            if (File.Exists(SAVE_FOLDER + "/SuperLegzai.txt"))
            {
                string saveString = File.ReadAllText(SAVE_FOLDER + "/SuperLegzai.txt");
                SaveData saveData = JsonUtility.FromJson<SaveData>(saveString);

                PlayerProfile.LoadData(saveData);
            }
        }
    }

    public static List<string> GetAllSaveFile()
    {
        //if saves folder is not exsist
        if (!Directory.Exists(SAVE_FOLDER))
        {
            //create saves folder
            Directory.CreateDirectory(SAVE_FOLDER);
        }

        DirectoryInfo dir = new DirectoryInfo(SAVE_FOLDER);
        FileInfo[] info = dir.GetFiles("*.*");
        string[] files = System.IO.Directory.GetFiles(SAVE_FOLDER);

        List<string> filesList = new List<string>();
        for (int i = 0; i < files.Length; i++)
        {
            string _filename = info[i].ToString();
            if(_filename.Contains(".meta"))
            {

            }
            else
            {
                string _temp = File.ReadAllText(files[i]);
                filesList.Add(_temp);
            }
        }

        if (filesList.Count != 0)
        {
            return filesList;
        }
        else
        {
            return null;
        }
    }
}
