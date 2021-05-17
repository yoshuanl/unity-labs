using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float speed = 15.0f;
    private float dashSpeed = 25.0f;
    private float leftBound = -15.0f;

    private PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        // move left if not yet game over
        if (!playerControllerScript.isGameOver)
        {
            // transform.Translate(Vector3.left * Time.deltaTime * speed);
            // dash speed
            if (playerControllerScript.isDashing)
            {
                transform.Translate(Vector3.left * Time.deltaTime * dashSpeed);
            }
            // normal speed
            else
            {
                transform.Translate(Vector3.left * Time.deltaTime * speed);
            }

        }

        // destroy out of bound obstacles
        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
