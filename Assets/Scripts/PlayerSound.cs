using NUnit.Framework;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    public float footstepVolume = 2.0f;
    public AudioClip[] footsteps;
    public void Footstep()
    {
        SoundManager.instance.PlaySound(footsteps[Random.Range(footsteps.Length, 10000) % footsteps.Length], transform.position, footstepVolume);
    }
}
