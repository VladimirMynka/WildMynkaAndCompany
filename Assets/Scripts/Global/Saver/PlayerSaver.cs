using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerSaver : Saver
{

    private static readonly int TokenTimes = 10+2+3+7+0+1+1;
    private static readonly string TransformRegex = 
        new StringBuilder(Token.Length*TokenTimes).Insert(0, Token, TokenTimes).ToString();

    protected static readonly Regex TransformMatcher = new Regex(TransformRegex);
    public override void Save()
    {
        savedName = "Player";
        
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

    public override void Load()
    {
        savedName = gameObject.name;
        matcher = new MatchHandler(TransformMatcher.Match(GetString()));
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
        health.current = matcher.nextFloat();
        health.maxHealth = matcher.nextFloat();
        health.regeneration = matcher.nextFloat();
    }
    private void LoadCharacteristics()
    {
        var characteristics = GetComponent<Characteristics>();
        characteristics.attack = matcher.nextFloat();
        characteristics.power = matcher.nextFloat();
        characteristics.protection = matcher.nextFloat();
        characteristics.attackMagic = matcher.nextFloat();
        characteristics.selfMagic = matcher.nextFloat();
        characteristics.strangeMagic = matcher.nextFloat();
        characteristics.camouflage = matcher.nextFloat();
    }
    private void LoadInventory()
    {
        var inventory = GetComponent<Inventory>();

        int weaponsCount = matcher.nextInt();
        inventory.weapons = new List<GameObject>(weaponsCount);
        for(int i = 0; i < weaponsCount; i++)
            inventory.weapons.Add(Resources.Load<GameObject>(matcher.nextString()));
        
        int spellsCount = matcher.nextInt();
        inventory.spells = new List<GameObject>(spellsCount);
        for(int i = 0; i < spellsCount; i++)
            inventory.spells.Add(Resources.Load<GameObject>(matcher.nextString()));

        int keysCount = matcher.nextInt();
        inventory.keys = new List<GameObject>(keysCount);
        for(int i = 0; i < keysCount; i++)
            inventory.keys.Add(Resources.Load<GameObject>(matcher.nextString()));

        int othersCount = matcher.nextInt();
        inventory.others = new List<GameObject>(othersCount);
        for(int i = 0; i < othersCount; i++)
            inventory.others.Add(Resources.Load<GameObject>(matcher.nextString()));
    }
    private void LoadPlayerAttack()
    {
        var attack = GetComponent<PlayerAttack>();
        attack.index = matcher.nextInt();
    }
    private void LoadPlayerCastSpell()
    {
        var castSpell = GetComponent<PlayerCastSpell>();
        castSpell.index = matcher.nextInt();
    }
}
