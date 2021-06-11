using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BranchSaver : Saver
{
    public override void Save(string saveName)
    {
        SaveTransform();
        SaveSprite();
    }

    public override void Load(string saveName)
    {
        savedName = gameObject.name;
        LoadTransform();
        LoadSprite();
    }
}