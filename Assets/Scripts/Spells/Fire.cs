using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
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
        if (health != null){
            health.current -= damage * Time.deltaTime * 5;
            characteristics.attackMagic += learningFactor * Time.deltaTime * 10;

            Destroy(gameObject, 0.1f);
        }
    }
}