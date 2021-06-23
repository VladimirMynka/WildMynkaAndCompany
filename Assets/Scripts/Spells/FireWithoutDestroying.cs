using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWithoutDestroying : MonoBehaviour
{
    public float damage;
    public float learningFactor;
    GameObject owner;
    Characteristics characteristics;

    void Start() {
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(-180, 0));
        owner = GetComponent<Spell>().owner;
        characteristics = owner.GetComponent<Characteristics>();
    }

    void OnTriggerStay2D(Collider2D other) {
        GameObject otherGO = other.gameObject;
        if (otherGO == owner) return;
        Health health = otherGO.GetComponent<Health>();
        CheckerAttacked ca = otherGO.GetComponent<CheckerAttacked>();

        if (health != null){
            health.current -= damage * Time.deltaTime;
            characteristics.attackMagic += learningFactor * Time.deltaTime;

            if (damage > 0 && ca != null) ca.SetAttacker(owner);
        }
    }
}