using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Following : MonoBehaviour
{
    Target target;
    public float followingDistance;
    public bool enable;
    GameObject mainTarget;
    CheckerAttacked checkerAttacked;
    void Awake()
    {
        target = GetComponent<Target>();
    }

    void Update()
    {
        if (!enable)
        {
            mainTarget = null;
            return;
        }

        if (mainTarget == null)
        {
            mainTarget = target.target;
            checkerAttacked = mainTarget.GetComponent<CheckerAttacked>();
        }

        if (checkerAttacked.GetAttacker() == null)
        {
            if (mainTarget != target.target)
            {
                target.target = mainTarget;
                target.relationship = 100;
            }
        }
        else if (target.target != checkerAttacked.GetAttacker())
        {
            target.target = checkerAttacked.GetAttacker();
            target.relationship = 30;
        }
        
        
        if (mainTarget == target.target)
        {
            target.normalDistance = followingDistance;
            target.normalInDifference = followingDistance;
            target.normalOutDifference = 0;
        }

    }

}