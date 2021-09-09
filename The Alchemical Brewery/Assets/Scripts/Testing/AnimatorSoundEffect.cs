using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorSoundEffect : MonoBehaviour
{
    public void DrinkingPotionVFX()
    {
        if(AudioSourceDTS.dts_AudioSource != null)
        {
            AudioSourceDTS.dts_AudioSource.PlayOneShot(SoundManager.drinkPotion);
        }
    }
}
