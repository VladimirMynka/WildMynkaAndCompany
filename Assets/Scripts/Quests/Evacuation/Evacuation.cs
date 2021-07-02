using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Evacuation : Quest
{
    LostSynopsis prevQuest;
    public GameObject elf;
    Dialog elfDialog;
    Target elfTarget;
    public GameObject[] men;
    Target[] menTargets;
    public int greetingIfOK;
    public int greetingIfWayIsLost;
    public int greetingWall;
    public int greetingEnd;
    public int goTopic;
    public int stopTopic;
    int goCount;
    int stopCount;
    GameObject player;

    public GameObject door1;
    public GameObject wall;
    public GameObject door2;
    public GameObject endPoint;

    public GameObject[,] points;

    public GameObject helpCanvas;
    [TextAreaAttribute(5, 20)] public string stringIfLoss;
    

    void Start()
    {
        elfDialog = elf.GetComponentInChildren<Dialog>();
        elfTarget = elf.GetComponent<Target>();
        menTargets = new Target[men.Length];
        for(int i = 0; i < men.Length; i++)
        {
            if (men[i] == null) continue;
            menTargets[i] = men[i].GetComponent<Target>();
        }

        prevQuest = FindObjectOfType<LostSynopsis>();
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if(index != -1 && index != 100)
        {
            bool loss = true;
            if (elf != null) loss = false;
            foreach(GameObject man in men)
                if (man != null) loss = false;
            if(loss){
                index = -1;
                Open(helpCanvas, stringIfLoss);
            }
        }

        if(index == 0 && prevQuest.index == 100 && elfDialog.GetGreetingCount() > 0)
        {
            elfTarget.target = door1;
            foreach(Target manTarget in menTargets)
                if (manTarget != null) manTarget.target = door2;
            
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