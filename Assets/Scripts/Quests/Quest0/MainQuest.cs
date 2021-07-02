using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainQuest : Quest
{
    public GameObject elf;
    Dialog elfDialog;
    public GameObject door;
    public int[] topics1;
    public int greeting1;
    public int greeting2;

    void Start()
    {
        elfDialog = elf.GetComponentInChildren<Dialog>();
    }

    void Update()
    {
        if(index == 0)
        {
            elfDialog.ChangeGreeting(greeting1);
            elfDialog.ChangeTopicsArray(topics1);
            index++;
        }
        if(index == 1 && elfDialog.GetGreetingCount() > 0)
        {
            elfDialog.ChangeGreeting(greeting2);
            door.SetActive(true);
            index++;
        }

        if(index > 1 && door.active == false)
        {
            door.SetActive(true);
        }
    }
}