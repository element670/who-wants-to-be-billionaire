using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip clip;

    public void PlaySound()
    {
        source.clip = clip;
        source.Play();
    }
}
