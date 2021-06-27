using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public string weaponName = "weapon";
    public GameObject owner;
    public Characteristics ownerPar;
    public float damage = 10;
    public float speed = 1;
    public float rotateSpan = 10;
    public AudioClip sound;
    bool doDamage;
    bool canSound;
    public float powerLearningFactor = 0.1f;
    public float attackLearningFactor = 0.1f;

    void Start() {
        ownerPar = owner.GetComponent<Characteristics>();
    }

    void OnTriggerStay2D(Collider2D other) 
    {
        if (!doDamage) return;
        GameObject otherGO = other.gameObject;
        if (otherGO == owner) return;
        if (otherGO.GetComponent<Health>())
        {
            otherGO.GetComponent<Health>().current -= calculateDamage();
            increaseCharacteristics();
            doDamage = false;

            CheckerAttacked ca = otherGO.GetComponent<CheckerAttacked>();
            if(ca != null && damage > 0) ca.SetAttacker(owner); 
            if(canSound)
            {
                GetComponent<AudioSource>().Stop();
                GetComponent<AudioSource>().PlayOneShot(sound);
                canSound = false;
            }
        }
    }
    public float calculateDamage()
    {
        if (damage == 0) return 0;
        return damage * ownerPar.power / 100 + ownerPar.attack;
    }
    public void increaseCharacteristics()
    {
        ownerPar.power += (100 - ownerPar.power) * powerLearningFactor / 100;
        ownerPar.attack += attackLearningFactor;
    }
    public void Hit()
    {
        doDamage = true;
        canSound = true;
    }
    public void EndHit()
    {
        doDamage = false;
    }
}