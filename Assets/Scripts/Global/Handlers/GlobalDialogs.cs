using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalDialogs : MonoBehaviour {
    public Topic[] topics;
}

[System.Serializable]
public class Topic{
    public string name;
    [TextArea()] public string[] texts;
    public AudioClip[] replicas;
}