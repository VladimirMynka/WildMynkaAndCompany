using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogButton : MonoBehaviour
{
    public Transform content;
    public string theme;
    public string[] replicas;
    public AudioClip[] audioReplicas;
    public GameObject audioSource;
    void Start()
    {
        transform.Find("Text").gameObject.GetComponent< Text >().text = theme;
    }

    public void onClick(){
        int index = Random.Range(0, replicas.Length);
        content.Find("Title").gameObject.GetComponent< Text >().text = theme;
        content.Find("Text").gameObject.GetComponent< Text >().text = replicas[index];
        audioSource.GetComponent< AudioSource >().Stop();
        audioSource.GetComponent< AudioSource >().PlayOneShot(audioReplicas[index]);
    }
}
