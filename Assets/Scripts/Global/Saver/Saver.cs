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
    protected static int currentTree = 0;

    
    protected static MatchHandler matcher = null;

    protected static StringBuilder output = new StringBuilder();

    public abstract void Save();
    
    public abstract void Load();

    protected internal string savedName;

    protected void Put(string s)
    {
        output.Append($"~{s}");
    }

    protected void Put(int i)
    {
        output.Append($"~{i}");
    }
    protected void Put(float i)
    {
        output.Append($"~{i}");
    }

    protected void Push()
    {
        SaveProperty(output.ToString());
        output.Clear();
    }

    protected void SaveProperty(string key, int property) => 
        PlayerPrefs.SetInt(savedName + "." + key, property);
    
    protected void SaveProperty(string key, float property) =>
        PlayerPrefs.SetFloat(savedName + "." + key, property);
    
    protected void SaveProperty(string key, string property) =>
        PlayerPrefs.SetString(savedName + "." + key, property);

    protected void SaveProperty(string property) =>
        PlayerPrefs.SetString(savedName, property);
    
    protected int GetInt(string key) =>
        PlayerPrefs.GetInt(savedName + "." + key);
    
    protected float GetFloat(string key) =>
        PlayerPrefs.GetFloat(savedName + "." + key);
    
    protected string GetString(string key) =>
        PlayerPrefs.GetString(savedName + "." + key);

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
        // SaveProperty("transform", "" +
        //                           $"~{position.x}~{position.y}~{position.z}" +
        //                           $"~{scale.x}~{scale.y}~{scale.z}" +
        //                           $"~{rotation.x}~{rotation.y}~{rotation.z}~{rotation.w}");
    }
    
    
    protected void LoadTransform()
    {
        var transformLocal = transform;
        
        transformLocal.localPosition =    new Vector3(matcher.nextFloat(), matcher.nextFloat(), matcher.nextFloat());
        transformLocal.localScale =       new Vector3(matcher.nextFloat(), matcher.nextFloat(), matcher.nextFloat());
        transformLocal.localRotation = new Quaternion(matcher.nextFloat(), matcher.nextFloat(), matcher.nextFloat(), matcher.nextFloat());
    }
    

    protected void SaveSprite()
    {
        var sprite = GetComponent<SpriteRenderer>();
        var path = GetPrefabPath(sprite.sprite);

        Put(path);
        Put(sprite.sortingOrder);
        
    }

    protected static string GetPrefabPath(UnityEngine.Object obj)
    {
        // Path's format: Assets/Recourses/.../file.png
        // We have to remove "Assets/Recourses/" and ".png" for Unity to correctly load sprites
        string fullPath = AssetDatabase.GetAssetPath(obj);
        Debug.Log(fullPath);

        // "Assets/Recourses/".length == 17, format's length == 2, 3 or 4
        string correctPath = fullPath.Substring(17, fullPath.LastIndexOf('.') - 17);
        return correctPath;
    }

    protected void LoadSprite()
    {
        var spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = Resources.Load<Sprite>(matcher.nextString());
        spriteRenderer.sortingOrder = matcher.nextInt();
    }
}