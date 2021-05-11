using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        // calculate offset between camera and player
        offset = transform.position - player.transform.position;
    }

    // LateUpdate is called every frame after all Update functions have been called
    void LateUpdate()
    {
        // offset the camera to be always behind the vehicle
        transform.position = player.transform.position + offset;
    }
}
