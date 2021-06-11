using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

public abstract class Saver : MonoBehaviour
{
    public const string TreesNumberKey = "TreesNumber";
    public const string Branch = ".branch";
    private const string Token = "~([^~]*)";
    private static readonly Regex TokenRegex = new Regex(Token);
    protected static int currentTree = 0;
    
    private static MatchHandler matcher = null;

    private static StringBuilder output = new StringBuilder();

    public abstract void Save(string saveName);
    public abstract void Load(string saveName);

    protected internal string savedName;
    protected void Initiate() =>
        matcher = new MatchHandler(TokenRegex.Match(GetString()));
    
    protected static void Put(string s)
    {
        output.Append($"~{s}");
    }

    protected static void Put(int i)
    {
        output.Append($"~{i}");
    }
    protected static void Put(float i)
    {
        output.Append($"~{i}");
    }

    protected void Push()
    {
        PlayerPrefs.SetString(savedName, output.ToString());
        output.Clear();
    }

    protected string GetString() =>
        PlayerPrefs.GetString(savedName);
    
    
    protected void SaveTransform()
    {
        var temp = transform;
        var position = temp.localPosition;
        var scale = temp.localScale;
        var rotation = temp.localRotation;
        Put(position.x);
        Put(position.y);
        Put(position.z);
        Put(scale.x);
        Put(scale.y);
        Put(scale.z);
        Put(rotation.x);
        Put(rotation.y);
        Put(rotation.z);
        Put(rotation.w);
    }
    
    protected void LoadTransform()
    {
        var transformLocal = transform;
        
        transformLocal.localPosition = new Vector3(NextFloat(), NextFloat(), NextFloat());
        transformLocal.localScale = new Vector3(NextFloat(), NextFloat(), NextFloat());
        transformLocal.localRotation = new Quaternion(NextFloat(), NextFloat(), NextFloat(), NextFloat());
    }

    protected static float NextFloat()
    {
        return matcher.nextFloat();
    }

    protected static int NextInt()
    {
        return matcher.nextInt();
    }

    protected static string NextString()
    {
        return matcher.nextString();
    }


    protected void SaveSprite()
    {
        var sprite = GetComponent<SpriteRenderer>();
        var path = GetPath(sprite.sprite);

        Put(path);
        Put(sprite.sortingOrder);
        
    }

    protected static string GetPath(UnityEngine.Object obj)
    {
        // Path's format: Assets/Recourses/.../file.smth
        // We have to remove "Assets/Recourses/" and ".smth" for Unity to correctly load file
        string fullPath = AssetDatabase.GetAssetPath(obj);

        // "Assets/Recourses/".length == 17
        string correctPath = fullPath.Substring(17, fullPath.LastIndexOf('.') - 17);
        return correctPath;
    }
    protected void LoadSprite()
    {
        var spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = Resources.Load<Sprite>(NextString());
        spriteRenderer.sortingOrder = NextInt();
    }
}