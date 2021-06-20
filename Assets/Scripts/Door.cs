using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    public Vector3 exit;
    public GameObject exitObject;
    public GameObject miniCanvas;
    public string text;
    public GameObject transportObject;
    public GameObject currentCamera;
    public bool active;


    void Start() {
        if(exitObject) exit = exitObject.transform.position;
    }

    void Update() {
        if(!active) return;
        if(Input.GetKeyDown("e")){
            transportObject.transform.position = exit;
        }
    }
    
    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag != "Player"){
            other.gameObject.transform.position = exit;
        }
        else{
            active = true;
            transportObject = other.gameObject;
            miniCanvas.GetComponent< Canvas >().planeDistance = 100;
            miniCanvas.transform.Find("Text").GetComponent< Text >().text = text;
            Vector3 position = currentCamera.GetComponent< Camera >().WorldToScreenPoint(transform.position);
            miniCanvas.GetComponent< RectTransform >().pivot = new Vector2(position.x, position.y);
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject == transportObject){
            active = false;
            miniCanvas.GetComponent< Canvas >().planeDistance = -10;
            transportObject = null;
        }
    }
}
