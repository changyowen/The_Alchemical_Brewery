using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlmanacInteraction : MonoBehaviour
{
    [Header("Reference")]
    public GameObject mainAlmanac_obj;
    public AlmanacIngredientInformationHandler ingredientInformationHandler;
    public AlmanacCustomerInformationHandler customerInformationHandler;
    public AlmanacPotionInformationHandler potionInformationHandler;
    public GameObject[] inactiveStateButton;
    public GameObject[] almanacEachPanel_obj;

    public int almanacState = 0;

    private void Start()
    {
        UpdateAll();
    }

    private void Update()
    {
        UpdateAlmenacEachPanel();
    }

    public void UpdateAll()
    {
        ingredientInformationHandler.UpdateButtonData();
        ingredientInformationHandler.UpdateIngredientInformation();
        customerInformationHandler.UpdateButtonData();
        customerInformationHandler.UpdateCustomerInformation();
        potionInformationHandler.UpdateButtonData();
    }

    void UpdateAlmenacEachPanel()
    {
        //update Each Panel
        for (int i = 0; i < almanacEachPanel_obj.Length; i++)
        {
            if (i == almanacState)
            {
                almanacEachPanel_obj[i].SetActive(true);
            }
            else
            {
                almanacEachPanel_obj[i].SetActive(false);
            }
        }
        //update inactive option button
        for (int i = 0; i < inactiveStateButton.Length; i++)
        {
            if (i == almanacState)
            {
                inactiveStateButton[i].SetActive(false);
            }
            else
            {
                inactiveStateButton[i].SetActive(true);
            }
        }
    }

    public void ChangeInformationState(int _buttonIndex)
    {
        almanacState = _buttonIndex;
        UpdateAll();
    }

    public void ExitAlmanac()
    {
        mainAlmanac_obj.SetActive(false);
    }
}
