using UnityEngine;

public class FireballBehavior : MonoBehaviour
{
    [SerializeField] private float speed = 0.1f;

    private Vector3 playerDir;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Vector3 playerPos = PlayerInstance.playerInstance.transform.position;
        playerPos.y += PlayerInstance.playerInstance.height;
        playerDir = playerPos - transform.position;
        playerDir = playerDir.normalized;

        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += speed * playerDir;

        Vector3 playerPos = PlayerInstance.playerInstance.transform.position;
        Vector3 myPos = transform.position;
        float dist = Vector3.Distance(playerPos, myPos);
        if (dist < 2)
        {
            PlayerInstance.playerInstance.TakeDamage();
            Destroy(gameObject);            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains("Bullet"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        } else if (collision.gameObject.name.Contains("Hammer"))
        {
            Destroy(gameObject);
        }
    }
}
