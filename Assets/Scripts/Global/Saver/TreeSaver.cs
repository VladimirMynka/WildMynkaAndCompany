using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

public class TreeSaver : Saver
{
    private static readonly int RegexTimes = 12 * (TreesCreator.branchCount + 1) + 1;
    private static readonly string TransformRegex = new StringBuilder("~([^~]*)".Length*RegexTimes).Insert(0, "~([^~]*)", RegexTimes).ToString();

    protected static readonly Regex TransformMatcher = new Regex(TransformRegex);
    public override void Save()
    {
        savedName = "Tree" + currentTree++;
        
        SaveTransform();
        SaveSprite();
        SaveChildren();
        Push();
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
        saver.Save();
    }

    public override void Load()
    {
        savedName = gameObject.name;
        matcher = new MatchHandler(TransformMatcher.Match(GetString()));
        LoadTransform();
        LoadSprite();
        LoadChildren();
    }

    private void LoadChildren()
    {
        int children = matcher.nextInt();
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