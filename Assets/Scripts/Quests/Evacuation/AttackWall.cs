using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackWall : MonoBehaviour 
{
    public GameObject wall;
    GameObject player;
    Health health;
    Target target;

    private void Awake() 
    {
        health = GetComponent<Health>();  
        target = GetComponent<Target>();
        player = GameObject.FindWithTag("Player");
    }

    private void Update() 
    {
        if (health.current < health.maxHealth / 2 && target.target != player) StartCoroutine(FollowPlayer());
        if (health.current > health.maxHealth / 2 && (target.target == player || target.target == null)) StartCoroutine(StartAttackWall());
    }

    IEnumerator FollowPlayer()
    {
        target.relationship = 50;
        yield return null;
        target.target = player;
        target.playerRelationship = 100;
    }

    IEnumerator StartAttackWall()
    {
        target.playerRelationship = 50;
        target.relationship = 50;
        yield return null;
        int index = Random.Range(0, wall.transform.childCount);
        target.target = wall.transform.GetChild(index).gameObject;
        target.playerRelationship = -20;
        target.relationship = 21;
    }

}