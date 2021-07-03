using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    public Vector3 exit;
    public GameObject exitObject;
    public GameObject miniCanvas;
    ActiveObject activeObject;
    public string text;
    public GameObject player;
    public bool active;
    public int orderInLayer;

    void Awake() 
    {
        if(exitObject != null) exit = exitObject.transform.position;
        player = GameObject.FindWithTag("Player");
        activeObject = player.GetComponent<ActiveObject>();
    }

    void Update() 
    {
        if(!active) return;
        if(activeObject.activeObject != gameObject) active = false;
        if(Input.GetKeyDown("e")){
            player.transform.position = exit;
            player.GetComponent<SpriteRenderer>().sortingOrder = orderInLayer;
        }
        miniCanvas.transform.Find("Image").position = transform.position;
    }
    
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject != player)
        {
            if(other.gameObject.GetComponent<Health>() == null) return;
            other.gameObject.transform.position = exit;
            other.GetComponent<SpriteRenderer>().sortingOrder = orderInLayer;
        }
        else
        {
            activeObject.activeObject = gameObject;
            active = true;
            miniCanvas.SetActive(true);
            miniCanvas.GetComponentInChildren<Text>().text = text;
        }
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        if(other.gameObject == player)
        {
            active = false;
            miniCanvas.SetActive(false);
        }
    }
}