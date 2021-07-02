using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LostSynopsis : Quest
{
    public GameObject elf;
    Dialog elfDialog;
    public GameObject synopsis;
    public int newTopic1;
    public int newTopic2;
    public int greeting1;
    public int greeting2;
    public int greeting3;
    public GameObject[] menInCave;
    public GameObject[] menInCountry;
    public Vector3 offset;
    GameObject player;

    void Start()
    {
        elfDialog = elf.GetComponentInChildren<Dialog>();
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if(index == 0)
        {
            bool next = true;
            for(int i = 0; i < menInCave.Length; i++)
            {
                if(menInCave[i] != null) next = false;
            }
            if (next)
            { 
                elfDialog.ChangeGreeting(greeting1);
                elfDialog.AddTopic(newTopic1);
                index++;
            }
        }

        if(index == 1 && elfDialog.GetGreetingCount() > 0)
        {
            elfDialog.ChangeGreeting(greeting2);
            synopsis.SetActive(true);
            index++;
        }

        if(index > 1 && synopsis != null && synopsis.active == false)
        {
            synopsis.SetActive(true);
        }

        if(index == 2 && synopsis == null)
        {
            Vector3 prev = elf.transform.position;
            for(int i = 0; i < menInCountry.Length; i++)
            {
                if (menInCountry[i] == null) continue;
                menInCountry[i].transform.position = prev + offset;
                prev = menInCountry[i].transform.position;
            }

            elfDialog.ChangeGreeting(greeting3);
            elfDialog.ChangeTopic(newTopic1, newTopic2);
            index = 100;
        }
    }
}