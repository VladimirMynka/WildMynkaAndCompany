using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subordination : MonoBehaviour
{
    public float damage;
    public float learningFactor;
    GameObject owner;
    GameObject player;
    Characteristics characteristics;
    Health ownerHealth;
    Target target;

    void Start() {
        owner = GetComponent<Spell>().owner;
        characteristics = owner.GetComponent<Characteristics>();
        ownerHealth = owner.GetComponent<Health>();
        player = GameObject.FindWithTag("Player");
        target = transform.parent.GetComponent<Target>();
        if (target == null) Destroy(gameObject);
    }

    void Update() 
    {
        if (owner != player) return;
        if (target.target != player || target.relationship != 100)
            StartCoroutine(FollowPlayer(target));
    }

    void OnDestroy() 
    {
        if(target != null);
        StartCoroutine(NotFollowPlayer(target));
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