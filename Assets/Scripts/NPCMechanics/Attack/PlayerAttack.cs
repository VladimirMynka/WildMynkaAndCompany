using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : Attack
{
    public GameObject currentWeaponText;
    
    protected override void Awake()
    {
        base.Awake();
        if(inventory.weapons.Count > 0)
            currentWeaponText.GetComponent<Text>().text = inventory.weapons[index].GetComponent<Weapon>().weaponName;
    }

    protected override void Update()
    {
        if (Time.timeScale == 0) return;
        base.Update();

        if(Input.GetKeyDown("f"))
        {
            enable = !enable;
            currentWeaponText.GetComponent<Text>().text = inventory.weapons[index].GetComponent<Weapon>().weaponName;
        }
        if(Input.GetKeyDown("["))
        {
            StartCoroutine(NextWeapon());
        }
        if(Input.GetButtonDown("Fire2"))
        {
            Hit();
        }
    }

    IEnumerator NextWeapon()
    {
        ChangeWeapon();
        yield return null;
        currentWeaponText.GetComponent<Text>().text = inventory.weapons[index].GetComponent<Weapon>().weaponName;
    }

    public override int NextIndex()
    {
        int i = index + 1;
        if (i >= inventory.weapons.Count) return 0;
        return i;
    }



}