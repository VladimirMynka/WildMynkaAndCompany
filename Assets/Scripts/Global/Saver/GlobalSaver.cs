using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSaver : Saver
{

    public static readonly string SavesKey = "Saves";
    public static readonly string LastSaveKey = "LastSave";
    public GameObject treeParent;

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

        
    }

    public override void Load(string saveName)
    {
        if(!checkFormat(saveName))
            return;
        string saves = PlayerPrefs.GetString(SavesKey, "");
        if (!saves.Contains($"~{saveName}"))
            throw new ArgumentException($"No save with name {saveName} exists.");        

        StartCoroutine(DestroyAndLoad(saveName));
    }

    IEnumerator DestroyAndLoad(string saveName)
    {
        var deather = FindObjectOfType<DeathSaver>();
        string deatherName = deather.gameObject.name;
        deather.gameObject.name = saveName + deatherName;
        deather.Load(saveName);
        deather.gameObject.name = deatherName;
        yield return null;

        var allCreators = FindObjectsOfType<NpcCreatorSaver>();
        foreach (var one in allCreators)
        {
            string oneName = one.gameObject.name;
            one.gameObject.name = saveName + oneName;
            Debug.Log(oneName);
            one.Load(saveName);
            yield return null;
            one.gameObject.name = oneName;
        }
        yield return null;


        var all = FindObjectsOfType<Saver>();
        foreach (var one in all)
        {
            if (!(one is GlobalSaver) && !(one is DeathSaver) && !(one is NpcCreatorSaver))
            {
                string oneName = one.gameObject.name;
                one.gameObject.name = saveName + oneName;
                Debug.Log(oneName);
                one.Load(saveName);
                yield return null;
                one.gameObject.name = oneName;
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