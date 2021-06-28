using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Assertions;

public class NpcCreatorSaver : Saver
{
    public override void Save(string saveName)
    {
        savedName = saveName + gameObject.name;;
        
        SaveNpcCreator();
        Push();
    }

    public override void Load(string saveName)
    {
        savedName = gameObject.name;
        
        Initiate();
        StartCoroutine(LoadCreator(saveName));
    }

    IEnumerator LoadCreator(string saveName)
    {
        LoadNpcCreator();
        yield return null;
        var currentCreature = GetComponent<NPCCreator>().currentCreature.GetComponent<Saver>();
        currentCreature.gameObject.name = saveName + currentCreature.gameObject.name;
        currentCreature.Load(saveName);
        currentCreature.gameObject.name = currentCreature.gameObject.name.Remove(0, saveName.Length);
    }
}