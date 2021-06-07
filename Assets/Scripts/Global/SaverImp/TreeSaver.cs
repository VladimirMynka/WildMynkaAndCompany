using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TreeSaver : Saver
{

    public override void Save()
    {
        int treeNumber = PlayerPrefs.GetInt(TreesNumberKey, 0);
        PlayerPrefs.SetInt(TreesNumberKey, treeNumber + 1);
        savedName = "Tree" + treeNumber;
        
        SaveSprite();
        SaveTransform();
        SaveChildren();
    }

    private void SaveChildren()
    {
        int childNumber = transform.childCount;
        SaveProperty(ChildNumber, childNumber);
        for (int i = 0; i < childNumber; i++)
            SaveChild(i);
    }

    private void SaveChild(int number)
    {
        var saver = transform.GetChild(number).GetComponent<Saver>();
        saver.savedName = savedName + Branch + number;
        saver.Save();
    }

    public override void Load()
    {
        savedName = gameObject.name;
        LoadChildren();
        LoadTransform();
        LoadSprite();
    }

    private void LoadChildren()
    {
        int children = GetInt(ChildNumber);
        for (int i = 0; i < children; i++)
            LoadChild(i);
    }

    private void LoadChild(int number)
    {
        var branch = new GameObject(savedName + Branch + number);
        var saver = branch.AddComponent<BranchSaver>();
        branch.transform.parent = gameObject.transform;
        saver.Load();
    }
}