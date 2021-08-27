using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockedStuffPanel : MonoBehaviour
{
    Animator anim;

    public ScriptableObjectHolder SO_holder;

    public Image unlockedStuff_image;
    public Text unlockedStuff_text;

    public bool enabledSkip = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {

    }

    public void EnableSkip()
    {
        enabledSkip = true;
    }

    public void AssignData(TodayUnlockStuff _todayUnlockStuff)
    {
        switch(_todayUnlockStuff.unlockType)
        {
            case UnlockType.Ingredient:
                {
                    unlockedStuff_image.sprite = SO_holder.ingredientSO[_todayUnlockStuff.unlockedIndex].ingredientSprite;
                    unlockedStuff_text.text = SO_holder.ingredientSO[_todayUnlockStuff.unlockedIndex].ingredientName + "";
                    break;
                }
            case UnlockType.Customer:
                {
                    unlockedStuff_image.sprite = SO_holder.customerDataSO[_todayUnlockStuff.unlockedIndex].customerSprite;
                    unlockedStuff_text.text = SO_holder.customerDataSO[_todayUnlockStuff.unlockedIndex].customerName + "";
                    break;
                }
            case UnlockType.MagicChest:
                {
                    unlockedStuff_image.sprite = SO_holder.magicChestSprite;
                    unlockedStuff_text.text = "magic chest x 1";
                    break;
                }
            case UnlockType.Pot:
                {
                    unlockedStuff_image.sprite = SO_holder.potSprite;
                    unlockedStuff_text.text = "pot x 1";
                    break;
                }
            case UnlockType.Counter:
                {
                    unlockedStuff_image.sprite = SO_holder.counterSprite;
                    unlockedStuff_text.text = "counter x 1";
                    break;
                }
            case UnlockType.RefinementStation:
                {
                    if(SO_holder.refinementStationSprite[_todayUnlockStuff.unlockedIndex] != null)
                    {
                        unlockedStuff_image.sprite = SO_holder.refinementStationSprite[_todayUnlockStuff.unlockedIndex];
                    }

                    if(_todayUnlockStuff.unlockedIndex == 0)
                    {
                        unlockedStuff_text.text = "refine1 x 1";
                    }
                    else if(_todayUnlockStuff.unlockedIndex == 1)
                    {
                        unlockedStuff_text.text = "refine2 x 1";
                    }
                    break;
                }
            case UnlockType.Region:
                {
                    if (SO_holder.regionSprite[_todayUnlockStuff.unlockedIndex] != null)
                    {
                        unlockedStuff_image.sprite = SO_holder.regionSprite[_todayUnlockStuff.unlockedIndex];
                    }

                    if (_todayUnlockStuff.unlockedIndex == 1)
                    {
                        unlockedStuff_text.text = "forest region";
                    }
                    else if (_todayUnlockStuff.unlockedIndex == 1)
                    {
                        unlockedStuff_text.text = "beach region";
                    }
                    else if (_todayUnlockStuff.unlockedIndex == 3)
                    {
                        unlockedStuff_text.text = "snow region";
                    }
                    break;
                }
            case UnlockType.CustomerAppearRate:
                {
                    unlockedStuff_image.sprite = null;
                    unlockedStuff_text.text = "Customer Appear Rate UP!";
                    break;
                }
        }
    }
}
