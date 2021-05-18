using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingBehavior : MonoBehaviour
{
    private Transform target;
    private float speed = 15.0f;
    private bool shooting;

    private float starStrength = 15.0f;
    private float aliveTimer = 5.0f;


    // Update is called once per frame
    void Update()
    {
        if (shooting && target != null)
        {
            Vector3 moveDirection = (target.transform.position - transform.position).normalized;
            transform.position += moveDirection * speed * Time.deltaTime;
            transform.LookAt(target);
        }
    }

    public void Fire(Transform newTarget)
    {
        target = newTarget;
        shooting = true;
        Destroy(gameObject, aliveTimer);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (target != null)
        {
            if (other.gameObject.CompareTag(target.tag))
            {
                Rigidbody targetRigidbody = other.gameObject.GetComponent<Rigidbody>();
                Vector3 away = -other.contacts[0].normal;
                targetRigidbody.AddForce(away * starStrength, ForceMode.Impulse);
                Destroy(gameObject); // destroy missile
            }
        }
    }
}
