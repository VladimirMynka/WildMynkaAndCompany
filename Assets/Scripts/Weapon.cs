using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject owner;
    public float damage;
    float damageTime = 1;
    public float damageWaiting;
    public AudioClip sound;

    void FixedUpdate() {
        if (damageTime != 0) damageTime += 0.02f;
        if (damageTime > damageWaiting + 1) damageTime = 0;
    }
    void OnTriggerEnter2D(Collider2D other) {
        if (damageTime != 0) return;
        if (!other.gameObject) return;
        GameObject otherGO = other.gameObject;
        if (otherGO == owner) return;
        if (otherGO.GetComponent< Health >()){
            otherGO.GetComponent< Health >().health -= damage;
            GetComponent< AudioSource >().PlayOneShot(sound, 1);
        }
        damageTime++;
    }
}