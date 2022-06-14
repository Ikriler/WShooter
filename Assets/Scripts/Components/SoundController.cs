using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioSource _reload;
    public AudioSource _walk;
    public AudioSource _pistolShoot;
    public AudioSource _rifleShoot;
    public AudioSource _rpgShoot;
    public AudioSource _explode;
    
    public void AudioReload()
    {
        _reload.Play();
    }

    public void AudioWalk()
    {
        _walk.Play();
    }

    public void AudioPistolShoot()
    {
        _pistolShoot.Play();
    }

    public void AudioRifleShoot()
    {
        _rifleShoot.Play();
    }

    public void AudioRpgShoot()
    {
        _rpgShoot.Play();
    }

    public void AudioExplode()
    {
        _explode.Play();
    }
}
