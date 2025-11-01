using System.Collections;
using UnityEngine;

public class Pumpkin : MonoBehaviour
{
    GameObject player;
    Witch witch;
    public Rigidbody rb;
    public ParticleSystem magicSmoke_Instance;
    public AudioClip[] fireworkSounds;
    

    private void Start()
    {
        player = PumpkinSpawner.instance.GetPlayer();

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Vector2 direction = transform.position - player.transform.position;
            rb.AddForce(direction * 500);
            StartCoroutine(TrackMovement());
        }
        
    }

    IEnumerator TrackMovement()
    {
        float current = 0.0f;

        while(current < 0.5f)
        {
            current += Time.deltaTime;

            yield return null;
        }

        GameObject witchObject = PumpkinSpawner.instance.GetWitch();

        if(witchObject)
        {
            witch = witchObject.GetComponent<Witch>();
            float pumpkinExplosionRadius = Vector3.Distance(witchObject.transform.position, player.transform.position);

            Debug.Log(pumpkinExplosionRadius);

            if (pumpkinExplosionRadius < 10.0f)
                witch.TakeDamage();
        }

        ParticleSystem magicSmoke = Instantiate(magicSmoke_Instance, transform.position, Quaternion.identity);
        Destroy(magicSmoke, magicSmoke.main.duration);
        SoundManager.instance.PlaySound(fireworkSounds[Random.Range(0, fireworkSounds.Length)], transform.position, 1.0f);
        PumpkinSpawner.instance.CanSpawnPumpkin();
        Destroy(gameObject);
    }
}
