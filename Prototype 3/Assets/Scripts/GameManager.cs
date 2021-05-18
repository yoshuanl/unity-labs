using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // store player score
    public float score = 0;
    private PlayerController playerControllerScript;

    // before start animation
    public Transform startingPoint;
    public float lerpSpeed; // linear interpolation

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();

        // playerControllerScript.isGameOver = true; // so user can't control the player at this time
        StartCoroutine(PlayIntro());
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
            Debug.Log("Your Final Score: " + score);
        }
    }

    IEnumerator PlayIntro()
    {
        Vector3 startPos = playerControllerScript.transform.position;
        Vector3 endPos = startingPoint.position;

        float journeyLength = Vector3.Distance(startPos, endPos);
        float startTime = Time.time;

        playerControllerScript.GetComponent<Animator>().SetFloat(playerControllerScript.animSpeedMultiplierName, 0.5f);

        float distanceCovered = 0;
        float journeyFraction = 0;
        while (journeyFraction < 1)
        {
            distanceCovered = (Time.time - startTime) * lerpSpeed;
            journeyFraction = distanceCovered / journeyLength;

            playerControllerScript.transform.position = Vector3.Lerp(startPos, endPos, journeyFraction);
            yield return null;

        }

        playerControllerScript.GetComponent<Animator>().SetFloat(playerControllerScript.animSpeedMultiplierName, 1.0f);
        // playerControllerScript.isGameOver = false;
    }
}
