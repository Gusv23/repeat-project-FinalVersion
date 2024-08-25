using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;

    private Vector3 offset;

    void Start()
    {
        // Calculate and stores the offset value by getting the distance between the player's pos and camera's pos
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        // Set the pos of the camera's transform to be the same as the player's, but offset by the calculated offset distance
        transform.position = player.transform.position + offset;
    }
}
