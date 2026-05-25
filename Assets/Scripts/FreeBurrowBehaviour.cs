using UnityEngine;
using UnityEngine.AI;

public class FreeBurrowBehaviour : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private NavMeshAgent agent;

    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip deathSfx;

    private bool isStunned = false;
    private float timeStunned;
    private float durationStun = 4f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent.speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStunned)
            agent.SetDestination(PlayerInstance.playerInstance.transform.position);
        
        if (isStunned && Time.time >= timeStunned + durationStun)
        {
            agent.enabled = true;
            isStunned = false;
        }

        Vector3 playerPos = PlayerInstance.playerInstance.transform.position;
        Vector3 myPos = transform.position;
        float dist = Vector3.Distance(playerPos, myPos);
        if (dist < 1)
            PlayerInstance.playerInstance.TakeDamage();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("Bullet"))
        {
            Destroy(collision.gameObject);
            source.PlayOneShot(deathSfx);
            EnemySpawner.enemySpawner.enemiesSpawned.Remove(gameObject);
            Destroy(gameObject);
        } else if (collision.gameObject.name.Contains("Hammer"))
        {
            collision.gameObject.GetComponent<Hammer>().enemiesKilled++;
            source.PlayOneShot(deathSfx);
            EnemySpawner.enemySpawner.enemiesSpawned.Remove(gameObject);
            Destroy(gameObject);
        } else if (collision.gameObject.name.Contains("Web"))
        {
            print("COLIDIU");
            agent.enabled = false;
            isStunned = true;
            timeStunned = Time.time;
        }
    }
}
