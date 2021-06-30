using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mana : MonoBehaviour
{
    
    public float current = 100;
    public float maxMana = 100;
    public float regeneration = 5;

    // Update is called once per frame
    void Update()
    {
        if (current < maxMana || regeneration < 0) current += Time.deltaTime * regeneration;
        if (current > maxMana) current = maxMana;
    }
}