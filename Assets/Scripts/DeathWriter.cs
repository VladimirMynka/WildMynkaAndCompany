using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathWriter : MonoBehaviour
{
    DeathSaver globalDeather;

    void Awake()
    {
        globalDeather = FindObjectOfType<DeathSaver>();
    }

    void OnDestroy() 
    {
        globalDeather.AddName(gameObject.name);
    }
}