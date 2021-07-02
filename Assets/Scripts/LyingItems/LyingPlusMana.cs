using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LyingPlusMana : LyingItem
{
    public float healing;

    protected override void OnActivate(GameObject creature)
    {
        PlusMana(creature);
    }

    void PlusMana(GameObject creature)
    {
        if (creature == null) return;
        var mana = creature.GetComponent<Mana>();
        if (mana == null) return;
        mana.current += healing;
        Destroy(gameObject);
    }
}