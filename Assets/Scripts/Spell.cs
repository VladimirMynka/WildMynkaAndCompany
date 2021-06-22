using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    public string spellName;
    public GameObject owner;
    public GameObject example;
    public AudioClip sound;
    public float aliveTime;
    public float afterSpellWaiting;
    public Vector3 begin;
    public Vector3 end;
    public float speed;
    public bool hasTarget;
    public float manaCost;
    public SpellType spellType;
    Vector2 velocity;

    void Start()
    {
        Destroy(gameObject, aliveTime);

        float distance = (end - begin).magnitude;
        if (distance == 0) distance = 1;
        float x = (end.x - begin.x) / distance;
        float y = (end.y - begin.y) / distance;
        velocity = new Vector2(x * speed, y * speed);
        
        var component = GetComponent<Rigidbody2D>();
        if(hasTarget)
            component.drag = 0f;
        component.velocity = velocity;
    }

    public float ManaCost(Characteristics characteristics)
    {
        switch (spellType)
        {
            case SpellType.Attack:
                return manaCost * 100 / characteristics.attackMagic;
            case SpellType.Self:
                return manaCost * 100 / characteristics.selfMagic;
            case SpellType.Strange:
                return manaCost * 100 / characteristics.strangeMagic;
        }

        throw new Exception("This cannot happen");
    }
}

public enum SpellType
{
    Attack,
    Self,
    Strange
}