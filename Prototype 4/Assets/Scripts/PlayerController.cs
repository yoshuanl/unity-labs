using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 20.0f;
    private bool hasPowerup = false;
    private float powerupStrength = 30.0f;

    public GameObject powerupIndicator;
    private Rigidbody playerRb;
    private GameObject focalPoint;

    // for shooting star
    public PowerUpType currentPowerUp = PowerUpType.None;
    public GameObject missilePrefab;
    private GameObject tmpMissile; // used for spawning stars
    private Coroutine powerupCountdown;

    // for smash
    public float hangTime;
    public float smashSpeed;
    public float explosionForce;
    public float explosionRadius;
    private bool smashing = false;
    private float floorY;


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("FocalPoint");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * forwardInput * speed);
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);

        // launch missiles
        if (currentPowerUp == PowerUpType.ShootingStar && Input.GetKeyDown(KeyCode.F))
        {
            LaunchMissiles();
        }

        // smash
        if (currentPowerUp == PowerUpType.Smash && Input.GetKeyDown(KeyCode.Space) && !smashing)
        {
            smashing = true;
            StartCoroutine(Smash());
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);

            // we can only have one powerup at a time
            currentPowerUp = other.gameObject.GetComponent<PowerUp>().powerUpType;
            Powerup(true);

            // restart countdown when a new powerup is equipped
            if (powerupCountdown != null)
            {
                StopCoroutine(powerupCountdown);
            }

            powerupCountdown = StartCoroutine(CountdownRoutine()); // start a new thread
        }
    }

    // CountdownRoutine countdown the powerup
    IEnumerator CountdownRoutine()
    {
        yield return new WaitForSeconds(8);
        Powerup(false);
        currentPowerUp = PowerUpType.None;
    }

    // Powerup turn on/off the power
    private void Powerup(bool power)
    {
        powerupIndicator.gameObject.SetActive(power);
        hasPowerup = power;
    }

    private void OnCollisionEnter(Collision other)
    {
        // push back enemy
        if (other.gameObject.CompareTag("Enemy") && currentPowerUp == PowerUpType.Pushback)
        {
            Rigidbody enemyRb = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayDirection = (other.gameObject.transform.position - transform.position).normalized;
            enemyRb.AddForce(awayDirection * powerupStrength, ForceMode.Impulse);
            Debug.Log("Player collided with: " + other.gameObject.name + " with powerup set to " + currentPowerUp.ToString());
        }
    }

    // LaunchMissiles launch missiles at each enemy
    void LaunchMissiles()
    {
        foreach (var enemy in FindObjectsOfType<Enemy>())
        {
            tmpMissile = Instantiate(missilePrefab, transform.position + Vector3.up, Quaternion.identity);
            tmpMissile.GetComponent<ShootingBehavior>().Fire(enemy.transform);
        }
    }

    IEnumerator Smash()
    {
        // store the original y position so we can come back
        floorY = transform.position.y;

        float jumpTime = Time.time + hangTime;

        // move player up once every frame
        while (Time.time < jumpTime)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, smashSpeed);
            yield return null;
        }

        // move player down once every frame
        while (transform.position.y > floorY)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, -smashSpeed * 2);
            yield return null;
        }

        var enemies = FindObjectsOfType<Enemy>();

        // loop through enemies
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
            {
                enemies[i].GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRadius, 0.0f, ForceMode.Impulse);
            }
        }

        smashing = false;
    }
}
