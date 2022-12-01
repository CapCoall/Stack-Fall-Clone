using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector3 cameraFollow;
    private Transform player, win;  

    void Awake()
    {
        player = FindObjectOfType<Player>().transform;
    }

    void Update()
    {
        if (win == null)
            win = GameObject.Find("win(Clone)").GetComponent<Transform>();

        if (transform.position.y > player.transform.position.y && transform.position.y > win.position.y + 3.6f)
            cameraFollow = new Vector3(transform.position.x, player.position.y, transform.position.z);

        transform.position = new Vector3(transform.position.x, cameraFollow.y, -5);
    }
}
