using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

public abstract class Saver : MonoBehaviour
{
    public const string TreesNumberKey = "TreesNumber";
    public const string Branch = ".branch";
    private const string Token = "~([^~]*)";
    private static readonly Regex TokenRegex = new Regex(Token);
    protected static int currentTree = 0;
    
    private static MatchHandler matcher = null;

    private static StringBuilder output = new StringBuilder();

    public abstract void Save(string saveName);
    public abstract void Load(string saveName);

    protected internal string savedName;
    public void Initiate() =>
        matcher = new MatchHandler(TokenRegex.Match(GetString()));
    
    protected static void Put(string s)
    {
        output.Append($"~{s}");
    }

    protected static void Put(int i)
    {
        output.Append($"~{i}");
    }
    protected static void Put(float i)
    {
        output.Append($"~{i}");
    }
    protected static void Put(bool b)
    {
        output.Append($"~{b}");
    }

    protected void Push()
    {
        PlayerPrefs.SetString(savedName, output.ToString());
        output.Clear();
    }

    protected string GetString() =>
        PlayerPrefs.GetString(savedName);
    

    protected static float NextFloat()
    {
        return matcher.nextFloat();
    }

    protected static int NextInt()
    {
        return matcher.nextInt();
    }

    protected static string NextString()
    {
        return matcher.nextString();
    }
    protected static bool NextBool()
    {
        return matcher.nextBool();
    }

    protected static string GetPath(UnityEngine.Object obj)
    {
        // Path's format: Assets/Recourses/.../file.smth
        // We have to remove "Assets/Recourses/" and ".smth" for Unity to correctly load file
        string fullPath = AssetDatabase.GetAssetPath(obj);

        // "Assets/Recourses/".length == 17
        string correctPath = fullPath.Substring(17, fullPath.LastIndexOf('.') - 17);
        return correctPath;
    }

    protected void SaveTransform()
    {
        var temp = transform;
        var position = temp.localPosition;
        var scale = temp.localScale;
        var rotation = temp.localRotation;
        Put(position.x);
        Put(position.y);
        Put(position.z);
        Put(scale.x);
        Put(scale.y);
        Put(scale.z);
        Put(rotation.x);
        Put(rotation.y);
        Put(rotation.z);
        Put(rotation.w);
    }
    protected void LoadTransform()
    {
        var transformLocal = transform;
        
        transformLocal.localPosition = new Vector3(NextFloat(), NextFloat(), NextFloat());
        transformLocal.localScale = new Vector3(NextFloat(), NextFloat(), NextFloat());
        transformLocal.localRotation = new Quaternion(NextFloat(), NextFloat(), NextFloat(), NextFloat());
    }

    protected void SaveSprite()
    {
        var sprite = GetComponent<SpriteRenderer>();
        var path = GetPath(sprite.sprite);

        Put(path);
        Put(sprite.sortingOrder);
    }
    protected void LoadSprite()
    {
        var spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if(spriteRenderer == null) spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = Resources.Load<Sprite>(NextString());
        spriteRenderer.sortingOrder = NextInt();
    }

    protected void SaveHealth()
    {
        var health = gameObject.GetComponent<Health>();
        Put(health.current);
        Put(health.maxHealth);
        Put(health.regeneration);
    }
    protected void LoadHealth()
    {
        var health = gameObject.GetComponent<Health>();
        if(health == null) health = gameObject.AddComponent<Health>();
        health.current = NextFloat();
        health.maxHealth = NextFloat();
        health.regeneration = NextFloat();
    }

    protected void SaveCharacteristics()
    {
        var characteristics = gameObject.GetComponent<Characteristics>();
        Put(characteristics.attack);
        Put(characteristics.power);
        Put(characteristics.protection);
        Put(characteristics.attackMagic);
        Put(characteristics.selfMagic);
        Put(characteristics.strangeMagic);
        Put(characteristics.camouflage);
    }
    protected void LoadCharacteristics()
    {
        var characteristics = gameObject.GetComponent<Characteristics>();
        if(characteristics == null) characteristics = gameObject.AddComponent<Characteristics>();
        characteristics.attack = NextFloat();
        characteristics.power = NextFloat();
        characteristics.protection = NextFloat();
        characteristics.attackMagic = NextFloat();
        characteristics.selfMagic = NextFloat();
        characteristics.strangeMagic = NextFloat();
        characteristics.camouflage = NextFloat();
    }

    protected void SaveInventory()
    {
        var inventory = gameObject.GetComponent<Inventory>();
        
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
    protected void LoadInventory()
    {
        var inventory = gameObject.GetComponent<Inventory>();
        if (inventory == null) inventory = gameObject.AddComponent<Inventory>();

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

    protected void SavePlayerAttack()
    {
        var attack = gameObject.GetComponent<PlayerAttack>();
        Put(attack.index);
    }
    protected void LoadPlayerAttack()
    {
        var attack = gameObject.GetComponent<PlayerAttack>();
        if (attack == null) attack = gameObject.AddComponent<PlayerAttack>();

        attack.index = NextInt();
    }

    protected void SavePlayerCastSpell()
    {
        var castSpell = gameObject.GetComponent<PlayerCastSpell>();
        Put(castSpell.index);
    }
    protected void LoadPlayerCastSpell()
    {
        var castSpell = gameObject.GetComponent<PlayerCastSpell>();
        if (castSpell == null) castSpell = gameObject.AddComponent<PlayerCastSpell>();
        castSpell.index = NextInt();
    }
    
}