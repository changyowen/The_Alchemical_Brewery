using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceDTS : MonoBehaviour
{
    public static AudioSourceDTS Instance { get; private set; }
    public static AudioSource dts_AudioSource;

    private void Awake()
    {
        Instance = this;
        dts_AudioSource = GetComponent<AudioSource>();
    }
}
