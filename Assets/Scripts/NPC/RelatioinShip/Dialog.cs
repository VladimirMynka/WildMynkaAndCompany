using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Dialog : MonoBehaviour
{
    [System.Serializable]
    public class Topic{
        public string name;
        public string[] texts;
        public AudioClip[] replicas;
    }

    public GameObject dialogCanvas;
    GameObject content;
    GameObject buttonExample;
    GameObject topicsRectangle;
    public Topic[] topics;
    public GameObject miniCanvas;
    public bool active;
    public string text;

    void Start() {
        topicsRectangle = dialogCanvas.transform.Find("Topics").Find("Viewport").Find("Content").gameObject;
        content = dialogCanvas.transform.Find("Replicas").Find("Viewport").Find("Content").gameObject;
    }
    void Update() {
        if(!active) return;
        if(Input.GetKeyDown("space")){
            miniCanvas.GetComponent< Canvas >().planeDistance = -10;

            for(int i = 0; i < topics.Length; i++){
                add(topicsRectangle, topics[i]);
            }
            dialogCanvas.GetComponent< Canvas >().planeDistance = 99;
            Time.timeScale = 0;
        }
    }
    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            active = true;
            miniCanvas.GetComponent< Canvas >().planeDistance = 98;
            miniCanvas.transform.Find("Text").GetComponent< Text >().text = text;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        active = false;
        miniCanvas.GetComponent< Canvas >().planeDistance = -10;
    }

    void add(GameObject topicsRectangle, Topic topic){
        GameObject newTopic = Instantiate(buttonExample) as GameObject;
        newTopic.transform.parent = topicsRectangle.transform;
        newTopic.GetComponent< Button >().onClick.AddListener(() => { onClick(topic.name, topic.texts, topic.replicas); });
        if (topicsRectangle.transform.childCount > 0){
            newTopic.transform.localPosition = topicsRectangle.transform.GetChild(this.transform.childCount - 1).transform.localPosition + new Vector3(0, 20, 0);
        }
        else{
            newTopic.transform.localPosition = new Vector3 (0, 20, 0);
        }
    }
    public void onClick(string name, string[] replicas, AudioClip[] audioReplicas){
        int index = Random.Range(0, replicas.Length);
        content.transform.Find("Title").gameObject.GetComponent< Text >().text = name;
        content.transform.Find("Text").gameObject.GetComponent< Text >().text = replicas[index];
        gameObject.GetComponent< AudioSource >().Stop();
        gameObject.GetComponent< AudioSource >().PlayOneShot(audioReplicas[index]);
    }
}
