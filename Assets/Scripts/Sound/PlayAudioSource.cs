using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioSource : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    public void Play_Audio()
    {
        audioSource.Play();
    }
}
