using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// GameManager store score
public class GameManager : MonoBehaviour
{
    public float score = 0;
    private PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerControllerScript.isGameOver)
        {
            if (playerControllerScript.isDashing)
            {
                score += 2;
            }
            else
            {
                score++;
            }
            Debug.Log("Score: " + score);
        }
        else
        {
            Debug.Log("Final Score: " + score);
        }
    }
}
