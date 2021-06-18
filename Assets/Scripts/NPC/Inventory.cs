using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject assetsHandler;
    public Prefabs prefabs;
    public List<GameObject> weapons;
    public List<GameObject> spells;
    public List<GameObject> keys;
    public List<GameObject> others;
    public List<int> weaponsIndeces;
    public List<int> spellsIndeces;
    public List<int> keysIndeces;
    public List<int> othersIndeces;

    void Start() 
    {
        prefabs = assetsHandler.GetComponent<Prefabs>();
    }

    public void AddWeapon(int index)
    {
        weaponsIndeces.Add(index);
        weapons.Add(prefabs.weapons[index]);
    }

    public void AddSpell(int index)
    {
        spellsIndeces.Add(index);
        spells.Add(prefabs.spells[index]);
    }

    public void AddKey(int index)
    {
        keysIndeces.Add(index);
        keys.Add(prefabs.keys[index]);
    }

    public void AddOther(int index)
    {
        othersIndeces.Add(index);
        others.Add(prefabs.others[index]);
    }
}