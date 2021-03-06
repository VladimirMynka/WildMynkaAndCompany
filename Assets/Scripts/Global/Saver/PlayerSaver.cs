using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerSaver : Saver
{
    public override void Save(string saveName)
    {
        savedName = saveName + gameObject.name;
        
        SaveTransform();
        SaveHealth();
        SavePlayerMoving();
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
        LoadPlayerMoving();
        LoadCharacteristics();
        LoadInventory();
        LoadPlayerAttack();
        LoadPlayerCastSpell();
    }
}