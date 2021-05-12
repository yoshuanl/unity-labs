using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private PlayerController playerControllerScript;

    private float fadeSpeed = 0.2f;
    public GameObject UIScreen;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        if (UIFade.instance == null)
        {
            UIFade.instance = Instantiate(UIScreen).GetComponent<UIFade>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControllerScript.isGameOver)
        {
            float curVol = GetComponent<AudioSource>().volume;
            // fade to black
            if (!UIFade.instance.faded)
            {
                UIFade.instance.FadeToBlack(fadeSpeed * 2);
            }
            // fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));

            // fade to silence
            // GetComponent<AudioSource>().volume = Mathf.MoveTowards(curVol, 0, fadeSpeed * Time.deltaTime);
            if (curVol > 0)
            {
                GetComponent<AudioSource>().volume = curVol - Time.deltaTime * fadeSpeed;
            }
        }
    }
}
