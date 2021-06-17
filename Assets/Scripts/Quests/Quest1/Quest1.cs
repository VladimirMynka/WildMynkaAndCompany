using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quest1 : Quest
{
    public GameObject king;
    public AudioClip[] replicas;
    public GameObject portal;
    public float beginWaiting;
    GameObject player;
    string playerName;
    GameObject playerCopy;
    public float playerDistance;
    public float portalDistance;
    public float normalDistance;
    public GameObject helpCanvas;
    public GameObject subsCanvas;
    public GameObject weaponCanvas;
    public GameObject newWeapon;
    public GameObject newSpell;
    public GameObject stone;
    public string[] helpStrings;
    public string[] subsStrings;

    void Awake()
    {
        time = 1;
        index = 0;
        waiting = beginWaiting;
    }

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerName = player.name;
    }

    void Update()
    {
        if(time != 0) time += Time.deltaTime;
        if(time > waiting + 1) time = 0;

        if(index == 0 && time == 0)
        {
            Open(helpCanvas, helpStrings[0]);
            Time.timeScale = 0;
            index++;
        }
        if(index == 1 && (player.transform.position - king.transform.position).magnitude <= normalDistance)
        {
            index++;
        }
        if(index == 2 && time == 0)
        {
            Say(king, subsStrings[0], replicas[0]);
            index++;
            time++;
        }
        if(index == 3 && time == 0)
        {
            Say(king, subsStrings[1], replicas[1]);
            index++;
            time++;
            player.GetComponent<Inventory>().weapons.Add(newWeapon);
            weaponCanvas.transform.Find("Image").Find("CurrentWeapon").gameObject.GetComponent<Text>().text = newWeapon.GetComponent<Weapon>().weaponName;
            playerCopy = Instantiate(
                player, 
                player.transform.position + new Vector3(playerDistance, 0, 0), 
                player.transform.rotation
            );
        }
        if(index == 4 && time == 0)
        {
            Say(king, subsStrings[2], replicas[2]);
            index++;
            time++;
        }
        if(index == 5 && time == 0)
        {
            Close(subsCanvas);
            Open(helpCanvas, helpStrings[1]);
            Time.timeScale = 0;
            index++;
        }
        if(index == 6  && time == 0 && (player == null || playerCopy == null))
        {
            player = GameObject.FindWithTag("Player");
            player.name = playerName;
            Say(king, subsStrings[3], replicas[3]);
            index++;
            time++;
        }
        if(index == 7 && time == 0)
        {
            Say(king, subsStrings[4], replicas[4]);
            index++;
            time++;
        }
        if(index == 8 && time == 0)
        {
            Say(king, subsStrings[5], replicas[5]);
            index++;
            time++;
        }
        if(index == 9 && time == 0)
        {
            Close(subsCanvas);
            Open(helpCanvas, helpStrings[2]);
            Time.timeScale = 0;
            portal.transform.position = transform.position + new Vector3(portalDistance, 0, 0);
            index++;
        }
        if(index == 10 && (player.transform.position - king.transform.position).magnitude > 10)
        {
            Open(weaponCanvas);
            weaponCanvas.transform.Find("Image").Find("CurrentSpell").gameObject.GetComponent<Text>().text = newSpell.GetComponent<Spell>().spellName;
            player.GetComponent<Inventory>().spells.Add(newSpell);
            index++;
        }
        if(index == 11 && time == 0)
        {
            Open(helpCanvas, helpStrings[3]);
            Time.timeScale = 0;
            index++;
        }
        if(index == 12 && stone == null)
        {
            Open(helpCanvas, helpStrings[4]);
            Time.timeScale = 0;
            index++;
        }
    }

    void Open(GameObject menu)
    {
        menu.GetComponent<Canvas>().planeDistance = 100;
    }
    void Open(GameObject menu, string text)
    {
        Open(menu);
        var textChild = menu.transform.Find("Image").Find("Text");
        if(textChild == null)
        {
            throw new ArgumentException("Object doesn't have child with name 'Text'");
        }
        textChild.GetComponent<Text>().text = text;
    }

    void Close(GameObject menu)
    {
        menu.GetComponent<Canvas>().planeDistance = -10;
    }

    void Say(GameObject sayer, string replica, AudioClip audioReplica)
    {
        AudioSource audioSource = sayer.GetComponent<AudioSource>();
        audioSource.Stop();
        audioSource.PlayOneShot(audioReplica);
        Open(subsCanvas, replica);
        waiting = audioReplica.length / audioSource.pitch;
    }
}