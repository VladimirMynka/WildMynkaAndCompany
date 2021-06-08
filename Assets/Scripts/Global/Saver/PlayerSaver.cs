using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerSaver : Saver
{
    private static readonly int RegexTimes = 1;
    private static readonly string TransformRegex = 
        new StringBuilder("~([^~]*)".Length*RegexTimes).Insert(0, "~([^~]*)", RegexTimes).ToString();

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
            Put(GetPrefabPath(weapon));
        
        Put(inventory.spells.Count);
        foreach (var spell in inventory.spells)
            Put(GetPrefabPath(spell));
        
        Put(inventory.keys.Count);
        foreach (var key in inventory.keys)
            Put(GetPrefabPath(key));
        
        Put(inventory.others.Count);
        foreach (var other in inventory.others)
            Put(GetPrefabPath(other));
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
        throw new System.NotImplementedException();
    }
}
