using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToSecretPlace : Quest
{
    public GameObject globalDialogsObject;
    GlobalDialogs globalDialogs;
    public GameObject mynka;
    Target mynkaTarget;
    public GameObject chestDimitriy;
    Dialog dimitriyDialog;
    public GameObject[] manCreators;
    public GameObject[] mans;
    public float[] timers;
    public GameObject manExample;
    public int needMans;
    int diededMans;
    public float afterDiedWaiting;

    GameObject player;

    public int newGreeting1;
    public int newGreeting2;
    public int newGreeting3;
    public int[] afterUpdateTopics;
    public int aboutTaskTopic;
    public int aboutSolomonTopic;

    public GameObject deletingFog;
    Health fogHealth;

    
    void Awake()
    {
        mynkaTarget = mynka.GetComponent<Target>();
        dimitriyDialog = chestDimitriy.transform.Find("DialogTrigger").GetComponent<Dialog>();
        timers = new float[manCreators.Length];
        mans = new GameObject[manCreators.Length];
        player = GameObject.FindWithTag("Player");
        globalDialogs = globalDialogsObject.GetComponent<GlobalDialogs>();
        fogHealth = deletingFog.GetComponent<Health>();
    }

    void Update()
    {
        if(index == 0)
        {
            if(dimitriyDialog.greeting.usingCount > 0)
            {
                Debug.Log(dimitriyDialog.greeting.usingCount);
                dimitriyDialog.ChangeGreeting(newGreeting1);
                index++;
            }
        }

        if(index == 1 && diededMans < needMans)
        {
            for(int i = 0; i < manCreators.Length; i++)
            {
                if(mans[i] == null)
                { 
                    if(timers[i] >= afterDiedWaiting) CreateMan(i);
                    else if(timers[i] == -1)
                    {
                        timers[i] = 0;
                        diededMans++;
                    }
                    else timers[i] += Time.deltaTime;
                }
            }
        }

        if(index == 1 && diededMans >= needMans)
        {
            for(int i = 0; i < manCreators.Length; i++)
            {
                CreateMan(i);
            }
            index++;
        }

        if(index == 2)
        {
            mynka.GetComponent<CastSpell>().enable = true;
            index++;
        }

        if(index == 3)
        {
            for (int i = 0; i < manCreators.Length; i++)
            {
                if(mans[i] != null)
                {
                    mynkaTarget.target = mans[i];
                    break;
                }
            }
            if (mynkaTarget.target == null)
            {
                mynka.GetComponent<CastSpell>().enable = false;
                mynkaTarget.target = player;
                dimitriyDialog.ChangeGreeting(newGreeting2);
                dimitriyDialog.ChangeTopicsArray(afterUpdateTopics);
                index++;
            }
        }
        if(index > 3 && mynkaTarget.target != player)
        {
            mynkaTarget.target = player;
        }

        if(index == 4)
        {
            if(dimitriyDialog.greeting.usingCount > 0)
            {
                dimitriyDialog.AddTopic(aboutTaskTopic);
                dimitriyDialog.UpdateDialog();
                dimitriyDialog.ChangeGreeting(newGreeting3);
                index++;
            }
        }

        if(index == 5)
        {
            if(globalDialogs.topics[aboutTaskTopic].usingCount > 0)
            {
                dimitriyDialog.AddTopic(aboutSolomonTopic);
                dimitriyDialog.UpdateDialog();
                index++;
            }
        }

        if(index == 6)
        {
            fogHealth.regeneration = -100;
            index++;
        }

        if(index == 7)
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

    void CreateMan(int i)
    {
        mans[i] = Instantiate(manExample) as GameObject;
        mans[i].transform.position = manCreators[i].transform.position;
        mans[i].GetComponent<Target>().target = mynka;
        timers[i] = -1;
    }
}
