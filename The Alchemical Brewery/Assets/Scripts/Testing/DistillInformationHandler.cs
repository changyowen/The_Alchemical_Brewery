using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistillInformationHandler : MonoBehaviour
{
    public int holderIngredient = -1;
    public bool isBoiling = false;

    public ScriptableObjectHolder SO_holder;
    public GameObject _stationCanvas;
    public Image _ingredientImage;

    private void Update()
    {
        if(isBoiling)
        {
            _stationCanvas.SetActive(false);
        }
        else
        {
            if(holderIngredient != -1)
            {
                _stationCanvas.SetActive(true);
                _ingredientImage.sprite = SO_holder.ingredientSO[holderIngredient].ingredientSprite;
            }
            else
            {
                _stationCanvas.SetActive(false);
            }
        }
    }

}
