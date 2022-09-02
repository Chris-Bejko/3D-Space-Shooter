using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    AudioClip[] _uiClips;

    [SerializeField]
    AudioSource _uiSFX;

    [SerializeField]
    AudioSource _inGameSFX;

    public void PlayInGameSound(AudioClip clip)
    {
        _inGameSFX.clip = clip;
        _inGameSFX.Play();
    }



}
