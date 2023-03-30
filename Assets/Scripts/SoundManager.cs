using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Sources")]
    public AudioSource music;
    public AudioSource explosionFX;
    public AudioSource shotFX;
    public AudioSource boostFX;
    public AudioSource alertFX;

    [Header("Clips")]
    public AudioClip victoryMusic;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayExplosion()
    {
        explosionFX.Play();
    }

    public void PlayShot()
    {
        shotFX.Play();
    }

    public void PlayBooster()
    {
        boostFX.volume = 1.0f;
        boostFX.Play();
    }

    public void StopBooster()
    {
        boostFX.volume = 0f;
        boostFX.Stop();
    }

    public void PlayAlert()
    {
        StartCoroutine(Alarm());
    }

    public void PlayVictoryMusic()
    {
        music.clip = victoryMusic;
        music.Play();
    }

    private IEnumerator Alarm()
    {
        alertFX.Play();

        yield return new WaitWhile(() => alertFX.time < 2f);

        alertFX.Stop();
    }
}
