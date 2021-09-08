using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class ChoosePotionSceneManager : MonoBehaviour
{
    public static ChoosePotionSceneManager Instance { get; private set; }

    [System.NonSerialized] public List<CustomerData> customerTypeToday = null;

    public Image introImage;

    public bool testing = false;

    public void Awake()
    {
        Instance = this;

        if(testing)
        {
            SaveManager.Load();
        }

        StartCoroutine(FadeScene(true, 0));
    }

    private void Start()
    {
        ///CHOSING & CHOSEN POTION PANEL
        Canvas.ForceUpdateCanvases();
        ChoosingPotionHandler.Instance.AssignAllAvailablePotion();
        ChoosingPotionHandler.Instance.InstantiateAvailablePotion();
        ChosenPotionHandler.Instance.UpdateChosenPotion();

        ///GET CUSTOMER TYPE TODAY (IF NOT NULL)
        if(StageManager.customerTypeToday != null)
        {
            customerTypeToday = StageManager.customerTypeToday;
        }

        ///START SPAWNING CUSTOMER
        StartCoroutine(CustomerEntranceHandler.Instance.SpawningCustomer());
    }

    IEnumerator FadeScene(bool startScene, int indexScene)
    {
        var sequence = DOTween.Sequence();

        if (startScene) //fade in effect
        {
            sequence.Join(introImage.DOFade(0f, 2));
            yield return new WaitForSeconds(2f);
            introImage.gameObject.SetActive(false);
        }
        else //fade out effect
        {
            introImage.gameObject.SetActive(true);
            sequence.Join(introImage.DOFade(255f, 2));

            //Start loading next scene
            LoadingScreenScript.nextSceneIndex = indexScene;
            AsyncOperation operation = SceneManager.LoadSceneAsync(0);
            operation.allowSceneActivation = false;

            yield return new WaitForSeconds(2f);

            //enable change scene
            operation.allowSceneActivation = true;
        }
    }

    public void OpenShop()
    {
        StartCoroutine(FadeScene(false, 4));
    }
    

}
