using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    public float speed;
    float x;
    float y;
    float rotateZ;
    public float oscillationAngle;
    Vector2 move;
    Rigidbody2D rb;
    bool onRight;

    void Start()
    {
        rb = GetComponent< Rigidbody2D >();
        onRight = true;
        rotateZ = 0;
    }

    void FixedUpdate()
    {
        Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float angle = 0;
        if(GetComponent< PlayerAttack >().currentWeapon){
            angle = toDegrees(Mathf.Atan2(target.y - transform.position.y, target.x - transform.position.x));
            angle -= GetComponent< Arms >().normalAngle;
        }

        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        if (x != 0 || y != 0){
            if (rotateZ < -oscillationAngle) onRight = false;
            else if (rotateZ > oscillationAngle) onRight = true;

            if (onRight) rotateZ -= 1;
            else rotateZ += 1;
        }

        transform.rotation = Quaternion.Euler(
            transform.rotation.x,
            transform.rotation.y,
            calculateAngle(angle) + rotateZ
        );

        move = new Vector2(x * speed, y * speed);
        rb.velocity = move;
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