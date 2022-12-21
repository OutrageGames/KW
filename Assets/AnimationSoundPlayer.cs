using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSoundPlayer : MonoBehaviour
{
    void Start()
    {
        
    }
    public void PlaySound(AudioClip audioClip)
    {
        AudioSource audioSource = GetComponentInParent<AudioSource>();
        audioSource.PlayOneShot(audioClip);
    }
}
