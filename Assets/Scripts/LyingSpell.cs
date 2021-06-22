using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LyingSpell : MonoBehaviour
{
    public int addingSpell;
    public GameObject miniCanvas;
    public GameObject player;
    ActiveObject activeObject;
    public string text;
    public bool active;

    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        activeObject = player.GetComponent<ActiveObject>();
    }

    void Update() 
    {
        if(!active) return;
        if(activeObject.activeObject != gameObject) active = false;
        if(Input.GetKeyDown("e")){
            AddSpell(player);
        }
        miniCanvas.transform.Find("Image").position = transform.position;
    }
    
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject != player)
        {
            AddSpell(other.gameObject);
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

    void AddSpell(GameObject creature)
    {
        if (creature == null) return;
        var inventory = creature.GetComponent<Inventory>();
        if (inventory == null) return;
        inventory.AddSpell(addingSpell);
        Destroy(gameObject);
    }
}