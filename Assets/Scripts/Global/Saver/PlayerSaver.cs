using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerSaver : Saver
{
    private void Start() {
        gameObject.name = "save1" + gameObject.name;
        Load("save1");
    }
    public override void Save(string saveName)
    {
        savedName = saveName + "Player";
        
        SaveTransform();
        SaveSprite();
        SaveHealth();
        SaveCharacteristics();
        SaveInventory();
        SavePlayerAttack();
        SavePlayerCastSpell();
        Push();
    }

    private void SaveHealth()
    {
        var health = GetComponent<Health>();
        Put(health.current);
        Put(health.maxHealth);
        Put(health.regeneration);
    }

    private void SaveCharacteristics()
    {
        var characteristics = GetComponent<Characteristics>();
        Put(characteristics.attack);
        Put(characteristics.power);
        Put(characteristics.protection);
        Put(characteristics.attackMagic);
        Put(characteristics.selfMagic);
        Put(characteristics.strangeMagic);
        Put(characteristics.camouflage);
    }

    private void SaveInventory()
    {
        var inventory = GetComponent<Inventory>();
        
        Put(inventory.weapons.Count);
        foreach (var weapon in inventory.weapons)
            Put(GetPath(weapon));
        
        Put(inventory.spells.Count);
        foreach (var spell in inventory.spells)
            Put(GetPath(spell));
        
        Put(inventory.keys.Count);
        foreach (var key in inventory.keys)
            Put(GetPath(key));
        
        Put(inventory.others.Count);
        foreach (var other in inventory.others)
            Put(GetPath(other));
    }

    private void SavePlayerAttack()
    {
        var attack = GetComponent<PlayerAttack>();
        Put(attack.index);
    }

    private void SavePlayerCastSpell()
    {
        var castSpell = GetComponent<PlayerCastSpell>();
        Put(castSpell.index);
    }

    public override void Load(string saveName)
    {
        savedName = gameObject.name;
        
        Initiate();
        LoadTransform();
        LoadSprite();
        LoadHealth();
        LoadCharacteristics();
        LoadInventory();
        LoadPlayerAttack();
        LoadPlayerCastSpell();
    }

    private void LoadHealth()
    {
        var health = GetComponent<Health>();
        health.current = NextFloat();
        health.maxHealth = NextFloat();
        health.regeneration = NextFloat();
    }
    private void LoadCharacteristics()
    {
        var characteristics = GetComponent<Characteristics>();
        characteristics.attack = NextFloat();
        characteristics.power = NextFloat();
        characteristics.protection = NextFloat();
        characteristics.attackMagic = NextFloat();
        characteristics.selfMagic = NextFloat();
        characteristics.strangeMagic = NextFloat();
        characteristics.camouflage = NextFloat();
    }
    private void LoadInventory()
    {
        var inventory = GetComponent<Inventory>();

        int weaponsCount = NextInt();
        inventory.weapons = new List<GameObject>(weaponsCount);
        for(int i = 0; i < weaponsCount; i++)
            inventory.weapons.Add(Resources.Load<GameObject>(NextString()));
        
        int spellsCount = NextInt();
        inventory.spells = new List<GameObject>(spellsCount);
        for(int i = 0; i < spellsCount; i++)
            inventory.spells.Add(Resources.Load<GameObject>(NextString()));

        int keysCount = NextInt();
        inventory.keys = new List<GameObject>(keysCount);
        for(int i = 0; i < keysCount; i++)
            inventory.keys.Add(Resources.Load<GameObject>(NextString()));

        int othersCount = NextInt();
        inventory.others = new List<GameObject>(othersCount);
        for(int i = 0; i < othersCount; i++)
            inventory.others.Add(Resources.Load<GameObject>(NextString()));
    }
    private void LoadPlayerAttack()
    {
        var attack = GetComponent<PlayerAttack>();
        attack.index = NextInt();
    }
    private void LoadPlayerCastSpell()
    {
        var castSpell = GetComponent<PlayerCastSpell>();
        castSpell.index = NextInt();
    }
}