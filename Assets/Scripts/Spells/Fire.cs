using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public float damage;

    void Start() {
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(-180, 0));
    }

    void OnTriggerStay2D(Collider2D other) {
        GameObject otherGO = other.gameObject;
        if (otherGO == GetComponent< Spell >().owner) return;
        if (!otherGO) return;
        if (otherGO.GetComponent< Health >()){
            otherGO.GetComponent< Health >().current -= damage * Time.deltaTime;
            Destroy(gameObject, 0.1f);
        }
    }
}
