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
        time = 1;
    }

    void Update()
    {
        if(time != 0)
        { 
            time += Time.deltaTime;
        }
        if(time > 3) time = 0;
        if(time != 0) return;

        if(index != -1 && index < 4 && man == null)
        {
            index = -1;
            manCopy.SetActive(true);
            Destroy(manCopy);
            elfDialog.AddTopic(newElfBadTopic);
        }

        if(index == 0)
        {
            manDialog.ChangeGreeting(greeting1);
            manDialog.ChangeTopicsArray(manTopics);
            if (manCopy != null) manCopy.SetActive(false);
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
        if(index == 2 && manDialog.GetGreetingCount() > 0)
        {
            manDialog.ChangeGreeting(greeting2);
            index++;
        }
        if(index == 3)
        {
            if ((man.transform.position - exit.transform.position).magnitude < 2) index++;
            if (manDialog.GetTopicCount(0) > lastYesCount)
            {
                FollowPlayer();
                lastYesCount++;
            }
            if (manDialog.GetTopicCount(1) > lastNoCount)
            {
                StartCoroutine(NotFollowPlayer());
                lastNoCount++;
            }
        }
        if(index == 4)
        {
            manDialog.ChangeGreeting(greeting3);
            manDialog.ChangeTopicsArray(new int[0]);
            manAfterDeath.items = new GameObject[1];
            manAfterDeath.items[0] = present;
            elfDialog.AddTopic(newElfGoodTopic);
            StartCoroutine(NotFollowPlayer());
            index++;
        }
        if(index == 5 && manDialog.GetGreetingCount() > 0)
        {
            man.GetComponent<Health>().current = 1;
            man.GetComponent<Health>().regeneration = -100;
            manCopy.SetActive(true);
            index++;
        }
        if(index > 5 && manCopy != null && manCopy.active == false)
        {
            manCopy.SetActive(true);
        }
    }

    void FollowPlayer()
    {
        manTarget.target = player;
        manTarget.playerRelationship = 100;
    }

    IEnumerator NotFollowPlayer()
    {
        manTarget.playerRelationship = 50;
        manTarget.relationship = 50;
        yield return null;
        manTarget.target = man;
        manTarget.playerRelationship = 120;
        manTarget.relationship = -20;
    }
}