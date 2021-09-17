using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    public GameObject newGamePanel_obj;
    public GameObject startNewGameButton_obj;
    public GameObject loadGamePanel_obj;
    public Transform loadPanel_container;
    public GameObject profileButton_obj;
    public GameObject mainMenuButtons_parent_obj;

    public TMP_InputField newgameName_inputField;

    List<SaveData> saveDataList = null;

    private void Update()
    {
        if(newgameName_inputField.text == "")
        {
            startNewGameButton_obj.GetComponent<Button>().interactable = false;
        }
        else
        {
            startNewGameButton_obj.GetComponent<Button>().interactable = true;
        }
    }

    public void NewGameButton()
    {
        newGamePanel_obj.SetActive(true);
        mainMenuButtons_parent_obj.SetActive(false);
    }

    public void LoadGameButton()
    {
        //activate panel
        loadGamePanel_obj.SetActive(true);
        mainMenuButtons_parent_obj.SetActive(false);

        //clear all buttons
        for (int i = loadPanel_container.childCount - 1; i >= 0; i--)
        {
            GameObject button_obj = loadPanel_container.GetChild(i).gameObject;
            Destroy(button_obj);
        }

        //get all save file
        saveDataList = GetAllSavedFile();

        //load all profile button
        if(saveDataList != null)
        {
            for (int i = 0; i < saveDataList.Count; i++)
            {
                GameObject newProfileButton = Instantiate(profileButton_obj, Vector3.zero, Quaternion.identity) as GameObject;
                newProfileButton.transform.SetParent(loadPanel_container, false);

                //assign button data
                ProfileButtonInformationHandler buttonScript = newProfileButton.GetComponent<ProfileButtonInformationHandler>();
                if(buttonScript != null)
                {
                    buttonScript.AssignProfileButtonData(saveDataList[i]);
                }
            }
        }
        else
        {

        }
    }

    public void ExitGameButton()
    {
        Application.Quit();
    }

    public void CloseNewGamePanel()
    {
        newGamePanel_obj.SetActive(false);
        mainMenuButtons_parent_obj.SetActive(true);
        //clear input field
        newgameName_inputField.Select();
        newgameName_inputField.text = "";
    }

    public void CloseLoadGamePanel()
    {
        loadGamePanel_obj.SetActive(false);
        mainMenuButtons_parent_obj.SetActive(true);
    }

    public void StartNewGame()
    {
        if(newgameName_inputField.text != "")
        {
            string newProfileName = newgameName_inputField.text;
            PlayerProfile.NewGameData(newProfileName);
            SaveManager.Save();
            //next scene
            LoadingScreenScript.nextSceneIndex = 2;
            SceneManager.LoadScene(1);
        }
    }

    List<SaveData> GetAllSavedFile()
    {
        List<string> allSaveFile = SaveManager.GetAllSaveFile();

        if(allSaveFile != null)
        {
            List<SaveData> _saveDataList = new List<SaveData>();
            for (int i = 0; i < allSaveFile.Count; i++)
            {
                SaveData _saveData = JsonUtility.FromJson<SaveData>(allSaveFile[i]);
                _saveDataList.Add(_saveData);
            }
            return _saveDataList;
        }
        else
        {
            return null;
        }
    }

    public void TestingGM()
    {
        PlayerProfile.GM_TestingUse();
        SaveManager.Save();
        SaveManager.Load();
        //next scene
        //Start loading next scene
        LoadingScreenScript.nextSceneIndex = 2;
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);
    }

    public void CreditButton()
    {
        LoadingScreenScript.nextSceneIndex = 7;
        SceneManager.LoadScene(1);
    }
}
