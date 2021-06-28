using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject player;
    public Camera thisCamera;
    public float changingSpeed;
    public float maxSize;
    public float minSize;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        thisCamera = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        if(!player) player = GameObject.FindWithTag("Player");
        transform.position = player.transform.position;
        transform.position += new Vector3(0, 0, -10);
        if (Input.GetKey("-")){
            thisCamera.orthographicSize += changingSpeed;
            if (thisCamera.orthographicSize > maxSize) thisCamera.orthographicSize = maxSize;
        }
        else if (Input.GetKey("=")){
            thisCamera.orthographicSize -= changingSpeed;
            if (thisCamera.orthographicSize < minSize) thisCamera.orthographicSize = minSize;
        }       
    }
}