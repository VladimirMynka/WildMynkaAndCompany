using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quest0 : Quest
{
    public float beginWaiting;
    GameObject player;
    Inventory inventory;
    public GameObject helpCanvas;
    public GameObject weaponCanvas;
    public GameObject skeleton1;
    public GameObject skeleton2;
    public GameObject elf;
    Target elfTarget;

    [TextAreaAttribute(5, 20)] 
    public string[] helpStrings;

    void Awake()
    {
        time = 1;
        index = 0;
        waiting = beginWaiting;
        player = GameObject.FindWithTag("Player");
        inventory = player.GetComponent<Inventory>();
        elfTarget = elf.GetComponent<Target>();
    }

    void Update()
    {
        if(time != 0) time += Time.deltaTime;
        if(time > waiting + 1) time = 0;


        if(index == 0 && time == 0)
        { 
            if(skeleton1 != null) skeleton1.GetComponent<Target>().target = null;
            if(skeleton2 != null) skeleton2.GetComponent<Target>().target = null;
            index++;
        }

        if(index == 1)
        {
            Open(helpCanvas, helpStrings[0]);
            index++;
        }

        if(index == 2 && inventory.weapons.Count > 0)
        {
            Open(helpCanvas, helpStrings[1]);
            skeleton1.GetComponent<Target>().target = player;
            player.GetComponent<PlayerAttack>().ChangeWeapon();
            weaponCanvas.SetActive(true);
            index++;
        }

        if(index > 2 && weaponCanvas.active == false) weaponCanvas.SetActive(true);

        if(index == 3 && skeleton1 == null)
        {
            Open(helpCanvas, helpStrings[2]);
            index++;
        }

        if(index == 4 && inventory.spells.Count > 0)
        {
            Open(helpCanvas, helpStrings[3]);
            skeleton2.GetComponent<Target>().target = player;
            player.GetComponent<PlayerCastSpell>().NextSpell();
            index++;
        }

        if(index == 5 && skeleton2 == null)
        {
            Open(helpCanvas, helpStrings[4]);
            elfTarget.playerRelationship = 120;
            index++;
        }

        if(index == 6 && (inventory.spells.Count > 1 || inventory.weapons.Count > 1))
        {
            Open(helpCanvas, helpStrings[5]);
            index++;
        }
    }
    void Open(GameObject menu)
    {
        menu.SetActive(true);
    }
    void Open(GameObject menu, string text)
    {
        Open(menu);
        menu.GetComponentInChildren<Text>().text = text;
        Time.timeScale = 0;
    }
}