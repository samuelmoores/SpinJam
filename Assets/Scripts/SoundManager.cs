using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    GameObject loopingSound;

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
        Destroy(loopingSound);
    }
}
