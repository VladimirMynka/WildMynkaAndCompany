using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject currentWeapon;
    public int index;
    Inventory inventory;
    void Start()
    {
        inventory = GetComponent< Inventory >();
    }

    void Update()
    {
        if(Input.GetKeyDown("f")){
            if (!currentWeapon) addWeapon();
            else removeWeapon();
        }
        if(Input.GetKeyDown("[")){
            nextWeapon();
            addWeapon();
        }
    }

    void FixedUpdate(){
        if(currentWeapon){
            currentWeapon.transform.localPosition = GetComponent< Arms >().arms;
        }
    }

    void addWeapon(){
        removeWeapon();
        if (!inventory) return;

        currentWeapon = Instantiate(inventory.weapons[index]) as GameObject;
        currentWeapon.transform.parent = transform;
        currentWeapon.transform.localPosition = GetComponent< Arms >().arms;
        currentWeapon.transform.rotation = transform.rotation;

        Weapon weapon = currentWeapon.GetComponent< Weapon >();
        if (weapon){
            weapon.owner = gameObject;
            weapon.damage *= GetComponent< Characteristics >().power / 100;
            weapon.damage += GetComponent< Characteristics >().attack;
        }
    }

    void removeWeapon(){
        if (currentWeapon){
            Destroy(currentWeapon);
        }
    }

    void nextWeapon(){
        if(!inventory) return;
        index++;
        if(index >= inventory.weapons.Capacity) index = 0;
    }
}
