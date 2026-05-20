using UnityEngine;
using UnityEngine.AI;

public class GhostBehavior : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private NavMeshAgent agent;

    [SerializeField] private float heightMod = 3f;

    [SerializeField] private float approachDist = 7f;

    //[SerializeField] private AudioSource source;
    //[SerializeField] private AudioClip deathSfx;

    private void Awake()
    {
        transform.position += new Vector3(0f, heightMod, 0f);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent.speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(transform.position, PlayerInstance.playerInstance.transform.position);

        if (dist < approachDist)
        {
            agent.SetDestination(PlayerInstance.playerInstance.transform.position);
        }
        else
        {
            Vector3 dest = new Vector3(PlayerInstance.playerInstance.transform.position.x, transform.position.y, PlayerInstance.playerInstance.transform.position.z);
            agent.SetDestination(dest);
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
        }
    }
}
