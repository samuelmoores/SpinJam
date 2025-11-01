using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    GameObject loopingSound;
    GameObject witchSound;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }
    
    public void PlaySound(AudioClip clip, Vector3 position, float volume)
    {
        GameObject soundObject = new GameObject(clip.name);
        AudioSource source = soundObject.AddComponent<AudioSource>();
        source.clip = clip;
        source.volume = volume;
        source.Play();
        Destroy(soundObject, clip.length);
    }

    public void PlayLoopingSound(AudioClip clip, Vector3 position, float volume)
    {
        loopingSound = new GameObject(clip.name);
        AudioSource source = loopingSound.AddComponent<AudioSource>();
        source.loop = true;
        source.clip = clip;
        source.volume = volume;
        source.Play();
    }

    public void StopLoopingSound()
    {
        if (loopingSound)
            StartCoroutine(FadeOutSound());
    }

    IEnumerator FadeOutSound()
    {
        AudioSource source = loopingSound.GetComponent<AudioSource>();

        while (source && source.volume > 0.0f)
        {
            source.volume -= Time.deltaTime;
            yield return null;
        }
        Destroy(loopingSound);
    }

    public void PlayWitchSound(AudioClip clip, Vector3 position)
    {
        witchSound = new GameObject(clip.name);
        AudioSource source = witchSound.AddComponent<AudioSource>();
        source.loop = true;
        source.clip = clip;
        //source.spatialBlend = 1.0f;
        source.volume = 1.0f;
        source.Play();
    }

    public void StopWitchSound()
    {
        witchSound.GetComponent<AudioSource>().Stop();
    }
}
