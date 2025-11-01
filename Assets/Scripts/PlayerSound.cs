using NUnit.Framework;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    public float footstepVolume = 2.0f;
    public AudioClip[] footsteps;
    int prevFootstep = 0;
    int footstep = 0;
    public void Footstep()
    {
        while(footstep == prevFootstep)
            footstep = Random.Range(footsteps.Length, 10000) % footsteps.Length;

        SoundManager.instance.PlaySound(footsteps[footstep], transform.position, footstepVolume);
        prevFootstep = footstep;
    }
}
