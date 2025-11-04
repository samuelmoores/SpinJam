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
        float flashInterval = 0.1f;
        float flashTimer = 0.0f;
        bool visible = true;
        MeshRenderer renderer = GetComponent<MeshRenderer>();

        while(current < 1.0f)
        {
            current += Time.deltaTime;
            flashTimer += Time.deltaTime;
            transform.localScale += new Vector3(Time.deltaTime * 3, Time.deltaTime * 3, Time.deltaTime  * 3);

            // handle flashing
            if (flashTimer > flashInterval)
            {
                visible = !visible;
                renderer.enabled = visible; // toggle visibility
                flashTimer = 0.0f;
            }

            //TODO: homing missile

            yield return null;
        }

        GameObject witchObject = PumpkinSpawner.instance.GetWitch();

        if(witchObject)
        {
            witch = witchObject.GetComponent<Witch>();
            float pumpkinExplosionRadius = Vector3.Distance(witchObject.transform.position, transform.position);

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
