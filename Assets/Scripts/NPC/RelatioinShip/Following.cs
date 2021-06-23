using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Following : MonoBehaviour
{
    Target target;
    public float followingDistance;
    public bool enable;
    void Awake()
    {
        target = GetComponent<Target>();
    }

    void Update()
    {
        if (!enable) return;
        target.normalDistance = followingDistance;
        target.normalInDifference = followingDistance;
        target.normalOutDifference = 0;
    }
}