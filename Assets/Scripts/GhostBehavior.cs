using UnityEngine;
using UnityEngine.AI;

public class GhostBehavior : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private NavMeshAgent agent;

    [SerializeField] private float approachDist = 2f;

    private bool isStunned = false;
    private float timeStunned;
    private float durationStun = 4f;

    //[SerializeField] private AudioSource source;
    //[SerializeField] private AudioClip deathSfx;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent.speed = speed;
        durationStun = PlayerInstance.playerInstance.durationStun;
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
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("Bullet"))
        {
            Destroy(collision.gameObject);
            //source.PlayOneShot(deathSfx);
            EnemySpawner.enemySpawner.enemiesSpawned.Remove(gameObject);
            Destroy(gameObject);
        } else if (collision.gameObject.name.Contains("Hammer"))
        {
            collision.gameObject.GetComponent<Hammer>().enemiesKilled++;
            // source.PlayOneShot(deathSfx);
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
