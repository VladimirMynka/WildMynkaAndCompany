using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    public GameObject dialogCanvas;
    public GameObject miniCanvas;
    public GameObject helpCanvas;
    
    public float toDegree(float angle){
        return angle * 180 / Mathf.PI;
    }

    public float toRad(float angle){
        return angle * Mathf.PI / 180;
    }
}
