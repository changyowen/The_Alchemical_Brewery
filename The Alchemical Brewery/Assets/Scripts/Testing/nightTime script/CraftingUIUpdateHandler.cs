using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingUIUpdateHandler : MonoBehaviour
{
    Animator anim;
    ScriptableObjectHolder so_Holder;

    public Image[] potIngredientImage;

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.keepAnimatorControllerStateOnDisable = true;
        so_Holder = CraftPotionManager.Instance.so_Holder;
    }

    void Update()
    {
        PotIngredientImageUpdate();
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
