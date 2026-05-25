using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.AI;

public class GolemBehavior : MonoBehaviour
{
    private int health = 10;

    [SerializeField] private float speed = 0.25f;
    [SerializeField] private NavMeshAgent agent;

    private bool isStunned = false;
    private float timeStunned;
    private float durationStun = 4f;

    [SerializeField] private Renderer renderer;
    private float lastDamage;
    private float damagedCooldown = 0.5f;
    private Color red = new Color(1.55930245f, 0.015431962f, 0, 1);
    private Color blue = new Color(0, 1.1647799f, 1.55930245f, 1f);

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent.speed = speed;
        agent.SetDestination(PlayerInstance.playerInstance.transform.position);
        durationStun = PlayerInstance.playerInstance.durationStun;
    }

    // Update is called once per frame
    void Update()
    {
        if (isStunned && Time.time >= timeStunned + durationStun)
        {
            agent.enabled = true;
            isStunned = false;
        }

        if (Time.time - lastDamage > damagedCooldown)
        {
            renderer.materials[1].color = blue;
        }
    }

    void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            EnemySpawner.enemySpawner.enemiesSpawned.Remove(gameObject);
            Destroy(gameObject);
        }
        renderer.materials[1].color = red;
        lastDamage = Time.time;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("Bullet"))
        {
            Destroy(collision.gameObject);
            TakeDamage(1);
        }
        else if (collision.gameObject.name.Contains("Hammer"))
        {
            collision.gameObject.GetComponent<Hammer>().enemiesKilled++;
            // source.PlayOneShot(deathSfx);
            EnemySpawner.enemySpawner.enemiesSpawned.Remove(gameObject);
            TakeDamage(1);

            // Para evitar bugs, martelo morre ao pater em bosses fortes
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.name.Contains("Web"))
        {
            agent.enabled = false;
            isStunned = true;
            timeStunned = Time.time;
        }
    }
}
