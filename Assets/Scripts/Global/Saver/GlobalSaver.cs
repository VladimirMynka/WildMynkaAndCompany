using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GlobalSaver : Saver
{

    public GameObject treeHandlder;
    // Start is called before the first frame update
    void Start()
    {
        Load();
    }
    private void OnApplicationQuit()
    {
        Save();
    }

    public override void Save()
    {
        var all = FindObjectsOfType<Saver>();
        foreach (var one in all)
        {
            if (!(one is BranchSaver) && !(one is GlobalSaver))
                one.Save();
        }

        PlayerPrefs.SetInt(TreesNumberKey, currentTree);
    }

    public override void Load()
    {
        int trees = PlayerPrefs.GetInt(TreesNumberKey);
        for (int i = 0; i < trees; i++)
        {
            GameObject go = new GameObject("Tree" + i);
            go.transform.parent = treeHandlder.transform;
            var saver = go.AddComponent<TreeSaver>();
            saver.Load();
        }
    }

    public override string ToString()
    {
        return $"{name}";
    }
}