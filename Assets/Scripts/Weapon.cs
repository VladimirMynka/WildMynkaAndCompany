using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public string weaponName = "weapon";
    public GameObject owner;
    public float damage = 10;
    public float speed = 1;
    public AudioClip sound;
    bool doDamage;
    bool canSound;

    void OnTriggerStay2D(Collider2D other) 
    {
        if (!doDamage) return;
        GameObject otherGO = other.gameObject;
        if (otherGO == owner) return;
        if (otherGO.GetComponent<Health>())
        {
            otherGO.GetComponent<Health>().current -= damage * Time.deltaTime;
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