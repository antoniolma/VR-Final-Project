using System;
using UnityEngine;
using UnityEngine.AI;

public class LichBehavior : MonoBehaviour
{
    [SerializeField] private float speed = 0.5f;
    [SerializeField] private NavMeshAgent agent;

    private int currentState;
    private int STATE_FOLLOWING = 0;
    private int STATE_ATTACKING = 1;

    [SerializeField] private Transform attackSpawnPoint;
    [SerializeField] private float attackRange = 3f;
    [SerializeField] private float attackCooldown = 5f;
    private float lastAttack;

    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private float fireballLifetime = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentState = STATE_FOLLOWING;
        agent.speed = speed;
        agent.SetDestination(PlayerInstance.playerInstance.transform.position);
        lastAttack = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(transform.position, PlayerInstance.playerInstance.transform.position);
        Debug.Log(dist);

        if (dist < attackRange)
            currentState = STATE_ATTACKING;
        else
            currentState = STATE_FOLLOWING;

        if (currentState == STATE_ATTACKING)
            Attack();
        else
        {
            agent.SetDestination(PlayerInstance.playerInstance.transform.position);
            agent.speed = speed;
        }
    }

    void Attack()
    {
        agent.speed = 0f;
        if (Time.time - lastAttack < attackCooldown)
            return;

        GameObject fireball = Instantiate(fireballPrefab, transform.position + attackSpawnPoint.localPosition, Quaternion.identity);
        Destroy(fireball, fireballLifetime);
        lastAttack = Time.time;
    }
}
