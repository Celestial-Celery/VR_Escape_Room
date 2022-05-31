using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_Object : MonoBehaviour
{
    public AudioSource Audio;

    public void PlaySound()
    {
        this.Audio.Play();
    }
}
