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
        CheckerAttacked ca = otherGO.GetComponent<CheckerAttacked>();

        if (mana != null){
            mana.current -= damage * Time.deltaTime;
            characteristics.attackMagic += learningFactor * Time.deltaTime;
            
            if (damage > 0 && ca != null) ca.SetAttacker(owner);
        }
    }
}
