using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public GameObject target;
    public Vector3 pointTarget;
    public int relationship;
    public int playerRelationship;
    public float normalDistance;
    public float defaultNormalDistance;
    public float normalInDifference;
    public float normalOutDifference;
    public float distance;
    public float speed;
    public float angle;
    public float oscillationAngle = 5;
    public float maxOscillationSpeed = 0.2f;
    Attack attack;
    Rigidbody2D rb;
    float rotateZ;
    bool onRight;

    void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
        attack = GetComponent<Attack>();
    }
    void FixedUpdate()
    {
        if (target)
        {
            pointTarget = target.transform.position;
        }

        Vector3 distanceVector = pointTarget - transform.position;
        distance = distanceVector.magnitude;
        float x = (distance != 0) ? distanceVector.x / distance * speed : 0;
        float y = (distance != 0) ? distanceVector.y / distance * speed : 0;
        Vector2 vector = new Vector2(x, y);

        if (distance - normalDistance > normalOutDifference)
        {
            rb.velocity = vector;
        }
        else if(normalDistance - distance > normalInDifference)
        {
            rb.velocity = -vector;
        }

        angle = 0;
        if(attack.currentWeapon != null){
            angle = ToDegrees(Mathf.Atan2(y, x));
            angle -= GetComponent< Arms >().normalAngle;
        }
        
        if (rb.velocity.sqrMagnitude > maxOscillationSpeed * maxOscillationSpeed){
            if (rotateZ < -oscillationAngle) onRight = false;
            else if (rotateZ > oscillationAngle) onRight = true;
        
            if (onRight) rotateZ -= 1;
            else rotateZ += 1;
        }
        transform.rotation = Quaternion.Euler(
            transform.rotation.x,
            transform.rotation.y,
            CalculateAngle(angle) + rotateZ
        );
    }

    float ToDegrees(float angle){
        return angle * 180 / Mathf.PI;
    }

    float CalculateAngle(float angle){
        while(angle > 180){
            angle -= 360;
        }
        while(angle < -180){
            angle += 360;
        }
        return angle;
    }
}