using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public GameObject target;
    public Vector3 pointTarget;
    public int relationShip;
    public float normalDistance;
    public float defaultNormalDistance;
    public float normalInDifference;
    public float normalOutDifference;
    public float distance;
    public float speed;
    public float angle;
    Rigidbody2D rb;

    void Start() {
        rb = GetComponent< Rigidbody2D >();
        
    }
    void FixedUpdate(){
        if (target){
            pointTarget = target.transform.position;
        }

        Vector3 distanceVector = pointTarget - transform.position;
        distance = distanceVector.magnitude;
        float x = (distance != 0) ? distanceVector.x / distance * speed : 0;
        float y = (distance != 0) ? distanceVector.y / distance * speed : 0;
        Vector2 vector = new Vector2(x, y);

        if (distance - normalDistance > normalOutDifference){
            rb.velocity = vector;
        }
        else if(normalDistance - distance > normalInDifference){
            rb.velocity = -vector;
        }

        angle = toDegrees(Mathf.Atan2(y, x));
        angle -= GetComponent< Arms >().normalAngle;
        transform.rotation = Quaternion.Euler(
            transform.rotation.x,
            transform.rotation.y,
            calculateAngle(angle)
        );
    }

    float toDegrees(float angle){
        return angle * 180 / Mathf.PI;
    }

    float calculateAngle(float angle){
        while(angle > 180){
            angle -= 360;
        }
        while(angle < -180){
            angle += 360;
        }
        return angle;
    }
}
