using System;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class GlobalSaver : Saver
{

    public static readonly string SavesKey = "Saves";
    public static readonly string LastSaveKey = "LastSave";
    public GameObject treeParent;
    public GameObject assetsHandler;

    // Start is called before the first frame update
    void Start()
    {
        if (FindObjectsOfType<GlobalSaver>().Length != 1)
            throw new Exception("More than one GlobalSaver exists!");
        else{
            string lastSave = PlayerPrefs.GetString(LastSaveKey, "");
            Load($"{lastSave}");
        }
    }
    
    public override void Save(string saveName)
    {
        checkFormat(saveName);
        string saves = PlayerPrefs.GetString(SavesKey, "");
        saves = saves.Replace("~" + saveName, "");
        saves = saves + "~" + saveName;
        PlayerPrefs.SetString(SavesKey, saves);
        PlayerPrefs.SetString(LastSaveKey, saveName);
        
        var all = FindObjectsOfType<Saver>();
        foreach (var one in all)
        {
            if (!(one is BranchSaver) && !(one is GlobalSaver))
                one.Save(saveName);
        }

        PlayerPrefs.SetInt(TreesNumberKey, currentTree);
        currentTree = 0;
    }

    public override void Load(string saveName)
    {
        if(!checkFormat(saveName))
            return;
        string saves = PlayerPrefs.GetString(SavesKey, "");
        if (!saves.Contains($"~{saveName}"))
            throw new ArgumentException($"No save with name {saveName} exists.");
        
        int trees = PlayerPrefs.GetInt(TreesNumberKey);
        for (int i = 0; i < trees; i++)
        {
            var go = new GameObject("Tree" + i);
            go.transform.parent = treeParent.transform;
            var saver = go.AddComponent<TreeSaver>();
        }

        var all = FindObjectsOfType<Saver>();
        foreach (var one in all)
        {
            if (!(one is BranchSaver) && !(one is GlobalSaver)){
                one.gameObject.name = saveName + one.gameObject.name;
                one.Load(saveName);
                one.gameObject.name = one.gameObject.name.Remove(0, saveName.Length);
            }
        }
    }

    private static bool checkFormat(string saveName)
    {
        if (saveName.Equals(""))
            return false;
        if (saveName.Contains('~'))
            throw new ArgumentException($"Illegal save name: {saveName}, it contains '~'");
        return true;
    }

    public override string ToString()
    {
        return $"{name}";
    }
}