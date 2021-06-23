using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : MonoBehaviour
{
    public float healing;
    public float learningFactor;
    GameObject owner;
    Characteristics characteristics;
    Health ownerHealth;
    public float scaling;
    Vector3 scalingVector;

    void Start() 
    {
        owner = GetComponent<Spell>().owner;
        characteristics = owner.GetComponent<Characteristics>();
        ownerHealth = owner.GetComponent<Health>();
        scalingVector = new Vector3(scaling, scaling, 0);
        transform.parent = owner.transform;
    }

    void Update()
    {
        transform.localScale += scalingVector * Time.deltaTime;
    }

    void OnDestroy() 
    {
        ownerHealth.current += healing;
        characteristics.selfMagic += learningFactor;
    }
}
