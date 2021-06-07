using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSaver : Saver
{

    public GameObject treeHandlder;
    // Start is called before the first frame update
    void Start()
    {
        Load();
    }

    private void OnDestroy() 
    {
        Save();
    }
    
    public override void Save()
    {
        var all = GameObject.FindObjectsOfType<Saver>();
        foreach (var one in all)
            if(!(one is BranchSaver) && !(one is GlobalSaver))
                one.Save();
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
}