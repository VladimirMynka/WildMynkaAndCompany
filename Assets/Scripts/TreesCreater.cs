using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreesCreater : MonoBehaviour
{
    public GameObject createObject;
    public Sprite[] imageVariants;
    public Sprite[] vetkaImageVariants;
    public float normalDifference;
    public float normalTreeDifference;
    public int minLayer;
    public int maxLayer;
    public int quantity;
    void Start()
    {
        for(int i = 0; i < quantity; i++){
            GameObject newTree = Instantiate(createObject) as GameObject;
            newTree.transform.parent = transform;
            float randomX = Random.Range(-normalDifference, normalDifference);
            float randomY = Random.Range(-normalDifference, normalDifference);
            newTree.transform.localPosition = new Vector3(randomX, randomY, 0);
            newTree.transform.rotation = transform.rotation;
            newTree.transform.localScale = new Vector3(1, 1, 1);

            int randomImage = Random.Range(0, imageVariants.Length);
            int randomLayer = Random.Range(minLayer, maxLayer+1);
            newTree.GetComponent< SpriteRenderer >().sprite = imageVariants[randomImage];
            newTree.GetComponent< SpriteRenderer >().sortingOrder = randomLayer;

            for(int j = 0; j < newTree.transform.childCount; j++){
                randomX = Random.Range(-normalTreeDifference, normalTreeDifference);
                randomY = Random.Range(0, normalTreeDifference);
                int randomAngle = Random.Range(-180, 180);
                newTree.transform.GetChild(j).transform.localPosition = new Vector3(randomX, randomY, 0);
                newTree.transform.GetChild(j).transform.localRotation = Quaternion.Euler(0, 0, randomAngle);

                randomImage = Random.Range(0, vetkaImageVariants.Length);
                randomLayer = Random.Range(minLayer, maxLayer+1);
                newTree.transform.GetChild(j).GetComponent< SpriteRenderer >().sprite = vetkaImageVariants[randomImage];
                newTree.transform.GetChild(j).GetComponent< SpriteRenderer >().sortingOrder = randomLayer;
            }
        }
    }
}
