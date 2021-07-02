using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LyingItem : MonoBehaviour
{
    GameObject miniCanvas;
    GameObject player;
    ActiveObject activeObject;
    public string text;
    bool active;

    void Awake()
    {
        Canvases global = FindObjectOfType<Canvases>();
        miniCanvas = global.miniCanvas;
        player = GameObject.FindWithTag("Player");
        activeObject = player.GetComponent<ActiveObject>();
    }

    void Update() 
    {
        if(!active) return;
        if(activeObject.activeObject != gameObject) active = false;
        if(Input.GetKeyDown("e")){
            OnActivate(player);
        }
        miniCanvas.transform.Find("Image").position = transform.position;
    }
    
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject != player)
        {
            OnActivate(other.gameObject);
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
        if(other.gameObject == player && active == true)
        {
            active = false;
            miniCanvas.SetActive(false);
        }
    }

    protected virtual void OnActivate(GameObject creature){}
}