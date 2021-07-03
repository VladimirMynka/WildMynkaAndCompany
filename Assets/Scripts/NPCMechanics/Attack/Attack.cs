using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public bool enable;
    public int index;
    public GameObject currentWeapon;
    protected Inventory inventory;
    Arms arms;
    float firstHitingSpeed;
    float secondHitingSpeed;
    float hitingIndex;
    float rotateZ;
    float rotateSpan = 20;

    protected virtual void Awake()
    {
        inventory = GetComponent<Inventory>();
        arms = GetComponent<Arms>();
    }

    protected virtual void Update()
    {
        if (Time.timeScale == 0) return;
        if(currentWeapon != null)
        {
            currentWeapon.transform.localPosition = arms.arms;
        }

        if(!enable && currentWeapon != null)
        {
            RemoveWeapon();
            return;
        }

        if(!enable) return;

        if(currentWeapon == null)
        {
            AddWeapon();
        }

        AnimateHiting();
    }

    protected void Hit()
    {
        if(hitingIndex != 0) return;
        if(currentWeapon == null) return;
        hitingIndex = 1;

        Weapon weapon = currentWeapon.GetComponent<Weapon>();
        firstHitingSpeed = weapon.firstRotateSpeed;
        secondHitingSpeed = weapon.secondRotateSpeed;
        rotateSpan = weapon.rotateSpan;
        if(!currentWeapon.GetComponent<SpriteRenderer>().flipX)
        {
            firstHitingSpeed *= -1;
            secondHitingSpeed *= -1;
            rotateSpan *= -1;
        }
    }

    protected void AnimateHiting()
    {
        if(currentWeapon == null)
        {
            hitingIndex = 0;
            return;
        }
        if(hitingIndex == 1)
        {
            
            if((rotateSpan > 0 && rotateZ >= rotateSpan) || (rotateSpan < 0 && rotateZ <= rotateSpan))
            { 
                hitingIndex = 2;
                currentWeapon.GetComponent<Weapon>().Hit();
            }
            rotateZ += firstHitingSpeed;
        }
        if(hitingIndex == 2)
        {
            if((rotateSpan > 0 && rotateZ <= 0) || (rotateSpan < 0 && rotateZ >= 0))
            {
                hitingIndex = 0;
                rotateZ = 0;
                currentWeapon.GetComponent<Weapon>().EndHit();
            }
            rotateZ -= secondHitingSpeed;
        }
        currentWeapon.transform.localRotation = Quaternion.Euler(0, 0, rotateZ);
    }

    public void AddWeapon()
    {
        RemoveWeapon();
        if (inventory == null) return;
        if (inventory.weapons.Count <= 0) return;

        currentWeapon = Instantiate(inventory.weapons[index]) as GameObject;
        currentWeapon.transform.parent = transform;
        currentWeapon.transform.localPosition = GetComponent<Arms>().arms;
        currentWeapon.transform.rotation = transform.rotation;

        Weapon weapon = currentWeapon.GetComponent<Weapon>();
        if (weapon != null) weapon.owner = gameObject;
    }
    
    public void ChangeWeapon()
    {
        index = NextIndex();
        if(enable) AddWeapon();
    }

    public virtual int NextIndex()
    {
        return 0;
    }

    protected void RemoveWeapon(){
        if(currentWeapon) Destroy(currentWeapon);
    }
}