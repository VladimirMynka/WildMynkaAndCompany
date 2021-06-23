using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    GameObject assetsHandler;
    public Prefabs prefabs;
    public List<GameObject> weapons;
    public List<GameObject> spells;
    public List<GameObject> keys;
    public List<GameObject> others;
    public List<int> weaponsIndeces;
    public List<int> spellsIndeces;
    public List<int> keysIndeces;
    public List<int> othersIndeces;

    void Awake() 
    {
        assetsHandler = GameObject.FindWithTag("GlobalAssets");
        prefabs = assetsHandler.GetComponent<Prefabs>();
    }

    public void AddWeapon(int index)
    {
        if(weaponsIndeces.IndexOf(index) != -1) return;
        weaponsIndeces.Add(index);
        weapons.Add(prefabs.weapons[index]);
    }

    public void AddSpell(int index)
    {
        if(spellsIndeces.IndexOf(index) != -1) return;
        spellsIndeces.Add(index);
        spells.Add(prefabs.spells[index]);
    }

    public void AddKey(int index)
    {
        if(keysIndeces.IndexOf(index) != -1) return;
        keysIndeces.Add(index);
        keys.Add(prefabs.keys[index]);
    }

    public void AddOther(int index)
    {
        if(othersIndeces.IndexOf(index) != -1) return;
        othersIndeces.Add(index);
        others.Add(prefabs.others[index]);
    }
}