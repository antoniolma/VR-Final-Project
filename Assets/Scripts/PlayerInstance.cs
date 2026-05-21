using NUnit.Framework;
using UnityEngine;
using UnityEngine.XR.Hands.Gestures;

public class PlayerInstance : MonoBehaviour
{
    public static PlayerInstance playerInstance;
    public PlayerSoundController soundController;

    public float height = 1f; // Altura para inimigos poderem seguir e atirar

    public DetectGesture detectGestureRight;
    public DetectGesture detectGestureLeft;
    private XRHandShape handShapeRight;
    private XRHandShape handShapeLeft;
    public Transform firePointLeft;
    public Transform firePointRight;

    // Poses control
    public float basicCooldown = 0.5f;
    private float basicTime;
    public float supportCooldown = 2f;
    private float supportTime;
    public float ultimateCooldown = 60f;
    private float ultimateTime;

    private float jackpotDuration = 15f;
    private float timeJackpot;
    private bool isInJackpot = false;

    public GameObject bulletPrefab;
    public GameObject hammerPrefab;
    public GameObject webPrefab;
    public float bulletSpeed = 20f;
    private float bulletLifetime = 5f;

    public int health = 3;

    private void Awake()
    {
        playerInstance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Negative value, can start shooting
        basicTime = -5f; 
        supportTime = -5f;
        ultimateTime = -5f;
        timeJackpot = -5f;
    }

    // Update is called once per frame
    void Update()
    {
        handShapeRight = detectGestureRight.shapeRecognized;
        handShapeLeft = detectGestureLeft.shapeRecognized;

        CheckBasic();
        CheckSupport();
        CheckUltimate();

        if (isInJackpot && Time.time >= timeJackpot + jackpotDuration)
        {
            isInJackpot = false;
            ControlJackpot();
        }
    }

    // ========================================================

    public void CheckBasic()
    {
        if (Time.time <= basicTime + basicCooldown)
            return;

        if (handShapeRight.name == "Gun_Right" && handShapeLeft.name == "Gun_Left")
        {
            FireBullet();
            soundController.PlayShootBullet();
            basicTime = Time.time;
        } else if (handShapeRight.name == "CallHammer_Right")
        {
            SummonHammer();
            basicTime = Time.time;
        }
    }

    public void CheckSupport()
    {
        if (Time.time <= supportTime + supportCooldown)
            return;

        if (handShapeRight.name == "SpiderMan_Right" && handShapeLeft.name == "SpiderMan_Left")
        {
            FireWeb();
            soundController.PlayShootWeb();
            supportTime = Time.time;
        }
    }

    public void CheckUltimate()
    {
        if (Time.time <= ultimateTime + ultimateCooldown)
            return;

        if (handShapeRight.name == "Hakari_Right" && handShapeLeft.name == "Hakari_Left")
        {
            print("JACKPOTTTTTTTTTTTTTTTT");
            isInJackpot = true;
            ControlJackpot();
            ultimateTime = Time.time;
        }
    }

    // ========================================================

    public void FireBullet()
    {
        // Spawn Bullets
        GameObject bulletLeft = Instantiate(
            bulletPrefab,
            firePointLeft.position,
            firePointLeft.rotation
        );

        GameObject bulletRight = Instantiate(
            bulletPrefab,
            firePointRight.position,
            firePointRight.rotation
        );

        // Apply Velocity
        Rigidbody rbLeft = bulletLeft.GetComponent<Rigidbody>();
        if (rbLeft != null)
        {
            rbLeft.linearVelocity = firePointLeft.forward * bulletSpeed;
            rbLeft.useGravity = false;
        }

        Rigidbody rbRight = bulletRight.GetComponent<Rigidbody>();
        if (rbRight != null)
        {
            rbRight.linearVelocity = firePointRight.forward * bulletSpeed;
            rbRight.useGravity = false;
        }

        // // Play gun sound
        // if (shootSound != null && source != null)
        // {
        //     source.PlayOneShot(shootSound);
        // }

        // Destroy bullet after time
        Destroy(bulletLeft, bulletLifetime);
        Destroy(bulletRight, bulletLifetime);
    }
    
    public void SummonHammer()
    {   
        GameObject hammer = Instantiate(
            hammerPrefab,
            firePointRight.position,
            Quaternion.Euler(0, firePointRight.rotation.y, 0)
        );
    }

    public void FireWeb()
    {
        // Spawn Bullets
        GameObject webLeft = Instantiate(
            webPrefab,
            firePointLeft.position,
            firePointLeft.rotation
        );

        GameObject webRight = Instantiate(
            webPrefab,
            firePointRight.position,
            firePointRight.rotation
        );

        // Apply Velocity
        Rigidbody rbLeft = webLeft.GetComponent<Rigidbody>();
        if (rbLeft != null)
        {
            rbLeft.linearVelocity = firePointLeft.forward * bulletSpeed;
            rbLeft.useGravity = false;
        }

        Rigidbody rbRight = webRight.GetComponent<Rigidbody>();
        if (rbRight != null)
        {
            rbRight.linearVelocity = firePointLeft.forward * bulletSpeed;
            rbRight.useGravity = false;
        }

        // // Play gun sound
        // if (shootSound != null && source != null)
        // {
        //     source.PlayOneShot(shootSound);
        // }

        // Destroy bullet after time
        Destroy(webLeft, bulletLifetime);
        Destroy(webRight, bulletLifetime);
    }

    public void ControlJackpot()
    {
        if (isInJackpot)
        {
            soundController.PlayJackpot();
            basicCooldown /= 10f;
            supportCooldown /= 10f;
            timeJackpot = Time.time;

            // ADICIONAR NAO TOMAR DANO
        }
        else
        {
            basicCooldown *= 10f;
            supportCooldown *= 10f;

            // ADICIONAR ACABOU IMORTALIDADE :(
        }
    }

    public void TakeDamage()
    {
        health -= 1;
        foreach (var enemy in EnemySpawner.enemySpawner.enemiesSpawned)
        {
            Destroy(enemy);
        }
        EnemySpawner.enemySpawner.enemiesSpawned = new System.Collections.Generic.List<GameObject>();
    }
}
