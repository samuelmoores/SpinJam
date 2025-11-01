using UnityEngine;
using UnityEngine.AI;

public class Witch : MonoBehaviour
{
    NavMeshAgent agent;
    GameObject player;
    Animator animator;
    public ParticleSystem deathSmoke_instance;
    public AudioClip laughSound;
    public AudioClip deathSound;
    bool damaged = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = PumpkinSpawner.instance.GetPlayer();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        SoundManager.instance.PlayWitchSound(laughSound, transform.position);
        Debug.Log(animator);
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = player.transform.position;

        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance < 1.25f && !damaged)
            player.GetComponent<PlayerMovement>().TakeDamage();
    }

    public void TakeDamage()
    {
        agent.isStopped = true;
        agent.speed -= 0.4f;
        SoundManager.instance.StopWitchSound();
        PlayerMovement playerMov = player.GetComponent<PlayerMovement>();

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
