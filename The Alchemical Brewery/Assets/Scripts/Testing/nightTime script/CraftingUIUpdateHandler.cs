using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingUIUpdateHandler : MonoBehaviour
{
    ScriptableObjectHolder so_Holder;

    public Image[] ingredientButtonImage;
    public Image[] potIngredientImage;

    void Start()
    {
        so_Holder = CraftPotionManager.Instance.so_Holder;
    }

    void Update()
    {
        IngredientButtonImageUpdate();
        PotIngredientImageUpdate();
    }

    void IngredientButtonImageUpdate()
    {
        for (int i = 1; i < ingredientButtonImage.Length + 1; i++)
        {
            Sprite ingSprite = so_Holder.ingredientSO[i].ingredientSprite;
            ingredientButtonImage[i - 1].sprite = ingSprite;
        }
    }

    void PotIngredientImageUpdate()
    {
        //get pot ingredient list
        List<int> potIngredientList = CraftPotionManager.Instance.potIngredientList;

        for (int i = 0; i < 4; i++)
        {
            if (i < potIngredientList.Count)
            {
                //get ingredient data
                IngredientData ingData = so_Holder.ingredientSO[potIngredientList[i]];
                potIngredientImage[i].sprite = ingData.ingredientSprite;
            }
            else
            {
                //get trasparent sprite
                potIngredientImage[i].sprite = so_Holder.transparentSprite;
            }
        }
    }
}
