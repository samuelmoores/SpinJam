using System.Collections;
using UnityEngine;

public class Pumpkin : MonoBehaviour
{
    GameObject player;
    Witch witch;
    public Rigidbody rb;
    public ParticleSystem magicSmoke_Instance;
    public ParticleSystem dustPuff_Instance;
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
            rb.AddForce(direction * 10 * player.GetComponent<PlayerMovement>().GetPlayerSpeed());
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
            float pumpkinExplosionRadius = Vector3.Distance(witchObject.transform.position, transform.position);

            Debug.Log(pumpkinExplosionRadius);

            if (pumpkinExplosionRadius < 7.5f)
                witch.TakeDamage();
        }

        SoundManager.instance.PlaySound(fireworkSounds[Random.Range(0, fireworkSounds.Length)], transform.position, 1.0f);

        ParticleSystem magicSmoke = Instantiate(magicSmoke_Instance, transform.position, Quaternion.Euler(Vector3.right * -90));
        magicSmoke.transform.localScale *= 1.5f;

        ParticleSystem smokePuff = Instantiate(dustPuff_Instance, transform.position, Quaternion.identity);
        smokePuff.transform.localScale *= 0.75f;
        Destroy(magicSmoke, magicSmoke.main.duration);
        Destroy(smokePuff, smokePuff.main.duration);

        PumpkinSpawner.instance.CanSpawnPumpkin();
        Destroy(gameObject);
    }
}
