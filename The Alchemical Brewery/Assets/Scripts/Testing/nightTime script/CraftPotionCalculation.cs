using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftPotionCalculation : MonoBehaviour
{
    public static CraftPotionCalculation Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    public int PotionEffectiveCalculation(List<int> potIngredient)
    {
        //get list of ingredient data
        List<IngredientData> ingredientDataList = GetIngredientData(potIngredient);

        //typeVariabe calculation
        int totalScore = 0;
        for (int i = 0; i < ingredientDataList.Count - 1; i++)
        {
            for (int j = i + 1; j < ingredientDataList.Count; j++)
            {
                totalScore += ingredientDataList[i].effectiveVariable * ingredientDataList[j].effectiveVariable;
            }
        }

        return totalScore;
    }

    List<IngredientData> GetIngredientData(List<int> potIngredient)
    {
        //assignn ingedient data
        List<IngredientData> ingredientDataList = new List<IngredientData>();
        for (int i = 0; i < potIngredient.Count; i++)
        {
            //get so_Holder
            ScriptableObjectHolder so_Holder = StageManager.Instance.so_Holder;
            //assign ingredient so into ingredient data list
            ingredientDataList.Add(so_Holder.ingredientSO[potIngredient[i]]);
        }

        //return ingredient data list
        return ingredientDataList;
    }

    public void AssignCraftingAnimationSprite(List<int> potIngredient)
    {
        //get list of ingredient data
        List<IngredientData> ingredientDataList = GetIngredientData(potIngredient);
        //get crafting Animation obj
        GameObject craftingAnimation_obj = CraftPotionManager.Instance.craftingAnimation_obj;

        int childNo = 0;
        for (int i = 0; i < ingredientDataList.Count - 1; i++)
        {
            for (int j = i + 1; j < ingredientDataList.Count; j++)
            {
                GameObject combination = craftingAnimation_obj.transform.GetChild(childNo).gameObject;
                combination.transform.GetChild(0).GetComponent<Image>().sprite = ingredientDataList[i].ingredientSprite;
                combination.transform.GetChild(2).GetComponent<Image>().sprite = ingredientDataList[j].ingredientSprite;
                childNo++;
                Debug.Log(potIngredient[i] + " " + potIngredient[j]);
            }
        }
    }

    public List<Element> ElementCalculation(List<int> potIngredient)
    {
        //get list of ingredient data
        List<IngredientData> ingredientDataList = GetIngredientData(potIngredient);
        //get list of element of ingredient list
        List<Element> elementList = new List<Element>();
        for (int i = 0; i < ingredientDataList.Count; i++)
        {
            elementList.Add(ingredientDataList[i].elementType);
        }

        //declare potionElement list
        List<Element> thisPotionElement = new List<Element>();
        //add first element
        thisPotionElement.Add(elementList[0]);

        //loop all element combination
        for (int i = 1; i < elementList.Count; i++)
        {
            //Element mixing function
            thisPotionElement = ElementMixing(thisPotionElement, elementList[i]);
        }

        //sort element accordingly
        thisPotionElement.Sort();
        //return element
        return thisPotionElement;
    }

    private List<Element> ElementMixing(List<Element> thisPotionElement, Element element_2)
    {
        switch (element_2)
        {
            case Element.Ignis:
                {
                    if (thisPotionElement.Contains(Element.Aqua)) //Aqua vs Ignis = win
                    {
                        //no need add element
                    }
                    else if (thisPotionElement.Contains(Element.Aer)) //Aer vs Ignis = lose
                    {
                        thisPotionElement.Remove(Element.Aer);
                        thisPotionElement.Add(Element.Ignis);
                    }
                    else //other elements are not affecting Ignis
                    {
                        thisPotionElement.Add(Element.Ignis);
                    }
                    return thisPotionElement;
                }
            case Element.Aqua:
                {
                    if (thisPotionElement.Contains(Element.Terra)) //Terra vs Aqua = win
                    {
                        //no need add element
                    }
                    else if (thisPotionElement.Contains(Element.Ignis)) //Ignis vs Aqua = lose
                    {
                        thisPotionElement.Remove(Element.Ignis);
                        thisPotionElement.Add(Element.Aqua);
                    }
                    else //other elements are not affecting Aqua
                    {
                        thisPotionElement.Add(Element.Aqua);
                    }
                    return thisPotionElement;
                }
            case Element.Terra:
                {
                    if (thisPotionElement.Contains(Element.Aer)) //Aer vs Terra = win
                    {
                        //no need add element
                    }
                    else if (thisPotionElement.Contains(Element.Aqua)) //Aqua vs Terra = lose
                    {
                        thisPotionElement.Remove(Element.Aqua);
                        thisPotionElement.Add(Element.Terra);
                    }
                    else //other elements are not affecting Terra
                    {
                        thisPotionElement.Add(Element.Terra);
                    }
                    return thisPotionElement;
                }
            case Element.Aer:
                {
                    if (thisPotionElement.Contains(Element.Ignis)) //Ignis vs Aer = win
                    {
                        //no need add element
                    }
                    else if (thisPotionElement.Contains(Element.Terra)) //Terra vs Aer = lose
                    {
                        thisPotionElement.Remove(Element.Terra);
                        thisPotionElement.Add(Element.Aer);
                    }
                    else //other elements are not affecting Aer
                    {
                        thisPotionElement.Add(Element.Aer);
                    }
                    return thisPotionElement;
                }
            case Element.Ordo:
                {
                    //other elemnt are not affecting Ordo
                    thisPotionElement.Add(Element.Ordo);
                    return thisPotionElement;
                }
            case Element.Null:
                {
                    //no change
                    return thisPotionElement;
                }
        }
        return null;
    }

    public PotionUsage PotionUsageDetermine(List<int> potIngredient)
    {
        //get list of ingredient data
        List<IngredientData> ingredientDataList = GetIngredientData(potIngredient);
        //get list of element of ingredient list
        List<PotionUsage> ingredientUsageList = new List<PotionUsage>();
        for (int i = 0; i < ingredientDataList.Count; i++)
        {
            ingredientUsageList.Add(ingredientDataList[i].ingredientUsage);
        }

        int totalScore = 0;
        for (int i = 0; i < ingredientUsageList.Count; i++)
        {
            totalScore += (int)ingredientUsageList[i];
        }

        if(totalScore < 0)
        {
            return PotionUsage.Damaging;
        }
        else if(totalScore == 0)
        {
            return PotionUsage.Neutral;
        }
        else
        {
            return PotionUsage.Healing;
        }
    }
}
