using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ensure a TrailRenderer and a BoxCollider are on the GameObject the script is attaching to
[RequireComponent(typeof(TrailRenderer), typeof(BoxCollider))]

public class ClickAndSwipe : MonoBehaviour
{
    private GameManager gameManager;
    private Camera cam;
    private Vector3 mousePos;
    private bool swiping = false;

    private TrailRenderer trail;
    private BoxCollider box;

    // Awake is called in initialization stage (way before Start())
    void Awake()
    {
        cam = Camera.main;
        trail = GetComponent<TrailRenderer>();
        box = GetComponent<BoxCollider>();
        trail.enabled = false;
        box.enabled = false;

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                swiping = true;
                UpdateComponents();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                swiping = false;
                UpdateComponents();
            }

            if (swiping)
            {
                UpdateMousePosition();
            }
        }

    }

    private void UpdateMousePosition()
    {
        mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
        transform.position = mousePos;
    }

    private void UpdateComponents()
    {
        trail.enabled = swiping;
        box.enabled = swiping;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<Target>())
        {
            other.gameObject.GetComponent<Target>().DestroyTarget();
        }
    }
}
