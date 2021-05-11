using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] animalPrefabs;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        int randomIndex = Random.Range(0, animalPrefabs.Length);

        if (Input.GetKeyDown(KeyCode.S))
        {
            Instantiate(animalPrefabs[randomIndex], transform.position, animalPrefabs[randomIndex].transform.rotation);
        }

    }
}
