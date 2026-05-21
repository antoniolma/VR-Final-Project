using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (
            other.gameObject.name.ToLower().Contains("fireball") ||
            other.gameObject.name.ToLower().Contains("lich") ||
            other.gameObject.name.ToLower().Contains("burrow") ||
            other.gameObject.name.ToLower().Contains("ghost")
            )
        {
            PlayerInstance.playerInstance.TakeDamage();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if (
            collision.gameObject.name.ToLower().Contains("fireball") ||
            collision.gameObject.name.ToLower().Contains("lich") ||
            collision.gameObject.name.ToLower().Contains("burrow") ||
            collision.gameObject.name.ToLower().Contains("ghost")
            )
        {
            PlayerInstance.playerInstance.TakeDamage();
        }
    }
}
