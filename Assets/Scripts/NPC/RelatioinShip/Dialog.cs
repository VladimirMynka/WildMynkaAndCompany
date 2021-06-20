using System;
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
    public Topic greeting;
    public int greetingIndex;
    public GameObject globalTopicsObject;
    GlobalDialogs globalTopics;
    public GameObject miniCanvas;
    public bool active;
    public string text;
    Target targetScript;

    void Awake() 
    {
        topicsRectangle = dialogCanvas.transform.Find("Topics").Find("Viewport").Find("Content").gameObject;
        content = dialogCanvas.transform.Find("Replicas").Find("Viewport").Find("Content").gameObject;
        topics = new Topic[topicsIndeces.Length];
        globalTopics = globalTopicsObject.GetComponent<GlobalDialogs>();
        for(int i = 0; i < topicsIndeces.Length; i++){
            topics[i] = globalTopics.topics[topicsIndeces[i]];
        }
        greeting = globalTopics.greetings[greetingIndex];
        targetScript = transform.parent.GetComponent<Target>();
    }
    void Update() 
    {
        if(!active) return;
        if(Input.GetKeyDown("e")){
            miniCanvas.GetComponent< Canvas >().planeDistance = -10;
            dialogCanvas.GetComponent< Canvas >().planeDistance = 100;
            Greet();
            StartCoroutine(ClearAndAddAll());
        }
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player" && targetScript.playerRelationship >= 60){
            active = true;
            miniCanvas.GetComponent< Canvas >().planeDistance = 98;
            miniCanvas.transform.Find("Text").GetComponent< Text >().text = text;
        }
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player"){
            active = false;
            miniCanvas.GetComponent< Canvas >().planeDistance = -10;
        }
    }

    void Greet()
    {
        int index = UnityEngine.Random.Range(0, greeting.texts.Length);
        content.transform.Find("Title").gameObject.GetComponent<Text>().text = greeting.name;
        content.transform.Find("Text").gameObject.GetComponent<Text>().text = greeting.texts[index];
        gameObject.GetComponent< AudioSource >().Stop();
        gameObject.GetComponent< AudioSource >().PlayOneShot(greeting.replicas[index]);
        greeting.usingCount++;
    }

    IEnumerator ClearAndAddAll()
    {
        yield return StartCoroutine(Clear());
        for(int i = 0; i < topics.Length; i++){
            Add(topics[i]);
        }
        Time.timeScale = 0;
    }

    IEnumerator Clear()
    {
        for(int i = 0; i < topicsRectangle.transform.childCount; i++){
            Destroy(topicsRectangle.transform.GetChild(i).gameObject);
        } 
        yield return null;
    }

    void Add(Topic topic)
    {
        GameObject newTopic = Instantiate(buttonExample) as GameObject;
        newTopic.GetComponent< Button >().onClick.AddListener(() => { OnClick(topic); });
        newTopic.transform.Find("Textic").gameObject.GetComponent<Text>().text = topic.name;
        newTopic.transform.SetParent(topicsRectangle.transform, false);
        if (topicsRectangle.transform.childCount > 1){
            newTopic.transform.localPosition = topicsRectangle.transform.GetChild(topicsRectangle.transform.childCount - 2).transform.localPosition + new Vector3(0, -20, 0);
        }
        else{
            newTopic.transform.localPosition = new Vector3(53.25f, -20, 0);
        }
        newTopic.transform.localScale = Vector3.one;
    }

    public void OnClick(Topic topic)
    {
        int index = UnityEngine.Random.Range(0, topic.texts.Length);
        content.transform.Find("Title").gameObject.GetComponent< Text >().text = topic.name;
        content.transform.Find("Text").gameObject.GetComponent< Text >().text = topic.texts[index];
        gameObject.GetComponent< AudioSource >().Stop();
        gameObject.GetComponent< AudioSource >().PlayOneShot(topic.replicas[index]);
        topic.usingCount++;
    }

    public void AddTopic(int topicIndex)
    {
        int[] newIndeces = new int[topicsIndeces.Length + 1];
        topics = new Topic[newIndeces.Length];
        for(int i = 0; i < topicsIndeces.Length; i++)
        {
            newIndeces[i] = topicsIndeces[i];
            topics[i] = globalTopics.topics[newIndeces[i]];
        }
        newIndeces[newIndeces.Length - 1] = topicIndex;
        topics[newIndeces.Length - 1] = globalTopics.topics[newIndeces[newIndeces.Length - 1]];
        topicsIndeces = newIndeces;
    }
    public void RemoveTopic(int topicIndex)
    {
        int j = Array.IndexOf(topicsIndeces, topicIndex);
        if(j == -1) return;
        int[] newIndeces = new int[topicsIndeces.Length - 1];
        topics = new Topic[topicsIndeces.Length - 1];
        for(int i = 0; i < j; i++)
        {
            newIndeces[i] = topicsIndeces[i];
            topics[i] = globalTopics.topics[newIndeces[i]];
        }
        for(int i = j; i < newIndeces.Length; i++)
        {
            newIndeces[i] = topicsIndeces[i + 1];
            topics[i] = globalTopics.topics[newIndeces[i]];
        }
        topicsIndeces = newIndeces;
    }
    public void ChangeTopic(int oldIndex, int newIndex)
    {
        int j = Array.IndexOf(topicsIndeces, oldIndex);
        if (j == -1)
        { 
            AddTopic(newIndex);
            return;
        }
        topicsIndeces[j] = newIndex;
        topics[j] = globalTopics.topics[newIndex];
    }
    public void ChangeGreeting(int newGreeting)
    {
        greetingIndex = newGreeting;
        greeting = globalTopics.greetings[newGreeting];
    }
    public void ChangeTopicsArray(int[] newIndeces)
    {
        topicsIndeces = newIndeces;
        topics = new Topic[newIndeces.Length];
        for(int i = 0; i < newIndeces.Length; i++)
        {
            topics[i] = globalTopics.topics[newIndeces[i]];
        }
    }
    public void UpdateDialog()
    {
        float oldTimeScale = Time.timeScale;
        Time.timeScale = 1;
        StartCoroutine(ClearAndAddAll());
        Time.timeScale = oldTimeScale;
    }
}
