using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arms : MonoBehaviour
{
    public Vector3 arms;
    public float normalAngle;
    void FixedUpdate()
    {
        normalAngle = toDegrees(Mathf.Atan2(arms.y, arms.x));
    }

    float toDegrees(float angle){
        return angle * 180 / Mathf.PI;
    }
}
