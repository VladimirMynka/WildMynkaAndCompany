using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    public GameObject currentWeapon;
    public GameObject currentWeaponText;
    public Arms arms;
    public int index;
    Inventory inventory;
    Characteristics characteristics;
    int hitingIndex;
    float rotateZ;
    public float rotateSpan;
    float speed;

    
    void Awake()
    {
        inventory = GetComponent< Inventory >();
        if(inventory.weapons.Count > 0)
            currentWeaponText.GetComponent<Text>().text = inventory.weapons[index].GetComponent<Weapon>().weaponName;
        arms = GetComponent<Arms>();
        characteristics = GetComponent<Characteristics>();
    }

    void Update()
    {
        if(currentWeapon != null)
        {
            currentWeapon.transform.localPosition = arms.arms;
        }

        if(Input.GetKeyDown("f"))
        {
            if (currentWeapon == null) AddWeapon();
            else RemoveWeapon();
        }
        if(Input.GetKeyDown("["))
        {
            NextWeapon();
            if (currentWeapon != null) AddWeapon();
            currentWeaponText.GetComponent<Text>().text = inventory.weapons[index].GetComponent<Weapon>().weaponName;
        }
        if(Input.GetButtonDown("Fire2"))
        {
            Hit();
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

    public void AddWeapon()
    {
        RemoveWeapon();
        if (inventory == null) return;

        currentWeapon = Instantiate(inventory.weapons[index]) as GameObject;
        currentWeapon.transform.parent = transform;
        currentWeapon.transform.localPosition = GetComponent< Arms >().arms;
        currentWeapon.transform.rotation = transform.rotation;

        Weapon weapon = currentWeapon.GetComponent<Weapon>();
        if (weapon)
        {
            weapon.owner = gameObject;
            weapon.damage *= characteristics.power / 100;
            weapon.damage += characteristics.attack;
        }
    }

    public void RemoveWeapon()
    {
        if (currentWeapon)
        {
            Destroy(currentWeapon);
        }
    }

    public void NextWeapon()
    {
        if(!inventory) return;
        index++;
        if(index >= inventory.weapons.Count) index = 0;
    }
}
