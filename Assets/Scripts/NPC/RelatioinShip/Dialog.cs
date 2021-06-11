using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Dialog : MonoBehaviour
{
    public GameObject dialogCanvas;
    GameObject content;
    public GameObject buttonExample;
    GameObject topicsRectangle;
    public Topic[] topics;
    public int[] topicsIndeces;
    public GameObject globalTopicsObject;
    public GameObject miniCanvas;
    public bool active;
    public string text;

    void Start() {
        topicsRectangle = dialogCanvas.transform.Find("Topics").Find("Viewport").Find("Content").gameObject;
        content = dialogCanvas.transform.Find("Replicas").Find("Viewport").Find("Content").gameObject;
        topics = new Topic[topicsIndeces.Length];
        GlobalDialogs globalTopics = globalTopicsObject.GetComponent<GlobalDialogs>();
        for(int i = 0; i < topicsIndeces.Length; i++){
            topics[i] = globalTopics.topics[topicsIndeces[i]];
        }
    }
    void Update() {
        if(!active) return;
        if(Input.GetKeyDown("space")){
            miniCanvas.GetComponent< Canvas >().planeDistance = -10;
            dialogCanvas.GetComponent< Canvas >().planeDistance = 99;
            StartCoroutine(clearAndAddAll());
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
        if(other.gameObject.tag == "Player"){
            active = false;
            miniCanvas.GetComponent< Canvas >().planeDistance = -10;
        }
    }

    IEnumerator clearAndAddAll(){
        clear();
        yield return new WaitForSeconds(0.05f);
        for(int i = 0; i < topics.Length; i++){
            add(topics[i]);
        }
        Time.timeScale = 0;
    }

    void add(Topic topic){
        GameObject newTopic = Instantiate(buttonExample) as GameObject;
        newTopic.GetComponent< Button >().onClick.AddListener(() => { onClick(topic.name, topic.texts, topic.replicas); });
        newTopic.transform.Find("Textic").gameObject.GetComponent<Text>().text = topic.name;
        newTopic.transform.parent = topicsRectangle.transform;
        if (topicsRectangle.transform.childCount > 1){
            newTopic.transform.localPosition = topicsRectangle.transform.GetChild(topicsRectangle.transform.childCount - 2).transform.localPosition + new Vector3(0, -20, 0);
        }
        else{
            newTopic.transform.localPosition = new Vector3(53.25f, -20, 0);
        }
        newTopic.transform.localScale = Vector3.one;
        Debug.Log(topicsRectangle.transform.childCount);
    }

    void clear(){
        content.transform.Find("Title").gameObject.GetComponent< Text >().text = "";
        content.transform.Find("Text").gameObject.GetComponent< Text >().text = "";
        for(int i = 0; i < topicsRectangle.transform.childCount; i++){
            Destroy(topicsRectangle.transform.GetChild(i).gameObject);
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
