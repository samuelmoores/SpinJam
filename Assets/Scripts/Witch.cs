using UnityEngine;
using UnityEngine.AI;

public class Witch : MonoBehaviour
{
    NavMeshAgent agent;
    GameObject player;
    Animator animator;
    public ParticleSystem deathSmoke_instance;
    public ParticleSystem poisonMagic;
    public AudioClip laughSound;
    public AudioClip deathSound;
    bool damaged = false;
    float damageRadius = 2.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = PumpkinSpawner.instance.GetPlayer();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        SoundManager.instance.PlayWitchSound(laughSound, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = player.transform.position;

        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance < damageRadius && !damaged)
            player.GetComponent<PlayerMovement>().TakeDamage();
    }

    public void TakeDamage()
    {
        agent.isStopped = true;
        agent.speed -= 0.3f;
        damageRadius -= 0.25f;
        poisonMagic.transform.localScale *= 0.75f;
        SoundManager.instance.StopWitchSound();
        PlayerMovement playerMov = player.GetComponent<PlayerMovement>();
        poisonMagic.Stop();

        playerMov.Heal();
        playerMov.SetPlayerSpeed(50);

        if (agent.speed <= 0.0f)
            animator.SetBool("die", true);
        else
        {
            animator.SetTrigger("damage");
            damaged = true;
        }
    }

    public void Unfreeze()
    {
        agent.isStopped = false;
        SoundManager.instance.PlayWitchSound(laughSound, transform.position);
        poisonMagic.Play();
        damaged = false;
    }

    public void Die()
    {
        SoundManager.instance.PlaySound(deathSound, transform.position, 0.25f);
        ParticleSystem deathSmoke = Instantiate(deathSmoke_instance, transform.position, Quaternion.identity);
        Destroy(deathSmoke, deathSmoke.main.duration);
        PumpkinSpawner.instance.CanSpawnWitch();
        Destroy(gameObject);
    }

}
