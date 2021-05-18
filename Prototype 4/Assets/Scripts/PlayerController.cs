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

    public PowerUpType currentPowerUp = PowerUpType.None;
    public GameObject missilePrefab;
    private GameObject tmpMissile; // used for spawning stars
    private Coroutine powerupCountdown;

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

        // launch missiles if current powerup type is "ShootingStar"
        if (currentPowerUp == PowerUpType.ShootingStar && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("F Key!");
            LaunchMissiles();
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
}
