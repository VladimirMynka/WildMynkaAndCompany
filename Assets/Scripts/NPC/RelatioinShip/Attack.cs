using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public bool enable;
    public GameObject currentWeapon;
    public AudioClip[] sounds;
    float changeWeaponTime;
    float attackTime;
    float audioTime;
    public float changeWeaponWaiting;
    public float attackWaiting;
    public float audioWaiting;
    public float attackDistance;
    public float bigAttackDistance;
    Inventory inventory;
    Target target;
    float speed;
    float hitingIndex;
    float rotateZ;
    public float rotateSpan = 10;
    void Start()
    {
        inventory = GetComponent< Inventory >();
        target = GetComponent< Target >();
    }

    void FixedUpdate()
    {
        if (changeWeaponTime != 0) changeWeaponTime += Time.deltaTime;
        if (attackTime != 0 && attackTime != attackWaiting / 2) attackTime += Time.deltaTime;
        if (audioTime != 0) audioTime += Time.deltaTime;
        if (changeWeaponTime > changeWeaponWaiting + 1) changeWeaponTime = 0;
        if (attackTime > attackWaiting + 1) attackTime = 0;
        else if (attackTime > attackWaiting / 2 + 0.5f && attackTime <= attackWaiting / 2 + 1) {
            attackTime = attackWaiting / 2 + 0.5f;
        }
        if (audioTime > audioWaiting + 1) audioTime = 0;

        if(currentWeapon)
        {
            currentWeapon.transform.localPosition = GetComponent<Arms>().arms;
        }

        if(!enable)
        {
            RemoveWeapon();
            return;
        }

        ChangeWeapon();
        AttackMoving();
        if(audioTime == 0 && sounds.Length > 0)
        {
            int random = Random.Range(0, sounds.Length);
            GetComponent<AudioSource>().PlayOneShot(sounds[random]);
            audioWaiting = sounds[random].length * 2;
            audioTime++;
        }
    }

    void AttackMoving()
    {
        if (target == null) return;
        if (target.distance - target.normalDistance > target.normalOutDifference) return;
        if (target.normalDistance - target.distance > target.normalInDifference) return;
        
        if(attackTime == 0){
            Hit();
            target.normalDistance = attackDistance;
            attackTime += 0.5f;
        }
        if(attackTime == attackWaiting / 2 + 0.5f){
            target.normalDistance = bigAttackDistance;
            attackTime += 0.5f;
        }
        AnimateHiting();
    }

    void Hit()
    {
        if(hitingIndex != 0) return;
        if(currentWeapon == null) return;
        hitingIndex = 1;
        currentWeapon.GetComponent<Weapon>().Hit();
        speed = currentWeapon.GetComponent<Weapon>().speed;
    }

    void AnimateHiting()
    {
        if(currentWeapon == null)
        {
            hitingIndex = 0;
            return;
        }
        if(hitingIndex == 1)
        {
            if(rotateZ >= rotateSpan) hitingIndex = 2;
            rotateZ += speed;
        }
        if(hitingIndex == 2)
        {
            if(rotateZ <= -rotateSpan) hitingIndex = 3;
            rotateZ -= speed;
        }
        if(hitingIndex == 3)
        {
            rotateZ += speed;
            if(rotateZ >= 0)
            {
                rotateZ = 0;
                hitingIndex = 0;
            }
            currentWeapon.GetComponent<Weapon>().EndHit();
        }
        currentWeapon.transform.localRotation = Quaternion.Euler(0, 0, rotateZ);
    }
    
    void ChangeWeapon()
    {
        if (changeWeaponTime != 0) return;

        RemoveWeapon();

        if (!inventory) return;
        if (inventory.weapons.Count <= 0) return;

        int length = inventory.weapons.Count;
        int random = Random.Range(0, inventory.weapons.Count);
        int i = 0;
        while (!inventory.weapons[random] && i < length * 2){
            random = Random.Range(0, length);
            i++;
        }

        currentWeapon = Instantiate(inventory.weapons[random]) as GameObject;
        currentWeapon.transform.parent = transform;
        currentWeapon.transform.localPosition = GetComponent< Arms >().arms;
        currentWeapon.transform.rotation = transform.rotation;

        Weapon weapon = currentWeapon.GetComponent< Weapon >();
        if (weapon){
            weapon.owner = gameObject;
            weapon.damage *= GetComponent< Characteristics >().power / 100;
            weapon.damage += GetComponent< Characteristics >().attack;
        }

        changeWeaponTime += 1;
    }

    void RemoveWeapon(){
        if(currentWeapon) Destroy(currentWeapon);
    }
}