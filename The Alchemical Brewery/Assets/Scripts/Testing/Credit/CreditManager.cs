using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class CreditManager : MonoBehaviour
{
    public TextMeshProUGUI creditText;

    private void Start()
    {
        StartCoroutine(ScrollCredit()); ;
    }

    IEnumerator ScrollCredit()
    {
        float _height = creditText.preferredHeight;
        float canvasHeight = 400;

        float _currentPosY = 0;
        while(_currentPosY < (_height + canvasHeight))
        {
            _currentPosY += 65 * Time.deltaTime;
            creditText.rectTransform.localPosition = new Vector3(0, _currentPosY, 0);
            yield return null;
        }

        //Start loading next scene
        LoadingScreenScript.nextSceneIndex = 0;
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);
    }
}
