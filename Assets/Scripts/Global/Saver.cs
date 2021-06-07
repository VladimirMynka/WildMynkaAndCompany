using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class Saver : MonoBehaviour
{
    public const string TreesNumberKey = "TreesNumber";
    protected const string ChildNumber = "childNumber";
    protected const string Branch = ".branch";
    private const string SpriteKey = "sprite";
    private const string OrderKey = "order";
    
    public abstract void Save();
    
    public abstract void Load();

    protected internal string savedName;

    protected void SaveProperty(string key, int property)
    {
        PlayerPrefs.SetInt(savedName + "." + key, property);
    }
    protected void SaveProperty(string key, float property)
    {
        PlayerPrefs.SetFloat(savedName + "." + key, property);
    }
    protected void SaveProperty(string key, string property)
    {
        PlayerPrefs.SetString(savedName + "." + key, property);
    }
    protected int GetInt(string key)
    {
        return PlayerPrefs.GetInt(savedName + "." + key);
    }
    protected float GetFloat(string key)
    {
        return PlayerPrefs.GetFloat(savedName + "." + key);
    }
    protected string GetString(string key)
    {
        return PlayerPrefs.GetString(savedName + "." + key);
    }
    
    protected void SaveTransform()
    {
        var position = transform.localPosition;
        SaveProperty("x", position.x);
        SaveProperty("y", position.y);
        SaveProperty("z", position.z);
        
        var scale = transform.localScale;
        SaveProperty("scaleX", scale.x);
        SaveProperty("scaleY", scale.y);
        SaveProperty("scaleZ", scale.z);
        
        var rotation = transform.localRotation;
        SaveProperty("rotationX", rotation.x);
        SaveProperty("rotationY", rotation.y);
        SaveProperty("rotationZ", rotation.z);
        SaveProperty("rotationW", rotation.w);
    }
    
    protected void LoadTransform()
    {
        var transformLocal = transform;
        transformLocal.position = new Vector3(
            GetFloat("x"), 
            GetFloat("y"), 
            GetFloat("z")
        );
        transformLocal.localScale = new Vector3(
            GetFloat("scaleX"), 
            GetFloat("scaleY"), 
            GetFloat("scaleZ")
        );
        transformLocal.localRotation = new Quaternion(
            GetFloat("rotationX"), 
            GetFloat("rotationY"), 
            GetFloat("rotationZ"),
            GetFloat("rotationW")
        );
    }
    

    protected void SaveSprite()
    {
        var sprite = GetComponent<SpriteRenderer>();
        
        // Path's format: Assets/Recourses/.../file.png
        // We have to remove "Assets/Recourses/" and ".png" for Unity to correctly load sprites
        string fullPath = AssetDatabase.GetAssetPath(sprite.sprite); 
        
        // "Assets/Recourses/".length == 17, ".png".length == 4
        string correctPath = fullPath.Substring(17, fullPath.Length - (17 + 4));
        
        SaveProperty(SpriteKey, correctPath);
        SaveProperty(OrderKey, sprite.sortingOrder);
    }
    protected void LoadSprite()
    {
        var spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = Resources.Load<Sprite>(GetString(SpriteKey));
        spriteRenderer.sortingOrder = GetInt(OrderKey);
    }
}