using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LyingWeapon : LyingItem
{
    public int addingWeapon;

    protected override void OnActivate(GameObject creature)
    {
        AddWeapon(creature);
    }

    void AddWeapon(GameObject creature)
    {
        if (creature == null) return;
        var inventory = creature.GetComponent<Inventory>();
        if (inventory == null) return;
        inventory.AddWeapon(addingWeapon);
        Destroy(gameObject);
    }
}