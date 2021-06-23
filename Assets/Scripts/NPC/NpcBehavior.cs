using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcBehavior : MonoBehaviour
{
    Target target;
    Attack attack;
    CastSpell castSpell;
    RunAway runAway;
    Following following;


    void Awake() 
    {
        target = GetComponent<Target>();
        attack = GetComponent<Attack>();
        castSpell = GetComponent<CastSpell>();
        runAway = GetComponent<RunAway>();
        following = GetComponent<Following>();
    }
    void Update()
    {
        if(target.relationship > 100) return;
        else if(target.relationship == 100)
        {
            following.enable = true;
            attack.enable = false;
            castSpell.enable = false;
        }
        else if(target.relationship > 80)
        {
            attack.enable = false;
        }
        else if(target.relationship > 60)
        {
            attack.enable = false;
            castSpell.enable = false;
        }
        else if(target.relationship > 40)
        {
            runAway.enable = true;
            following.enable = false;
            attack.enable = false;
            castSpell.enable = false;
        }
        else if(target.relationship > 20)
        {
            attack.enable = true;
            castSpell.enable = true;
            runAway.enable = false;
        }
        else if(target.relationship >= 0)
        {
            attack.enable = false;
            castSpell.enable = true;
            runAway.enable = true;
            following.enable = false;
        }
        if (!attack.enable && !runAway.enable && !following.enable)
        {
            target.normalDistance = target.defaultNormalDistance;
            target.normalInDifference = target.defaultNormalInDifference;
            target.normalOutDifference = target.defaultNormalOutDifference;
        }
    }
}