using System;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    public float hammerSpeed = 3f;

    public List<GameObject> enemyList;
    public float targetRotation;

    public float killDistance = 0.2f; 
    public int enemiesKilled = 0;
    public int maxKillsPerHammer = 3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        enemyList = EnemySpawner.enemySpawner.enemiesSpawned;
        GameObject closest = GetClosestEnemy();
        if (closest != null)
        {
            Transform center = closest.transform.Find("Center");
            Vector3 relativePos = center.position - transform.position;

            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.forward);
            transform.rotation = rotation;

            float step = hammerSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, center.position, step);
        }

        if (enemiesKilled >= maxKillsPerHammer)
        {
            Destroy(gameObject);
            PlayerInstance.playerInstance.hammersList.Remove(gameObject);
        }
    }

    public GameObject GetClosestEnemy()
    {
        GameObject closest = null;
        float minDist = Mathf.Infinity;

        // TO DO:
        // Add later to go after the CENTER of the enemy
        Vector3 hammerPosition = transform.position;

        foreach (var enemy in enemyList)
        {
            Vector3 enemyPosition = enemy.transform.position;

            float d = Vector3.Distance(hammerPosition, enemyPosition);
            if (d < minDist)
            {
                minDist = d;
                closest = enemy;
            }
        }

        return closest;
    }
}
