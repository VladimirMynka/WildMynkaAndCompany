using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    public GameObject currentWeapon;
    public GameObject currentWeaponText;
    public int index;
    Inventory inventory;
    void Start()
    {
        inventory = GetComponent< Inventory >();
        currentWeaponText.GetComponent<Text>().text = inventory.weapons[index].GetComponent<Weapon>().weaponName;
    }

    void Update()
    {
        if(Input.GetKeyDown("f")){
            if (currentWeapon == null) addWeapon();
            else removeWeapon();
        }
        if(Input.GetKeyDown("[")){
            nextWeapon();
            if (currentWeapon != null) addWeapon();
            currentWeaponText.GetComponent<Text>().text = inventory.weapons[index].GetComponent<Weapon>().weaponName;
        }
    }

    void FixedUpdate(){
        if(currentWeapon != null){
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

    public void nextWeapon(){
        if(!inventory) return;
        index++;
        if(index >= inventory.weapons.Count) index = 0;
    }
}
