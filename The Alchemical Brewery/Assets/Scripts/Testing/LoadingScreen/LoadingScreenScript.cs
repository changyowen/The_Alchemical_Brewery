using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreenScript : MonoBehaviour
{
    public static int nextSceneIndex = 0;

    public GameObject[] witchGirlAnimation_obj;

    public void Start()
    {
        RandomAnimation();
        StartCoroutine(NextScene());
    }

    IEnumerator NextScene()
    {
        //Start loading next scene
        AsyncOperation operation = SceneManager.LoadSceneAsync(nextSceneIndex);
        operation.allowSceneActivation = false;

        float tempTimer = 0;
        while (operation.progress >= 0.9f)
        {
            tempTimer += Time.unscaledDeltaTime;
            yield return null;
        }

        while(tempTimer < 5f)
        {
            tempTimer += Time.unscaledDeltaTime;
            yield return null;
        }

        operation.allowSceneActivation = true;
    }

    void RandomAnimation()
    {
        int temp = Random.Range(0, witchGirlAnimation_obj.Length);
        witchGirlAnimation_obj[temp].SetActive(true);
    }
}
