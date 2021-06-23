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
    public AudioClip sound;
    bool doDamage;
    bool canSound;
    public float learningFactor;
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
            otherGO.GetComponent<Health>().current -= damage * Time.deltaTime;
            ownerPar.attack += learningFactor * Time.deltaTime;
            ownerPar.power += learningFactor * Time.deltaTime;

            CheckerAttacked ca = otherGO.GetComponent<CheckerAttacked>();
            if(ca != null && damage > 0) ca.SetAttacker(owner); 
            if(canSound)
            {
                GetComponent<AudioSource>().PlayOneShot(sound);
                canSound = false;
            }
        }
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