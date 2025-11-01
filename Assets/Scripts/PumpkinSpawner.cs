using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.Rendering;

public class PumpkinSpawner : MonoBehaviour
{
    public static PumpkinSpawner instance;

    public GameObject pumpkinInstance;
    public GameObject player;
    public AudioClip[] spawnSounds;
    public GameObject witchInstance;
    GameObject witchCurrent;
    public Vector3 witchSpawnPosition;
    int pumpkinSpawnCount = 0;
    bool canSpawnWitch = true;
    bool canSpawnPumpkin = true;

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

    public void SpawnPumpkin()
    {
        pumpkinSpawnCount++;
        canSpawnPumpkin = false;

        GameObject pumpkin = Instantiate(pumpkinInstance, transform.position, Quaternion.identity);
        Rigidbody rb = pumpkin.GetComponent<Rigidbody>();
        Vector3 direction = transform.forward;

        if(witchCurrent)
            direction = (witchCurrent.transform.position - transform.position).normalized;

        direction.y = 0.25f;
        rb.AddForce(direction * 400);
        rb.angularVelocity = new Vector3(Random.Range(1, 10), Random.Range(1, 10), Random.Range(1, 10));
        SoundManager.instance.PlaySound(spawnSounds[Random.Range(0, 2)], transform.position, 1.0f);
    }

    public void SpawnWitch()
    {
        if (pumpkinSpawnCount % 2 == 0 && canSpawnWitch)
        {
            witchCurrent = Instantiate(witchInstance, witchSpawnPosition, Quaternion.identity);
            canSpawnWitch = false;
        }
    }

    public GameObject GetPlayer()
    {
        return player;
    }

    public GameObject GetWitch()
    {
        return witchCurrent;
    }

    public void CanSpawnWitch()
    {
        canSpawnWitch = true;
    }

    public void CanSpawnPumpkin()
    {
        canSpawnPumpkin = true;
    }

    public bool GetSpawnPumpkin()
    {
        return canSpawnPumpkin;
    }
}
