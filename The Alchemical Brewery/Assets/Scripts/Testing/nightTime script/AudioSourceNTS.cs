using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceNTS : MonoBehaviour
{
    public static AudioSourceNTS Instance { get; private set; }
    public static AudioSource nts_AudioSource;

    private void Awake()
    {
        Instance = this;
        nts_AudioSource = GetComponent<AudioSource>();
    }
}
