using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CraftingSystemManager : MonoBehaviour
{
    //system data
    public int[] IngredientAmountArray;
    public int[] IngredientSlot = { 0, 0, 0, 0 };

    //access
    public Text[] ingredientAmount_text;
    public GameObject[] IngredientSlot_gameObject;
    public Sprite[] ingredientSlot_image;
    public GameObject potCenter;
    Animator potAnim;
    public GameObject blackScreen;
    public GameObject resultImage;
    public Sprite[] result_sprite;
    public GameObject resultPanel;
    Animator resultPanel_anim;

    // Start is called before the first frame update
    void Start()
    {
        potAnim = potCenter.GetComponent<Animator>();
        resultPanel_anim = resultPanel.GetComponent<Animator>();
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

    public void StartBrewing()
    {
        //disable button;
        StartCoroutine(ShowCraftResult());
    }

    IEnumerator ShowCraftResult()
    {
        potAnim.SetBool("Pot_Bounce", true);
        blackScreen.SetActive(true);

        yield return new WaitForSeconds(1f);

        GameObject result_1 = Instantiate(resultImage, resultImage.transform.position, resultImage.transform.rotation) as GameObject;
        result_1.transform.SetParent(blackScreen.transform, false);
        result_1.GetComponent<Image>().sprite = result_sprite[0]; 

        yield return new WaitForSeconds(2f);

        GameObject result_2 = Instantiate(resultImage, resultImage.transform.position, resultImage.transform.rotation) as GameObject;
        result_2.transform.SetParent(blackScreen.transform, false);
        result_2.GetComponent<Image>().sprite = result_sprite[1];

        yield return new WaitForSeconds(2f);

        GameObject result_3 = Instantiate(resultImage, resultImage.transform.position, resultImage.transform.rotation) as GameObject;
        result_3.transform.SetParent(blackScreen.transform, false);
        result_3.GetComponent<Image>().sprite = result_sprite[2];

        yield return new WaitForSeconds(2f);

        GameObject result_4 = Instantiate(resultImage, resultImage.transform.position, resultImage.transform.rotation) as GameObject;
        result_4.transform.SetParent(blackScreen.transform, false);
        result_4.GetComponent<Image>().sprite = result_sprite[3];

        yield return new WaitForSeconds(2f);

        GameObject result_5 = Instantiate(resultImage, resultImage.transform.position, resultImage.transform.rotation) as GameObject;
        result_5.transform.SetParent(blackScreen.transform, false);
        result_5.GetComponent<Image>().sprite = result_sprite[4];

        yield return new WaitForSeconds(2f);

        GameObject result_6 = Instantiate(resultImage, resultImage.transform.position, resultImage.transform.rotation) as GameObject;
        result_6.transform.SetParent(blackScreen.transform, false);
        result_6.GetComponent<Image>().sprite = result_sprite[5];

        yield return new WaitForSeconds(3f);

        potAnim.SetBool("Pot_Bounce", false);
        blackScreen.SetActive(false);
        ResetIngredientSlot();

        resultPanel_anim.SetTrigger("ShowResult");
        DailyStart.forthPotionUnlocked = true;
        //brew button enable;
    }

    void ResetIngredientSlot()
    {
        for(int i = 0; i < 4; i++)
        {
            IngredientSlot[i] = 0;
        }
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

    public void ExitScene()
    {
        SceneManager.LoadScene(2);
    }
}
