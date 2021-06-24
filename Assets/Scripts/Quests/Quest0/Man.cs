using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Man : Quest
{
    public GameObject elf;
    public GameObject man;
    public GameObject manCopy;
    public GameObject soldier;
    public GameObject exit;
    public GameObject present;
    Dialog elfDialog;
    Dialog manDialog;
    Target manTarget;
    AfterDeath manAfterDeath;
    GameObject player;
    public int newElfGoodTopic;
    public int newElfBadTopic;
    public int[] manTopics;
    public int lastYesCount;
    public int lastNoCount;
    public int greeting1;
    public int greeting2;
    public int greeting3;

    void Start()
    {
        elfDialog = elf.GetComponentInChildren<Dialog>();
        manDialog = man.GetComponentInChildren<Dialog>();
        manTarget = man.GetComponent<Target>();
        manAfterDeath = man.GetComponent<AfterDeath>();
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if(index == -1)
        {
            manAfterDeath.enabled = false;
            Destroy(man);
        }
        if(index != -1 && index < 4 && man == null)
        {
            index = -1;
            elfDialog.AddTopic(newElfBadTopic);
        }
        if(index == 0)
        {
            manDialog.ChangeGreeting(greeting1);
            manDialog.ChangeTopicsArray(manTopics);
            index++;
        }
        if(index == 1 && soldier == null)
        {
            manTarget.playerRelationship = 120;
            index++;
        }
        if(index > 1 && soldier != null)
        {
            soldier.GetComponent<AfterDeath>().enabled = false;
            Destroy(soldier);
        }
        if(index == 2 && manDialog.greeting.usingCount > 0)
        {
            manDialog.ChangeGreeting(greeting2);
            index++;
        }
        if(index == 3)
        {
            if ((man.transform.position - exit.transform.position).magnitude < 2) index++;
            if (manDialog.topics[0].usingCount > lastYesCount)
            {
                followPlayer();
                lastYesCount++;
            }
            if (manDialog.topics[1].usingCount > lastNoCount)
            {
                notFollowPlayer();
                lastNoCount++;
            }
        }
        if(index == 4)
        {
            manDialog.ChangeGreeting(greeting3);
            manAfterDeath.items = new GameObject[1];
            manAfterDeath.items[0] = present;
            elfDialog.AddTopic(newElfGoodTopic);
            notFollowPlayer();
            index++;
        }
        if(index == 5 && manDialog.greeting.usingCount > 0)
        {
            Destroy(man, 0.1f);
            manCopy.SetActive(true);
            index++;
        }
        if(index > 5 && man != null)
        {
            manAfterDeath.enabled = false;
            Destroy(man);
        }
        if(index > 5 && manCopy.active == false)
        {
            manCopy.SetActive(true);
        }
    }

    void followPlayer()
    {
        manTarget.target = player;
        manTarget.playerRelationship = 100;
    }

    IEnumerator notFollowPlayer()
    {
        manTarget.playerRelationship = 50;
        yield return null;
        manTarget.target = man;
        manTarget.playerRelationship = 120;
        manTarget.relationship = -20;
    }
}