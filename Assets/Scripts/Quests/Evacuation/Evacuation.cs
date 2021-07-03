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
    public int greetingForest;
    public int greetingEnd;
    public int goTopic;
    int localIndexGoTopic;
    public int stopTopic;
    int localIndexStopTopic;
    int goCount;
    int stopCount;
    GameObject player;

    public GameObject door1;
    public GameObject wall;
    public GameObject door2;
    public GameObject endPoint;
    public GameObject[] pointsLeftUp;
    public GameObject[] pointsRightDown;
    Vector3[,] pointsPos;
    GameObject helpCanvas;
    [TextAreaAttribute(5, 20)] public string stringIfLoss;

    public GameObject spell;
    

    void Start()
    {
        Canvases global = FindObjectOfType<Canvases>();
        helpCanvas = global.helpCanvas;

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

        pointsPos = new Vector3[pointsLeftUp.Length, 2];
        for(int i = 0; i < pointsLeftUp.Length; i++)
        {
            pointsPos[i, 0] = pointsLeftUp[i].transform.position;
            pointsPos[i, 1] = pointsRightDown[i].transform.position;
        }
    }

    void Update()
    {
        if(index != -1 && index != 100)
        {
            bool loss = true;
            if (elf != null) loss = false;
            if(loss){
                index = -1;
                Open(helpCanvas, stringIfLoss);
            }
        }

        if(index == 0 && prevQuest.index == 100 && elfDialog.GetGreetingCount() > 0)//before quest
        {
            elfTarget.target = door1;
            foreach(Target manTarget in menTargets)
                if (manTarget != null) manTarget.target = door1;
            elfDialog.ChangeGreeting(greetingIfOK);
            index++;
        }

        if(index == 1)//before cave
        {
            bool next = true;
            if(elfTarget.distance > 10) elfTarget.target = elf;
            else if(elfTarget.target != elf) next = false;
            foreach(Target manTarget in menTargets)
            {
                if(manTarget == null) continue;
                if(manTarget.distance > 10)
                { 
                    manTarget.target = elf;
                    manTarget.relationship = 100;
                }
                else if(manTarget.target != elf) next = false;
            }
            if(Distance(player, door1) <= 10 || Distance(player, elf) > 10) next = false;
            if(next)
            {
                localIndexGoTopic = elfDialog.AddTopic(goTopic);
                localIndexStopTopic = elfDialog.AddTopic(stopTopic);
                index++;
            }
        }

        if(index > 1 && localIndexGoTopic == 0) localIndexGoTopic = elfDialog.GetLocalIndexByGlobal(goTopic);
        if(index > 1 && localIndexStopTopic == 0) localIndexStopTopic = elfDialog.GetLocalIndexByGlobal(stopTopic);

        if(index == 2)//to wall
        {
            if(elfDialog.GetTopicCount(localIndexGoTopic) > goCount)
            {
                StartCoroutine(FollowPlayer(elfTarget));
                foreach(Target manTarget in menTargets)
                    if(manTarget != null) StartCoroutine(FollowPlayer(manTarget));
                goCount++;
            }
            if(elfDialog.GetTopicCount(localIndexStopTopic) > stopCount)
            {
                StartCoroutine(NotFollowPlayer(elfTarget));
                stopCount++;
            }
            if(CheckBadWay()) elfDialog.ChangeGreeting(greetingIfWayIsLost);
            else elfDialog.ChangeGreeting(greetingIfOK);

            if(Distance(elf, wall) < 3)
            {
                var attackWall = elf.AddComponent<AttackWall>();
                attackWall.wall = wall;

                foreach(GameObject man in men)
                {
                    if(man == null) continue;
                    attackWall = man.AddComponent<AttackWall>();
                    attackWall.wall = wall;
                }

                foreach(Transform child in wall.transform)
                {
                    var wallStone = child.gameObject.AddComponent<WallStone>();
                    wallStone.spell = spell;
                }

                elfDialog.ChangeGreeting(greetingWall);
                index++;
            }
        }

        if(index == 3)//on wall
        {
            if(wall.transform.childCount == 0)
            {
                Destroy(wall);
                Destroy(elf.GetComponent<AttackWall>());
                foreach(GameObject man in men)
                    if(man != null) Destroy(man.GetComponent<AttackWall>());

                StartCoroutine(FollowPlayer(elfTarget));
                foreach(Target manTarget in menTargets)
                    if(manTarget != null) StartCoroutine(FollowPlayer(manTarget));

                elfDialog.ChangeGreeting(greetingIfOK);
                index++;
            }
        }

        if(index == 4)//to forest
        {
            if(elfDialog.GetTopicCount(localIndexGoTopic) > goCount)
            {
                StartCoroutine(FollowPlayer(elfTarget));
                foreach(Target manTarget in menTargets)
                    if(manTarget != null) StartCoroutine(FollowPlayer(manTarget));
                goCount++;
            }
            if(elfDialog.GetTopicCount(localIndexStopTopic) > stopCount)
            {
                StartCoroutine(NotFollowPlayer(elfTarget));
                foreach(Target manTarget in menTargets)
                    if(manTarget != null) StartCoroutine(NotFollowPlayer(manTarget));
                stopCount++;
            }
            if(CheckBadWay()) elfDialog.ChangeGreeting(greetingIfWayIsLost);
            else elfDialog.ChangeGreeting(greetingIfOK);

            if(Distance(elf, door2) < 2)
            {
                StartCoroutine(NotFollowPlayer(elfTarget));
                elfTarget.target = door2;
                foreach(Target manTarget in menTargets)
                {
                    if(manTarget == null) continue; 
                    StartCoroutine(NotFollowPlayer(manTarget));
                    manTarget.target = door2;
                }
                index++;           
            }
        }

        if(index == 5)
        {
            if (elfTarget.target != door2)
            { 
                elfTarget.target = door2;
                elfTarget.relationship = 40;
            }
            foreach(Target manTarget in menTargets)
            {
                if(manTarget == null) continue; 
                if(manTarget.target != door2)
                { 
                    manTarget.target = door2;
                    manTarget.relationship = 40;
                }
            }
            if(elfTarget.target == door2) index++;
        }

        if(index == 6)
        {
            bool next = true;
            if(elfTarget.distance > 5)
            { 
                elfTarget.target = elf;
                elfTarget.relationship = 90;
            }
            else if(elfTarget.target != elf) next = false;
            foreach(Target manTarget in menTargets)
            {
                if(manTarget == null) continue;
                if(manTarget.distance > 5)
                { 
                    manTarget.target = elf;
                    manTarget.relationship = 100;
                }
                else if(manTarget.target != elf) next = false;
            }
            if(Distance(player, door2) <= 5 || Distance(player, elf) > 5) next = false;
            if(next)
            {
                elfDialog.ChangeGreeting(greetingForest);
                index++;
            }            
        }

        if(index == 7)
        {
            if(elfDialog.GetTopicCount(localIndexGoTopic) > goCount)
            {
                StartCoroutine(FollowPlayer(elfTarget));
                goCount++;
            }
            if(elfDialog.GetTopicCount(localIndexStopTopic) > stopCount)
            {
                StartCoroutine(NotFollowPlayer(elfTarget));
                stopCount++;
            }

            if(Distance(elf, endPoint) < 2)
            {
                StartCoroutine(NotFollowPlayer(elfTarget));
                elfDialog.ChangeGreeting(greetingEnd);
                elfDialog.RemoveTopic(goTopic);
                elfDialog.RemoveTopic(stopTopic);
                index = 100;
            }
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
    float Distance(GameObject one, GameObject other)
    {
        return (one.transform.position - other.transform.position).magnitude;
    }
    IEnumerator FollowPlayer(Target target)
    {
        target.relationship = 50;
        yield return null;
        target.target = player;
        target.playerRelationship = 100;
    }

    IEnumerator NotFollowPlayer(Target target)
    {
        target.playerRelationship = 50;
        target.relationship = 50;
        yield return null;
        target.target = target.gameObject;
        target.playerRelationship = 120;
        target.relationship = -20;
    }

    bool CheckBadWay()
    {
        Vector3 elfPos = elf.transform.position;
        for(int i = 0; i < pointsPos.Length / 2; i++)
        {
            if(elfPos.x >= pointsPos[i, 0].x && elfPos.x <= pointsPos[i, 1].x)
            {
                if(elfPos.y <= pointsPos[i, 0].y && elfPos.x >= pointsPos[i, 1].y) return true;
                else return false;
            }
        }
        return false;
    }
}