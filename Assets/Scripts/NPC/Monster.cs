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
    CheckerAttacked checkerAttacked;

    void Awake() 
    {
        player = GameObject.FindWithTag("Player");
        target = GetComponent<Target>();
        normalPoint = transform.position;
        checkerAttacked = GetComponent<CheckerAttacked>();
    }
    void Update()
    {
        distanceSqr = (player.transform.position - transform.position).sqrMagnitude;
        if (checkerAttacked.GetAttacker() != null)
        {
            target.target = checkerAttacked.GetAttacker();
        }
        if (distanceSqr < followingDistance * followingDistance && checkerAttacked.GetAttacker() == null) 
        {
            target.target = player;
        }
        if (target.distance > followingDistance)
        {
            target.target = null;
            target.pointTarget = normalPoint;
        }
    }
}