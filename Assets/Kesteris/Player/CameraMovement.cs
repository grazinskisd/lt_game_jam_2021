using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    float MinX = 0;
    [SerializeField]
    float MaxX = 100;
    [SerializeField]
    float MinZ = 0;
    [SerializeField]
    float MaxZ = 0;
    GameObject Player;

    private void Awake()
    {
        Player = GameObject.Find("PostmanV2");
    }

    // Update is called once per frame
    void Update()
    {
        float x = transform.position.x;
        float z = transform.position.z;
        if (Player.transform.position.x > MinX && Player.transform.position.x < MaxX)
            x = Player.transform.position.x;
        if (Player.transform.position.z > MinZ && Player.transform.position.z < MaxZ)
            z = Player.transform.position.z - 7;
        transform.position = new Vector3(x, transform.position.y, z);
    }
}
