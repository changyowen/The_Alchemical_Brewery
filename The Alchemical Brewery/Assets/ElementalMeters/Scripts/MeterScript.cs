using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MeterScript : MonoBehaviour
{
    public int elementIndex = 0;

    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public TextMeshProUGUI skillRemaining_TMP;
    public GameObject elementReadyGlowing_obj;
    public GameObject clickGlow_obj;

    private Coroutine clickGlow_coroutine = null;

    private void Awake()
    {
        SetMaxHealth(4); //set element maximum 4
    }

    private void Update()
    {
        if(ElementMeterPanel.Instance != null)
        {
            //update meter panel
            SetHealth((float)ElementMeterPanel.Instance.elementMana[elementIndex]);

            if (skillRemaining_TMP != null)
            {
                if(ElementSkillManager.Instance.skillActivated[elementIndex])
                {
                    //update skill remaining
                    if (ElementMeterPanel.Instance.elementSkillRemaining[elementIndex] != 0)
                        skillRemaining_TMP.text = "" + ElementMeterPanel.Instance.elementSkillRemaining[elementIndex];
                    else
                        skillRemaining_TMP.text = "";
                }
                else
                {
                    skillRemaining_TMP.text = "";
                }
            }

            //update skill ready glowing
            if (elementReadyGlowing_obj != null)
            {
                if (ElementMeterPanel.Instance.elementReady[elementIndex])
                    elementReadyGlowing_obj.SetActive(true);
                else
                    elementReadyGlowing_obj.SetActive(false);
            }
        }
    }

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate (1f) ;

    }

    public void SetHealth(float health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void ActivateSkill()
    {
        if(ElementMeterPanel.Instance != null)
        {
            //if skill is ready
            if (ElementMeterPanel.Instance.elementReady[elementIndex])
            {
                //deactivate glow & stop all current coroutine
                clickGlow_obj.SetActive(false);
                if (clickGlow_coroutine != null)
                {
                    StopCoroutine(clickGlow_coroutine);
                }
                ///REACTIVATE CLICK GLOW
                clickGlow_coroutine = StartCoroutine(ActivateClickGlow());

                ///ACTIVATE SKILL
                if (ElementMeterPanel.Instance != null)
                {
                    ElementMeterPanel.Instance.ActivateElementSkill(elementIndex);
                }
            }
            else //if skill not ready
            {
                //
            }
        }
    }

    IEnumerator ActivateClickGlow()
    {
        clickGlow_obj.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        clickGlow_obj.SetActive(false);
        yield return null;
    }
}
