using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TreesCreator : MonoBehaviour
{
    public GameObject treeHandler;
    public Sprite[] trunkOptions;
    public Sprite[] branchImageOptions;
    public float normalDifference;
    public float normalTreeDifference;
    public int branchCount = 7;
    public int minLayer;
    public int maxLayer;
    public int quantity;

    string TreesCountKey = "TreesCount";
    string LastSaveKey = "LastSave";

    void Start()
    {
        string lastSave = PlayerPrefs.GetString(LastSaveKey);
        int existingTrees = PlayerPrefs.GetInt(lastSave + TreesCountKey, 0);
        for (int i = 0; i < quantity - existingTrees; i++)
        {
            CreateTree(i);
        }
        if(lastSave != "")
            PlayerPrefs.SetInt(lastSave + TreesCountKey, quantity);
    }

    private void CreateTree(int number)
    {
        var tree = new GameObject("Tree" + number);
        tree.transform.parent = treeHandler.transform;

        InitRandomTransform(tree);
        InitSprite(tree, trunkOptions, 1);
        var saver = tree.AddComponent<TreeSaver>();
        InitChildren(tree);
    }
    
    private void InitRandomTransform(GameObject tree)
    {
        float x = Random.Range(-normalDifference, normalDifference);
        float y = Random.Range(-normalDifference, normalDifference);

        var treeTransform = tree.transform;
        treeTransform.localPosition = new Vector3(x, y, 0);
        treeTransform.transform.rotation = transform.rotation;
        treeTransform.localScale = Vector3.one;
    }
    
    private void InitSprite(GameObject tree, Sprite[] from, int layer = -1)
    {
        int image = Random.Range(0, from.Length);
        if(layer < minLayer)
            layer = Random.Range(minLayer, maxLayer + 1);
        var spriteRenderer = tree.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = from[image];
        spriteRenderer.sortingOrder = layer;
        var savingSprite = tree.AddComponent<SavingSprite>();
        savingSprite.imageIndex = image;
        savingSprite.layer = layer;
    }
    
    private void InitChildren(GameObject tree)
    {
        for (int i = 0; i < branchCount; i++)
            InitChild(tree, i);
    }

    private void InitChild(GameObject tree, int i)
    {
        GameObject branch = new GameObject(tree.name + Saver.Branch + i);
        branch.transform.parent = tree.transform;
        InitRandomBranchTransform(branch);
        branch.AddComponent<BranchSaver>();

        InitSprite(branch, branchImageOptions);
        
    }

    private void InitRandomBranchTransform(GameObject branch)
    {
        float x = Random.Range(-normalTreeDifference, normalTreeDifference);
        float y = Random.Range(0, normalTreeDifference);
        int angle = Random.Range(-180, 180);

        var branchTransform = branch.transform;
        branchTransform.localPosition = new Vector3(x, y, 0);
        branchTransform.localRotation = Quaternion.Euler(0, 0, angle);
        branchTransform.localScale = Vector3.one;
    }
}