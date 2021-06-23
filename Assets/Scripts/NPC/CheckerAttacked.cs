using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckerAttacked : MonoBehaviour
{
    GameObject attacker;

    public void SetAttacker(GameObject newAttacker)
    {
        if(attacker == null) attacker = newAttacker;
    }

    public GameObject GetAttacker()
    {
        return attacker;
    }

    void Update() {
        if (!attacker) return;
        if ((transform.position - attacker.transform.position).sqrMagnitude < 3 * 3) attacker = null;
    }

}