using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmSound : MonoBehaviour
{
    public AudioSource bgmAudio;
    void Start()
    {
        bgmAudio = GetComponent<AudioSource>();
    }
}
