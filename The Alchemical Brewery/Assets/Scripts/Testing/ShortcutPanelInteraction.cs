using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShortcutPanelInteraction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(StageManager.dayTimeGameplay)
        {
            anim.SetBool("showPanel", true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        anim.SetBool("showPanel", false);
    }
}
