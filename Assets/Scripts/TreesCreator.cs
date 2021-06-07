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
    public int branchCount;
    public int minLayer;
    public int maxLayer;
    public int quantity;

    void Start()
    {
        int existingTrees = PlayerPrefs.GetInt(Saver.TreesNumberKey);
        for(int i = 0; i < quantity - existingTrees; i++)
            CreateTree(i);
    }

    GameObject CreateTree(int number)
    {
        GameObject tree = new GameObject("Tree" + number);
        tree.transform.parent = treeHandler.transform;

        InitRandomTransform(tree);
        InitSprite(tree, trunkOptions);
        tree.AddComponent<TreeSaver>();
        InitChildren(tree);

        return tree;
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
    
    private void InitSprite(GameObject tree, Sprite[] from)
    {
        int randomImage = Random.Range(0, from.Length);
        int randomLayer = Random.Range(minLayer, maxLayer + 1);
        tree.GetComponent<SpriteRenderer>().sprite = from[randomImage];
        tree.GetComponent<SpriteRenderer>().sortingOrder = randomLayer;
    }
    
    private void InitChildren(GameObject tree)
    {
        for (int i = 0; i < branchCount; i++)
            InitChild(tree, i);
    }

    private void InitChild(GameObject tree, int i)
    {
        GameObject branch = new GameObject(tree.name + ".branch" + i);
        branch.transform.parent = tree.transform;
        InitRandomBranchTransform(branch);
        branch.AddComponent<BranchSaver>();
        branch.AddComponent<SpriteRenderer>();

        InitSprite(branch, branchImageOptions);
        
    }

    private void InitRandomBranchTransform(GameObject branch)
    {
        float x = Random.Range(-normalTreeDifference, normalTreeDifference);
        float y = Random.Range(0, normalTreeDifference);
        int angle = Random.Range(-180, 180);

        var branchTransform = branch.transform;
        branchTransform.position = new Vector3(x, y, 0);
        branchTransform.rotation = Quaternion.Euler(0, 0, angle);
        branchTransform.localScale = Vector3.one;
    }
}