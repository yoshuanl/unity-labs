using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // 

public class UIFade : MonoBehaviour
{
    public static UIFade instance;

    public Image fadeScreen;
    private float fadeSpeed;

    private bool shouldFadeToBlack;
    public bool faded = false;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldFadeToBlack)
        {
            // Time.deltaTime: the time in seconds it took to complete the last frame
            // more powerful/ faster machine would have smaller Time.deltaTime
            // multiply fadespeed with it makes the UI experience in different computer similar
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a == 1f)
            {
                shouldFadeToBlack = false;
                faded = true;
            }
        }
    }

    public void FadeToBlack(float speed)
    {
        shouldFadeToBlack = true;
        fadeSpeed = speed;
    }
}
