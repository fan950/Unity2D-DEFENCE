using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSound : MonoBehaviour
{
    public AudioClip audioClip;
    public AudioSource audioSource;
    void Start()
    {
        audioSource.clip = audioClip;

        for(int i=0;i<3; ++i)
        {
            audioSource.Play();
        }
    }

  
}
