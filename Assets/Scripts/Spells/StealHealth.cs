using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealHealth : MonoBehaviour
{
    public float damage;
    public float learningFactor;
    GameObject owner;
    Characteristics characteristics;
    Health ownerHealth;

    void Start() {
        owner = GetComponent<Spell>().owner;
        characteristics = owner.GetComponent<Characteristics>();
        ownerHealth = owner.GetComponent<Health>();
    }

    void OnTriggerStay2D(Collider2D other) {
        GameObject otherGO = other.gameObject;
        if (otherGO == owner) return;
        Health health = otherGO.GetComponent<Health>();
        CheckerAttacked ca = otherGO.GetComponent<CheckerAttacked>();
        if (health != null){
            health.current -= damage * Time.deltaTime * 10;
            ownerHealth.current += damage * Time.deltaTime * 10;
            characteristics.attackMagic += learningFactor * Time.deltaTime * 10;
            
            if (damage > 0 && ca != null) ca.SetAttacker(owner);
        }
    }
}
