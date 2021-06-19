using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

public class TreeSaver : Saver
{
    public TreesCreator treesCreator;
    public override void Save(string saveName)
    {
        savedName = saveName + "Tree" + currentTree++;
        
        SaveTransform();
        SaveSprite();
        SaveChildren();
        
        Push();
    }

    public override void Load(string saveName)
    {
        savedName = gameObject.name;
        Initiate();
        LoadTransform();
        LoadSprite();
        LoadChildren();

        var spriteRenderer = GetComponent<SpriteRenderer>();
        var savingSprite = GetComponent<SavingSprite>();
        spriteRenderer.sprite = treesCreator.trunkOptions[savingSprite.imageIndex];
        spriteRenderer.sortingOrder = savingSprite.layer;
    }

    private void SaveChildren()
    {
        int childNumber = transform.childCount;
        Put(childNumber);
        for (int i = 0; i < childNumber; i++)
            SaveChild(i);
    }

    private void SaveChild(int number)
    {
        var saver = transform.GetChild(number).GetComponent<Saver>();
        saver.savedName = savedName + Branch + number;
        saver.Save("");
    }

    private void LoadChildren()
    {
        int children = NextInt();
        for (int i = 0; i < children; i++)
            LoadChild(i);
    }

    private void LoadChild(int number)
    {
        var branch = new GameObject(savedName + Branch + number);
        var saver = branch.AddComponent<BranchSaver>();
        branch.transform.parent = gameObject.transform;
        saver.treesCreator = treesCreator;
        saver.Load("");
    }
}