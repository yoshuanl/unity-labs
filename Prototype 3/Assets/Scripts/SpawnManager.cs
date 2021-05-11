using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject obstaclePrefabs;
    private Vector3 spawnPos = new Vector3(25, 0, 0);

    private float startDelay = 1.0f;
    private float internval = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObstacles", startDelay, internval);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnObstacles()
    {
        Instantiate(obstaclePrefabs, spawnPos, obstaclePrefabs.transform.rotation);
    }
}
