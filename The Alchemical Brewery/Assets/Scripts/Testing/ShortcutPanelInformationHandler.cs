using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShortcutPanelInformationHandler : MonoBehaviour
{
    List<PotionData> potionListToday;

    public ScriptableObjectHolder SO_holder;
    public Image[] potionIcon_image;
    public GameObject[] formularPanel_obj;

    private void Start()
    {
        potionListToday = StageManager.potionListToday;
        UpdatePanelInformation();
    }

    public void UpdatePanelInformation()
    {
        for (int i = 0; i < formularPanel_obj.Length; i++)
        {
            if(i < potionListToday.Count) //if slot not empty
            {
                PotionData _potionData = potionListToday[i]; //get potion data

                //update potion icon
                potionIcon_image[i].sprite = SO_holder.potionIconList[_potionData.potionSpriteIndex];

                //update ingredient image
                for (int a = 0; a < 4; a++)
                {
                    Image _ingImage = formularPanel_obj[i].transform.GetChild(0).GetChild(a).GetComponent<Image>();
                    Image _refinementImg = formularPanel_obj[i].transform.GetChild(0).GetChild(a).GetChild(0).GetComponent<Image>();
                    //get original sprite
                    _ingImage.sprite = SO_holder.ingredientSO[_potionData.potionFormular[a]].originalIngredient.ingredientSprite;
                    //get refinement sprite
                    switch(SO_holder.ingredientSO[_potionData.potionFormular[a]].refineStage)
                    {
                        case RefinementStage.Normal:
                            {
                                _refinementImg.sprite = SO_holder.transparentSprite;
                                break;
                            }
                        case RefinementStage.Crushed:
                            {
                                _refinementImg.sprite = SO_holder.crushedLogoSprite;
                                break;
                            }
                        case RefinementStage.Extract:
                            {
                                _refinementImg.sprite = SO_holder.extractLogoSprite;
                                break;
                            }
                    }
                }
            }
            else //if slot empty
            {
                potionIcon_image[i].sprite = SO_holder.transparentSprite;

                for (int a = 0; a < 4; a++)
                {
                    Image _ingImage = formularPanel_obj[i].transform.GetChild(0).GetChild(a).GetComponent<Image>();
                    Image _refinementImg = formularPanel_obj[i].transform.GetChild(0).GetChild(a).GetChild(0).GetComponent<Image>();

                    _ingImage.sprite = SO_holder.transparentSprite;
                    _refinementImg.sprite = SO_holder.transparentSprite;
                }
            }
        }
    }
}
