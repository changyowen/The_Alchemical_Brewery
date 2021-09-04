using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotHolderUpdateHandler : MonoBehaviour
{
    public ScriptableObjectHolder SO_holder;

    int potIndex;

    public PotInformationHandler potInformationHandler;

    public GameObject potionHolder;
    public GameObject ingredientHolder;
    public GameObject craftButton;
    public Image potionHolderImage;
    public Image[] ingredientHolderImage;

    void Start()
    {
        //get current pot index from PotInteractionHandler
        potIndex = potInformationHandler.potIndex;
        //get camera for canvas
        GetComponent<Canvas>().worldCamera = Camera.main;
    }

    void Update()
    {
        ////get pot class
        //Pot thisPot = PotSystem_Daytime.Instance.potList[potIndex];

        //if pot not boiling
        if(!potInformationHandler.potBoiling)
        {
            //if pot holding potion
            if(potInformationHandler.potPotionHolder != -1)
            {
                //get PotionData
                PotionData potionData = StageManager.potionListToday[potInformationHandler.potPotionHolder];
                //potion holder image SetActive
                HolderSetActive(true, false, false);
                //set image.....
                potionHolderImage.sprite = SO_holder.potionIconList[potionData.potionSpriteIndex];
            }
            else //if pot not holding potion
            {
                //if ingredient holder NOT empty
                if(potInformationHandler.potIngredientHolderList.Count != 0)
                {
                    //if ingredient holder is full
                    if(potInformationHandler.potIngredientHolderList.Count == 4)
                    {
                        //ingredient holder & craft button image SetActive
                        HolderSetActive(false, true, true);
                    }
                    else //if ingredient holder is NOT full
                    {
                        //ingredient holder image SetActive
                        HolderSetActive(false, true, false);
                    }

                    //update ingredient holder image
                    IngredientHolderImageUpdate();
                }
                else //if ingredient holder is empty
                {
                    //all holder image unactive
                    HolderSetActive(false, false, false);
                }
            }
        }
    }

    //for switching potion/ingredient holder SetActive
    void HolderSetActive(bool potionHolderActive, bool ingredientHolderActive, bool craftButtonActive)
    {
        potionHolder.SetActive(potionHolderActive);
        ingredientHolder.SetActive(ingredientHolderActive);
        craftButton.SetActive(craftButtonActive);
    }

    //For updating ingredient holder image
    void IngredientHolderImageUpdate()
    {
        //get total empty slot
        int totalEmptySlot = ingredientHolderImage.Length - potInformationHandler.potIngredientHolderList.Count;

        //update image of filled slot
        for (int i = 0; i < potInformationHandler.potIngredientHolderList.Count; i++)
        {
            //get so_Holder
            ScriptableObjectHolder so_Holder = StageManager.Instance.so_Holder;
            //assign ingredient index
            int _ingredientIndex = potInformationHandler.potIngredientHolderList[i];
            //get sprite of ingredient
            Sprite ingSprite = so_Holder.ingredientSO[_ingredientIndex].ingredientSprite;
            ingredientHolderImage[i].sprite = ingSprite;
        }
        //update image of empty slot
        for (int j = ingredientHolderImage.Length - 1; j >= ingredientHolderImage.Length - totalEmptySlot; j--)
        {
            ScriptableObjectHolder so_Holder = StageManager.Instance.so_Holder;
            Sprite transSprite = so_Holder.transparentSprite;
            ingredientHolderImage[j].sprite = transSprite;
        }
    }
}
