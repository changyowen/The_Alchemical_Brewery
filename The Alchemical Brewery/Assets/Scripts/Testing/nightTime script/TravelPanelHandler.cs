using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TravelPanelHandler : MonoBehaviour
{
    public TextMeshProUGUI stageName_text;
    public TextMeshProUGUI description_text;

    [Header("Description")]
    public string[] stageName;
    [TextArea(3, 10)]
    public string[] stageDescription;

    bool showDescription = false;

    private void Update()
    {
        if(showDescription)
        {
            stageName_text.gameObject.SetActive(true);
            description_text.gameObject.SetActive(true);
        }
        else
        {
            stageName_text.gameObject.SetActive(false);
            description_text.gameObject.SetActive(false);
        }
    }

    public void EnableDescription()
    {
        showDescription = true;
    }

    public void DisableDescription()
    {
        showDescription = false;
    }

    public void AssignText(int stageIndex)
    {
        switch(stageIndex)
        {
            case 0:
                {
                    stageName_text.text = "" + stageName[0];
                    description_text.text = "" + stageDescription[0];
                    break;
                }
            case 1:
                {
                    stageName_text.text = "" + stageName[1];
                    description_text.text = "" + stageDescription[1];
                    break;
                }
            case 2:
                {
                    stageName_text.text = "" + stageName[2];
                    description_text.text = "" + stageDescription[2];
                    break;
                }
        }
    }

    public void EnablePanel()
    {
        this.gameObject.SetActive(true);
    }

    public void ChangeStage(int _stageIndex)
    {
        if(PlayerProfile.stageChosen == _stageIndex)
        {
            NotificationSystem.Instance.SendPopOutNotification("You already in this location!");
        }
        else
        {
            PlayerProfile.stageChosen = _stageIndex;
            PlayerProfile.dayResetTravel += 5;

            this.gameObject.SetActive(false);
        }
    }
}
