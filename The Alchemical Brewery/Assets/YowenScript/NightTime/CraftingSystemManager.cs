using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingSystemManager : MonoBehaviour
{
    //system data
    public int[] IngredientAmountArray;
    public int[] IngredientSlot = { 0, 0, 0, 0 };

    //access
    public Text[] ingredientAmount_text;
    public GameObject[] IngredientSlot_gameObject;
    public Sprite[] ingredientSlot_image;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        IngredientAmountUpdate();

        UpdateIngredientSlot();
    }

    void IngredientAmountUpdate()
    {
        for(int i = 0; i < ingredientAmount_text.Length; i++)
        {
            ingredientAmount_text[i].text = "" + IngredientAmountArray[i];
        }
    }

    public void AddIngredient(int index)
    {
        for(int i = 0; i < 4; i++)
        {
            if(IngredientSlot[i] == 0)
            {
                IngredientSlot[i] = index;
                i = 4;
            }
        }
    }

    public void DropIngredient(int index)
    {
        IngredientSlot[index] = 0;
    }

    void UpdateIngredientSlot()
    {
        for(int i = 0; i < 4; i++)
        {
            if (IngredientSlot[i] == 0)
            {
                IngredientSlot_gameObject[i].SetActive(false);
            }
            else
            {
                IngredientSlot_gameObject[i].SetActive(true);
            }

            IngredientSlot_gameObject[i].GetComponent<Image>().sprite = ingredientSlot_image[IngredientSlot[i]];
        }
    }
}
