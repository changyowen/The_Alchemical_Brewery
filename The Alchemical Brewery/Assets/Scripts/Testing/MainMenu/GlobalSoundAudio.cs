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

        if(FindObjectsOfType<GlobalSoundAudio>().Length > 1)
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator ChangeNewMusic(AudioClip _music, float fadeInTime, float _volume)
    {
        StopAllCoroutines();
        BGM.Stop();
        BGM.clip = _music;
        BGM.Play();
        BGM.volume = 0;

        float tempTimer = 0;
        while(tempTimer < fadeInTime)
        {
            BGM.volume = Mathf.Lerp(0, _volume, tempTimer / fadeInTime);
            tempTimer += Time.fixedDeltaTime;
            yield return null;
        }
        BGM.volume = _volume;
    }

    public IEnumerator SwitchToNewMusic(AudioClip _music, float fadeOutTime, float _volume)
    {
        StopAllCoroutines();
        
        float tempTimer = 0;
        float tempVolume = BGM.volume;
        while (tempTimer < fadeOutTime)
        {
            BGM.volume = Mathf.Lerp(tempVolume, 0, tempTimer / fadeOutTime);
            tempTimer += Time.fixedDeltaTime;
            yield return null;
        }
        
        BGM.volume = 0;
        tempTimer = 0;
        BGM.Stop();
        BGM.clip = _music;
        BGM.Play();
       
        while (tempTimer < fadeOutTime)
        {
            BGM.volume = Mathf.Lerp(0, _volume, tempTimer / fadeOutTime);
            tempTimer += Time.fixedDeltaTime;
            yield return null;
        }
        BGM.volume = _volume;
    }

    public void SwitchNewMusic_directly(AudioClip _music, float _volume)
    {
        BGM.Stop();
        BGM.clip = _music;
        BGM.Play();
        BGM.volume = _volume;
    }

    public IEnumerator FadeOutMusic(float _lerpTime)
    {
        StopAllCoroutines();
        float tempTimer = 0;
        float tempVolume = BGM.volume;
        while (tempTimer < _lerpTime)
        {
            BGM.volume = Mathf.Lerp(tempVolume, 0, tempTimer / _lerpTime);
            tempTimer += Time.fixedDeltaTime;
            yield return null;
        }
        BGM.volume = 0;
    }

    public IEnumerator FadeInMusic(float _lerpTime, float _volume)
    {
        StopAllCoroutines();
        float tempTimer = 0;
        float tempVolume = BGM.volume;
        while (tempTimer < _lerpTime)
        {
            BGM.volume = Mathf.Lerp(tempVolume, _volume, tempTimer / _lerpTime);
            tempTimer += Time.fixedDeltaTime;
            yield return null;
        }
        BGM.volume = _volume;
    }

    public void ChangeVolume(float _volume)
    {
        BGM.volume = _volume;
    }
}
