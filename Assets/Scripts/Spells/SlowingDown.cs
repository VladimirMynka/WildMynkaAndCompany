using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowingDown : MonoBehaviour
{
    public float factor;
    GameObject owner;
    GameObject player;
    Characteristics characteristics;
    Health ownerHealth;
    Target target;
    float beginSpeed;

    void Start() {
        owner = GetComponent<Spell>().owner;
        characteristics = owner.GetComponent<Characteristics>();
        ownerHealth = owner.GetComponent<Health>();
        player = GameObject.FindWithTag("Player");
        if (transform.parent.Find("SlowingDown") != null) Destroy(gameObject);
        target = transform.parent.GetComponent<Target>();
        if (target == null) Destroy(gameObject);
        beginSpeed = target.speed;
        target.speed *= factor;
    }

    void OnDestroy() 
    {
        if (target == null) return;
        if (transform.parent.Find("SlowingDown") != null) return;
        target.speed = beginSpeed;
    }

    IEnumerator FollowPlayer(Target target)
    {
        target.relationship = 50;
        yield return null;
        target.target = player;
        target.playerRelationship = 100;
    }

    IEnumerator NotFollowPlayer(Target target)
    {
        target.playerRelationship = 50;
        target.relationship = 50;
        yield return null;
        target.target = target.gameObject;
        target.playerRelationship = 120;
        target.relationship = -20;
    }
}