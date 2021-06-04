using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject player;
    public float scale;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if(!player) player = GameObject.FindWithTag("Player");
        transform.position = player.transform.position;
        transform.position += new Vector3(0, 0, -10);
    }
}