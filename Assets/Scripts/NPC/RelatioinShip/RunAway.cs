using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAway : MonoBehaviour
{
    Target target;
    public float selfDistance;
    public bool enable;
    void Awake()
    {
        target = GetComponent<Target>();
    }

    void Update()
    {
        if(Time.timeScale == 0) return;
        if (!enable) return;
        target.normalDistance = selfDistance;
        target.normalInDifference = 0;
        target.normalOutDifference = selfDistance;

        if (target.distance >= selfDistance)
        {
            enable = false;
            target.target = gameObject;
        }
    }
}