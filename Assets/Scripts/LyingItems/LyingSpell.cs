using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LyingSpell : LyingItem
{
    public int addingSpell;

    protected override void OnActivate(GameObject creature)
    {
        AddSpell(creature);
    }
    void AddSpell(GameObject creature)
    {
        if (creature == null) return;
        var inventory = creature.GetComponent<Inventory>();
        if (inventory == null) return;
        inventory.AddSpell(addingSpell);
        Destroy(gameObject);
    }
}