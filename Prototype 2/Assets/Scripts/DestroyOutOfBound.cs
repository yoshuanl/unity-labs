using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBound : MonoBehaviour
{
    private float topBound = 30.0f;
    private float lowBound = -10.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z > topBound || transform.position.z < lowBound)
        {
            Destroy(gameObject);
        }
    }
}
