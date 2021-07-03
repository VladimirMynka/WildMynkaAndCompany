using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallStone : MonoBehaviour 
{
    public GameObject spell;

    void OnCollisionEnter2D(Collision2D other) {
        GameObject otherGO = other.gameObject;
        if (otherGO.GetComponent<AttackWall>() != null) Destroy(gameObject);
    }

    void OnDestroy()
    {
        var currentSpell = Instantiate(spell, transform.position, transform.rotation);
        currentSpell.GetComponent<Spell>().owner = spell;
    }

}