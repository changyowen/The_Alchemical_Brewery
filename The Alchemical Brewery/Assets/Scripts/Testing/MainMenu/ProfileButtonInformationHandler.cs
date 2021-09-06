using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ProfileButtonInformationHandler : MonoBehaviour, IPointerClickHandler
{
    [System.NonSerialized] public SaveData saveData;

    public Text profileName_text;
    public Text profileLastSave_text;

    public void AssignProfileButtonData(SaveData _saveData)
    {
        if(_saveData != null)
        {
            saveData = _saveData;

            profileName_text.text = "" + _saveData.profileName;
            profileLastSave_text.text = "" + _saveData.lastSave;
        }
    }

    void LoadThisSaveProfile()
    {
        PlayerProfile.LoadData(saveData);
        //go next scene
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        PlayerProfile.LoadData(saveData);

        SceneManager.LoadScene(1);
    }
}
