using UnityEngine;

public class PlayerInstance : MonoBehaviour
{
    public static PlayerInstance playerInstance;

    public float height = 1f; // Altura para inimigos poderem seguir e atirar

    private void Awake()
    {
        playerInstance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
