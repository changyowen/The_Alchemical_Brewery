using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PotScript : MonoBehaviour
{
    //access 
    public GameObject dailySystem_gameObject;
    DailyStart dailyStart;
    public GameObject ImageReference_system;
    ImageReference imageReference;
    public GameObject ingredientContain;
    public GameObject potionContain;
    public GameObject pot_center;
    Animator pot_anim;

    //will be improved
    int[,] potionToday = new int[4, 4]
        {
            { 1, 1, 1, 1 },
            { 2, 2, 8, 11 },
            { 5, 7, 7, 9 },
            { 3, 4, 10, 10 },
        };

    //system data
    public int[] ingredientArray = { 0, 0, 0, 0};
    public int potionCrafted = 0;
    public bool readyForCraft = true;
    float pot_timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        dailyStart = dailySystem_gameObject.GetComponent<DailyStart>();
        imageReference = ImageReference_system.GetComponent<ImageReference>();
        pot_anim = pot_center.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        int ingredientCheck = 0;

        for(int i = 0; i < 4; i++)
        {
            if(ingredientArray[i] != 0)
            {
                ingredientCheck++;
            }
        }

        if(ingredientCheck >= 4)
        {
            pot_anim.SetBool("Pot_Bounce", true);
            pot_timer += Time.deltaTime;
            int CheckedPotion = 0;

            if(readyForCraft == true)
            {
                readyForCraft = false;
                CheckedPotion = CompareRecipe(ingredientArray);
                StartCoroutine(CraftingPotion(CheckedPotion));
            }
        }
        else
        {
            pot_anim.SetBool("Pot_Bounce", false);
        }

        if(ingredientCheck == 0)
        {
            ingredientContain.SetActive(false);
        }
        else
        {
            ingredientContain.SetActive(true);
            IngredientSpriteUpdate();
        }

        if(potionCrafted != 0)
        {
            potionContain.SetActive(true);
            PotionSpriteUpdate();
        }
        else
        {
            potionContain.SetActive(false);
        }
    }

    //will improved later
    int CompareRecipe(int[] ingredientArray)
    {
        Array.Sort(ingredientArray);
        
        for(int i = 0; i < 4; i++)
        {
            int checkRecipe = 0;

            for (int j = 0; j < 4; j++)
            {
                if(ingredientArray[j] ==  potionToday[i,j])
                {
                    checkRecipe++;
                }
            }

            if(checkRecipe >= 4)
            {
                return i + 101;
            }
        }

        return 0;
    }

    IEnumerator CraftingPotion(int potionIndex)
    {
       
        if (potionIndex == 0)
        {
            for (int i = 0; i < 4; i++)
            {
                ingredientArray[i] = 0;
            }
            readyForCraft = true;
            pot_timer = 0;
        }
        else
        {
            yield return new WaitForSeconds(8);
            potionCrafted = potionIndex;
            for (int i = 0; i < 4; i++)
            {
                ingredientArray[i] = 0;
            }
        }
    }

    void IngredientSpriteUpdate()
    {
        for(int i = 0; i < 4; i++)
        {
            SpriteRenderer sp = ingredientContain.transform.GetChild(0).transform.GetChild(i).GetComponent<SpriteRenderer>();
            if(sp != null)
            {
                sp.sprite = imageReference.Ingredient[ingredientArray[i]];
            }
        }
    }

    void PotionSpriteUpdate()
    {
        SpriteRenderer sp = potionContain.transform.GetChild(1).GetComponent<SpriteRenderer>();
        if(sp != null)
        {
            sp.sprite = imageReference.Potion[potionCrafted - 100];
        }
    }
}
