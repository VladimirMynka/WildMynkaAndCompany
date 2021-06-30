using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Assertions;

public class DeathSaver : Saver
{
    public List<string> destroyedNames = new List<string>();

    public override void Save(string saveName)
    {
        savedName = saveName + gameObject.name;;
        
        SaveDeathes();
        Push();
    }

    public override void Load(string saveName)
    {
        savedName = gameObject.name;
        
        Initiate();
        LoadDeathes();
    }

    public void AddName(string name)
    {
        destroyedNames.Add(name);
    }

    protected void SaveDeathes()
    {
        Put(destroyedNames.Count);
        foreach(string str in destroyedNames)
        {
            Put(str);
        }
    }
    protected void LoadDeathes()
    {
        int count = NextInt();
        destroyedNames = new List<string>();
        for (int i = 0; i < count; i++)
        {
            string destroyingName = NextString();
            var destroyingObject = GameObject.Find(destroyingName);
            destroyingObject.SetActive(false);
            Destroy(destroyingObject);
            AddName(destroyingName);
        }
    }
}
