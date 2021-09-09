using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSoundAudio : MonoBehaviour
{
    AudioSource BGM;

    private void Awake()
    {
        BGM = GetComponent<AudioSource>();
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public IEnumerator ChangeNewMusic(AudioClip _music, float fadeInTime)
    {
        StopAllCoroutines();
        BGM.Stop();
        BGM.clip = _music;
        BGM.Play();
        BGM.volume = 0;

        float tempTimer = 0;
        while(tempTimer < fadeInTime)
        {
            BGM.volume = Mathf.Lerp(0, 1, tempTimer / fadeInTime);
            tempTimer += Time.fixedDeltaTime;
            yield return null;
        }
        BGM.volume = 1;
    }
}
