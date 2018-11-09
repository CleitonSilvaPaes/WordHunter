using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [Header("Limite de movimento")]
    public float limiteMinimoX;
    public float limeteMaxX;


    public Transform player;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x + 6, 0, -10);

        if (transform.position.x < limiteMinimoX)
        {
            transform.position = new Vector3(limiteMinimoX, transform.position.y, 0);
        }
    }
}
