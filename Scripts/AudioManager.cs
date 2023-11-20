using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource asMusic;
    public AudioClip menuMusic;
    public AudioClip gameMusic;
    public AudioSource asSFX;
    public AudioSource asSFX_2;
    public AudioSource asSFX_3;
    public AudioSource asSFX_4;
    public AudioClip powSFX;
    public AudioClip buildSFX;
    public AudioClip build_2SFX;
    public AudioClip finishSFX;
    public AudioClip finish_2SFX;
    public AudioClip houseDestroySFX;
    public AudioClip bossReadySFX;
    public AudioClip bossDamagedSFX;
    public AudioClip monolith1;
    public AudioClip monolith2;
    public AudioClip monolith3;
    public void PlayMenuMusic()
    {
        asMusic.clip = menuMusic;
        asMusic.Play();
    }
    public void PlayGameMusic()
    {
        asMusic.clip = gameMusic;
        asMusic.Play();
    }
    public void PowPlayerOneSFX()
    {
        asSFX.clip = powSFX;
        asSFX.loop = false;
        asSFX.Play();
    }
    public void PowPlayerTwoSFX()
    {
        asSFX_2.clip = powSFX;
        asSFX_2.loop = false;
        asSFX_2.Play();
    }
    public void BuildPlayerOneSFX()
    {
        asSFX.clip = buildSFX;
        asSFX.loop = true;
        asSFX.Play();
    }
    public void BuildPlayerTwoSFX()
    {
        asSFX_2.clip = build_2SFX;
        asSFX.loop = true;
        asSFX_2.Play();
    }
    public void FinishPlayerOneSFX()
    {
        asSFX.clip = finishSFX;
        asSFX.loop = false;
        asSFX.Play();
    }
    public void FinishPlayerTwoSFX()
    {
        asSFX_2.clip = finish_2SFX;
        asSFX_2.loop = false;
        asSFX_2.Play();
    }
    public void DamageHouseSFX()
    {
        asSFX_3.clip = houseDestroySFX;
        asSFX_3.Play();
    }
    public void BossReadySFX()
    {
        asSFX_3.clip = bossReadySFX;
        asSFX_3.Play();
    }
    public void BossDefeatedSFX()
    {
        asSFX_3.clip = bossDamagedSFX;
        asSFX_3.Play();
    }
    public void Monolith1()
    {
        asSFX_4.clip = monolith1;
        asSFX_4.Play();
    }
    public void Monolith2()
    {
        asSFX_4.clip = monolith2;
        asSFX_4.Play();
    }
    public void Monolith3()
    {
        asSFX_4.clip = monolith3;
        asSFX_4.Play();
    }
    public void StopPlayerOneSFX()
    {
        asSFX.Stop();
    }
    public void StopPlayerTwoSFX()
    {
        asSFX_2.Stop();
    }
}
