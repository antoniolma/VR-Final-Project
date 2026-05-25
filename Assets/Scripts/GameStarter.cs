using UnityEngine;

public class GameStarter : MonoBehaviour
{
    [SerializeField] private GameObject startSign;
    [SerializeField] private GameObject signGame;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (!other.gameObject.name.Contains("PlayerController"))
        {
            startSign.SetActive(false);
            signGame.SetActive(true);
            EnemySpawner.enemySpawner.StartGame();

        }
    }
}
