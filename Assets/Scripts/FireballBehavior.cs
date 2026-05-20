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
    }
}
