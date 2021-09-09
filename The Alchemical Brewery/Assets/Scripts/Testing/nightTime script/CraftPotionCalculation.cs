using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftPotionCalculation : MonoBehaviour
{
    public static CraftPotionCalculation Instance { get; private set; }

    public ScriptableObjectHolder SO_holder;

    void Awake()
    {
        Instance = this;
    }

    public int PotionEffectiveCalculation(List<int> potIngredient)
    {
        //get list of ingredient data
        List<IngredientData> ingredientDataList = GetIngredientData(potIngredient);

        //typeVariabe calculation
        int totalScore = ingredientDataList[0].effectiveVariable;
        for (int i = 1; i < ingredientDataList.Count; i++)
        {
            totalScore *= ingredientDataList[i].effectiveVariable;
        }

        return totalScore;
    }

    public int PotionPriceCalculation(List<int> ingredientList, float quality)
    {
        //get list of ingredient data
        List<IngredientData> ingredientDataList = GetIngredientData(ingredientList);

        int totalScore = 0;
        for (int i = 0; i < ingredientList.Count - 1; i++)
        {
            for (int j = i + 1; j < ingredientList.Count; j++)
            {
                totalScore += ingredientList[i] * ingredientList[j];
            }
        }

        float _price = (quality/100) * (float)totalScore;
        return (int)_price;
    }

    List<IngredientData> GetIngredientData(List<int> potIngredient)
    {
        //assignn ingedient data
        List<IngredientData> ingredientDataList = new List<IngredientData>();
        for (int i = 0; i < potIngredient.Count; i++)
        {
            //get so_Holder
            ScriptableObjectHolder so_Holder = CraftPotionManager.Instance.so_Holder;
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
                //update combination result sprite
                int _combinationResult = ElementCombinationCompare(ingredientDataList[i], ingredientDataList[j]);
                switch(_combinationResult)
                {
                    case -1:
                        {
                            combination.transform.GetChild(1).GetComponent<Image>().sprite = SO_holder.crossedSprite;
                            break;
                        }
                    case 1:
                        {
                            combination.transform.GetChild(1).GetComponent<Image>().sprite = SO_holder.okayLogoSprite;
                            break;
                        }
                    case 0:
                        {
                            combination.transform.GetChild(1).GetComponent<Image>().sprite = SO_holder.boredLogoSprite;
                            break;
                        }
                }
                childNo++;
            }
        }
    }

    public int ElementCombinationCompare(IngredientData ing1, IngredientData ing2)
    {
        switch(ing1.elementType)
        {
            case Element.Ignis:
                {
                    if(ing2.elementType == Element.Ignis)
                    {
                        return 1;
                    }
                    else if(ing2.elementType == Element.Aqua || ing2.elementType == Element.Aer)
                    {
                        return -1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            case Element.Aqua:
                {
                    if (ing2.elementType == Element.Aqua)
                    {
                        return 1;
                    }
                    else if (ing2.elementType == Element.Ignis || ing2.elementType == Element.Terra)
                    {
                        return -1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            case Element.Terra:
                {
                    if (ing2.elementType == Element.Terra)
                    {
                        return 1;
                    }
                    else if (ing2.elementType == Element.Aqua || ing2.elementType == Element.Aer)
                    {
                        return -1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            case Element.Aer:
                {
                    if (ing2.elementType == Element.Aer)
                    {
                        return 1;
                    }
                    else if (ing2.elementType == Element.Ignis || ing2.elementType == Element.Terra)
                    {
                        return -1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            case Element.Ordo:
                {
                    return 0;
                }
        }
        return 0;
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

    public bool CompareLists(List<int> aListA, List<int> aListB)
    {
        if (aListA == null || aListB == null || aListA.Count != aListB.Count)
            return false;
        if (aListA.Count == 0)
            return true;
        Dictionary<int, int> lookUp = new Dictionary<int, int>();
        // create index for the first list
        for (int i = 0; i < aListA.Count; i++)
        {
            int count = 0;
            if (!lookUp.TryGetValue(aListA[i], out count))
            {
                lookUp.Add(aListA[i], 1);
                continue;
            }
            lookUp[aListA[i]] = count + 1;
        }
        for (int i = 0; i < aListB.Count; i++)
        {
            int count = 0;
            if (!lookUp.TryGetValue(aListB[i], out count))
            {
                // early exit as the current value in B doesn't exist in the lookUp (and not in ListA)
                return false;
            }
            count--;
            if (count <= 0)
                lookUp.Remove(aListB[i]);
            else
                lookUp[aListB[i]] = count;
        }
        // if there are remaining elements in the lookUp, that means ListA contains elements that do not exist in ListB
        return lookUp.Count == 0;
    }
}
