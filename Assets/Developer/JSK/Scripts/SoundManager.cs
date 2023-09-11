using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    #region Singleton
    private static SoundManager instance = null;
    public static SoundManager Instance
    {
        get { return instance; }
    }
    #endregion
    private void Awake()
    {
        instance = this;
    }
    private EffectSound effectSound;
    private BgmSound bgmSound;

    private void Start()
    {
        effectSound = GetComponent<EffectSound>();
        bgmSound = GetComponent<BgmSound>();
    }







    // º¼·ýÁ¶Àý
    public void ControlTotalVolume(float value)
    {
        ControlBgmVolume(value);
        ControlEffectVolume(value);
    }
    public void ControlBgmVolume(float value)
    {
        effectSound.effectAudio.volume = value;
    }
    public void ControlEffectVolume(float value)
    {
        bgmSound.bgmAudio.volume = value;
    }
}
