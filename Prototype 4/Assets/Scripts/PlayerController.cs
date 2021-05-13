using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 5.0f;
    private bool hasPowerup = false;
    private float powerupStrength = 15.0f;

    public GameObject powerupIndicator;
    private Rigidbody playerRb;
    private GameObject focalPoint;

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
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
        playerRb.AddForce(focalPoint.transform.forward * forwardInput * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            Powerup(true);
            Destroy(other.gameObject);
            StartCoroutine(CountdownRoutine()); // start a new thread
        }
    }

    // CountdownRoutine countdown the powerup
    IEnumerator CountdownRoutine()
    {
        yield return new WaitForSeconds(8);
        Powerup(false);
    }

    // Powerup turn on/off the power
    private void Powerup(bool power)
    {
        powerupIndicator.gameObject.SetActive(power);
        hasPowerup = power;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRb = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayDirection = (other.gameObject.transform.position - transform.position).normalized;
            enemyRb.AddForce(awayDirection * powerupStrength, ForceMode.Impulse);
        }
    }
}
