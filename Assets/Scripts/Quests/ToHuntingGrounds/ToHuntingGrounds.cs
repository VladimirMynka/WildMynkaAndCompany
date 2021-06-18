using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToHuntingGrounds : Quest
{
    public GameObject globalDialogObject;
    GlobalDialogs globalDialogs;
    public GameObject ivan;
    Dialog ivanDialog;
    public GameObject george;
    Dialog georgeDialog;
    GameObject player;
    PlayerAttack playerAttack;
    public GameObject deletingFog;
    Health fogHealth;
    public int newWeaponIndex;
    


    public int ivanAddTopicIndex1;
    public int ivanAddTopicIndex2;
    public int ivanAddTopicIndex3;
    public int georgeAddTopicIndex1;
    public int georgeAddTopicIndex2;

    void Start()
    {
        globalDialogs = globalDialogObject.GetComponent<GlobalDialogs>();
        ivanDialog = ivan.transform.Find("DialogTrigger").gameObject.GetComponent<Dialog>();
        georgeDialog = george.transform.Find("DialogTrigger").gameObject.GetComponent<Dialog>();
        player = GameObject.FindWithTag("Player");
        playerAttack = player.GetComponent<PlayerAttack>();
        fogHealth = deletingFog.GetComponent<Health>();
    }

    void Update()
    {
        if(player == null)
        {
            player = GameObject.FindWithTag("Player");  
            playerAttack = player.GetComponent<PlayerAttack>();          
        }
        if(index == 0)
        {
            foreach(Topic topic in georgeDialog.topics)
            {
                if(topic.usingCount > 0)
                {
                    index++;
                    break;
                }
            }
        }

        if(index == 1)
        {
            ivanDialog.AddTopic(ivanAddTopicIndex1);
            index++;
        }

        if(index == 2)
        {
            if(globalDialogs.topics[ivanAddTopicIndex1].usingCount > 0)
            {
                georgeDialog.AddTopic(georgeAddTopicIndex1);
                index++;
            }
        }

        if(index == 3)
        {
            if(globalDialogs.topics[georgeAddTopicIndex1].usingCount > 0)
            {
                georgeDialog.ChangeTopic(georgeAddTopicIndex1, georgeAddTopicIndex2);
                ivanDialog.ChangeTopic(ivanAddTopicIndex1, ivanAddTopicIndex2);
                index++;
            }
        }

        if(index == 4)
        {
            if(globalDialogs.topics[ivanAddTopicIndex2].usingCount > 0)
            {
                ivanDialog.AddTopic(ivanAddTopicIndex3);
                ivanDialog.UpdateDialog();
                index++;
            }
        }

        if(index == 5)
        {
            if(globalDialogs.topics[ivanAddTopicIndex3].usingCount > 0)
            {
                index++;
            }
        }

        if(index == 6)
        {
            if(playerAttack.currentWeapon != null)
            {
                player.GetComponent<Inventory>().AddWeapon(newWeaponIndex);
                playerAttack.NextWeapon();
                index++;
            }
        }

        if(index == 7)
        {
            fogHealth.regeneration = -100;
            index++;
        }

        if(index == 8)
        {
            if(deletingFog == null)
            {
                index++;
            }
        }

        if(index > 7 && deletingFog != null)
        {
            Destroy(deletingFog);
        }
    }
}
