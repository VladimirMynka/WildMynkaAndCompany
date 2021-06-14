using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Assertions;

public class NpcSaver : Saver
{
    public override void Save(string saveName)
    {
        savedName = saveName + gameObject.name;;
        
        SaveTransform();
        SaveHealth();
        SaveCharacteristics();
        SaveInventory();
        SavePlayerAttack();
        SavePlayerCastSpell();
        Push();
    }

    public override void Load(string saveName)
    {
        savedName = gameObject.name;
        
        Initiate();
        LoadTransform();
        LoadHealth();
        LoadCharacteristics();
        LoadInventory();
        LoadPlayerAttack();
        LoadPlayerCastSpell();
    }
}