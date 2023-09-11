using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSound : MonoBehaviour
{
    public AudioSource effectAudio;
    void Awake()
    {
        effectAudio = GetComponent<AudioSource>();
    }

}