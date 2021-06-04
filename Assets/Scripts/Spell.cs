using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    public GameObject owner;
    public AudioClip sound;
    public float aliveTime;
    public float afterSpellWaiting;
    public Vector3 begin;
    public Vector3 end;
    public float speed;
    public bool hasTarget;
    Vector2 velocity;

    void Start()
    {
        Destroy(gameObject, aliveTime);

        float distance = (end - begin).magnitude;
        float x = (end.x - begin.x) / distance;
        float y = (end.y - begin.y) / distance;
        velocity = new Vector2(x * speed, y * speed);
    }

    void FixedUpdate()
    {
        if(hasTarget && GetComponent< Rigidbody2D >()){
            GetComponent< Rigidbody2D >().velocity = velocity;
        }
    }
}
