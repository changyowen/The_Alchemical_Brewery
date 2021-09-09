using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerResultPanel : MonoBehaviour
{
    public Transform customerContent_transform;

    public IEnumerator StartCustomerContent(CustomerLevelStoring[] initialCustomerLevelArray, CustomerLevelStoring[] finalCustomerLevelArray)
    {
        ///ACTIVATE EACH CUSTOMER CONTENT
        for (int i = 0; i < finalCustomerLevelArray.Length; i++)
        {
            //set active customer content
            Transform currentCustomerContent = customerContent_transform.GetChild(i);
            currentCustomerContent.gameObject.SetActive(true);

            //reference customer content
            Image customerImage_image = currentCustomerContent.GetChild(0).GetComponent<Image>();
            Text customerName_text = currentCustomerContent.GetChild(1).GetComponent<Text>();
            Text customerLevel_text = currentCustomerContent.GetChild(2).GetComponent<Text>();
            Transform customerExperienceBarPivot = currentCustomerContent.GetChild(3).GetChild(0);

            //assign initial customer level
            customerImage_image.sprite = initialCustomerLevelArray[i].customerSprite;
            customerName_text.text = initialCustomerLevelArray[i].customerName;
            if (initialCustomerLevelArray[i].customerLevel == 6)
            {
                customerLevel_text.text = "LV MAX";
            }
            else
            {
                customerLevel_text.text = "LV " + initialCustomerLevelArray[i].customerLevel;
            }
            Vector3 newlocalScale = new Vector3(initialCustomerLevelArray[i].customerExperiencePercentage, 1, 1);
            customerExperienceBarPivot.localScale = newlocalScale;

            //get initial and final level
            float initialLevel = 0;
            if (initialCustomerLevelArray[i].customerLevel != 6)
            {
                initialLevel = (float)initialCustomerLevelArray[i].customerLevel + initialCustomerLevelArray[i].customerExperiencePercentage;
            }
            else
            {
                initialLevel = 6f;
            }

            float finalLevel = 0f;
            if (finalCustomerLevelArray[i].customerLevel != 6)
            {
                finalLevel = (float)finalCustomerLevelArray[i].customerLevel + finalCustomerLevelArray[i].customerExperiencePercentage;
            }
            else
            {
                finalLevel = 6f;
            }

            ///START LEVELING ANIMATION
            while (initialLevel != finalLevel)
            {
                initialLevel += .5f * Time.unscaledDeltaTime;

                if (initialLevel >= finalLevel)
                {
                    initialLevel = finalLevel;
                }

                if (initialLevel >= 6f)
                {
                    Vector3 tempVector3 = new Vector3(1, 1, 1);
                    customerExperienceBarPivot.localScale = tempVector3;
                }
                else
                {
                    Vector3 tempVector3 = new Vector3(initialLevel % 1, 1, 1);
                    customerExperienceBarPivot.localScale = tempVector3;
                }

                if (initialLevel < 6)
                {
                    customerLevel_text.text = "LV " + (int)initialLevel;
                }
                else
                {
                    customerLevel_text.text = "LV MAX";
                }
                yield return null;
            }
            yield return new WaitForSeconds(0.2f);
        }

    }
}
