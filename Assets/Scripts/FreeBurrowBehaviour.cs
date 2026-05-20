using UnityEngine;
using UnityEngine.AI;

public class FreeBurrowBehaviour : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private NavMeshAgent agent;

    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip deathSfx;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent.speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(PlayerInstance.playerInstance.transform.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("Bullet"))
        {
            Destroy(collision.gameObject);
            source.PlayOneShot(deathSfx);
            EnemySpawner.enemySpawner.enemiesSpawned.Remove(gameObject);
            Destroy(gameObject);
        }
    }
}
