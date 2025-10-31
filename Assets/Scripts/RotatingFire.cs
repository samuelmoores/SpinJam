using UnityEngine;

public class RotatingFire : MonoBehaviour
{
    public ParticleSystem ps;
    public float fireIgniteVolume;
    public AudioClip rotatingSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ps.Play();
            SoundManager.instance.PlaySound(rotatingSound, transform.position, fireIgniteVolume);
        }
    }

}
