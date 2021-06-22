using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    GameObject player;
    Target target;
    float distanceSqr;
    public float followingDistance;
    Vector3 normalPoint;

    void Awake() 
    {
        player = GameObject.FindWithTag("Player");
        target = GetComponent<Target>();
        normalPoint = transform.position;
    }
    void Update()
    {
        distanceSqr = (player.transform.position - transform.position).sqrMagnitude;
        if (distanceSqr < followingDistance * followingDistance) target.target = player;
        if (distanceSqr > followingDistance * followingDistance)
        {
            target.target = null;
            target.pointTarget = normalPoint;
        }
    }
}