using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealMagic : MonoBehaviour
{
    public float damage;
    public float learningFactor;
    GameObject owner;
    Characteristics characteristics;
    Mana ownerMana;

    void Start() {
        owner = GetComponent<Spell>().owner;
        characteristics = owner.GetComponent<Characteristics>();
        ownerMana = owner.GetComponent<Mana>();
    }

    void OnTriggerStay2D(Collider2D other) {
        GameObject otherGO = other.gameObject;
        if (otherGO == owner) return;
        Mana mana = otherGO.GetComponent<Mana>();
        if (mana != null){
            mana.current -= damage * Time.deltaTime * 10;
            ownerMana.current += damage * Time.deltaTime * 10;
            characteristics.attackMagic += learningFactor * Time.deltaTime * 10;

            Destroy(gameObject, 0.1f);
        }
    }
}
