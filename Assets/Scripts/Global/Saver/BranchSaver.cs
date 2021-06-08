using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BranchSaver : Saver
{
    public override void Save()
    {
        SaveTransform();
        SaveSprite();
    }

    public override void Load()
    {
        savedName = gameObject.name;
        LoadTransform();
        LoadSprite();
    }

}