using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class ChosenPotionHandler : MonoBehaviour
{
    public static ChosenPotionHandler Instance { get; private set; }

    public Animator choosingPotionSystem_anim;

    public ScriptableObjectHolder SO_holder;
    public GameObject[] chosenPotion_obj;
    public Transform choosingPotionContainer_transform;
    public TextMeshProUGUI totalIngredient_text;

    public List<PotionData> chosenPotionList;
    public List<IngredientData> chosenIngredientList;

    [System.NonSerialized] public bool choosingIngredientMode = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        choosingPotionSystem_anim.SetBool("chooseIngredient", choosingIngredientMode);
    }

    public void UpdateChosenPotion()
    {
        for (int i = 0; i < chosenPotion_obj.Length; i++)
        {
            ChosenPotionInformation currentChosenPotionInfomation = chosenPotion_obj[i].GetComponent<ChosenPotionInformation>();
            if (i < chosenPotionList.Count)
            {
                currentChosenPotionInfomation.UpdatePanelInformation(chosenPotionList[i]);
            }
            else
            {
                currentChosenPotionInfomation.UpdateEmptyInformation();
            }
        }

        UpdateChosenIngredientTotal();
    }

    public void RemoveChosenPotion(int index)
    {
        if(chosenPotionList.Count > index)
        {
            ///REMOVE POTION DATA
            chosenPotionList.RemoveAt(index);

            ///UPDATE CHOOSING POTION PANEL
            int childs = choosingPotionContainer_transform.childCount;
            for (int i = 0; i < childs; i++)
            {
                PotionInformationHandler potionInformationHandler = choosingPotionContainer_transform.GetChild(i).GetComponent<PotionInformationHandler>();
                potionInformationHandler.UpdateAlreadyChosen();
            }

            ///UPDATE CHOSEN POTION PANEL
            UpdateChosenPotion();
        }
        
    }

    public void UpdateChosenIngredientTotal()
    {
        //get all ingredient into a list
        List<IngredientData> _allIngredientData = new List<IngredientData>();
        for (int i = 0; i < chosenPotionList.Count; i++)
        {
            //get potion formular in int
            int[] potionFormularINT = chosenPotionList[i].potionFormular;
            //convert into List of ingredientData
            List<IngredientData> _ingredientDataList = new List<IngredientData>();
            for (int a = 0; a < potionFormularINT.Length; a++)
            {
                _ingredientDataList.Add(SO_holder.ingredientSO[potionFormularINT[a]]);
            }
            //get back original ingredient
            List<IngredientData> _originalIngredientDataList = new List<IngredientData>();
            for (int b = 0; b < _ingredientDataList.Count; b++)
            {
                _originalIngredientDataList.Add(_ingredientDataList[b].originalIngredient);
            }
            //adding into all ingredientData list
            for (int c = 0; c < _originalIngredientDataList.Count; c++)
            {
                _allIngredientData.Add(_originalIngredientDataList[c]);
            }
        }

        //remove all repititive ingredient data
        _allIngredientData = _allIngredientData.Distinct().ToList();
        //assign into chosen ingredient list
        chosenIngredientList = _allIngredientData;

        //update totalIngredient text
        UpdateTotalIngredientText();
    }

    void UpdateTotalIngredientText()
    {
        if(chosenIngredientList.Count < 2)
        {
            totalIngredient_text.text = "Total " + chosenIngredientList.Count + " ingredient";
            totalIngredient_text.color = Color.white;
        }
        else if(chosenIngredientList.Count <= 10)
        {
            totalIngredient_text.text = "Total " + chosenIngredientList.Count + " ingredients";
            totalIngredient_text.color = Color.white;
        }
        else
        {
            totalIngredient_text.text = "Total " + chosenIngredientList.Count + " ingredients";
            totalIngredient_text.color = Color.red;
        }
    }

}
