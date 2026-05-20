using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private float startTime;

    private int horda = 1;
    private float hordaIncreaseCooldown = 60f;
    private float lastIncreaseHorda;

    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI hordaText;

    private void Awake()
    {
        startTime = Time.time;
        lastIncreaseHorda = startTime;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        string tempo = "";

        float t = Time.time - startTime;
        int min = (int)t / 60;
        if (min < 10) tempo += "0";
        tempo += min.ToString("N0") + ":";

        float sec = t % 60;
        if (sec < 10) tempo += "0";
        tempo += sec.ToString("F2");

        timeText.text = $"Tempo: {tempo}";

        if (t - lastIncreaseHorda > hordaIncreaseCooldown)
        {
            horda += 1;
            hordaText.text = $"Horda: {horda}";
            lastIncreaseHorda = t;
            EnemySpawner.enemySpawner.NextHorda();
        }
    }
}
