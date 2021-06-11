using System;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class GlobalSaver : Saver
{

    public static readonly string SavesKey = "Saves";
    public GameObject treeParent;

    // Start is called before the first frame update
    void Start()
    {
        if (FindObjectsOfType<GlobalSaver>().Length != 1)
            throw new Exception("More than one GlobalSaver exists!");
        else
            Load($"save1");
    }
    private void OnApplicationQuit()
    {
        string input = PlayerPrefs.GetString(SavesKey, "");
        MatchHandler handler = new MatchHandler(new Regex("~save(\\d+)").Match(input));
        int maxSave = 0;
        while (handler.hasNext())
        {
            int saveNumber = handler.nextInt();
            if (saveNumber > maxSave)
                maxSave = saveNumber;
        }
        Save($"save{maxSave+1}");
    }

    public override void Save(string saveName)
    {
        checkFormat(saveName);
        string saves = PlayerPrefs.GetString(SavesKey, "");
        if (!saves.Contains($"~{saveName}"))
        {
            saves = saves + "~" + saveName;
            PlayerPrefs.SetString(SavesKey, saves);
        }
        
        
        var all = FindObjectsOfType<Saver>();
        foreach (var one in all)
        {
            if (!(one is BranchSaver) && !(one is GlobalSaver))
                one.Save(saveName);
        }

        PlayerPrefs.SetInt(TreesNumberKey, currentTree);
    }

    public override void Load(string saveName)
    {
        checkFormat(saveName);
        string saves = PlayerPrefs.GetString(SavesKey, "");
        if (!saves.Contains($"~{saveName}"))
            throw new ArgumentException($"No save with name {saveName} exists.");
        
        int trees = PlayerPrefs.GetInt(TreesNumberKey);
        Debug.Log(trees);
        for (int i = 0; i < trees; i++)
        {
            var go = new GameObject(saveName + "Tree" + i);
            go.transform.parent = treeParent.transform;
            var saver = go.AddComponent<TreeSaver>();
            saver.Load(saveName);
        }
    }

    private static void checkFormat(string saveName)
    {
        if (saveName.Contains('~'))
            throw new ArgumentException($"Illegal save name: {saveName}, it contains '~'");
    }

    public override string ToString()
    {
        return $"{name}";
    }
}