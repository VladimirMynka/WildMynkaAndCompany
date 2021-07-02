using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LyingPlusHealth : LyingItem
{
    public float healing;

    protected override void OnActivate(GameObject creature)
    {
        Heal(creature);
    }

    void Heal(GameObject creature)
    {
        if (creature == null) return;
        var health = creature.GetComponent<Health>();
        if (health == null) return;
        health.current += healing;
        Destroy(gameObject);
    }
}