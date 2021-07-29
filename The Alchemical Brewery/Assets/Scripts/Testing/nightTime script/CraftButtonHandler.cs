using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftButtonHandler : MonoBehaviour
{
    CraftPotionManager craftPotionManager;

    void Awake()
    {
        craftPotionManager = CraftPotionManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        //activate when pot is full
        if(craftPotionManager.potIngredientList.Count  == 4)
        {
            this.gameObject.SetActive(true);
        }
        else //deactivate when pot is not full
        {
            this.gameObject.SetActive(false);
        }
    }
}
