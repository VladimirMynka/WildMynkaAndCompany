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
    void Start()
    {
        inventory = GetComponent< Inventory >();
        if(inventory.weapons.Count > 0)
            currentWeaponText.GetComponent<Text>().text = inventory.weapons[index].GetComponent<Weapon>().weaponName;
        arms = GetComponent<Arms>();
    }

    void Update()
    {
        if(Input.GetKeyDown("f")){
            if (currentWeapon == null) AddWeapon();
            else RemoveWeapon();
        }
        if(Input.GetKeyDown("[")){
            NextWeapon();
            if (currentWeapon != null) AddWeapon();
            currentWeaponText.GetComponent<Text>().text = inventory.weapons[index].GetComponent<Weapon>().weaponName;
        }
    }

    void FixedUpdate(){
        if(currentWeapon != null){
            currentWeapon.transform.localPosition = arms.arms;
        }
    }

    public void AddWeapon(){
        RemoveWeapon();
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

    public void RemoveWeapon(){
        if (currentWeapon){
            Destroy(currentWeapon);
        }
    }

    public void NextWeapon(){
        if(!inventory) return;
        index++;
        if(index >= inventory.weapons.Count) index = 0;
    }
}
