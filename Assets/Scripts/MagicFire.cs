using System.Collections;
using UnityEngine;

public class MagicFire : MonoBehaviour
{
    public ParticleSystem ps;
    public float shrinkSpeed;
    public AudioClip fireIgnite;
    public AudioClip fireSound;
    bool stopPortal = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            stopPortal = false;
            ps.transform.localScale = new Vector3(4.0f, 4.0f, 4.0f);
            ps.Play();
            StartCoroutine(RunPortal());
            SoundManager.instance.PlaySound(fireIgnite, transform.position, 1.0f);
            SoundManager.instance.PlayLoopingSound(fireSound, transform.position, 1.0f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            stopPortal = true;
            ps.Stop();
            SoundManager.instance.StopLoopingSound();
        }
    }
    IEnumerator RunPortal()
    {
        while(ps.transform.localScale.x > 0.0f && !stopPortal)
        {
            float scaleSpeed = Time.deltaTime * shrinkSpeed;
            ps.transform.localScale -= new Vector3 (scaleSpeed, scaleSpeed, scaleSpeed);
            yield return null;
        }

        SoundManager.instance.StopLoopingSound();

    }
}
